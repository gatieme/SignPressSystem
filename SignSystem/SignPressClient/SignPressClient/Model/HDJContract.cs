using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressClient.Model
{

    public class HDJContract
    {
        public HDJContract( )
        { 
        }
        
        public HDJContract(HDJContract contract)
        {
            this.m_name = contract.m_name;
            this.m_submitDate = contract.m_submitDate;
            this.m_id = contract.m_id;
            this.m_columnDatas = contract.m_columnDatas;
            this.m_submitEmployee = contract.m_submitEmployee;
            this.m_conTemp = contract.m_conTemp;
//            this.m_currLevel = contract.m_currLevel;
//            this.m_maxLevel = contract.m_maxLevel;
            this.m_signResults = contract.m_signResults;
            this.m_signRemarks = contract.m_signRemarks;
        }

        private String m_name;
        public String Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        private String m_submitDate;
        public String SubmitDate
        {
            get { return this.m_submitDate; }
            set { this.m_submitDate = value; }
        }

        private Templete m_conTemp;     //  所对应的会签单模版的信息
        public Templete ConTemp
        {
            get { return this.m_conTemp; }
            set { this.m_conTemp = value; }
        }

        private String m_id;                    //  审批会签单编号
        public String Id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        private List<String> m_columnDatas;     //  存储会签单
        public List<String> ColumnDatas
        {
            get { return this.m_columnDatas; }
            set { this.m_columnDatas = value; }
        }

        private List<String> m_signRemarks;      //  每一个人的评论信息
        public List<String> SignRemarks
        {
            get { return this.m_signRemarks; }
            set { this.m_signRemarks = value; }
        }

        private Employee m_submitEmployee;
        public Employee SubmitEmployee
        {
            get { return this.m_submitEmployee; }
            set { this.m_submitEmployee = value; }
        }

        private List<int> m_signResults;            // 每个人的信息
        public List<int> SignResults
        {
            get { return this.m_signResults; }
            set { this.m_signResults = value; }
        }


        #region   通过编号获取出会签单的一些信息
        // 佳内2015001
        //private static int DepartmentIndexFromConId = 1;           //  编号的第1位标识了单子所属的部门
        //private static int CatgoryIndexFromConId = 2;              //  编号的第2位标识了单子的类别
        //private static int IsOnlineIndexFromConId = 3;             //  编号的第3~6位标识了单子是否是被在线签署的
        //private static int NumIndexFromConId = 4;                  //  编号的第7~9位
        public static char GetDepartmentShortCallFromContractId(string contractId)
        {
            return contractId[0];
        }
        public static string GetCatgoryShortCallFromContractId(string contractId)
        {
            return contractId[1].ToString()+contractId[2].ToString();
        }
        public static int GetYearFromContractId(string contractId)
        {
            return int.Parse(contractId.Substring(3, 6));
        }
        public static bool GetIsOnlineFromContractId(string contractId)
        {
            return (contractId[7] == '0');
        }
        public static int GetNumFromContractId(string contractId)
        {
            return int.Parse(contractId.Substring(8, 2));
        }



        #endregion





    }

    public class HDJContractWithWorkload : HDJContract
    {
        public HDJContractWithWorkload()
            : base()
        {

        }

        public HDJContractWithWorkload(HDJContract contract)
        : base(contract)
        {
            this.WorkLoads = new List<ContractWorkload>();
        }
        private List<ContractWorkload> m_workloads;
        public List<ContractWorkload> WorkLoads
        {
            get { return this.m_workloads; }
            set { this.m_workloads = value; }
        }
    }
}
