package com.example.signpress;

import java.io.File;
import java.io.IOException;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Calendar;

import signsocket.SocketClient;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.DataSetObserver;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.sax.StartElementListener;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;
import android.widget.Toast;
import com.hwj.signpress.R;

public class DownLoadActivity extends Activity{
	private ImageButton ivBack;
	private Spinner yearSpinner;
	private String year;
	private ArrayList<String> yearList;

	private Spinner styleSpinner;
	private ArrayList<String> styleList;

	private Button btnDownLoad;

	private int choiceYear;
	private int choiceStyleID;
	private String choiceStyle="";


	private String downloadFile = null;    
	private final static int PORT = 7070;    
	private static String serverIP = "218.7.0.37";    
//	private static String serverIP = "192.168.1.100";    
	private Socket socket;    



	private Handler handler=new Handler(){
		public void handleMessage(android.os.Message msg) {
			switch(msg.what){
			case 0x123:
				Hint((String) msg.obj);
				break;
			}

		}
	};


	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_download);

		initView();
	}


	private void initView() {
		ivBack=(ImageButton) this.findViewById(R.id.leftutton);
		ivBack.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				finish();
			}
		});



		yearSpinner=(Spinner) this.findViewById(R.id.year);
		Calendar calendar=Calendar.getInstance();
		year=String.valueOf(calendar.get(calendar.YEAR));
		Log.i("nowYear", year);
		yearList=new ArrayList<String>();
		yearList.add(String.valueOf(Integer.parseInt(year)-1));
		yearList.add(year);
		yearList.add(String.valueOf(Integer.parseInt(year)+1));
		yearSpinner.setAdapter(new ArrayAdapter<String>(DownLoadActivity.this,android.R.layout.simple_spinner_item,yearList));

		yearSpinner.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> arg0, View arg1,
					int arg2, long arg3) {
				choiceYear=Integer.valueOf(yearList.get(arg2));

			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
				// TODO Auto-generated method stub

			}
		});





		styleList=new ArrayList<String>();
		styleSpinner=(Spinner) this.findViewById(R.id.style);
		styleList.add("界河航道例行养护工程");
		styleList.add("界河航道专项养护工程");
		styleList.add("内河航道例行养护工程");
		styleList.add("内河航道专项养护工程");
		styleSpinner.setAdapter(new ArrayAdapter<String>(DownLoadActivity.this,android.R.layout.simple_spinner_item,styleList));

		styleSpinner.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> arg0, View arg1,
					int arg2, long arg3) {
				choiceStyleID=arg2+1;
				choiceStyle=styleList.get(arg2);

			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
				// TODO Auto-generated method stub

			}
		});





		btnDownLoad=(Button) this.findViewById(R.id.btndownload);
		btnDownLoad.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Search search=new Search();
				search.setCategoryId(choiceStyleID);
				search.setYear(choiceYear);
				getDataThread(search);
			}


		});



	}

	private void getDataThread(final Search search) {
		new Thread(){
			public void run() {
				try {
					String str=DownLoadSocketClient.instance().downLoadRequest(search,choiceStyle);
					Message msg=new Message();
					msg.what=0x123;
					msg.obj=str;
					handler.sendMessage(msg);
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}.start();

	}
	
	
	public  void openFile(String filenameTemp){  
        File file = new File(filenameTemp);  
        if(!file.exists()){
        	
        }else{
        	 /* 取得扩展名 */  
            String end=file.getName().substring(file.getName().lastIndexOf(".") + 1,file.getName().length()).toLowerCase();   
            /* 依扩展名的类型决定MimeType */  
             getExcelFileIntent(filenameTemp);  
        }
      
    }  
      
  
    
  
    //Android获取一个用于打开Excel文件的intent     
    public  void getExcelFileIntent( String param ){    
        Intent intent = new Intent("android.intent.action.VIEW");     
        intent.addCategory("android.intent.category.DEFAULT");     
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);     
        Uri uri = Uri.fromFile(new File(param ));     
        intent.setDataAndType(uri, "application/vnd.ms-excel");     
         startActivity(intent) ;
    }     
  
 
    
    
    private void Hint(final String filenameTemp) {
        final AlertDialog.Builder builder = new AlertDialog.Builder(DownLoadActivity.this);
        builder.setTitle("文件下载成功，是否打开？");
        builder.setPositiveButton("确定",
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    	openFile(filenameTemp);
                    }
                });

        builder.setNegativeButton("取消", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        builder.create().show();
    }



}
