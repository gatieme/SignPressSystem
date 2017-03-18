using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using SignPressServer.SignData;


using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using SignPressServer.SignTools;



namespace SignPressServer.SignDAL
{

    enum STATIS_KIND
    {
        /// 统计部门department当年year的工作量item--工程project--项目category
        STATIS_DEPARTMENT_YEAR_ITEM_WORKLOAD,     //
        STATIS_DEPARTMENT_YEAR_PROJECT_WORKLOAD,
        STATIS_DEPARTMENT_YEAR_CATEGORY_WORKLOAD,
        STATIS_YEAR_PROJECT_REGULARLOAD,

        /// 统计部门department当年year的工作量item--工程project--项目category
        STATIS_YEAR_ITEM_WORKLOAD,       //  统计所有部门当前year工作量item的汇总统计信息
        STATIS_YEAR_PROJECT_WORKLOAD,
        STATIS_YEAR_CATEGORY_WORKLOAD,
        STATIS_YEAR_CATEGORY_REGULARLOAD,

    }
    /// <summary>
    /// 该类型是统计功能操作数据库的接口 
    /// </summary>
 
    ///  注意我们的数据结构
    ///  Category  标识了会签单的基本类别-[界内应例]，其直接影响着工程名称  
    ///  Project   标识了会签单的项目名称，其依赖于category, 标识了工程下面的每个子项
    ///  Item      标识了会签单的工作量信息，其依赖于Project，标识了项目下面的每个子项
    ///  Workload  标识了会签单的工作量集合，他由Item类别 + 工作量大小 + 花费金额组成
    public class DALContractStatistic
    {
        ///  modify by gatieme @ 2016-1-23
        ///  修复了统计的一处BUG
        ///  统计只能统计那些已经通过的会签单的申请额度, 正在审批的会签单是不应该被统计的

        #region  查询部门department当年year的单项工作量Item的统计信息

        //private static String STATIS_DEPARTMENT_YEAR_ITEM_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `workload` WHERE `contractid` like @SDepartmentYear AND `itemid` = @ItemId";
        // 
        private static String STATIS_DEPARTMENT_YEAR_ITEM_STR = @"SELECT IFNULL(Sum(w.work), 0) works, IFNULL(Sum(w.expense),0) expenses FROM `workload` w, `signaturestatus` s WHERE w.contractid = s.conid AND s.totalresult = 1 AND `contractid` like @SDepartmentYear AND `itemid` = @ItemId";
        #region  查询部门department当年year的单项工作量Item的统计信息
        ///  SELECT w.contractid, w.itemid, i.item, w.work, w.expense FROM `workload` w, `item` i WHERE w.itemid = i.id AND `contractid` like "申%" AND `itemid` = 1
        ///  SELECT w.contractid,  p.id projectid, p.project, i.id itemid, i.item,w.work, w.expense FROM `workload` w, `item` i, `project` p WHERE w.itemid = i.id AND i.projectid = p.id AND `contractid` like "申%" AND `itemid` = 1
        
        /// SELECT Sum(work) works, Sum(expense) expenses FROM `workload` WHERE `contractid` like "申_2015%" AND itemid = 1 GROUP BY itemid
        /// SELECT i.id "工作量编号", i.item "工作量名称", Sum(wl.work) "工作量大小", Sum(wl.expense) "花费" FROM `workload` wl, item i WHERE wl.itemid = i.id AND `contractid` like "申_2015%" GROUP BY i.id

        /// <summary>
        /// 查询部门department当年year的单项工作量的统计信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="year"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static ContractWorkload StatisDepartmentYearItemWorkLoad(string departmentShortCall, int year, int itemId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_ITEM_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", departmentShortCall + "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_ITEM_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }

        public static ContractWorkload StatisDepartmentYearItemWorkLoad(HSDepartment department, int year, ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_ITEM_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", department.ShortCall + "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@ItemId", item.Id);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_ITEM_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    /// BUG
                    //ContractItem item = new ContractItem();
                    //item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }
        #endregion


        #region  统计当前有下属部门的一级部门的信息
        public static ContractWorkload StatisHigherDepartmentYearItemWorkLoad(HSDepartment highDepartment, int year, ContractItem item)
        {
            //  首先获取到当前一级部门的所有下级部门信息
            List<HSDepartment> departments = DALHSDepartment.QuerySubHSDepartment(highDepartment.Id);
            ContractWorkload totalWorkloads = new ContractWorkload
            {
                Work = 0,
                Expense = 0,
            };
            foreach (HSDepartment department in departments)
            {
                ContractWorkload workload = DALContractStatistic.StatisDepartmentYearItemWorkLoad(department, year, item);
                totalWorkloads.Work += workload.Work;
                totalWorkloads.Expense += workload.Expense;
            }

            return totalWorkloads;
        }
        #endregion


        #region 统计所有部门当前year工作量item的汇总统计信息
        //private static String STATIS_YEAR_ITEM_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `workload` WHERE `contractid` like @Year AND `itemid` = @ItemId";
        public static ContractWorkload StatisYearItemWorkLoad(int year, ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_ITEM_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@ItemId", item.Id);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_YEAR_ITEM_WORKLOAD.ToString();
                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    /// BUG
                    //ContractItem item = new ContractItem();
                    //item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }
        #endregion


        #region 统计当前Project的reguload计划额度
        //private static String STATIS_YEAR_ITEM_REGULARLOAD_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `regularload` r WHERE (year = @Year AND itemid = @ItemId)";
        private static String STATIS_YEAR_ITEM_REGULARLOAD_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `regularload` r WHERE (year = @Year AND itemid = @ItemId)";
        
        public static ContractWorkload StatisYearItemRegularLoad(int year, ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_YEAR_ITEM_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@ItemId", item.Id);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_YEAR_PROJECT_REGULARLOAD.ToString();
                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;
        }
        #endregion


        #region  统计当前部门deaprtment当年year申请的所有工作量为Item的统计信息 --  带一二级部门处理
        public static ContractWorkload StatisHSDepartmentYearItemWorkLoad(HSDepartment department, int year, ContractItem item)
        {
            if (department.IsHigherDepatmentHasSub() == true)   //  如果当前部门是一个有下属部门的一级部门
            {
                //Console.WriteLine("部门{0}是一个有子部门的一级部门", department.Name);
                return DALContractStatistic.StatisHigherDepartmentYearItemWorkLoad(department, year, item);
            }
            else if (department.IsHigherDepatmentNoSub() == true)
            {
                //Console.WriteLine("部门{0}是无子部门的一级部门", department.Name);
                return DALContractStatistic.StatisDepartmentYearItemWorkLoad(department, year, item);
            }
            else if (department.IsStatisTotalDepartment() == true)
            {
                //Console.WriteLine("部门{0}要求统计计划任务的总和", department.Name);
                return DALContractStatistic.StatisYearItemWorkLoad(year, item);
            }
            else if (department.IsStatisRegularDepartment() == true)
            {
                //Console.WriteLine("部门{0}要求统计计划任务额度", department.Name);
                return DALContractStatistic.StatisYearItemRegularLoad(year, item); 
            }
            else if (department.IsSubDepartment() == true)      //  如果当前部门是一个下属子部门
            {
                Console.WriteLine("部门{0}是下属子部门", department.Name);
                return null;
            }

            return null;
        }
        #endregion

        

        #endregion


        #region  统计部门department当年year工程project的统计信息
        
        /// SELECT p.id, p.project, Sum(wl.work) work, Sum(wl.expense) expense FROM `workload` wl, item i, project p WHERE wl.itemid = i.id AND i.projectid = p.id AND `contractid` like "申_2015%" GROUP BY p.id
        //private static String STATIS_DEPARTMENT_YEAR_PROJECT_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `workload` w, `item` i WHERE w.itemid = i.id AND w.contractid like @SDepartmentYear AND i.projectid = @ProjectId";
        private static String STATIS_DEPARTMENT_YEAR_PROJECT_STR = @"SELECT IFNULL(Sum(w.work), 0) works, IFNULL(Sum(w.expense),0) expenses FROM `workload` w, `item` i, `signaturestatus` s  WHERE  w.contractid = s.conid AND s.totalresult = 1 AND w.itemid = i.id AND w.contractid like @SDepartmentYear AND i.projectid = @ProjectId";


        #region  统计当前部门deaprtment当年year申请的所有工程为Project的统计信息
         /// <summary>
        ///  统计当前部门申请的所有工程Project下的会签单信息[Search数据填写SDepartmentShortCall + ItemId]       -  不带一二级部门标识   [此接口已经废弃]
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ContractWorkload StatisDepartmentYearProjectWorkLoad(string departmentShortCall, int year, int projectId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_PROJECT_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", departmentShortCall + "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@ProjectId", projectId);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_PROJECT_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }

        /// <summary>
        ///  统计当前部门申请的所有工程Project下的会签单信息[Search数据填写SDepartmentShortCall + ItemId]
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ContractWorkload StatisDepartmentYearProjectWorkLoad(HSDepartment department, int year, ContractProject project)
        {
            if (project.Id == 10)
            {

            }
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_PROJECT_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", department.ShortCall + "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@ProjectId", project.Id);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_PROJECT_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }
        #endregion


        #region  统计当前有下属部门的一级部门的信息
        public static ContractWorkload StatisHigherDepartmentYearProjectWorkLoad(HSDepartment highDepartment, int year, ContractProject project)
        {
            //  首先获取到当前一级部门的所有下级部门信息
            List<HSDepartment> departments = DALHSDepartment.QuerySubHSDepartment(highDepartment.Id);
            ContractWorkload totalWorkloads = new ContractWorkload 
            { 
                Work = 0,
                Expense = 0,
            };
            foreach (HSDepartment department in departments)
            { 
                ContractWorkload workload = DALContractStatistic.StatisDepartmentYearProjectWorkLoad(department, year, project);
                totalWorkloads.Work += workload.Work;
                totalWorkloads.Expense += workload.Expense;
            }

            return totalWorkloads;
        }
        #endregion

       
        #region 统计所有部门当前year工程project的汇总统计信息
        //private static String STATIS_YEAR_PROJECT_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `workload` w, `item` i WHERE w.itemid = i.id AND w.contractid like @SDepartmentYear AND i.projectid = @ProjectId";
        public static ContractWorkload StatisYearProjectWorkLoad(int year, ContractProject project)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_PROJECT_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", "___" + year + "%");
                cmd.Parameters.AddWithValue("@ProjectId", project.Id);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_YEAR_PROJECT_WORKLOAD.ToString();
                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    /// BUG
                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }
        #endregion


        #region 统计当前Project的reguload计划额度
        private static String STATIS_YEAR_PROJECT_REGULARLOAD_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `regularload` r, `item` i WHERE (r.itemid = i.id AND r.`year` = @Year AND i.projectid = @ProjectId)";
        public static ContractWorkload StatisYearProjectRegularLoad(int year, ContractProject project)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_YEAR_PROJECT_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@ProjectId", project.Id);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_YEAR_PROJECT_REGULARLOAD.ToString();
                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    /// BUG
                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;
        }
        #endregion


        #region  统计当前部门deaprtment当年year申请的所有工程为Project的统计信息 --  带一二级部门处理
        public static ContractWorkload StatisHSDepartmentYearProjectWorkLoad(HSDepartment department, int year, ContractProject project)
        {
            if (department.IsHigherDepatmentHasSub() == true)   //  如果当前部门是一个有下属部门的一级部门
            {
                //Console.WriteLine("部门{0}是一个有子部门的一级部门", department.Name);
                return DALContractStatistic.StatisHigherDepartmentYearProjectWorkLoad(department, year, project);
            }
            else if (department.IsHigherDepatmentNoSub() == true)
            {
                //Console.WriteLine("部门{0}是无子部门的一级部门", department.Name);

                return DALContractStatistic.StatisDepartmentYearProjectWorkLoad(department, year, project);
            }
            else if (department.IsStatisTotalDepartment() == true)
            {
                //Console.WriteLine("部门{0}要求统计所有部门的总和", department.Name);
                return DALContractStatistic.StatisYearProjectWorkLoad(year, project);
            }
            else if (department.IsStatisRegularDepartment() == true)
            {
                //Console.WriteLine("部门{0}要求统计计划任务额度", department.Name);
                return DALContractStatistic.StatisYearProjectRegularLoad(year, project);
            }
            else if (department.IsSubDepartment() == true)      //  如果当前部门是一个下属子部门
            {
                //Console.WriteLine("部门{0}是下属子部门", department.Name);
                return null;
            }

            return null;
        }
        #endregion


        #endregion


        #region   统计部门department当年year项目category的统计信息
        /// SELECT p.id, p.project, Sum(wl.work) work, Sum(wl.expense) expense FROM `workload` wl, item i, project p WHERE wl.itemid = i.id AND i.projectid = p.id AND `contractid` like "申_2015%" GROUP BY p.id
        private static String STATIS_DEPARTMENT_YEAR_CATEGORY_STR = @"SELECT IFNULL(Sum(w.work), 0) works, IFNULL(Sum(w.expense),0) expenses
FROM `workload` w, `signaturestatus` s, `category` c, `project` p, `item` i
WHERE  w.contractid = s.conid AND w.itemid = i.id AND c.id = p.categoryid AND p.id = i.projectid
AND s.totalresult = 1 AND w.contractid like @SDepartmentYear AND c.id = CategoryId";


        #region  统计当前部门deaprtment当年year申请的类别为Category的统计信息     works是工作量
        private static String STATIS_DEPCATESHORTCALL_YEAR_CATEGORY_STR = @"SELECT IFNULL(Sum(w.work), 0) works, IFNULL(Sum(w.expense),0) expenses
FROM `workload` w, `signaturestatus` s
WHERE  w.contractid = s.conid 
AND s.totalresult = 1 AND w.contractid like @SDepartmentCategoryYear";
        public static ContractWorkload StatisDepartmentYearCategoryWorkLoad(string departmentShortCall, string categoryShortCall, int year)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPCATESHORTCALL_YEAR_CATEGORY_STR;
                cmd.Parameters.AddWithValue("@SDepartmentCategoryYear", departmentShortCall + categoryShortCall + year.ToString() + "%");
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_CATEGORY_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }

        /// <summary>
        ///  统计当前部门申请的所有工程Project下的会签单信息[Search数据填写SDepartmentShortCall + ItemId]
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ContractWorkload StatisDepartmentYearCategoryWorkLoad(string departmentShortCall, int year, int categoryId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_CATEGORY_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", departmentShortCall + "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_CATEGORY_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }

        /// <summary>
        ///  统计当前部门申请的所有工程Project下的会签单信息[Search数据填写SDepartmentShortCall + ItemId]
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ContractWorkload StatisDepartmentYearCategoryWorkLoad(HSDepartment department, int year, ContractCategory category)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_CATEGORY_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", department.ShortCall + "__" + year.ToString() + "%");
                cmd.Parameters.AddWithValue("@CategoryId", category.Id);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_DEPARTMENT_YEAR_CATEGORY_WORKLOAD.ToString();

                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }
        #endregion


        #region 统计当前一级部门的类别category的统计信息
        public static ContractWorkload StatisHigherDepartmentYearCategoryWorkLoad(HSDepartment highDepartment, int year, ContractCategory category)
        {
            //  首先获取到当前一级部门的所有下级部门信息
            List<HSDepartment> departments = DALHSDepartment.QuerySubHSDepartment(highDepartment.Id);
            ContractWorkload totalWorkloads = new ContractWorkload
            {
                Work = 0,
                Expense = 0,
            };
            foreach (HSDepartment department in departments)
            {
                ContractWorkload workload = DALContractStatistic.StatisDepartmentYearCategoryWorkLoad(department, year, category);
                totalWorkloads.Work += workload.Work;
                totalWorkloads.Expense += workload.Expense;
            }

            return totalWorkloads;
        }
        #endregion                                                                                                                            


        #region 统计所有部门当前year项目category的汇总统计信息
        //private static String STATIS_YEAR_CATEGORY_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `workload` w, `item` i WHERE w.itemid = i.id AND i.projectid = @ProjectId";
        public static ContractWorkload StatisYearCategoryWorkLoad(int year, ContractCategory category)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_DEPARTMENT_YEAR_CATEGORY_STR;
                cmd.Parameters.AddWithValue("@SDepartmentYear", "__" + year + "%");
                cmd.Parameters.AddWithValue("@CategoryId", category.Id);
                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_YEAR_CATEGORY_WORKLOAD.ToString();
                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    /// BUG
                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;

        }
        #endregion


        #region 统计当前Project的reguload计划额度
        private static String STATIS_YEAR_CATEGORY_REGULARLOAD_STR = @"SELECT IFNULL(Sum(work), 0) works, IFNULL(Sum(expense),0) expenses FROM `regularload` r, `item` i, `project` p WHERE (r.itemid = i.id AND i.projectid = p.id AND r.`year` = @Year AND p.categoryid = @CategoryId)";
        public static ContractWorkload StatisYearCategoryRegularLoad(int year, ContractCategory category)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            ContractWorkload workload = null;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = STATIS_YEAR_CATEGORY_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@CategoryId", category.Id);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    workload = new ContractWorkload();

                    workload.ContractId = STATIS_KIND.STATIS_YEAR_CATEGORY_REGULARLOAD.ToString();
                    workload.Work = double.Parse(sqlRead["works"].ToString());
                    workload.Expense = double.Parse(sqlRead["expenses"].ToString());

                    /// BUG
                    ContractItem item = new ContractItem();
                    item.Id = -1;
                    workload.Item = item;

                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return workload;
        }
        #endregion


        #region  统计当前部门一级hsdeaprtment当年year申请的类别为Category的统计信息
        public static ContractWorkload StatisHSDepartmentYearCategoryWorkLoad(HSDepartment department, int year, ContractCategory category)
        {
            if (department.IsHigherDepatmentHasSub() == true)   //  如果当前部门是一个有下属部门的一级部门
            {
                //Console.WriteLine("部门{0}是一个有子部门的一级部门", department.Name);
                return DALContractStatistic.StatisHigherDepartmentYearCategoryWorkLoad(department, year, category);
            }
            else if (department.IsHigherDepatmentNoSub() == true)
            {
                //Console.WriteLine("部门{0}是无子部门的一级部门", department.Name);
                return DALContractStatistic.StatisDepartmentYearCategoryWorkLoad(department, year, category);
            }
            else if (department.IsStatisTotalDepartment() == true)
            {
                //Console.WriteLine("部门{0}要求统计所有部门的总和", department.Name);
                return DALContractStatistic.StatisYearCategoryWorkLoad(year, category);
            }
            else if (department.IsStatisRegularDepartment() == true)
            {
                //Console.WriteLine("部门{0}要求统计计划任务额度", department.Name);
                return DALContractStatistic.StatisYearCategoryRegularLoad(year, category);
            }
            else if (department.IsSubDepartment() == true)      //  如果当前部门是一个下属子部门
            {
               // Console.WriteLine("部门{0}是下属子部门", department.Name);
                return null;
            }
            
            return null;
        }
        #endregion





        #endregion


        #region  查询信息, 用于替代会签单PROJECT, ITEM, WORKLOAD的管理操作
        /*private static String QUERY_ITEM_WITH_STATIS_STR = 
@"SELECT p.id, p.categoryId, p.project,
IFNULL(Sum(w.expense),0) realexpenses,
IFNULL(Sum(r.expense),0) regualrexpense 
FROM `workload` w, `item` i, project p, regularload r 
WHERE w.itemid = i.id AND p.id = i.projectid AND r.itemid = i.id
  AND w.contractid like "__2016%" AND i.projectid = 1
        public List<ContractProjectWithStatisticData> QueryCategoryProject(int categoryId)

        private const String QUERY_CATEGORY_PROJECT_BY_ID_STR = @"SELECT id, categoryid, project FROM `project` WHERE (`categoryid` = @CategoryId)";
        public static List<ContractProject> QueryCategoryProject(int categoryId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<ContractProject> projects = new List<ContractProject>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_CATEGORY_PROJECT_BY_ID_STR;
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    ContractProject project = new ContractProject();

                    project.Id = int.Parse(sqlRead["id"].ToString());
                    project.CategoryId = int.Parse(sqlRead["categoryid"].ToString());
                    project.Project = sqlRead["project"].ToString();

                    projects.Add(project);
                }


                con.Close();
                con.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return projects;
        }
                */
        #endregion
    }
}
