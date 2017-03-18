package signdata;


import java.util.List;
public class ContractTemplate
{
    public String CreateDate;
    
    public  int TempId;
    
    public String Name;
    

    public int ColumnCount = 6;

    public List<String> ColumnNames;     //  存储5个栏目项的信息
        
    public int SignCount = 12;

    public List<SignatureTemplate> SignDatas;

}
