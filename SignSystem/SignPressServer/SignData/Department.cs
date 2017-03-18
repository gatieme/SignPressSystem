using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressServer.SignData
{
    /*
     * 
     *  部门类Department
     *  对应于数据库中的部门表department
     *  
     */
    public class Department
    {
        protected int m_id;           // 部门编号
        public int Id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        protected String m_name;      // 部门姓名
        public String Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        //  modify by gatieme at 2015-08-08 16:09
        //  为部门添加部门简称
        protected String m_shortCall;     //  部门简称
        public String ShortCall
        {
            get { return this.m_shortCall; }
            set { this.m_shortCall = value; }
        }


        public override string ToString()
        {
            return "Id = " + this.m_id.ToString() + ", Name = " + this.m_name.ToString() + ", ShortCall = " + this.m_shortCall.ToString(); 
        }
    }

    //  此部门信息包含了当前部门的申请权限
    //  申请选线标识是当前部门是否可以申请，界河项目，内河项目，应急项目和例会项目
    public class SDepartment :  Department
    {
        //private int m_id;           // 部门编号
        //public int Id
        //{
        //    get { return this.m_id; }
        //    set { this.m_id = value; }
        //}

        //private String m_name;      // 部门姓名
        //public String Name
        //{
        //    get { return this.m_name; }
        //    set { this.m_name = value; }
        //}

        ////  modify by gatieme at 2015-08-08 16:09
        ////  为部门添加部门简称
        //private String m_shortCall;     //  部门简称
        //public String ShortCall
        //{
        //    get { return this.m_shortCall; }
        //    set { this.m_shortCall = value; }
        //}


       
        private int m_canBoundary;   //  当前部门是否可以申请界河项目
        public int CanBoundary
        {
            get { return  this.m_canBoundary; }
            set { this.m_canBoundary = value; }
        }

        private int m_canInland;     //  当前部门是否可以申请内河项目1
        public int CanInland
        {
            get { return  this.m_canInland; }
            set { this.m_canInland = value; }
        }

            
        public override string ToString()
        {
            return "Id = " + this.m_id.ToString() + ", Name = " + this.m_name.ToString() + ", ShortCall = " + this.m_shortCall.ToString();
        }


    }

    public class HSDepartment : SDepartment
    {
        ///  modify by gatieme at 2015-12-17 22:12
        ///  因为新需求中省航道局是一个一级部门, 部分部门是其下属部门, 因此需要新的接口进行转换
        ///  由于此接口仅在服务器端有效, 因此为了简便我们的实现,  暂时不对客户端提供接口
        ///  
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///  BUG
        ///  
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///   
        ///  modify by gatieme at 2015-12-18 18 : 31
        ///  增加了新的需求, 新的部门中有几个是属于二级部门，统计时只需要统计其一级部门的信息，而不希望统计耳机部门的信息
        ///  因此在新的部门类型中，增加了一个新的字段HigherDepartment用于标识其上级部门的信息
        ///  如果HigherDepartment == 0 说明此部门自己是一个一级部门，下面没有部门
        ///  如果HigherDepartment == DepartmentId  说明此部门是一个有下属部门的一级部门
        ///  其他情况下，此部门是一个二级部门，其上级部门由HigherDepartment标识
        private int m_higherDepartmentId;
        public int HigherDepartmentId
        {
            get { return this.m_higherDepartmentId; }
            set { this.m_higherDepartmentId = value; }
        }
        
        /// <summary>
        /// 判断当前部门是不是higherDepartmentId的子部门
        /// </summary>
        /// <param name="higherDepartmentId"></param>
        public bool IsSubDepartment(int higherDepartmentId)
        {
            return (this.Id > 0 && this.Id != higherDepartmentId && this.m_higherDepartmentId == higherDepartmentId);
        }
        public bool IsSubDepartment( )
        {
            Console.WriteLine("id = {0}, higherdep = {1}", this.Id, this.HigherDepartmentId);
            return (this.Id > 0 && this.m_higherDepartmentId != 0 && this.m_higherDepartmentId != this.m_id);
        }

        /// <summary>
        /// 判断当前部门是不是一级部门
        /// </summary>
        /// <returns></returns>
        public bool IsHigherDepatment()
        {
            return this.Id > 0 &&  (this.IsHigherDepatmentHasSub() || this.IsHigherDepatmentNoSub());
        }

        /// <summary>
        /// 判断当前部门是不是一个有下属部门的一级部门
        /// </summary>
        /// <returns></returns>
        public bool IsHigherDepatmentHasSub()
        {
            ///  如果HigherDepartment == DepartmentId  说明此部门是一个有下属部门的一级部门
            return this.Id > 0 && this.m_higherDepartmentId == this.m_id;
        }

        public bool IsHigherDepatmentNoSub()
        {
            ///  如果HigherDepartment == 0 说明此部门自己是一个一级部门，下面没有部门
            return this.Id > 0 && this.m_higherDepartmentId == 0;
        }

        public static HSDepartment CreateStatisTotalHSDepartment()
        {
            HSDepartment deparment = new HSDepartment
            {
                Id = 0,
                Name = "实际总费用",  //"统计用的总部门信息",
                CanBoundary = 1,
              
                CanInland = 1,
              
                HigherDepartmentId = 0,
            };
            return deparment;
        }
        public bool IsStatisTotalDepartment()
        {
            return this.Id == 0;
        }

        public static HSDepartment CreateStatisRegularHSDepartment()
        {
            HSDepartment deparment = new HSDepartment
            {
                Id = -1,
                Name = "计划费用",//"统计用的计划总部门信息",
                CanBoundary = 1,
              
                CanInland = 1,
              
                HigherDepartmentId = 0,
            };
            return deparment;
        }

        public bool IsStatisRegularDepartment()
        {
            return this.m_id == -1;
        }
    }
}
