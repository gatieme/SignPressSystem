using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    用于存储不同类型模版的Id和name好让不同类型的模版一起显示
 */
namespace SignPressServer.SignData
{
    public class ContractTemplateAll
    {
        private String m_createDate;        // 会签单模版的创建时间
        public String CreateDate
        {
            get { return this.m_createDate; }
            set { this.m_createDate = value; }
        }

        protected int m_tempId;   //  会签单模版的编号
        public int TempId
        {
            get { return this.m_tempId; }
            set { this.m_tempId = value; }
        }

        protected String m_name;     //  会签单名称
        public String Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }
    }
}
