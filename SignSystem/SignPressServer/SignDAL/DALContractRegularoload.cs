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

    //  数据库中regularload表的操作接口
    //  用于对年度的计划任务额度进行操作
    public class DALContractRegularoload
    {


        #region 插入会签单模版信息
        private static String INSERT_REGULARLOAD_STR = @"INSERT INTO `regularload` (`id`, `itemid`, `year`, `work`, `expense`) VALUES (@Id, @ItemId, @Year, @Work, @Expense)";
        public static bool InsertRegularload(ContractRegularload regularload)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();

            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = INSERT_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@Id", regularload.Id);             //  当前工作量所属的会签单信息
                cmd.Parameters.AddWithValue("@ItemId", regularload.ItemId);                    //  当前工作量的工作量信息
                cmd.Parameters.AddWithValue("@Year", regularload.Year);
                cmd.Parameters.AddWithValue("@Work", regularload.Work);                         //  当前工作量的工作量大小
                cmd.Parameters.AddWithValue("@Expense", regularload.Expense);                   //   当前工作量的报价

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("计划任务工作量{0}信息插入成功", regularload.Id);
                    return true;
                }
                else
                {
                    Console.WriteLine("计划任务工作量{0}插入失败", regularload.Id);
                    return false;
                }
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

        }
        #endregion


        #region 删除工作量信息
        /// <summary>
        /// 删除部门的信息DeleteEmployee
        /// </summary>
        private static String DELETE_REGULARLOAD_STR = @"DELETE FROM `regularload` WHERE `id` = @RegularloadId)";

        public static bool DeleteRegularloadd(ContractRegularload regularload)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@RegularloadId", regularload.Id);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除计划任务工作量表{0}成功", regularload.Id);
                    
                    return true;
                }
                else
                {
                    Console.WriteLine("删除计划任务工作量表{0}失败", regularload.Id);

                    return false;
                }
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
        }
        
        
        //private static String DELETE_WORKLOAD_BY_ID_STR = @"DELETE FROM `workload` WHERE `id` = @RegularloadId)";

        public static bool DeleteRegularload(String regularloadId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@RegularloadId", regularloadId);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除计划任务工作量表{0}成功", regularloadId);

                    return true;
                }
                else
                {
                    Console.WriteLine("删除计划任务工作量表{0}失败", regularloadId);

                    return false;
                }
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
        }
        #endregion
                               
                     

        #region  删除年份为year, 类别为catgory的所有计划任务
        private static String DELETE_YEAR_CATEGORY_REGULARLOAD_STR = //@"SELECT r.id FROM `regularload` r, `project` p, `item` i WHERE (r.itemid = i.id AND p.id = i.projectid AND r.year = @Year AND p.categoryid = @CategoryId))";
        @"DELETE FROM `regularload` 
          WHERE id in 
          (SELECT id FROM (
		  SELECT r.id id 
		  FROM `regularload` r, `project` p, `item` i 
		  WHERE (r.itemid = i.id AND p.id = i.projectid AND r.year = @Year AND p.categoryid = @CategoryId)
		  )table1  )";
        
        public static bool DeleteYearCategoryRegularload(int year, int categoryId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_YEAR_CATEGORY_REGULARLOAD_STR;
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count > 0)
                {
                    Console.WriteLine("删除计划任务工作量表信息{0}年类别{1}成功", year, categoryId);

                    return true;
                }
                else
                {
                    Console.WriteLine("删除计划任务工作量表信息{0}年类别{1}失败", year, categoryId);
                    return false;
                }
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
        }
        #endregion
      
        
        #region    修改工作量信息
        private const String MODIFY_WORKLOAD_STR = @"UPDATE `regularload` SET `work` = @Work, `expense` = @Expense WHERE (`id` = @Id)";
        /// <summary>
        /// 修改工作量信息
        /// </summary>
        /// <param name="workload"></param>
        /// <returns></returns>
        public static bool ModifyWorkload(ContractRegularload regularload)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = MODIFY_WORKLOAD_STR;
                cmd.Parameters.AddWithValue("@Id", regularload.Id);
                cmd.Parameters.AddWithValue("@Work", regularload.Work);
                cmd.Parameters.AddWithValue("@Expense", regularload.Expense);


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除计划任务工作量{0}成功", regularload.Id);
                    return true;
                }
                else
                {
                    Console.WriteLine("删除计划任务工作量{0}失败", regularload.Id);

                    return false;
                }
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
        }
        #endregion

        //#region 查询会签单计划额度的信息
        //public QueryYearCategoryRegularload(int year, int categoryId)
        //{
            
        //}
        
        //#endregion

    }
}
