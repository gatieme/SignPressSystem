package com.example.signpress;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
 







import signdata.HDJContract;
import signdata.SignatureTemplate;
import signsocket.SocketClient;
import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;
import com.hljhw.signature.R;

public class EnDetailActivity extends Activity implements OnClickListener {
	
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
        setContentView(R.layout.activity_en_detail);
        getWindow().setFeatureInt(Window.FEATURE_CUSTOM_TITLE, R.layout.backtitlebar);
		
        btnBack=(Button)findViewById(R.id.back);
        btnBack.setOnClickListener(this);
        
		titleList=new ArrayList<String>();
		contentList=new ArrayList<String>();
		titleList.add("会签单名称：");
		titleList.add("编号：");
		
		app = (AppContext)getApplication();
		contractId = app.getContractId();
		String employeename=app.getEmployee().Name;
		
		HDJContract contract = new HDJContract();
		contract=SocketClient.instance().GetHDJContract(contractId);
		for(String s : contract.ConTemp.ColumnNames)
		{
			titleList.add(s+"：");
		}
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
		
		for(int i=0;i<contract.ConTemp.SignDatas.size();i++)
		{
			if(contract.ConTemp.SignDatas.get(i).SignEmployee.Name.equals(employeename))
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
					contentList.add(name+"("+result+")");
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
        listView.setAdapter(new SimpleAdapter(EnDetailActivity.this, // 上下文对象
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
                Toast.makeText(EnDetailActivity.this, "您选择了" +Title[psition]+ contents[psition],
                        Toast.LENGTH_SHORT).show();//土司提示
            }
        });
	}
	
	@Override
	public void onClick(View v)
	{
		switch(v.getId())
		{
		case R.id.back:
			EnDetailActivity.this.finish();
			break;
			
			default:
				break;
		}
	}

}
