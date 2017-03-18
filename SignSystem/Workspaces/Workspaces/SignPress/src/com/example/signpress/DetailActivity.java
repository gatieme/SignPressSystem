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
    //栏目6的签字人名字	
	private String NHColumn6SignName="";
	private String JHLXColumn6SignName="";
	private String CurrentEmployeeName="";
	//2016-5-15  加入审核意见	
	private EditText Advice;
	private EditText Remarks;
	
	private AppContext app;
	private String contractId;
	
	private  String Title[] = new String[]{};// 标题数据
    private  String contents[] = new String[]{};//内容数据
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
		//读取并显示activity_detail.xml中的资料到画面上
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
		titleList.add("会签单名称：");
		titleList.add("编号：");
		
		app = (AppContext)getApplication();
		contractId = app.getContractId();
		//登录用户名
		String employeename=app.getEmployee().Name;
		
		CurrentEmployeeName=employeename;
		//Toast.makeText(DetailActivity.this, CurrentEmployeeName, Toast.LENGTH_SHORT).show();
		HDJContract contract = new HDJContract();
		//获取选择的会签单信息
		contract=SocketClient.instance().GetHDJContract(contractId);
		//获取栏目信息
		for(String s : contract.ConTemp.ColumnNames)
		{
			titleList.add(s+"：");
		}
		//获取签字信息
		for(SignatureTemplate s : contract.ConTemp.SignDatas)
		{
			titleList.add(s.SignInfo+"：");
		}
		contentList.add(contract.Name);
		contentList.add(contractId);
		
		for(String s:contract.ColumnDatas)
		{
			contentList.add(s);
		}
		
		//判断该用户是否有view功能
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
				String result=contract.SignResults.get(i)==1?"同意":(contract.SignResults.get(i)==0?"未处理":"拒绝");
				if(contractId.contains("内例")&&i==3)
				{
					contentList.add("无需签字");
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
	
		
		//如果是内河  或界河例行就需要填写设和意见
		if(contractId.contains("内"))
		{
			NHColumn6SignName=contract.ConTemp.SignDatas.get(4).SignEmployee.Name;
			if(NHColumn6SignName.equals(employeename))
			{
				this.Advice.setVisibility(View.VISIBLE);
				Toast.makeText(DetailActivity.this, "你是填写审核意见的人员需填写审核意见！", Toast.LENGTH_SHORT).show();
			}
			//this.Advice.setVisibility(View.GONE);
		}
		
		if(contractId.contains("界例"))
		{
			//界河例行就获取第3个签字人的姓名
			JHLXColumn6SignName=contract.ConTemp.SignDatas.get(2).SignEmployee.Name;
			if(JHLXColumn6SignName.equals(employeename))
			{
				this.Advice.setVisibility(View.VISIBLE);
				Toast.makeText(DetailActivity.this, "你是填写审核意见的人员需填写审核意见！", Toast.LENGTH_SHORT).show();
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
        list = new ArrayList<Map<String, Object>>();// 实例化list
        for (int i = 0; i < Title.length; i++) 
        {// for循环向list中增加数据
            Map<String,Object> map = new HashMap<String,Object>();// 创建map对象
            map.put("title", Title[i]);
            map.put("content", contents[i]);
            list.add(map);// 将map数据增加到list中去
        }
        listView.setAdapter(new SimpleAdapter(DetailActivity.this, // 上下文对象
                list,// List数据
                R.layout.list_item_style,// ListView中数据的显示方式
                new String[] { "title", "content" },// 此处的String数据必须与List当中的key值对应
                new int[] { android.R.id.text1, android.R.id.text2 }));// android.R.layout.simple_list_item_2中提供的文本控件
        // android. R.id.text1,android. R.id.text2 这两个属性是由
        // android.R.layout.simple_list_item_2这个系统布局提供的，大家可以尝试里面其他控件的使用
//      为listView中的数据项设置单击事件
        listView.setOnItemClickListener(new OnItemClickListener()
        {
            @Override
            public void onItemClick(AdapterView<?> parent, View view,
                    int psition, long id) {
                // TODO Auto-generated method stub
                Toast.makeText(DetailActivity.this, Title[psition]+ contents[psition],
                        Toast.LENGTH_SHORT).show();//土司提示
            }
        });
        
        //点击同意按钮事件
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
		          
		          //在doInBackground 执行完成后，onPostExecute 方法将被UI 线程调用，
		          // 后台的计算结果将通过该方法传递到UI 线程，并且在界面上展示给用户.
		          @Override
				protected void onPostExecute(Object result)
		          {
		            super.onPostExecute(result);
		            //activity_main_btn1.setText("请求结果为："+result);//可以更新UI
		        	//  获取备注中的信息
					String remarks=Remarks.getText().toString();
					String advice="0#";
					//Toast.makeText(DetailActivity.this, "我还没进入", Toast.LENGTH_SHORT).show();
					//Toast.makeText(DetailActivity.this, "栏目6的姓名:"+NHColumn6SignName+"."+JHLXColumn6SignName+CurrentEmployeeName, Toast.LENGTH_SHORT).show();
					if(contractId.contains("内")||contractId.contains("界例"))
					{
						if(CurrentEmployeeName.equals(NHColumn6SignName)||CurrentEmployeeName.equals(JHLXColumn6SignName))
						{	
							//Toast.makeText(DetailActivity.this, "我进入了", Toast.LENGTH_SHORT).show();
							//审核意见
							   if(Advice.getText().toString().trim()=="")
							   {
								Toast.makeText(DetailActivity.this, "请填写审核意见！", Toast.LENGTH_SHORT).show();
								return;
							   }
						    //如果该人是需要填写审核意见的人还需要获取他所填的审核意见
						     advice="1#"+Advice.getText().toString();
						}
					}
					if(remarks.equals(""))
					{
						remarks="未填";
					}
					
					
	
					// 手机能联网，读socket数据
					//if (NetManager.instance().isNetworkConnected())
					{
						SignatureDetail sd = new SignatureDetail();
						sd.Remark=remarks;
						sd.Result=1;
						sd.Advice=advice;
						sd.ConId=contractId;
						//Toast.makeText(DetailActivity.this, "栏目6的姓名:"+NHColumn6SignName+"."+JHLXColumn6SignName, Toast.LENGTH_SHORT).show();
						//Toast.makeText(DetailActivity.this, "审核意见:"+sd.Advice+"名字:"+CurrentEmployeeName, Toast.LENGTH_SHORT).show();
						app=(AppContext)getApplication();
				        final Employee emp = app.getEmployee();
						
				        sd.EmpId=emp.Id;
						boolean re=SocketClient.instance().InsertSignatureDetail(sd);
						if(re)
						{
							Toast.makeText(DetailActivity.this, "签字成功", Toast.LENGTH_SHORT).show();
							Intent myIntent = new Intent();
							myIntent.putExtra("Flush", true);
							//myIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
							setResult(Activity.RESULT_OK,myIntent);
							DetailActivity.this.finish();
						}
						else
						{
							// 使用弹窗，告诉用户没有签字权限无法登录
							Toast.makeText(DetailActivity.this, "签字失败", Toast.LENGTH_SHORT).show();
						}
					
		              }
		          }

		          //该方法运行在后台线程中，因此不能在该线程中更新UI，UI线程为主线程
		          @Override
				protected Object doInBackground(String... params)
		          {
						 return true;
		          }

		        }.execute();
		        
		      }
		});

        //点击拒绝按钮事件
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
		          
		          //在doInBackground 执行完成后，onPostExecute 方法将被UI 线程调用，
		          // 后台的计算结果将通过该方法传递到UI 线程，并且在界面上展示给用户.
		          @Override
				protected void onPostExecute(Object result)
		          {
		            super.onPostExecute(result);
		            //activity_main_btn1.setText("请求结果为："+result);//可以更新UI
		        	//  获取备注中的信息
					String remarks=Remarks.getText().toString();

					if(remarks.equals(""))
					{
						Toast.makeText(DetailActivity.this, "需填写拒绝理由", Toast.LENGTH_SHORT).show();
					}
					
	
					// 手机能联网，读socket数据
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
							Toast.makeText(DetailActivity.this, "签字成功", Toast.LENGTH_SHORT).show();
							Intent myIntent = new Intent();
							//myIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
							myIntent.putExtra("Flush", true);
							setResult(Activity.RESULT_OK,myIntent);
							DetailActivity.this.finish();
						}
						else
						{
							Toast.makeText(DetailActivity.this, "签字失败", Toast.LENGTH_SHORT).show();
						}
					
		              }
		          }

		          //该方法运行在后台线程中，因此不能在该线程中更新UI，UI线程为主线程
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
