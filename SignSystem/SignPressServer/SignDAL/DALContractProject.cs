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

    //  对底层数据结构CobtractProject操作的数据库接口类
    //  操作数据库中的project表结构
    public class DALContractProject
    {

        #region  插入会签单工程
        private const String INSERT_CONTRACT_PROJECT_STR = @"INSERT INTO `project` (`categoryid`, `project`) VALUES (@CategoryId, @Project)";
        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static bool InsertContractProject(ContractProject project)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();

            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = INSERT_CONTRACT_PROJECT_STR;

                cmd.Parameters.AddWithValue("@CategoryId", project.CategoryId);                     //  项目类型     
                cmd.Parameters.AddWithValue("@Project", project.Project);   //  项目类型简写


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("插入项目类型成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("插入项目类型失败");
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


        #region  删除会签单工程
        /// <summary>
        /// 删除会签单工程
        /// </summary>
        /// <returns></returns>
        private static string DELETE_PROJECT_STR = @"DELETE FROM `project` WHERE (`id` = @Id)";
        public static bool DeleteContractProject(int projectId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_PROJECT_STR;
                cmd.Parameters.AddWithValue("@Id", projectId);                        // 员工姓名


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除会签单工程" + projectId.ToString() + "成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("删除会签单工程" + projectId.ToString() + "失败");
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
        private static string MODIFY_PROJECT_STR = @"UPDATE `project` SET `categoryId` = @CategoryId, `Project` = @Project WHERE (`id` = @Id)";
        public static bool ModifyProject(ContractProject project)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = MODIFY_PROJECT_STR;

                cmd.Parameters.AddWithValue("@Id", project.Id);                             //  部门职位
                cmd.Parameters.AddWithValue("@CategoryId",project.CategoryId);
                cmd.Parameters.AddWithValue("@Project", project.Project);



                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("工程类别{0}修改成功", project.Project);
                    return true;
                }
                else
                {
                    Console.WriteLine("工程类别{0}修改失败", project.Project);
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


        #region  获取到会签单的信息
        private static string GET_PROJECT_ID_BY_NAME_STR = @"SELECT id FROM `category` WHERE (category = @CategoryName)";
        public static int GetProjectId(String categoryName)
        { 
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            int id = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = GET_PROJECT_ID_BY_NAME_STR;
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    ContractProject project = new ContractProject();

                    id = int.Parse(sqlRead["id"].ToString());

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
            return id;
        
        }
        #endregion


        #region 获取到当会签单工程类别下的的项目列表
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

        #endregion


        #region  检查project的完整性
        private static string CHECK_PROJECT_INTEGRITY_STR = @"SELECT Count(id) count FROM `project` WHERE (id = @Id AND project = @Project)";
        public static bool CheckIntegrity(ContractProject project)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = CHECK_PROJECT_INTEGRITY_STR;

                cmd.Parameters.AddWithValue("@Id", project.Id);
                cmd.Parameters.AddWithValue("@Project", project.Project);

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
                    Console.WriteLine("PROJECT : Id = {0}, Project = {1}检查失败", project.Id, project.Project);
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
