﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressClient.Model
{
    /// <summary>
    ///   当前任务的工作量信息
    /// </summary>
    public class ContractWorkload
    {
        //modify by gatieme at 2015-11-18 15:09
        private String m_id;
        public String Id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        /// <summary>
        /// 工作量所属的会签单编号
        /// </summary>
        private string m_contractId;
        public string ContractId
        {
            get { return this.m_contractId; }
            set { this.m_contractId = value; }
        }

        /// <summary>
        /// 工作量所属的工作量项目类别
        /// 要么存储工作量的真个信息Item,要么存储其ID
        /// </summary>
        //private int m_itemId;
        //public int ItemId
        //{
        //    get { return this.m_itemId; }
        //    set { this.m_itemId = value; }
        //}
        private ContractItem m_item;
        public ContractItem Item
        {
            get { return this.m_item; }
            set { this.m_item = value; }
        }

        /// <summary>
        ///  任务工作量的大小
        /// </summary>
        private double m_work;
        public double Work
        {
            get { return this.m_work; }
            set { this.m_work = value; }
        }

        /// <summary>
        /// 任务的价格
        /// </summary>
        private double m_expense;
        public double Expense
        {
            get { return this.m_expense; }
            set { this.m_expense = value; }
        }
    }
}
