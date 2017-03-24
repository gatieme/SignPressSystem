package com.example.signpress;

//import android.support.v7.app.ActionBarActivity;

import java.io.BufferedWriter;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;








import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener; 
import android.widget.EditText;
import android.widget.Toast;
import android.view.View;
import android.view.View.OnClickListener;




import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import com.hljhw.signature.R;
import signdata.Employee;
import signdata.User;
import signsocket.NetManager;
import signsocket.SocketClient;

/*
 * ԭ�� 
android.os.NetworkOnMainThreadException��˵��Ҫ�����߳��з������磬
�����android3.0�汾��ʼ��ǿ�Ƴ����������߳��з������磬Ҫ�ѷ���������ڶ������߳��С�

�ڿ����У�Ϊ�˷�ֹ���������������̣߳�
һ�㶼Ҫ�ѷ���������ڶ����߳��л����첽�߳�AsyncTask�С�

1����Ҫ������Щǿ�Ʋ�������Ļ���������onCreate()����������� 
StrictMode.ThreadPolicy policy=new StrictMode.ThreadPolicy.Builder().permitAll().build();
StrictMode.setThreadPolicy(policy);
���ڷ����ϼ���@SuppressLint("NewApi")�����ԣ�OK��

2����������ʷŵ������߳���

3����������ʷŵ��첽����AsyncTask��
���ǲ��ô˷���ʵ��
 * */

//@SuppressWarnings("deprecation")
public class LoginActivity extends Activity 
{
	
	//  �û��������
	private EditText editTextUsername;
	
	// ���������
	private EditText editTextPassword;

	// ��¼��ť
	private Button buttonLogin;
	
	//��ס����
	private CheckBox cbPassWord;
	
	private PrintWriter output ;
    
	private ProgressDialog progressBar = null;
	
	private SharedPreferences sp;
	public static Context s_context;

	
	private AppContext app;
	@Override
	@SuppressLint("NewApi")
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);		//  ���ò����ļ�Ϊactivity_login.xml
		
		// �õ�����EditText����
		this.editTextUsername = (EditText)findViewById(R.id.editTextUsername);
		this.editTextPassword = (EditText)findViewById(R.id.editTextPassword);
		this.cbPassWord=(CheckBox)findViewById(R.id.cb_mima);
		this.sp = this.getSharedPreferences("userInfo", Context.MODE_WORLD_READABLE); 
		
		// �õ�����button����
		this.buttonLogin = (Button)findViewById(R.id.buttonLogin);
		
		//StrictMode.ThreadPolicy policy=new StrictMode.ThreadPolicy.Builder().permitAll().build();
		//StrictMode.setThreadPolicy(policy);
		//���������ӵĴ���
	    StrictMode.setThreadPolicy(new StrictMode.ThreadPolicy.Builder() 
	        .detectDiskReads() 
	        .detectDiskWrites() 
	        .detectNetwork()
	        .penaltyLog() 
	        .build()); 
	        StrictMode.setVmPolicy(new StrictMode.VmPolicy.Builder() 
	        .detectLeakedSqlLiteObjects() 
	        .detectLeakedClosableObjects() 
	        .penaltyLog() 
	        .penaltyDeath() 
	        .build());
		//NetManager.instance().init(this);
		
		NetManager.instance().init(this);;
		s_context = this;
		
		//SocketThreadManager.sharedInstance();
		
		if(sp.getBoolean("ISCHECK", false))  
        {  
          //����Ĭ���Ǽ�¼����״̬  
          cbPassWord.setChecked(true);  
          editTextUsername.setText(sp.getString("USER_NAME", ""));  
          editTextPassword.setText(sp.getString("PASSWORD", ""));  
        }
		
		// Ϊ��¼��ť�󶨵����¼�
		this.buttonLogin.setOnClickListener(new OnClickListener()
		{
			@Override
			public void onClick(View v)
			{
				progressBar = new ProgressDialog(v.getContext());
				progressBar.setCancelable(true);
				progressBar.setMessage("���ڵ�½");
				progressBar.setProgressStyle(ProgressDialog.STYLE_SPINNER);
				progressBar.show();
				loginAsyncTask();
			}
			
			private void loginAsyncTask()
			{
		        new AsyncTask<String, Void, Object>()
		        {
		          
		          //��doInBackground ִ����ɺ�onPostExecute ��������UI �̵߳��ã�
		          // ��̨�ļ�������ͨ���÷������ݵ�UI �̣߳������ڽ�����չʾ���û�.
		          @Override
				protected void onPostExecute(Object result)
		          {
		            super.onPostExecute(result);
		            //activity_main_btn1.setText("������Ϊ��"+result);//���Ը���UI
		        	//  ��ȡ�ı����е��û���������
		            boolean r = isNetAvailable(LoginActivity.this);
		            if(r)
		            {
					String username = editTextUsername.getText().toString();
					String password = editTextPassword.getText().toString();
					
					if(username.equals("")||password.equals(""))
					{
						Toast.makeText(LoginActivity.this, "�û����������벻��Ϊ��", Toast.LENGTH_SHORT).show();
					}
					else
					{
					User user = new User(username, password);
			        app=(AppContext)getApplication();

					app.setUser(user);
					
	
					// �ֻ�����������socket����
					//if (NetManager.instance().isNetworkConnected())
					{
						//SocketClient client = SocketClient.instance();
						//SocketMessage message = new SocketMessage(ClientRequest.LOGIN_REQUEST, user);
						Employee employee;
						employee = SocketClient.instance().loginRequest(user);
						if(employee==null)
						{
							Toast.makeText(LoginActivity.this, "Զ�̷�����δ��Ӧ", Toast.LENGTH_SHORT).show();
						}
 						else if(employee.Id != -1 && employee.CanSign == 1)
						{
							if(cbPassWord.isChecked())
							{
								Editor editor = sp.edit();  
			                    editor.putString("USER_NAME", username);  
			                    editor.putString("PASSWORD",password);  
			                    editor.commit(); 
							}
							
							app.setEmployee(employee);
							Intent intent = new Intent();  
							//����Intent��class���ԣ���ת��SecondActivity  
							intent.setClass(LoginActivity.this, MainActivity.class);  
							//Ϊintent��Ӷ������Ϣ  
							//intent.putExtra("Usename", username);  
							//intent.putExtra("Password", password); 
							//����Activity  
							startActivity(intent);  
							finish();
						}
						else if(employee.Id != -1 && employee.CanSign != 1)
						{
							progressBar.dismiss();
							// ʹ�õ����������û�û��ǩ��Ȩ���޷���¼
							Toast.makeText(LoginActivity.this, "��û��ǩ��Ȩ�ޣ��޷���½", Toast.LENGTH_SHORT).show();
						}
						else        
						{
							progressBar.dismiss();
							// ʹ�õ����������û�������û���������������
							Toast.makeText(LoginActivity.this, "�û������������", Toast.LENGTH_SHORT).show();
						}
					}
		              }
					}
		            else
					{
		            	progressBar.dismiss();
		            	Toast.makeText(LoginActivity.this, "��ǰδ������������粻����", Toast.LENGTH_SHORT).show();
					}
		          }

		          private boolean isNetAvailable(Context context) {
					// TODO �Զ����ɵķ������ 
		        	 ConnectivityManager manager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE); 
		        	 if(manager != null)
		        	 {
		             NetworkInfo info = manager.getActiveNetworkInfo();  
		        	 return (info != null && info.isAvailable());  
		        	 }
		             else
		             {
		            	 return false;
		             }
				}

				//�÷��������ں�̨�߳��У���˲����ڸ��߳��и���UI��UI�߳�Ϊ���߳�
		          @Override
				protected Object doInBackground(String... params)
		          {
						 return true;
		          }

		        }.execute();
		        
		      }
		});
		
		cbPassWord.setOnCheckedChangeListener(new OnCheckedChangeListener() {  
			@Override
			public void onCheckedChanged(CompoundButton buttonView,boolean isChecked) {  
                if (cbPassWord.isChecked()) {  
                      
                    System.out.println("��ס������ѡ��");  
                    sp.edit().putBoolean("ISCHECK", true).commit();  
                      
                }else {  
                      
                    System.out.println("��ס����û��ѡ��");  
                    sp.edit().putBoolean("ISCHECK", false).commit();  
                      
                }  
  
            }  
        });  
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	
	
	/*********�����ͻ��˷���***********/
    public void Connect()
    {   
       try 
       {
           InetAddress addr = InetAddress.getByName("10.0.213.117");//������ֻ�����IP��ַ����һ��wifi�Ϳ���֪��
           System.out.println("�ͻ��˷�������");
                 
           //�� ���������˷�����������
           Socket socket = new Socket(addr,6666);
           System.out.println("���ӳɹ���socket=" + socket);
               
           //  ͨ������socketͨ���õ������
           output = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);
           System.out.println("�������ȡ�ɹ�");
           output.println(editTextUsername.getEditableText().toString());
           output.flush();
             

            
           /****/
           Intent intentdump =new Intent();
           intentdump.putExtra("username", editTextUsername.getText().toString());
           intentdump.putExtra("password", editTextPassword.getText().toString());
           //intentdump.setClass(LoginActivity.this, MainActivity.class);
           
           LoginActivity.this.startActivity(intentdump);
       }
       catch (Exception e) 
       {
           e.printStackTrace();
       }
             
    }
}
