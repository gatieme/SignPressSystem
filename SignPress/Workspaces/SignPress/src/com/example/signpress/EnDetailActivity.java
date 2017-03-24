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
        setContentView(R.layout.activity_en_detail);
        getWindow().setFeatureInt(Window.FEATURE_CUSTOM_TITLE, R.layout.backtitlebar);
		
        btnBack=(Button)findViewById(R.id.back);
        btnBack.setOnClickListener(this);
        
		titleList=new ArrayList<String>();
		contentList=new ArrayList<String>();
		titleList.add("��ǩ�����ƣ�");
		titleList.add("��ţ�");
		
		app = (AppContext)getApplication();
		contractId = app.getContractId();
		String employeename=app.getEmployee().Name;
		
		HDJContract contract = new HDJContract();
		contract=SocketClient.instance().GetHDJContract(contractId);
		for(String s : contract.ConTemp.ColumnNames)
		{
			titleList.add(s+"��");
		}
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
				String result=contract.SignResults.get(i)==1?"ͬ��":(contract.SignResults.get(i)==0?"δ����":"�ܾ�");
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
        list = new ArrayList<Map<String, Object>>();// ʵ����list
        for (int i = 0; i < Title.length; i++) 
        {// forѭ����list����������
            Map<String,Object> map = new HashMap<String,Object>();// ����map����
            map.put("title", Title[i]);
            map.put("content", contents[i]);
            list.add(map);// ��map�������ӵ�list��ȥ
        }
        listView.setAdapter(new SimpleAdapter(EnDetailActivity.this, // �����Ķ���
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
                Toast.makeText(EnDetailActivity.this, "��ѡ����" +Title[psition]+ contents[psition],
                        Toast.LENGTH_SHORT).show();//��˾��ʾ
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
