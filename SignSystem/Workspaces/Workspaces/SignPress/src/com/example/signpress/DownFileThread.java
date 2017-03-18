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
    // �����������������󣬷��������ļ��Ĵ�С    
    private long request(String fileName, String password) throws IOException {    
        // ��ȡsocket������������װ��DataInputStream    
        DataInputStream in = new DataInputStream(socket.getInputStream());    
        // ��ȡsocket�����������װ��PrintWriter    
        PrintWriter out = new PrintWriter(new OutputStreamWriter(    
                socket.getOutputStream()));    
        // �������������ַ���    
        String requestString = fileName + "@ " + password;    
        out.println(requestString); // ������������    
        out.flush();    
        return in.readLong(); // ���ղ����������ļ�����    
    }    
    // ���ղ������ļ�    
    private void receiveFile(String localFile) throws Exception {    
        // ��ȡsocket������������װ��BufferedInputStream    
        BufferedInputStream in = new BufferedInputStream(    
                socket.getInputStream());    
        // ��ȡ��ָ�������ļ��������ļ������    
        FileOutputStream out = new FileOutputStream(localFile);    
        byte[] buf = new byte[Buffer];    
        int len;    
        // ������ȡ���ļ��е����ݣ�ֱ�������ĳ���Ϊ-1    
        while ((len = in.read(buf)) >= 0) {    
            out.write(buf, 0, len); // �����������ݣ��������ĳ���д�������    
            out.flush();    
        }    
        out.close();    
        in.close();    
    }    
    // �ӷ����������ļ�    
    public void download(String downloadFile) throws Exception {    
        try {    
            String password = "password";    
            // String downloadFile ="imissyou.mp3";    
            String localpath = "/sdcard/";    
            String localFile = localpath + downloadFile;    
            long fileLength = request(downloadFile, password);    
            // ����ȡ���ļ����ȴ��ڵ���0��˵���������أ�����˵���ܾ�����    
            if (fileLength >= 0) {    
                System.out.println("fileLength: " + fileLength + " B");    
                System.out.println("downing...");    
                receiveFile(localFile); // �ӷ����������ļ��������������ļ�    
                System.out.println("file:" + downloadFile + " had save to "    
                        + localFile);    
            } else {    
                System.out.println("download " + downloadFile + " error! ");    
            }    
        } catch (IOException e) {    
            System.out.println(e.toString());    
        } finally {    
            socket.close(); // �ر�socket    
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