package org.piaozhiye.study;    
import java.io.BufferedInputStream;    
import java.io.DataInputStream;    
import java.io.FileOutputStream;    
import java.io.IOException;    
import java.io.OutputStreamWriter;    
import java.io.PrintWriter;    
import java.net.Socket;    

// http://blog.csdn.net/chenlei1889/article/details/6367410
// http://blog.csdn.net/nupt123456789/article/details/8248967
// http://bbs.csdn.net/topics/390992045

public class DownFileThread extends Thread
{    
	private Serach search;				//  请求数据
    private Socket socket;				//  套接字描述符
    private String downloadFilePath;    //  下载文件名
    private final static int Buffer = 8 * 1024;


    public static DataOutputStream out = null;
    public static DataInputStream in = null;


    public DownFileThread(Socket socket, String downloadFile)
	{    
        super();    
        this.socket = socket;    
        this.downloadFile = downloadFile;    
    }

    
	public Socket getSocket()
	{    
        return socket;    
    }    
    
	public void setSocket(Socket socket)
	{    
        this.socket = socket;    
    }    
    
	public String getDownloadFile()
	{    
        return downloadFile;    
    }    
    
	public void setDownloadFile(String downloadFile)
	{
        this.downloadFile = downloadFile;    
    }    
    
	// 向服务器提出下载请求，返回下载文件的大小    
    private long request(Search search) throws IOException
	{    
        // 获取socket的输入流并包装成DataInputStream    
        in = new DataInputStream(m_socket.getInputStream());
        // 获取socket的输出流并包装成DataOutputStream
		out = new DataOutputStream(m_socket.getOutputStream());          

		//  发送查询为签字的会签单的信息以及员工的ID
		SocketMessage message = new SocketMessage(ClientRequest.DOWNLOAD_STATISTIC_REQUEST, serach);
			
		out.write(message.Package.getBytes("utf-8"));
		out.flush();

		//  先接收文件的大小
        return in.readLong(); // 接收并返回下载文件长度    
    }    
    
	// 接收并保存文件    
    private void receiveFile(String localFile) throws Exception
		{    
        // 获取socket的输入流并包装成BufferedInputStream    
        BufferedInputStream in = new BufferedInputStream(socket.getInputStream());    
        // 获取与指定本地文件关联的文件输出流    
        FileOutputStream out = new FileOutputStream(localFile);

        byte[] buf = new byte[Buffer];    
        int len;    
        // 反复读取该文件中的内容，直到读到的长度为-1    
        while ((len = in.read(buf)) >= 0)
		{    
            out.write(buf, 0, len); // 将读到的数据，按读到的长度写入输出流    
            out.flush();    
        }    
        out.close();    
        in.close();    
    }

    // 从服务器下载文件    
    public void download(Search search, String downloadFile) throws Exception
	{    
        try {    
            String localpath = "/sdcard/";    
            String localFile = localpath + downloadFile;    
            
			long fileLength = request(search);    
            
			// 若获取的文件长度大于等于0，说明允许下载，否则说明拒绝下载    
            if (fileLength >= 0)
			{    
                System.out.println("fileLength: " + fileLength + " B");    
                System.out.println("downing...");    
                receiveFile(localFile); // 从服务器接收文件并保存至本地文件    
                System.out.println("file:" + downloadFile + " had save to " + localFile);    
            } else {    
                System.out.println("download " + downloadFile + " error! ");    
            }    
        } catch (IOException e)
		{    
            System.out.println(e.toString());    
        } finally
		{    
            socket.close(); // 关闭socket    
        }    
    }    

    @Override    
    public void run()
	{    
        System.out.println("DownFileThread currentThread--->" + DownFileThread.currentThread().getId());    
        // TODO Auto-generated method stub    
        try {    
            download(downloadFile);    
        } catch (Exception e) {    
            // TODO Auto-generated catch block    
            e.printStackTrace();    
        }    
        super.run();    
    }    
        
}    