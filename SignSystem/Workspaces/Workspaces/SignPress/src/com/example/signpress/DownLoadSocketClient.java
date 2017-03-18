package com.example.signpress;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.InetSocketAddress;
import java.net.Socket;

import signdata.Employee;
import signdata.User;
import signsocket.ClientRequest;
import signsocket.ServerResponse;
import signsocket.SocketClient;
import signsocket.SocketMessage;

import android.util.Log;

import com.google.gson.Gson;

public class DownLoadSocketClient {

	// 设置服务器IP和端口
	public static Socket m_socket = null;
	public static DownLoadSocketClient downLoadSocketClient = null;
	private static PrintWriter  m_printWriter    = null;         

	public static BufferedReader m_buffer=null;
	public byte[] m_recvBuffer;
	public static DataOutputStream out=null;
	public static DataInputStream in=null;
	//public static BufferedReader inbuff=null;
    //private static final String SERVER_IP   = "192.168.1.100"; //"221.212.58.67";
	private static final String SERVER_IP   = "218.7.0.37"; //"221.212.58.67";
	//private static final String SERVER_IP   =  "10.0.51.141";
	private static final int    SERVER_PORT      = 7070;//7777;


	DataInputStream getMessageStream = null;

	//文件存储路径
	//	String savePath = "/mnt/sdcard/socket/";
	//	String fileName="";
	//	String filenameTemp = savePath+ "/aa" + ".xls";  


	//文件存储路径
	private String savePath = "/mnt/sdcard/HWJ统计表/";
	private String filenameTemp = "";



	public static synchronized DownLoadSocketClient instance()
	{
		if (downLoadSocketClient == null)
		{
			downLoadSocketClient = new DownLoadSocketClient();
		}

		return downLoadSocketClient;
	}

	public DownLoadSocketClient()
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

	// 处理丢包问题的接收函数
	public int receiveMessage() throws IOException
	{
		int len = in.read(m_recvBuffer, 0, 1024 * 4);

		return len;  
	}

	public int receiveMessage(byte[] recvBuffer) throws IOException
	{
		int len = in.read(recvBuffer, 0, 1024 * 4);

		return len;  
	}


	public void  initialize() throws IOException
	{
		m_socket = new Socket( );
		m_socket.connect(new InetSocketAddress(SERVER_IP, SERVER_PORT), 5000);              //inbuff=new BufferedReader(new InputStreamReader(_socket.getInputStream()));
		//m_recvBuffer = new byte[1024*1024]; 
		//m_socket = new Socket(InetAddress.getByName(SERVER_IP), SERVER_PORT);
		m_socket.setTcpNoDelay(true);


		m_buffer = new BufferedReader(new InputStreamReader(m_socket.getInputStream(),"UTF-8"));; //一次性接收完成读取Socket的输入流，在其中读出返回信息
		in = new DataInputStream(m_socket.getInputStream());
		out = new DataOutputStream(m_socket.getOutputStream());
		//  发送数据
		//PrintStream m_printWriter = new PrintStream(m_socket.getOutputStream()); //发送数据,PrintStream最方便
		//m_printWriter.write(message.getBytes());
		// 接收返回信息


	}



	public String downLoadRequest(Search search,String choiceStyle) throws Exception
	{
		
		filenameTemp=savePath+ "/"+search.getYear()+choiceStyle + ".xls";  
		CreateText();
		//  发送登录请求以及数据
		SocketMessage message = new SocketMessage(ClientRequest.DOWNLOAD_STATISTIC_REQUEST, search);
		Log.d("msg", message.toString());
		try {
			out.write(message.Package.getBytes("utf-8"));
			out.flush();



			long tmp = System.currentTimeMillis();//获取当前系统时间
			System.out.println("开始发送时间：" + "\n"+tmp);

			FileOutputStream fileOutputStream=new FileOutputStream(filenameTemp);


			FileInputStream fs=new FileInputStream(filenameTemp);

			//			fw = new FileWriter(filenameTemp, true);//  
			// 创建FileWriter对象，用来写入字符流  


			
			while (true) {
				int read = 0;
				if (in != null) {
					byte buffer[]=new byte[1024];
					int count=in.read(buffer);
					if(count!=-1){
						Log.i("count",String.valueOf(count));			
						fileOutputStream.write(buffer, 0,count);
					}else {
						break;
					}
				}
			
			}
			tmp = System.currentTimeMillis();//当前时间


			System.out.println("文件保存路径：" + filenameTemp+"---时间："+tmp);
			out.close();
			fileOutputStream.close();
			fs.close();
			


		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return filenameTemp;

	}





	//创建文件夹及文件  
	public void CreateText() throws IOException {  
		File file = new File(savePath);  
		if (!file.exists()) {  
			try {  
				//按照指定的路径创建文件夹  
				file.mkdirs();  
			} catch (Exception e) {  
				// TODO: handle exception  
			}  
		}  
		File dir = new File(filenameTemp);  
		if (!dir.exists()) {  
			try {  
				//在指定的文件夹中创建文件  
				dir.createNewFile();  
			} catch (Exception e) {  
			}  
		}  

	}  






}
