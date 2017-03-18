using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressServer.SignData
{

    /*  
     * 
     *  会签单模版类ContractTemplate
     *  对用数据库中的ConTemp
     */
    public class ContractTemplate
    {
        private String m_createDate;        // 会签单模版的创建时间
        public String CreateDate
        {
            get { return this.m_createDate; }
            set { this.m_createDate = value;  }
        }

        protected int m_tempId;   //  会签单模版的编号
        public  int TempId
        {
            get{ return this.m_tempId;  }
            set{ this.m_tempId = value; }
        }

        //2016-4-23
        /*
         *10->内例
         *11->内专
         *20->界例
         *22->界专
         */
        protected int m_type;   //  会签单模版类型
        public int Type
        {
            get { return this.m_type; }
            set { this.m_type = value; }
        }


        protected String m_name;     //  会签单名称
        public String Name
        {
            get{ return this.m_name;}
            set{ this.m_name = value;}
        }

        #region   column栏目信息[6个栏目]
        
        protected int m_columnCount = 6;      // 目前此接口无用，因为数据栏目定死是6个
        public int ColumnCount
        {
            get{ return this.m_columnCount; }
            set{ this.m_columnCount = value; }
        }

        protected List<String> m_columnNames;     //  存储6个栏目项的信息
        public List<String> ColumnNames
        {
            get { return this.m_columnNames; }
            set { this.m_columnNames = value; }
        }
        
        #endregion


        #region  signinfo签字人信息[12个签字人]
        
        
        protected int m_signCount = 12;            //  签字人人数  此接口暂时无用，供扩展哟个，因为暂时签字人就是6+2=8个
        public int SignCount
        {
            get { return this.m_signCount; }
            set { this.m_signCount = value; }
        }


        protected List<SignatureTemplate> m_signDatas;     //  签字人信息
        public List<SignatureTemplate> SignDatas
        {
            get { return this.m_signDatas; }
            set { this.m_signDatas = value; }
        }
        
        
        #endregion
    }
}
