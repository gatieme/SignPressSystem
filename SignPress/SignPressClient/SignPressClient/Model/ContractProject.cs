using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressClient.Model
{
    public class ContractProject
    {
        private int m_id;
        public int Id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        //  要不存储ContractCategory信息，要不存储ContractCategory的id=ContractCategory
        //public ContractCategory Category;
        //private ContractCategory m_category
        //{
        //    get { return this.Category; }
        //    set { this.Category = value; }
        //}
        private int m_categoryId;
        public int CategoryId
        {
            get { return this.m_categoryId; }
            set { this.m_categoryId = value; }
        }



        private String m_project;
        public String Project
        {
            get { return this.m_project; }
            set { this.m_project = value; }
        }
    }
}
