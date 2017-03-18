package com.example.signpress;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
 






import signdata.Employee;
import signdata.HDJContract;
import signdata.SignatureDetail;
import signdata.SignatureTemplate;
import signsocket.SocketClient;
import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.view.View;
import android.view.Window;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import com.hwj.signpress.R;

public class DetailActivity extends Activity implements OnClickListener {
	
	private Button btnAgree;
	private Button btnRefuse;
    //��Ŀ6��ǩ��������	
	private String NHColumn6SignName="";
	private String JHLXColumn6SignName="";
	private String CurrentEmployeeName="";
	//2016-5-15  ����������	
	private EditText Advice;
	private EditText Remarks;
	
	private AppContext app;
	private String contractId;
	
	private  String Title[] = new String[]{};// ��������
    private  String contents[] = new String[]{};//��������
    private ListView listView = null;
    ArrayList<Map<String,Object>> list = null;
	
    private List<String> titleList;
    private List<String> contentList;
    
    private Button btnBack;
    private boolean canview = false; 
    
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_CUSTOM_TITLE);
		//��ȡ����ʾactivity_detail.xml�е����ϵ�������
        setContentView(R.layout.activity_detail);
        getWindow().setFeatureInt(Window.FEATURE_CUSTOM_TITLE, R.layout.backtitlebar);
		
        btnBack=(Button)findViewById(R.id.back);
        btnBack.setOnClickListener(this);
        
        TextView tvAdvice=(TextView)findViewById(R.id.Advice);
		tvAdvice.setMovementMethod(ScrollingMovementMethod.getInstance());
		
		TextView tvAndroid=(TextView)findViewById(R.id.tvCWJ);
		tvAndroid.setMovementMethod(ScrollingMovementMethod.getInstance());
		
		
		
		this.btnAgree = (Button)findViewById(R.id.btnAgree);
		this.btnRefuse=(Button)findViewById(R.id.btnRefuse);
		this.Remarks=(EditText)findViewById(R.id.tvCWJ);
		this.Advice=(EditText)findViewById(R.id.Advice);
	
		//this.Advice.setVisibility(View.GONE);
		
		titleList=new ArrayList<String>();
		contentList=new ArrayList<String>();
		titleList.add("��ǩ�����ƣ�");
		titleList.add("��ţ�");
		
		app = (AppContext)getApplication();
		contractId = app.getContractId();
		//��¼�û���
		String employeename=app.getEmployee().Name;
		
		CurrentEmployeeName=employeename;
		//Toast.makeText(DetailActivity.this, CurrentEmployeeName, Toast.LENGTH_SHORT).show();
		HDJContract contract = new HDJContract();
		//��ȡѡ��Ļ�ǩ����Ϣ
		contract=SocketClient.instance().GetHDJContract(contractId);
		//��ȡ��Ŀ��Ϣ
		for(String s : contract.ConTemp.ColumnNames)
		{
			titleList.add(s+"��");
		}
		//��ȡǩ����Ϣ
		for(SignatureTemplate s : contract.ConTemp.SignDatas)
		{
			titleList.add(s.SignInfo+"��");
		}
		contentList.add(contract.Name);
		contentList.add(contractId);
		
		for(String s:contract.ColumnDatas)
		{
			contentList.add(s);
		}
		
		//�жϸ��û��Ƿ���view����
		for(int i=0;i<contract.ConTemp.SignDatas.size();i++)
		{
			String n=contract.ConTemp.SignDatas.get(i).SignEmployee.Name;
			if(n.equals(employeename))
			{
				if(contract.ConTemp.SignDatas.get(i).CanView == 1)
				{
					canview=true;
				}
			}
		}
		
		if(canview)
		{
			for(int i=0;i<contract.ConTemp.SignDatas.size();i++)
			{
				String name=contract.ConTemp.SignDatas.get(i).SignEmployee.Name;
				String result=contract.SignResults.get(i)==1?"ͬ��":(contract.SignResults.get(i)==0?"δ����":"�ܾ�");
				if(contractId.contains("����")&&i==3)
				{
					contentList.add("����ǩ��");
			    }
				else
				{
					contentList.add(name+"("+result+")");
				}
			}
		}
		else
		{
			for(int i=0;i<contract.ConTemp.SignDatas.size();i++)
			{
				String name=contract.ConTemp.SignDatas.get(i).SignEmployee.Name;
				contentList.add(name);
			}
		}
	
		
		//������ں�  �������о���Ҫ��д������
		if(contractId.contains("��"))
		{
			NHColumn6SignName=contract.ConTemp.SignDatas.get(4).SignEmployee.Name;
			if(NHColumn6SignName.equals(employeename))
			{
				this.Advice.setVisibility(View.VISIBLE);
				Toast.makeText(DetailActivity.this, "������д����������Ա����д��������", Toast.LENGTH_SHORT).show();
			}
			//this.Advice.setVisibility(View.GONE);
		}
		
		if(contractId.contains("����"))
		{
			//������оͻ�ȡ��3��ǩ���˵�����
			JHLXColumn6SignName=contract.ConTemp.SignDatas.get(2).SignEmployee.Name;
			if(JHLXColumn6SignName.equals(employeename))
			{
				this.Advice.setVisibility(View.VISIBLE);
				Toast.makeText(DetailActivity.this, "������д����������Ա����д��������", Toast.LENGTH_SHORT).show();
			}
		}
		
		Title=new String[titleList.size()];
		for(int i=0;i<titleList.size();i++)
        {
			Title[i]=titleList.get(i);
        }
		
		contents=new String[contentList.size()];
		for(int i=0;i<contentList.size();i++)
		{
			contents[i]=contentList.get(i);
		}
		
		listView = (ListView) this.findViewById(R.id.ContractDetails);
        list = new ArrayList<Map<String, Object>>();// ʵ����list
        for (int i = 0; i < Title.length; i++) 
        {// forѭ����list����������
            Map<String,Object> map = new HashMap<String,Object>();// ����map����
            map.put("title", Title[i]);
            map.put("content", contents[i]);
            list.add(map);// ��map�������ӵ�list��ȥ
        }
        listView.setAdapter(new SimpleAdapter(DetailActivity.this, // �����Ķ���
                list,// List����
                R.layout.list_item_style,// ListView�����ݵ���ʾ��ʽ
                new String[] { "title", "content" },// �˴���String���ݱ�����List���е�keyֵ��Ӧ
                new int[] { android.R.id.text1, android.R.id.text2 }));// android.R.layout.simple_list_item_2���ṩ���ı��ؼ�
        // android. R.id.text1,android. R.id.text2 ��������������
        // android.R.layout.simple_list_item_2���ϵͳ�����ṩ�ģ���ҿ��Գ������������ؼ���ʹ��
//      ΪlistView�е����������õ����¼�
        listView.setOnItemClickListener(new OnItemClickListener()
        {
            @Override
            public void onItemClick(AdapterView<?> parent, View view,
                    int psition, long id) {
                // TODO Auto-generated method stub
                Toast.makeText(DetailActivity.this, Title[psition]+ contents[psition],
                        Toast.LENGTH_SHORT).show();//��˾��ʾ
            }
        });
        
        //���ͬ�ⰴť�¼�
        this.btnAgree.setOnClickListener(new OnClickListener()
		{
			@Override
			public void onClick(View v)
			{
				AgreeAsyncTask();
			}
			
			private void AgreeAsyncTask()
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
		        	//  ��ȡ��ע�е���Ϣ
					String remarks=Remarks.getText().toString();
					String advice="0#";
					//Toast.makeText(DetailActivity.this, "�һ�û����", Toast.LENGTH_SHORT).show();
					//Toast.makeText(DetailActivity.this, "��Ŀ6������:"+NHColumn6SignName+"."+JHLXColumn6SignName+CurrentEmployeeName, Toast.LENGTH_SHORT).show();
					if(contractId.contains("��")||contractId.contains("����"))
					{
						if(CurrentEmployeeName.equals(NHColumn6SignName)||CurrentEmployeeName.equals(JHLXColumn6SignName))
						{	
							//Toast.makeText(DetailActivity.this, "�ҽ�����", Toast.LENGTH_SHORT).show();
							//������
							   if(Advice.getText().toString().trim()=="")
							   {
								Toast.makeText(DetailActivity.this, "����д��������", Toast.LENGTH_SHORT).show();
								return;
							   }
						    //�����������Ҫ��д���������˻���Ҫ��ȡ�������������
						     advice="1#"+Advice.getText().toString();
						}
					}
					if(remarks.equals(""))
					{
						remarks="δ��";
					}
					
					
	
					// �ֻ�����������socket����
					//if (NetManager.instance().isNetworkConnected())
					{
						SignatureDetail sd = new SignatureDetail();
						sd.Remark=remarks;
						sd.Result=1;
						sd.Advice=advice;
						sd.ConId=contractId;
						//Toast.makeText(DetailActivity.this, "��Ŀ6������:"+NHColumn6SignName+"."+JHLXColumn6SignName, Toast.LENGTH_SHORT).show();
						//Toast.makeText(DetailActivity.this, "������:"+sd.Advice+"����:"+CurrentEmployeeName, Toast.LENGTH_SHORT).show();
						app=(AppContext)getApplication();
				        final Employee emp = app.getEmployee();
						
				        sd.EmpId=emp.Id;
						boolean re=SocketClient.instance().InsertSignatureDetail(sd);
						if(re)
						{
							Toast.makeText(DetailActivity.this, "ǩ�ֳɹ�", Toast.LENGTH_SHORT).show();
							Intent myIntent = new Intent();
							myIntent.putExtra("Flush", true);
							//myIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
							setResult(Activity.RESULT_OK,myIntent);
							DetailActivity.this.finish();
						}
						else
						{
							// ʹ�õ����������û�û��ǩ��Ȩ���޷���¼
							Toast.makeText(DetailActivity.this, "ǩ��ʧ��", Toast.LENGTH_SHORT).show();
						}
					
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

        //����ܾ���ť�¼�
        this.btnRefuse.setOnClickListener(new OnClickListener()
		{
			@Override
			public void onClick(View v)
			{
				RefuseAsyncTask();
			}
			
			private void RefuseAsyncTask()
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
		        	//  ��ȡ��ע�е���Ϣ
					String remarks=Remarks.getText().toString();

					if(remarks.equals(""))
					{
						Toast.makeText(DetailActivity.this, "����д�ܾ�����", Toast.LENGTH_SHORT).show();
					}
					
	
					// �ֻ�����������socket����
					//if (NetManager.instance().isNetworkConnected())
					else
					{
						SignatureDetail sd = new SignatureDetail();
						sd.Remark=remarks;
						sd.Result=-1;
						sd.ConId=contractId;
						
						app=(AppContext)getApplication();
				        final Employee emp = app.getEmployee();
						
				        sd.EmpId=emp.Id;
						boolean re=SocketClient.instance().InsertSignatureDetail(sd);
						if(re)
						{
							Toast.makeText(DetailActivity.this, "ǩ�ֳɹ�", Toast.LENGTH_SHORT).show();
							Intent myIntent = new Intent();
							//myIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
							myIntent.putExtra("Flush", true);
							setResult(Activity.RESULT_OK,myIntent);
							DetailActivity.this.finish();
						}
						else
						{
							Toast.makeText(DetailActivity.this, "ǩ��ʧ��", Toast.LENGTH_SHORT).show();
						}
					
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
	}
	
	@Override
	public void onClick(View v)
	{
		switch(v.getId())
		{
		case R.id.back:
			DetailActivity.this.finish();
			break;
			
			default:
				break;
		}
	}
}
