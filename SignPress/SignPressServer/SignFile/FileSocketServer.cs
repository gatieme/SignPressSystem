using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

using Newtonsoft.Json;


/// <summary>
/// BUG 2015/6/24 17:52
/// 今天被一个BUG困扰了一天，是传输文件和图片的丢包和粘包问题
/// 我们发送信息的机制使用的是变长字节，这就意味着我们的长度不可控
/// 因此发送文件和图片的时候，头和数据粘连在了一起，我们后来修改为定长头的方式进行了解决
/// </summary>


using SignPressServer.SignData;
using SignPressServer.SignDAL;
using SignPressServer.SignTools;

namespace SignPressServer.SignFile
{
    /*
     * 文件传输的套接字信息
     */
    public class FileSocketServer
    {
        private const String SIGNATURE_PICTURE_PATH = ".\\signature\\";
        private const String HDJCONTDACT_PATH = @".\\hdjcontract\\";
        private static Thread m_threadWatch = null;
        private static Socket m_socketWatch = null;



        private static String m_filePath;         // 文件路径
        public static String FilePath
        {
            get { return m_filePath; }
            set { m_filePath = value; }
        }

        //public static int m_flag;                       // 0为上传，1为下载

        //private static ListBox lstbxMsgView;//显示接受的文件等信息
        //private static ListBox listbOnline;//显示用户连接列表

        private static Dictionary<string, Socket> dict = new Dictionary<string, Socket>();


        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="localIp"></param>
        /// <param name="localPort"></param>
        public  void BeginListening(string localIp, string localPort)
        {


            //创建服务端负责监听的套接字，参数（使用IPV4协议，使用流式连接，使用Tcp协议传输数据）
            m_socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //获取Ip地址对象
            IPAddress address = IPAddress.Parse(localIp);
            //创建包含Ip和port的网络节点对象
            IPEndPoint endpoint = new IPEndPoint(address, int.Parse(localPort));
            //将负责监听的套接字绑定到唯一的Ip和端口上
            m_socketWatch.Bind(endpoint);
            //设置监听队列的长度
            m_socketWatch.Listen(10);
            //创建负责监听的线程，并传入监听方法
            m_threadWatch = new Thread(WatchConnecting);
            m_threadWatch.IsBackground = true;//设置为后台线程
            m_threadWatch.Start();//开始线程
            //ShowMgs("服务器启动监听成功");
            Console.WriteLine("文件传输服务器启动监听成功");
        }

        public void BeginListening(IPAddress localIPAddress, int listenPort)
        {


            //创建服务端负责监听的套接字，参数（使用IPV4协议，使用流式连接，使用Tcp协议传输数据）
            m_socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //创建包含Ip和port的网络节点对象
            IPEndPoint endpoint = new IPEndPoint(localIPAddress, listenPort);
            //将负责监听的套接字绑定到唯一的Ip和端口上
            m_socketWatch.Bind(endpoint);
            //设置监听队列的长度
            m_socketWatch.Listen(10);
            //创建负责监听的线程，并传入监听方法
            m_threadWatch = new Thread(WatchConnecting);
            m_threadWatch.IsBackground = true;//设置为后台线程
            m_threadWatch.Start();//开始线程
            //ShowMgs("服务器启动监听成功");
            Console.WriteLine("文件传输服务器启动监听成功");
        }

        /// <summary>
        /// 连接客户端
        /// </summary>
        private  void WatchConnecting()
        {
            while (true)//持续不断的监听客户端的请求
            {
                //开始监听 客户端连接请求，注意：Accept方法，会阻断当前的线程
                Socket connection = m_socketWatch.Accept();
                if (connection.Connected)
                {
                    //向列表控件中添加一个客户端的Ip和端口，作为发送时客户的唯一标识
                    //listbOnline.Items.Add(connection.RemoteEndPoint.ToString());
                    //将与客户端通信的套接字对象connection添加到键值对集合中，并以客户端Ip做为健
                    dict.Add(connection.RemoteEndPoint.ToString(), connection);
                    Console.WriteLine("文件传输客户端{0}连接成功", connection.RemoteEndPoint.ToString());

                    // 创建通信线程，并为线程要调用的方法RecMsg 传入参数sokConnection
                    ParameterizedThreadStart pts = new ParameterizedThreadStart(RecMsg);
                    Thread thradRecMsg = new Thread(pts);
                    thradRecMsg.IsBackground = true;
                    thradRecMsg.Start(connection);
                    //ShwMsgForView.ShwMsgforView(lstbxMsgView, "客户端连接成功" + connection.RemoteEndPoint.ToString());
                }
            }
        }

        /// <summary>
        /// 接收消息
        /// 如果是上传签字图片，则数据格式为[报头 + id + 图片信息流]
        /// 如果是下载会签单信息，则数据格式为[报头 + 编号]
        /// </summary>
        /// <param name="socketClientPara"></param>
        private  void RecMsg(object socketClientPara)
        {
            Socket socketClient = socketClientPara as Socket;
            byte[] recvBuffer = new byte[512];
            // SocketMessage message = new SocketMessage(); 

            // modify by gatieme 205-07-04 12:44
            //  正常来说线程执行完毕以后，应该自行终止，
            //  但是由于之前这个使用了while循环的错误方式，
            //  导致用户上传下载签字图片，或者下载会签单信息后，线程一直在跑，未中断
            //  导致服务器的CPU占有率达到100%
            //while (true)
            //{
                //定义一个接受用的缓存区（100M字节数组）
                //byte[] arrMsgRec = new byte[1024 * 1024 * 100];
                //将接收到的数据存入arrMsgRec数组,并返回真正接受到的数据的长度   
                if (socketClient.Connected)
                {
                    Console.WriteLine("开始传输文件");
                    
                    try
                    {
                        // 首先接收到信息头，确定是要上传还是下载，以及文件的编号信息
                        socketClient.Receive(recvBuffer, 0, 300, SocketFlags.None);
                        String message = Encoding.UTF8.GetString(recvBuffer);
                        Console.WriteLine("大小{0},{1}", message.Length, message);
                        string[] split = message.Split('~');    //返回由'/'分隔的子字符串数组

                        Console.WriteLine("LENGTH : {0}", split.Length);
                        String id = split[2];
                        //int pos = message.LastIndexOf(';');
                        //Byte[] picture = new Byte[1024];
                        //recvBuffer.CopyTo(picture, pos + 1);
                        //Console.WriteLine(picture.ToString());

                        switch (split[0])
                        {
                            ///  签字图片的上传和下载
                            // UPLOAD_PICTURE_REQUEST;2;30
                            case "UPLOAD_PICTURE_REQUEST":             // 用户上传签字图片
                                {
                                    UploadFile(socketClient, id);
                                    break;
                                }
                            case "DOWNLOAD_PICTURE_REQUEST":         // 用户下载签字图片
                                {
                                    String empId = JsonConvert.DeserializeObject<String>(id);
                                    DownloadSignatureFile(socketClient, empId);
                                    break;
                                }
                            
                            ///  会签单的下载
                            case "DOWNLOAD_HDJCONTRACT_REQUEST":       // 用户下载会签单
                                {
                                    String conId = JsonConvert.DeserializeObject<String>(id);
                                    DownloadContractFile(socketClient, conId);
                                    break;
                                }
                            
                            ///  上传计划额度表模版
                            case "UPLOAD_REGULARLOAD_REQUEST":
                                {
                                    //  传送year + id 即可
                                    Search search = JsonConvert.DeserializeObject<Search>(id);
                                    UploadRegularloadFile(socketClient, search.Year, search.CategoryId);     //  开始接收文件

                                    ///// 此处有性能问题，最后能交给MSOfficeThread来完成这个工作
                                    // 接收完毕后将数据插入到数据库中
                                    //  BUG BUG
                                    //  先删除数据库里面的计划额度表信息
                                    DALContractRegularoload.DeleteYearCategoryRegularload(search.Year, search.CategoryId);
                                    //  再将excel中的数据插入到数据库中
                                    MSExcelTools.UploadRegularLoad(search.Year, search.CategoryId);
                                    break;
                                }

                            //  统计报表的下载
                            case "DOWNLOAD_STATISTIC_REQUEST":
                                {
                                    Search search = JsonConvert.DeserializeObject<Search>(id);
                                    DownloadStatisticFile(socketClient, search.Year, search.CategoryId);

                                    break;
                                }

                            //  下载计划额度表模版
                            case "DOWNLOAD_REGULARLOAD_REQUEST":
                                {
                                    //  传送id即可
                                    Search search = JsonConvert.DeserializeObject<Search>(id);
                                    DownloadRegularloadTemplateFile(socketClient, search.Year, search.CategoryId);
                                    break;
                                }

                            //  统计报表的下载 -=>  为了适应安卓的下载
                            case "DOWNLOADING_STATISTIC_REQUEST":
                                {
                                    Search search = JsonConvert.DeserializeObject<Search>(id);
                                    DownloadingStatisticFile(socketClient, search.Year, search.CategoryId);

                                    break;
                                }

                            //  下载计划额度表模版 -=>  为了适应安卓的下载
                            case "DOWNLOADING_REGULARLOAD_REQUEST":
                                {
                                    //  传送id即可
                                    Search search = JsonConvert.DeserializeObject<Search>(id);
                                    DownloadingRegularloadTemplateFile(socketClient, search.Year, search.CategoryId);
                                    break;
                                }

                        }

                        dict.Remove(socketClient.RemoteEndPoint.ToString());
                        socketClient.Close();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    ///  客户端未正常连接
                    Console.WriteLine("文件传输客户端未正常连接自文件服务器");
                    
                }
            }
      //  }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public  void CloseTcpSocket(Socket socketClient)
        {
            dict.Clear();
            m_threadWatch.Abort();
            m_socketWatch.Close();
            Console.WriteLine("服务器关闭监听");
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Stop()
        {
            dict.Clear();
            m_threadWatch.Abort();
            m_socketWatch.Close();
            Console.WriteLine("服务器关闭监听");
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadFile(Socket socketClient, String employeeId)
        {
            Console.WriteLine("开始上传文件");
            // UPLOAD_PICTURE_REQUEST;2;30;length;byte
            // 取出后的结果为
            // 0 UPLOAD_PICTURE_REQUEST
            // 1 2
            // 2 30
            

            //因为终端每次发送文件的最大缓冲区是512字节，所以每次接收也是定义为512字节
            byte[] buffer = new byte[512];
            //int size = split[3].Length;
            int size = 0;
            long len = 0;

            string fileName = SIGNATURE_PICTURE_PATH + employeeId + ".jpg";//获得用户保存文件的路径
            Console.WriteLine("文件的存储路径" + fileName);
            
            //创建文件流，然后让文件流来根据路径创建一个文件
            FileStream fs = new FileStream(fileName, FileMode.Create);

            Console.WriteLine("开始写入文件");
            //fs.Write(message, 0, size);
            //Byte msg = Encoding.UTF8.GetBytes(split[3]);
            //fs.Write(split[3], 0, size);
            //len += size;
            //从终端不停的接受数据，然后写入文件里面，只到接受到的数据为0为止，则中断连接
            while ((size = socketClient.Receive(buffer, 0, buffer.Length, SocketFlags.None)) > 0)
            {
                fs.Write(buffer, 0, size);
                len += size;
            }
            Console.WriteLine("开始写入文件,共{0}个字节", len);

            fs.Flush();
            fs.Close();
            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();
        }


        /// <summary>
        /// 下载会签单信息文件
        /// </summary>
        private void DownloadContractFile(Socket socketClient, String contractId)
        {
            //  首先生成会签单信息
            String filePath = MSWordTools.DEFAULT_HDJCONTRACT_PATH + contractId + ".pdf";
            if(!(File.Exists((String)filePath)))     // 首先检测文件是否存在
            {
                String wordPath = MSWordTools.DEFAULT_HDJCONTRACT_PATH + contractId + ".doc";
                HDJContract contract = DALHDJContract.GetHDJContactAgree(contractId);       // 获取待生成的会签单信息
                MSWordTools.CreateHDJContractWordWithReplace(contract, wordPath);
                MSWordTools.WordConvertToPdf(wordPath, filePath);

                File.Delete((String)wordPath);
                MSWordTools.KillWordProcess();

            }
            DALSignatureStatus.SetAgreeContractDownload(contractId);

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            Console.WriteLine("开始下载文件{0}", filePath);
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();
        }


        /// <summary>
        /// 下载会签单信息文件
        /// </summary>
        private void DownloadFile(Socket socketClient, String filePath)
        {
            if (!(File.Exists((String)filePath)))     // 首先检测文件是否存在
            {
                return;
            }

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            Console.WriteLine("开始下载文件{0}", filePath);
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();
        }


        /// <summary>
        /// 下载会签单信息文件
        /// </summary>
        private void DownloadSignatureFile(Socket socketClient, String employeeId)
        {
            //  首先生成会签单信息
            String filePath = MSWordTools.DEFAULT_SIGNATURE_PATH + employeeId + ".jpg";
            if (!(File.Exists((String)filePath)))     // 如果文件不存在
            {
                return;
            }

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            Console.WriteLine("开始下载文件{0}", filePath);
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();
        }



        #region  下载会签单统计文件

        /// <summary>
        /// 下载会签单信息文件
        /// </summary>
        private void DownloadStatisticFile(Socket socketClient, int year, int categoryId)
        {
            //  首先生成会签单信息
            //ContractCategory category = DALContractIdCategory.GetCategory(categoryId);
            //String filePath = MSExcelTools.DEFAULT_STATISTIC_PATH + year +category.Category + ".xls";
            //Console.WriteLine("统计能申请当前Id = {0}, Category = {1}信息", category.Id, category.Category); 
            //if (File.Exists((String)filePath) == true)     // 首先检测文件是否存在
            //{
            //    File.Delete((String)filePath);
            //}

            //String excelFilePath = MSExcelTools.StatisticYearCategory(year, categoryId);
            String excelFilePath = MSExcelTools.DEFAULT_STATISTIC_FILE(year, categoryId);
            if (File.Exists((String)excelFilePath) == false)     // 首先检测文件是否存在
            {
                Console.WriteLine("统计表{0}不存在, 无法下载", excelFilePath);
                return ;
            }

            //MSExcelTools.KillExcelProcess();

            // 开始下载文件
            FileStream fs = new FileStream(excelFilePath, FileMode.OpenOrCreate, FileAccess.Read);
            Console.WriteLine("开始下载统计文件{0}", excelFilePath);
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();
        }


        /// <summary>
        /// 下载会签单信息文件
        /// </summary>
        private void DownloadingStatisticFile(Socket socketClient, int year, int categoryId)
        {
            //  首先生成会签单信息
            //ContractCategory category = DALContractIdCategory.GetCategory(categoryId);
            //String filePath = MSExcelTools.DEFAULT_STATISTIC_PATH + year +category.Category + ".xls";
            //Console.WriteLine("统计能申请当前Id = {0}, Category = {1}信息", category.Id, category.Category); 
            //if (File.Exists((String)filePath) == true)     // 首先检测文件是否存在
            //{
            //    File.Delete((String)filePath);
            //}

            //String excelFilePath = MSExcelTools.StatisticYearCategory(year, categoryId);
            String excelFilePath = MSExcelTools.DEFAULT_STATISTIC_FILE(year, categoryId);
            if (File.Exists((String)excelFilePath) == false)     // 首先检测文件是否存在
            {
                Console.WriteLine("统计表{0}不存在, 无法下载", excelFilePath);
                return;
            }

            //MSExcelTools.KillExcelProcess();

            // 开始下载文件
            FileStream fs = new FileStream(excelFilePath, FileMode.OpenOrCreate, FileAccess.Read);

            Console.WriteLine("开始下载文件{0}, 文件大小{1}", excelFilePath, fs.Length);
            
            //  先发送文件大小
            socketClient.Send(System.Text.Encoding.Unicode.GetBytes( fs.Length.ToString()));

            //  再发送文件信息
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();
        }
        #endregion

        #region  计划额度表的下载和上传
        /// <summary>
        /// 下载会签单计划额度分配表模版
        /// </summary>
        /// <param name="socketClient"></param>
        /// <param name="categoryId"></param>
        private void DownloadRegularloadTemplateFile(Socket socketClient, int year, int categoryId)
        {
            string filePath = "";
            //  如果计划额度表已经存在则直接下载即可
            //  计划文件的下载地址
            string regularloadFilePath = MSExcelTools.DEFAULT_REGULARLOAD_FILE(year, categoryId);
            if (File.Exists((String)regularloadFilePath) == true)     // 首先检测文件是否存在
            {
                Console.WriteLine("计划额度表{0}文件存在, 直接下载", regularloadFilePath);

                filePath = regularloadFilePath;
            }
            else
            {
                Console.WriteLine("计划额度表{0}文件不存在, 需要下载模版表", regularloadFilePath);

                string regularTemplateFilePath = MSExcelTools.DEFAULT_REGULARLOAD_TEMPLATE_FILE(categoryId);
                if (File.Exists((String)regularTemplateFilePath) == true)     // 首先检测文件是否存在
                {
                    Console.WriteLine("计划额度模版{0}文件存在, 可以下载", regularTemplateFilePath);
                    filePath = regularTemplateFilePath;
                }
                else
                {
                    Console.WriteLine("计划模版{0}文件不存在， 下载失败", regularTemplateFilePath);
                    return;
                }
            }


            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            Console.WriteLine("开始下载文件{0}, 文件大小{1}", filePath, fs.Length);
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

        }

        /// <summary>
        /// 下载会签单计划额度分配表模版
        /// </summary>
        /// <param name="socketClient"></param>
        /// <param name="categoryId"></param>
        private void DownloadingRegularloadTemplateFile(Socket socketClient, int year, int categoryId)
        {
            string filePath = "";
            //  如果计划额度表已经存在则直接下载即可
            //  计划文件的下载地址
            string regularloadFilePath = MSExcelTools.DEFAULT_REGULARLOAD_FILE(year, categoryId);
            if (File.Exists((String)regularloadFilePath) == true)     // 首先检测文件是否存在
            {
                Console.WriteLine("计划额度表{0}文件存在, 直接下载", regularloadFilePath);

                filePath = regularloadFilePath;
            }
            else
            {
                Console.WriteLine("计划额度表{0}文件不存在, 需要下载模版表", regularloadFilePath);

                string regularTemplateFilePath = MSExcelTools.DEFAULT_REGULARLOAD_TEMPLATE_FILE(categoryId);
                if (File.Exists((String)regularTemplateFilePath) == true)     // 首先检测文件是否存在
                {
                    Console.WriteLine("计划额度模版{0}文件存在, 可以下载", regularTemplateFilePath);
                    filePath = regularTemplateFilePath;
                }
                else
                {
                    Console.WriteLine("计划模版{0}文件不存在， 下载失败", regularTemplateFilePath);
                    return;
                }
            }


            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            
            Console.WriteLine("开始下载文件{0}, 文件大小{1}", filePath, fs.Length);
            
            //  先发送文件大小
            socketClient.Send(System.Text.Encoding.Unicode.GetBytes(fs.Length.ToString()));

            //  再发送文件信息
            byte[] fssize = new byte[fs.Length];
            BinaryReader reader = new BinaryReader(fs);
            reader.Read(fssize, 0, fssize.Length - 1);
            socketClient.Send(fssize);
            fs.Flush();
            fs.Close();
            Console.WriteLine("下载文件结束");

        }


        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadRegularloadFile(Socket socketClient, int year, int categoryId)
        {
            string filePath = MSExcelTools.DEFAULT_REGULARLOAD_FILE(year, categoryId);
            //  如果计划额度表已经存在则直接下载即可
            //  计划文件的下载地址
            //string regularloadFilePath = MSExcelTools.DEFAULT_REGULARLOAD_FILE(year, categoryId);
            //if (File.Exists((String)regularloadFilePath) == true)     // 首先检测文件是否存在
            //{
            //    Console.WriteLine("计划额度表{0}文件存在, 需要修改", regularloadFilePath);

            //    filePath = regularloadFilePath;
            //}
            //else
            //{
            //    return;
            //}

            Console.WriteLine("开始上传文件");
            // UPLOAD_PICTURE_REQUEST;2;30;length;byte
            // 取出后的结果为
            // 0 UPLOAD_PICTURE_REQUEST
            // 1 2
            // 2 30
            

            //  因为终端每次发送文件的最大缓冲区是512字节，所以每次接收也是定义为512字节
            byte[] buffer = new byte[1024 * 1024];
            //int size = split[3].Length;
            int size = 0;
            long len = 0;

            //string filePath = MSExcelTools.DEFAULT_REGULARLOAD_PATH + year.ToString() + categoryId.ToString() + "regularload.xls"; //获得用户保存文件的路径
            Console.WriteLine("文件的存储路径" + filePath);

            //创建文件流，然后让文件流来根据路径创建一个文件
            FileStream fs = new FileStream(filePath, FileMode.Create);

            Console.WriteLine("开始写入文件");
            //fs.Write(message, 0, size);
            //Byte msg = Encoding.UTF8.GetBytes(split[3]);
            //fs.Write(split[3], 0, size);
            //len += size;
            //从终端不停的接受数据，然后写入文件里面，只到接受到的数据为0为止，则中断连接
            while ((size = socketClient.Receive(buffer, 0, buffer.Length, SocketFlags.None)) > 0)
            {
                fs.Write(buffer, 0, size);
                len += size;
            }
            Console.WriteLine("开始写入文件,共{0}个字节", len);

            fs.Flush();
            fs.Close();
            //dict.Remove(socketClient.RemoteEndPoint.ToString());
            //socketClient.Close();


        }
        #endregion


    }
}
