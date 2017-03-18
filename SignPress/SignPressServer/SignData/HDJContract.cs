﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignPressServer.SignData
{
    ///
    ///  会签单的简单信息
    ///  用于在列表中显示
    ///
    public class SHDJContract
    {
        private String m_id;                    //  审批会签单编号
        public String Id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        private String m_name;              // 方案名称
        public String Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        private String m_projectName;       // 工程名称
        public String ProjectName
        {
            get { return this.m_projectName; }
            set { this.m_projectName = value; }
        }

        private String m_submitDate;        // 提交日期
        public String SubmitDate
        {
            get { return this.m_submitDate; }
            set { this.m_submitDate = value; }
        }

        private String m_submitEmployeeName;      //  提交人的信息
        public String SubmitEmployeeName
        {
            get { return this.m_submitEmployeeName; }
            set { this.m_submitEmployeeName = value; }
        }

        private int m_currLevel;                //  当前进度节点
        public int CurrLevel
        {
            get { return this.m_currLevel; }
            set { this.m_currLevel = value; }
        }

        private int m_maxLevel;                 //  最大节点信息
        public int MaxLevel
        {
            get { return this.m_maxLevel; }
            set { this.m_maxLevel = value; }
        }

        private String m_signDate;          //  签字日期
        public String SignDate
        {
            get { return this.m_signDate; }
            set { this.m_signDate = value; }
        }

        private String m_signRemark;        // 本人签字备注
        public String SignRemark
        {
            get { return this.m_signRemark; }
            set { this.m_signRemark = value; }
        }

        private String m_signResult;        //  本人签字结果
        public String SignResult
        {
            get { return this.m_signResult; }
            set { this.m_signResult = value; }
        }
    }

    /*
     * 合同会签单的信息
     */
    public class HDJContract
    {
        public HDJContract()
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
            this.m_currLevel = contract.m_currLevel;
            this.m_maxLevel = contract.m_maxLevel; 
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

        //
        ///  编号共5位。
        ///  第一位代表是单位名称简称；
        ///  第二位代表类别简称；类别最多有四类，即界、内、应和例。具体的含义是为界河（简称为界）航道养护工程、内河（简称为内）航道养护工程、应急抢通（简称为应）工程、例会项目（简称为例）工程。
        ///  第三位如果为0，表示该会签审批单是在线（通过我们的系统）审批完成的；否则该位为1，表示离线审批（通过传统方式，没有通过系统，而是领导手工签字完成）；
        ///  第四位、第五位表示本年度的分类编号，要求年度内实现自加功能，年初重新初始化为0（可人工、或系统自动完成）。
        ///  该系统的使用对象有黑河航道局、佳木斯航道局、哈总段、一中心、二中心、三中心、测绘中心、省航道局八个科室，具体明细如下：
        private String m_id;                    //  审批会签单编号
        public String Id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }



        private List<String> m_columnDatas;     //  存储会签单的前5项栏目信息
        public List<String> ColumnDatas
        {
            get { return this.m_columnDatas; }
            set { this.m_columnDatas = value; }
        }

        private Employee m_submitEmployee;      //  提交人的信息
        public Employee SubmitEmployee
        {
            get { return this.m_submitEmployee; }
            set { this.m_submitEmployee = value; }
        }

        private ContractTemplate m_conTemp;     //  所对应的会签单模版的信息
        public ContractTemplate ConTemp
        {
            get { return this.m_conTemp; }
            set { this.m_conTemp = value; }
        }

        //private List<String> m_signRemark;      //  每一个人的批注信息
        //public List<String> SignRemark
        //{
        //    get { return this.m_signRemark; }
        //    set { this.m_signRemark = value; }
        //}

        private int m_currLevel;                //  当前进度节点
        public int CurrLevel
        {
            get { return this.m_currLevel; }
            set { this.m_currLevel = value; }
        }

        private int m_maxLevel;                 //  最大节点信息
        public int MaxLevel
        {
            get { return this.m_maxLevel; }
            set { this.m_maxLevel = value; }
        }

        private List<int> m_signResults;            // 每个人的信息
        public List<int> SignResults
        { 
            get{ return this.m_signResults; }
            set{ this.m_signResults = value;}
        }


        private List<String> m_signRemarks;            // 每个人的信息备注信息
        public List<String> SignRemarks
        {
            get { return this.m_signRemarks; }
            set { this.m_signRemarks = value; }
        }

        
    }

    public class HDJContractWithWorkload : HDJContract
    {
        public HDJContractWithWorkload()
           :base()
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
