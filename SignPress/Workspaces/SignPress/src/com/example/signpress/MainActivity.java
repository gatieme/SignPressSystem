package com.example.signpress;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Timer;
import java.util.TimerTask;

import signdata.Employee;
import signdata.HDJContract;
import signdata.SHDJContract;
import signsocket.SocketClient;
import android.app.Activity;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.BaseExpandableListAdapter;
import android.widget.Button;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnChildClickListener;
import android.widget.TextView;
import android.widget.Toast;
import com.hljhw.signature.R;

public class MainActivity extends Activity implements OnClickListener {
	
	private AppContext app;
	private ArrayList<SHDJContract> unsignedList;
    private ArrayList<SHDJContract> signedList;
    
    private List<Map<String,String>> child1;
    private List<Map<String,String>> child2;
    private List<List<Map<String, String>>> childs;
    
    private Button btnExit;
    private static boolean isExit = false;
    private BaseExpandableListAdapter adapter ;
    private Button btnDowload;
    
    
    
    private Handler handler=new Handler();
    private Runnable runnable = new Runnable() 
    {
    	@Override
    	public void run()
    	{
    		app=(AppContext)getApplication();
            final Employee emp = app.getEmployee();
    		unsignedList = SocketClient.instance().QueryUnsignedHDJContract(emp.Id);
            int currlength1=unsignedList.size();
            if(currlength1>0)
            {
            	List<Map<String,String>> currentunsignList=new ArrayList<Map<String,String>>();
                for(int i=0;i<unsignedList.size();i++)
                {
                	Map<String,String> map = new HashMap<String,String>();
                	map.put("Name",unsignedList.get(i).ProjectName );
                	map.put("Num", unsignedList.get(i).Id);
                	map.put("Submit", unsignedList.get(i).SubmitEmployeeName);
                	currentunsignList.add(map);
                }
                
                childs.clear();
                childs.add(currentunsignList);
                childs.add(child2);
                adapter.notifyDataSetChanged();
                SendNotification();
            }
            handler.postDelayed(this, 120*1000);
    	}
    	
		private void SendNotification() {
			// TODO �Զ����ɵķ������
			String ns = Context.NOTIFICATION_SERVICE;
	        NotificationManager mNotificationManager = (NotificationManager) getSystemService(ns);
	        //����֪ͨ��չ�ֵ�������Ϣ
	        //int icon = R.drawable.notification_template_icon_bg;
	        //CharSequence tickerText = "�ҵ�֪ͨ������";
	        //long when = System.currentTimeMillis();
	        //Notification notification = new Notification(icon, tickerText, when);
	         
	        //��������֪ͨ��ʱҪչ�ֵ�������Ϣ
	        Context context = getApplicationContext();
	        //CharSequence contentTitle = "�ҵ�֪ͨ����չ������";
	        //CharSequence contentText = "�ҵ�֪ͨ��չ����ϸ����";
	        Intent notificationIntent = new Intent(context, MainActivity.class);
	        notificationIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
	        
	        PendingIntent contentIntent = PendingIntent.getActivity(context, 0,
	                notificationIntent, 0);
	        //notification.setLatestEventInfo(context, contentTitle, contentText,
	                //contentIntent);
	         
	        Notification notification = new Notification.Builder(context)    
	         .setAutoCancel(true)    
	         .setContentTitle("ǩ����Ϣ")    
	         .setContentText("�����ύ������Ҫ����")    
	         .setContentIntent(contentIntent)    
	         .setSmallIcon(R.drawable.ic_launcher)    
	         .setWhen(System.currentTimeMillis())
	         .setDefaults(Notification.DEFAULT_ALL)
	         .build();   
	        
	        //��mNotificationManager��notify����֪ͨ�û����ɱ�������Ϣ֪ͨ
	        mNotificationManager.notify(1, notification);
		}
    };
     
    
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		requestWindowFeature(Window.FEATURE_CUSTOM_TITLE);
        setContentView(R.layout.activity_main);
        getWindow().setFeatureInt(Window.FEATURE_CUSTOM_TITLE, R.layout.titlebar);
        
        btnExit=(Button)findViewById(R.id.exit);
        btnExit.setOnClickListener(this);
        
        
        btnDowload=(Button) this.findViewById(R.id.download);
        btnDowload.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				if(app.getEmployee().CanStatistic==1)
				{
				Intent intent=new Intent(MainActivity.this,DownLoadActivity.class);
				startActivity(intent);
				}
				else 
				{
					Toast.makeText(MainActivity.this, "�Բ�����û������Ȩ�ޣ�", Toast.LENGTH_SHORT).show();
				}
				
			}
		});
        
		app=(AppContext)getApplication();
        final Employee emp = app.getEmployee();
        unsignedList = SocketClient.instance().QueryUnsignedHDJContract(emp.Id);
        signedList=SocketClient.instance().QuerySignedTopHDJContract(emp.Id);
		
		Map<String,String> title1=new HashMap<String,String>();
		Map<String,String> title2=new HashMap<String,String>();
		
		title1.put("group","��Ҫǩ��");
		title2.put("group", "�Ѿ�ǩ��");
		
		//����һ����Ŀ����
        final List<Map<String, String>> groups = new ArrayList<Map<String,String>>();
        groups.add(title1);
        groups.add(title2);
        
        child1=new ArrayList<Map<String,String>>();
        for(int i=0;i<unsignedList.size();i++)
        {
        	Map<String,String> map = new HashMap<String,String>();
        	map.put("Name",unsignedList.get(i).ProjectName );
        	map.put("Num", unsignedList.get(i).Id);
        	map.put("Submit", unsignedList.get(i).SubmitEmployeeName);
        	child1.add(map);
        }
        
        child2=new ArrayList<Map<String,String>>();
        for(int i=0;i<signedList.size();i++)
        {
        	Map<String,String> map = new HashMap<String,String>();
        	map.put("Name",signedList.get(i).ProjectName );
        	map.put("Num", signedList.get(i).Id);
        	map.put("Submit", signedList.get(i).SubmitEmployeeName);
        	child2.add(map);
        }
        
        //����������Ŀ
        childs = new ArrayList<List<Map<String,String>>>();
        childs.add(child1);
        childs.add(child2);
               
         adapter = new BaseExpandableListAdapter()
        {
        	@Override
            public int getGroupCount() {
                // TODO Auto-generated method stub
                return groups.size();
            }

            @Override
            public Object getGroup(int groupPosition) {
                // TODO Auto-generated method stub
                return groups.get(groupPosition);
            }
            
            @Override
            public long getGroupId(int groupPosition) {
                // TODO Auto-generated method stub
                return groupPosition;
            }

            @Override
            public int getChildrenCount(int groupPosition) {
                // TODO Auto-generated method stub
                return childs.get(groupPosition).size();
            }

            @Override
            public Object getChild(int groupPosition, int childPosition) {
                // TODO Auto-generated method stub
                return childs.get(groupPosition).get(childPosition);
            }

            @Override
            public long getChildId(int groupPosition, int childPosition) {
                // TODO Auto-generated method stub
                return childPosition;
            }

            @Override
            public boolean hasStableIds() {
                // TODO Auto-generated method stub
                return true;
            }
            
            @Override
            public View getGroupView(int groupPosition, boolean isExpanded,
                    View convertView, ViewGroup parent)
            {
            	ViewHolder holder;
            	if(convertView == null)
            	{
            		convertView=LayoutInflater.from(MainActivity.this).inflate(R.layout.contractgroup, null);
            		holder=new ViewHolder();
            		holder.title=(TextView)convertView.findViewById(R.id.myGroup);
            		convertView.setTag(holder);
            	}
            	else
            	{
            		holder = (ViewHolder)convertView.getTag();
            	}
            	holder.title.setText(groups.get(groupPosition).get("group").toString());
            	
            	return convertView;
            }
            
            @Override
            public View getChildView(int groupPosition, int childPosition,
                    boolean isLastChild, View convertView, ViewGroup parent) 
                {
            	ViewHolder holder;
            	if(convertView == null)
            	{
            		convertView=LayoutInflater.from(MainActivity.this).inflate(R.layout.contractdetails, null);
            		holder=new ViewHolder();
            		holder.title=(TextView)convertView.findViewById(R.id.contract_name);
            		holder.Num=(TextView)convertView.findViewById(R.id.contract_num);
            		holder.Submit=(TextView)convertView.findViewById(R.id.contract_submit);
            		convertView.setTag(holder);
            	}
            	else
            	{
            		holder = (ViewHolder)convertView.getTag();
            	}
            	holder.title.setText("��������:"+childs.get(groupPosition).get(childPosition).get("Name").toString());
            	holder.Num.setText("���:"+childs.get(groupPosition).get(childPosition).get("Num").toString());
            	holder.Submit.setText("�ύ��:"+childs.get(groupPosition).get(childPosition).get("Submit").toString());
            	
            	return convertView;
                }
            
            @Override
            public boolean isChildSelectable(int groupPosition,
                    int childPosition) {
                // TODO Auto-generated method stub
                return true;
            }
        };
        
        ExpandableListView expandableListView = (ExpandableListView) findViewById(R.id.mainlist); 
        expandableListView.setAdapter(adapter);
        //expandableListView.expandGroup(0);
        
        expandableListView.setOnChildClickListener(new OnChildClickListener() {
        	      	
            @Override
            public boolean onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id)
            {	
                if(groupPosition == 0)
                {
	                HDJContract contract = new HDJContract();
	                contract.Id = childs.get(groupPosition).get(childPosition).get("Num").toString();
	                
	                //app.setHDJContract(contract);
					app.setContractId(contract.Id);
					
	                Intent intent = new Intent();  
					//  ����Intent��class����Test��ת��SecondActivity  
					intent.setClass(MainActivity.this, DetailActivity.class);  
	
					//  Ϊintent��Ӷ������Ϣ  
					//  ����Activity  
					startActivityForResult(intent,0);  
                }
                
                else
                {
                	 HDJContract contract = new HDJContract();
                	 contract.Id = childs.get(groupPosition).get(childPosition).get("Num").toString();
 	                
 	                //app.setHDJContract(contract);
 					app.setContractId(contract.Id);
 					
 	                Intent intent = new Intent();  
 					//  ����Intent��class����Test��ת��SecondActivity  
 					intent.setClass(MainActivity.this, EnDetailActivity.class);  
 					
 					//  Ϊintent��Ӷ������Ϣ  
 					//  ����Activity  
 					startActivity(intent);
                }
                return false;
            }
        });  
        handler.postDelayed(runnable, 1000*60);
	}
	
	@Override
    protected void onDestroy() {
        handler.removeCallbacks(runnable); //ֹͣˢ��
        super.onDestroy();
    }
	
	@Override
	public void onClick(View v)
	{
		switch(v.getId())
		{
		case R.id.exit:
			exit();
			break;
			
			default:
				break;
		}
	}
	
	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data)
	{
		if(requestCode == 0 && resultCode == Activity.RESULT_OK)
		{
			boolean r = data.getExtras().getBoolean("Flush");
			if(r)
			{
			    app=(AppContext)getApplication();
                final Employee emp = app.getEmployee();
    		    unsignedList = SocketClient.instance().QueryUnsignedHDJContract(emp.Id);
            	List<Map<String,String>> currentunsignList=new ArrayList<Map<String,String>>();
                for(int i=0;i<unsignedList.size();i++)
                {
                	Map<String,String> map = new HashMap<String,String>();
                	map.put("Name",unsignedList.get(i).ProjectName );
                	map.put("Num", unsignedList.get(i).Id);
                	map.put("Submit", unsignedList.get(i).SubmitEmployeeName);
                	currentunsignList.add(map);
                }
                
                childs.clear();
                childs.add(currentunsignList);
                childs.add(child2);
                adapter.notifyDataSetChanged();
			}
		}
	}
	
	@Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(keyCode == KeyEvent.KEYCODE_BACK) {
        	moveTaskToBack(false);
        	return true;
         }
        return super.onKeyDown(keyCode, event);
	   }

	private void exit() {
		// TODO �Զ����ɵķ������
		if(!isExit)
		{
			isExit = true;
			Toast.makeText(this, "�ٰ�һ���˳�����", Toast.LENGTH_SHORT).show();
			             new Timer().schedule(new TimerTask() {
			                 
			                 @Override
		                 public void run() {
			                      isExit = false;
			                 }
			              }, 2000);
			          } else {
			              MainActivity.this.finish();
			              
			    }
		}
}

