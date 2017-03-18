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
    public class DALContractItem
    {


        #region  插入任务工作量Item
        private static string INSERT_ITEM_STR = @"INSERT INTO `item` (`projectid`, `item`) VALUES (@ProjectId, @Item)";
        public static bool InserItem(ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = INSERT_ITEM_STR;

                cmd.Parameters.AddWithValue("@ProjectId", item.ProjectId); 
                cmd.Parameters.AddWithValue("@Item", item.Item);


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("任务工作量{0}插入成功", item.Item);
                    return true;
                }
                else
                {
                    Console.WriteLine("任务工作量{0}插入失败", item.Item);
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


        #region  删除任务工作量
        private static string DELETE_ITEM_BY_ID_STR = @"DELETE FROM `item` WHERE (id = @Id)";
        public static bool DeleteItem(int itemId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_ITEM_BY_ID_STR;
                cmd.Parameters.AddWithValue("@Id", itemId);                        // 员工姓名


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除任务工作量{0}成功", itemId);
                    return true;
                }
                else
                {
                    Console.WriteLine("删除任务工作量{0}失败", itemId);
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

        private static string DELETE_ITEM_BY_NAME_STR = @"DELETE FROM `item` WHERE (item = @ItemName)";
        public static bool DeleteItem(string itemName)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_ITEM_BY_NAME_STR;
                cmd.Parameters.AddWithValue("@ItemName", itemName);                        // 员工姓名


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除任务工作量{0}成功", itemName);
                    return true;
                }
                else
                {
                    Console.WriteLine("删除任务工作量{0}失败", itemName);
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


        #region  修改任务工作量Item
        private static string MODIFY_ITEM_STR = @"UPDATE `item` SET `projectId` = @ProjectId, `Item` = @Item WHERE (`id` = @Id)";
        public static bool ModifyItem(ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = MODIFY_ITEM_STR;

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@ProjectId", item.ProjectId);
                cmd.Parameters.AddWithValue("@Item", item.Item);


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("任务工作量{0}修改成功", item.Item);
                    return true;
                }
                else
                {
                    Console.WriteLine("任务工作量{0}修改失败", item.Item);
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


        #region  删除Project的所有工作量任务工作量
        private static string DELETE_PROJECT_ITEM_BY_ID_STR = @"DELETE FROM `item` WHERE (projectId = @ProjectId)";
        public static bool DeleteProjectItem(int projectId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_PROJECT_ITEM_BY_ID_STR;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);                        // 员工姓名


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除工程{0}的任务工作量集合成功", projectId);
                    return true;
                }
                else
                {
                    Console.WriteLine("删除任务{0}的任务工作量集合失败", projectId);
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

        private static string DELETE_PROJECT_ITEM_BY_NAME_STR = @"DELETE FROM `item` WHERE (item = @ProjectName)";
        public static bool DeleteProjectItem(string projectName)
        {
            ///////////////////
            ///
            ///
            ///  本接口暂时未实现
            ///
            ///////////////////
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_PROJECT_ITEM_BY_NAME_STR;
                cmd.Parameters.AddWithValue("@ProjectName", projectName);                       


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除任务工作量{0}成功", projectName);
                    return true;
                }
                else
                {
                    Console.WriteLine("删除任务工作量{0}失败", projectName);
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


        #region 获取到当前部门所申请的会签单的可申请工程的项目列表
        private const String QUERY_PROJECT_ITEM_BY_PROJECT_ID_STR = @"SELECT id, projectid, item FROM `item` WHERE (`projectid` = @ProjectId)";

        public static List<ContractItem> QueryProjectItem(int projectId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<ContractItem> items = new List<ContractItem>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_PROJECT_ITEM_BY_PROJECT_ID_STR;
                cmd.Parameters.AddWithValue("@ProjectId", projectId);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    ContractItem item = new ContractItem();

                    item.Id = int.Parse(sqlRead["id"].ToString());
                    item.ProjectId = int.Parse(sqlRead["projectid"].ToString());
                    item.Item = sqlRead["item"].ToString();

                    items.Add(item);
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
            return items;
        }


        //private const String QUERY_PROJECT_ITEM_BY_PROJECT_NAME_STR = @"SELECT i.id id, i.projectid projectid, i.item item FROM `item` i, `project` p WHERE (p.id = i.projectid AND p.project = @ProjectName)";
        //
        private const String QUERY_PROJECT_ITEM_BY_PROJECT_NAME_STR = @"SELECT i.id id, i.projectid projectid, i.item item FROM `item` i, `project` p, category c WHERE (c.id = p.categoryid AND p.id = i.projectid AND c.category = @CategoryName AND p.project = @ProjectName)";
        public static List<ContractItem> QueryProjectItem(string categoryName, string projectName)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<ContractItem> items = new List<ContractItem>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_PROJECT_ITEM_BY_PROJECT_NAME_STR;
                cmd.Parameters.AddWithValue("CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@ProjectName", projectName);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    ContractItem item = new ContractItem();

                    item.Id = int.Parse(sqlRead["id"].ToString());
                    item.ProjectId = int.Parse(sqlRead["projectid"].ToString());
                    item.Item = sqlRead["item"].ToString();

                    items.Add(item);
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
            return items;
        }
        #endregion


        #region  获取当前item所属的category
        private static string GET_CATEGORY_ID_STR = @"SELECT p.categoryid id FROM `item` i, `project` p WHERE (i.projectid = p.id AND i.item = @ItemName AND i.projectId = @ProjectId)";
        public static int GetCategoryId(ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            int categoryId = -1;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = GET_CATEGORY_ID_STR;
                cmd.Parameters.AddWithValue("@ProjectId", item.ProjectId);
                cmd.Parameters.AddWithValue("@ItemName", item.Item);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    categoryId = int.Parse(sqlRead["id"].ToString());
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
            return categoryId;
        }

        private static string GET_CATEGORY_ID_BY_ITEM_ID_STR = @"SELECT p.categoryid id FROM `item` i, `project` p WHERE (i.projectid = p.id AND i.id = @ItemId)";
        public static int GetCategoryId(int itemId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            int categoryId = -1;

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = GET_CATEGORY_ID_BY_ITEM_ID_STR;
                cmd.Parameters.AddWithValue("@ItemId", itemId);

                MySqlDataReader sqlRead = cmd.ExecuteReader();

                cmd.Dispose();

                while (sqlRead.Read())
                {
                    categoryId = int.Parse(sqlRead["id"].ToString());
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
            return categoryId;
        }
        #endregion


        #region  检查item的完整性
        private static string CHECK_ITEM_INTEGRITY_STR = @"SELECT Count(id) count FROM `item` WHERE (id = @Id AND projectid = @ProjectId AND item = @Item)";
        public static bool CheckIntegrity(ContractItem item)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = CHECK_ITEM_INTEGRITY_STR;

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@ProjectId", item.ProjectId);
                cmd.Parameters.AddWithValue("@Item", item.Item);

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {

                    count = int.Parse(sqlRead["count"].ToString());

                }

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("PROJECT : Id = {0}, ProjectId = {1}, Item = {2}检查失败", item.Id, item.ProjectId, item.Item);
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


    }
}
