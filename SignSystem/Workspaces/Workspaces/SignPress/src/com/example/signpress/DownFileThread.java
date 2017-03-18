package com.example.signpress;

import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;

public class DownFileThread extends Thread {    
    private Socket socket;    
    private String downloadFile;    
    private final static int Buffer = 8 * 1024;    
    public DownFileThread(Socket socket, String downloadFile) {    
        super();    
        this.socket = socket;    
        this.downloadFile = downloadFile;    
    }    
    public Socket getSocket() {    
        return socket;    
    }    
    public void setSocket(Socket socket) {    
        this.socket = socket;    
    }    
    public String getDownloadFile() {    
        return downloadFile;    
    }    
    public void setDownloadFile(String downloadFile) {    
        this.downloadFile = downloadFile;    
    }    
    // 向服务器提出下载请求，返回下载文件的大小    
    private long request(String fileName, String password) throws IOException {    
        // 获取socket的输入流并包装成DataInputStream    
        DataInputStream in = new DataInputStream(socket.getInputStream());    
        // 获取socket的输出流并包装成PrintWriter    
        PrintWriter out = new PrintWriter(new OutputStreamWriter(    
                socket.getOutputStream()));    
        // 生成下载请求字符串    
        String requestString = fileName + "@ " + password;    
        out.println(requestString); // 发出下载请求    
        out.flush();    
        return in.readLong(); // 接收并返回下载文件长度    
    }    
    // 接收并保存文件    
    private void receiveFile(String localFile) throws Exception {    
        // 获取socket的输入流并包装成BufferedInputStream    
        BufferedInputStream in = new BufferedInputStream(    
                socket.getInputStream());    
        // 获取与指定本地文件关联的文件输出流    
        FileOutputStream out = new FileOutputStream(localFile);    
        byte[] buf = new byte[Buffer];    
        int len;    
        // 反复读取该文件中的内容，直到读到的长度为-1    
        while ((len = in.read(buf)) >= 0) {    
            out.write(buf, 0, len); // 将读到的数据，按读到的长度写入输出流    
            out.flush();    
        }    
        out.close();    
        in.close();    
    }    
    // 从服务器下载文件    
    public void download(String downloadFile) throws Exception {    
        try {    
            String password = "password";    
            // String downloadFile ="imissyou.mp3";    
            String localpath = "/sdcard/";    
            String localFile = localpath + downloadFile;    
            long fileLength = request(downloadFile, password);    
            // 若获取的文件长度大于等于0，说明允许下载，否则说明拒绝下载    
            if (fileLength >= 0) {    
                System.out.println("fileLength: " + fileLength + " B");    
                System.out.println("downing...");    
                receiveFile(localFile); // 从服务器接收文件并保存至本地文件    
                System.out.println("file:" + downloadFile + " had save to "    
                        + localFile);    
            } else {    
                System.out.println("download " + downloadFile + " error! ");    
            }    
        } catch (IOException e) {    
            System.out.println(e.toString());    
        } finally {    
            socket.close(); // 关闭socket    
        }    
    }    
    @Override    
    public void run() {    
        System.out.println("DownFileThread currentThread--->"    
                + DownFileThread.currentThread().getId());    
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