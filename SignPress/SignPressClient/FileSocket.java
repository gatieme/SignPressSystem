package signsocket;


import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintStream;
import java.io.PrintWriter;
import java.lang.reflect.Type;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import signdata.Employee;
import signdata.HDJContract;
import signdata.SHDJContract;
import signdata.SignatureDetail;
import signdata.User;

/*
Socket 提供了getInputStream()和getOutputStream()用来得到输入流和输出流进行读写操作，
这两个方法分别返回InputStream和OutputStream。
为了方便读写，我们常常在InputStream和OutputStream基础上进行包装得到
DataInputStream, DataOutputStream, 
PrintStream, InputStreamReader, 
OutputStreamWriter, printWriter等。
示例代码：
PrintStream printStream = new PrintStream(new BufferedOutputStream(socket.getOutputStream()));
PrintWriter printWriter = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream(), true)));
printWriter.println(String msg);
DataInputStream dis = new DataInputStream(socket.getInputStream());
BufferedReader br =  new BufferedReader(new InputStreamReader(socket.getInputStream()));
String line = br.readLine();
*/
public class FileSocketClient
{
	// 设置服务器IP和端口
    public static Socket m_socket = null;
    public static SocketClient socketClient = null;
    //private static PrintWriter  m_printWriter    = null;         
        
    //public static BufferedReader m_buffer = null;
    public static byte[] m_recvBuffer;
   // public static DataOutputStream out = null;
    //public static DataInputStream in = null;
    //public static BufferedReader inbuff=null;
    private static final String SERVER_IP   = "192.168.253.1"; //"192.168.1.200";
    private static final int    SERVER_PORT      = 6060;//7777;
    
	public static synchronized FileSocketClient instance()
	{
		if (socketClient == null)
		{
			socketClient = new SocketClient();
		}
		
		return socketClient;
	}
	
	public FileSocketClient()
	{
		try
		{
			initialize();
			
		} catch (IOException e)
		{
			// TODO 自动生成的 catch 块
			e.printStackTrace();
		}
	}


    public static void close()
    {
    	try
    	{
    		//_printWriter.close();
    		out.close();
            in.close();                
            m_socket.close();
            if(m_socket.isClosed())
            	System.out.println("socket is closed...");        
                
        }        
        catch (Exception e)
    	{
            e.printStackTrace();
            System.out.println("socket isn't closed...");                
        }
    }

    public void sendMessage(String message)
    {
        try
        {
            out.writeBytes(message);
            out.flush();
        }
        catch (Exception e)
        {
        	System.out.println(e.getStackTrace());        

        }
    }
    

    
	public void  initialize() throws IOException
	{
		m_socket = new Socket( );
        m_socket.connect(new InetSocketAddress(SERVER_IP, SERVER_PORT), 5000);              //inbuff=new BufferedReader(new InputStreamReader(_socket.getInputStream()));
        m_recvBuffer = new byte[1024*1024]; 
    	//m_socket = new Socket(InetAddress.getByName(SERVER_IP), SERVER_PORT);
        //in = new DataInputStream(m_socket.getInputStream());
        //out = new DataOutputStream(m_socket.getOutputStream());    

        //  发送数据
        //PrintStream m_printWriter = new PrintStream(m_socket.getOutputStream()); //发送数据,PrintStream最方便
        //m_printWriter.write(message.getBytes());
        // 接收返回信息
        //BufferedReader m_buffer = new BufferedReader(new InputStreamReader(m_socket.getInputStream()));; //一次性接收完成读取Socket的输入流，在其中读出返回信息

	}
	

	// 下载统计文件
	public boolean DownloadStatistic(Search search, String filePath)
	{
		try
		{
			FileSocketClient socket FileSocketClient();
			
			//  发送查询为签字的会签单的信息以及员工的ID
			//  DOWNLOADING_STATISTIC_REQUEST和DOWNLOAD_STATISTIC_REQUEST
			//  两个请求的区别是DOWNLOADING会先接收文件大小
			//SocketMessage message = new SocketMessage(ClientRequest.DOWNLOADING_STATISTIC_REQUEST, serach);
			
			//out.write(message.Package.getBytes("utf-8"));
			//out.flush();
			
			Thread downFileThread = new Thread(new DownFileThread(socket, search, downloadFile));    
			downFileThread.start();    
			return true;
		}
		catch (IOException e)
		{
			// TODO 自动生成的 catch 块
			e.printStackTrace();
		}
		return false;	
	}


	// 下载计划文件
	public boolean DownloadRegularload(Search search, String filePath)
	{
		try
		{
			FileSocketClient socket FileSocketClient();
			
			//  发送查询为签字的会签单的信息以及员工的ID
			//  DOWNLOADING_STATISTIC_REQUEST和DOWNLOAD_STATISTIC_REQUEST
			//  两个请求的区别是DOWNLOADING会先接收文件大小
			//SocketMessage message = new SocketMessage(ClientRequest.DOWNLOADING_REGULARLOAD_REQUEST, serach);
			
			//out.write(message.Package.getBytes("utf-8"));
			//out.flush();
			
			Thread downFileThread = new Thread(new DownFileThread(socket, search, downloadFile));    
			downFileThread.start();    
			return true;
		}
		catch (IOException e)
		{
			// TODO 自动生成的 catch 块
			e.printStackTrace();
		}
		return false;	
	}
}



