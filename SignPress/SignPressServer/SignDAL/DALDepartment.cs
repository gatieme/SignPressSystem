﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;


using SignPressServer.SignData;
using SignPressServer.SignTools;

namespace SignPressServer.SignDAL
{


    #region 基础部门信息Department的数据库操作接口
    public class DALDepartment
    {
        #region  数据库信息串

        /// <summary>
        /// 插入数据库的信息串
        /// </summary>
        private const String INSERT_DEPARTMENT_STR = @"INSERT INTO `department` (`name`, `shortcall`) VALUES (@Name, @ShortCall)";

        /// <summary>
        /// 删除数据库的信息串
        /// </summary>
        //  按照部门ID删除部门
        private const String DELETE_DEPARTMENT_ID_STR = @"DELETE FROM `department` WHERE (`id` = @Id)";
        //  按照部门Name删除部门
        private const String DELETE_DEPARTMENT_NAME_STR = @"DELETE FROM `department` WHERE (`name` = @Name)";

        /// <summary>
        /// 修改部门名称的信息串
        /// </summary>
        private const String MODIFY_DEPARTMENT_ID_STR = @"UPDATE `department` SET `name` = @Name, `shortcall` = @ShortCall WHERE (`id` = @Id)";
        /// <summary>
        /// 查询部门信息的信息串
        /// </summary>
        private const String QUERY_DEPARTMENT_STR = @"SELECT id, name, shortcall FROM `department` ORDER BY id";

        #endregion


        #region  插入部门信息
        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static bool InsertDepartment(Department department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();

            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = INSERT_DEPARTMENT_STR;
                cmd.Parameters.AddWithValue("@Name", department.Name);                  //  部门职位
                cmd.Parameters.AddWithValue("@ShortCall", department.ShortCall);         //  部门简称

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("部门插入成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("部门插入失败");
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


        #region 删除部门信息
        /// <summary>
        /// 删除部门的信息DeleteEmployee
        /// </summary>
        /// <param name="id">部门的部门号</param>
        /// <returns></returns>
        public static bool DeleteDepartment(int departmentId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_DEPARTMENT_ID_STR;
                cmd.Parameters.AddWithValue("@Id", departmentId);                        // 部门姓名


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除部门" + departmentId.ToString() + "成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("删除部门" + departmentId.ToString() + "失败");
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

        public static bool DeleteDepartment(String departmentName)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = DELETE_DEPARTMENT_NAME_STR;
                cmd.Parameters.AddWithValue("@Name", departmentName);                        // 部门姓名


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("删除部门" + departmentName + "成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("删除部门" + departmentName + "失败");
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


        #region    修改部门信息
        public static bool ModifyDepartment(Department department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = MODIFY_DEPARTMENT_ID_STR;
                cmd.Parameters.AddWithValue("@Id", department.Id);
                cmd.Parameters.AddWithValue("@Name", department.Name);                        // 员工姓名
                cmd.Parameters.AddWithValue("@ShortCall", department.ShortCall);              //  部门简称


                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("修改部门名称" + department.Id.ToString() + "成功");

                    return true;
                }
                else
                {
                    Console.WriteLine("修改部门名称" + department.Id.ToString() + "失败");

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


        #region 查询部门的信息
        public static List<Department> QueryDepartment()
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<Department> departments = new List<Department>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_DEPARTMENT_STR;


                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    Department department = new Department();

                    department.Id = int.Parse(sqlRead["id"].ToString());
                    department.Name = sqlRead["name"].ToString();
                    department.ShortCall = sqlRead["shortcall"].ToString();

                    departments.Add(department);
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
            return departments;
        }
        #endregion



    }
    #endregion


    #region 带申请权限的部门信息SDepartment的数据库操作接口
    /// <summary>
    /// 带可申请权限的部门信息处理
    /// </summary>
    public class DALSDepartment : DALDepartment
    {

        #region 查询部门的信息项目信息        /// <summary>
        /// 查询部门信息的信息串
        /// </summary>
        private const String QUERY_SDEPARTMENT_STR = @"SELECT id, name, shortcall, canboundary, caninland, canemergency, canregular FROM `department` ORDER BY id";

        public static List<SDepartment> QuerySDepartment()
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<SDepartment> departments = new List<SDepartment>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_SDEPARTMENT_STR;

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    SDepartment department = new SDepartment();

                    department.Id = int.Parse(sqlRead["id"].ToString());
                    department.Name = sqlRead["name"].ToString();
                    department.ShortCall = sqlRead["shortcall"].ToString();

                    department.CanBoundary = int.Parse(sqlRead["canboundary"].ToString());
                    department.CanInland = int.Parse(sqlRead["caninland"].ToString());
                    department.CanEmergency = int.Parse(sqlRead["canemergency"].ToString());
                    department.CanRegular = int.Parse(sqlRead["canregular"].ToString());


                    departments.Add(department);
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
            return departments;
        }
        #endregion


        //#region 查询部门的信息项目信息        /// <summary>
        ///// 查询部门信息的信息串
        ///// </summary>
        //private const String QUERY_SDEPARTMENT_CATEGORY_STR = @"SELECT id, name, shortcall, canboundary, caninland, canemergency, canregular FROM `department` ORDER BY id";

        //public static List<SDepartment> QuerySDepartmentCategory()
        //{
        //    MySqlConnection con = DBTools.GetMySqlConnection();
        //    MySqlCommand cmd;

        //    List<SDepartment> departments = new List<SDepartment>();

        //    try
        //    {
        //        con.Open();

        //        cmd = con.CreateCommand();

        //        cmd.CommandText = QUERY_SDEPARTMENT_CATEGORY_STR;

        //        MySqlDataReader sqlRead = cmd.ExecuteReader();
        //        cmd.Dispose();

        //        while (sqlRead.Read())
        //        {
        //            SDepartment department = new SDepartment();

        //            department.Id = int.Parse(sqlRead["id"].ToString());
        //            department.Name = sqlRead["name"].ToString();
        //            department.ShortCall = sqlRead["shortcall"].ToString();

        //            department.CanBoundary = (int.Parse(sqlRead["canboundary"].ToString()) == 1) ? "界" : "";
        //            department.CanInland = (int.Parse(sqlRead["caninland"].ToString()) == 1) ? "内" : "";
        //            department.CanEmergency = (int.Parse(sqlRead["canemergency"].ToString()) == 1) ? "应" : "";
        //            department.CanRegular = (int.Parse(sqlRead["canregular"].ToString()) == 1) ? "例" : "";


        //            departments.Add(department);
        //        }


        //        con.Close();
        //        con.Dispose();

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {

        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return departments;
        //}
        //#endregion


        #region  插入部门信息

        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private const String INSERT_SDEPARTMENT_STR = @"INSERT INTO `department` (`name`, `shortcall`, `canboundary`, `caninland`, `canemergency`, `canregular`) VALUES (@Name, @ShortCall, @CanBoundary, @CanInland, @CanEmergency, @CanRegular)";

        public static bool InsertSDepartment(SDepartment department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();

            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = INSERT_SDEPARTMENT_STR;
                cmd.Parameters.AddWithValue("@Name", department.Name);                  //  部门职位
                cmd.Parameters.AddWithValue("@ShortCall", department.ShortCall);         //  部门简称

                cmd.Parameters.AddWithValue("@CanBoundary", department.CanBoundary);
                cmd.Parameters.AddWithValue("@CanInland", department.CanInland);
                cmd.Parameters.AddWithValue("@CanEmergency", department.CanEmergency);
                cmd.Parameters.AddWithValue("@CanRegular", department.CanRegular);
                    
                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("部门详细插入成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("部门详细插入失败");
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


        #region    修改部门详细权限信息
        /// <summary>
        /// 修改部门名称的信息串
        /// </summary>
        private const String MODIFY_SDEPARTMENT_ID_STR = @"UPDATE `department` SET `name` = @Name, `shortcall` = @ShortCall, `canboundary` = @CanBoundary, `caninland` = @CanInland, `canemergency` = @CanEmergency, `canregular` = @CanRegular WHERE (`id` = @Id)";

        public static bool ModifySDepartment(SDepartment department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count = -1;
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = MODIFY_SDEPARTMENT_ID_STR;
                cmd.Parameters.AddWithValue("@Id", department.Id);
                cmd.Parameters.AddWithValue("@Name", department.Name);                        // 员工姓名
                cmd.Parameters.AddWithValue("@ShortCall", department.ShortCall);              //  部门简称

                cmd.Parameters.AddWithValue("@CanBoundary", department.CanBoundary);
                cmd.Parameters.AddWithValue("@CanInland", department.CanInland);
                cmd.Parameters.AddWithValue("@CanEmergency", department.CanEmergency);
                cmd.Parameters.AddWithValue("@CanRegular", department.CanRegular);
                
                
                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();

                if (count == 1)
                {
                    Console.WriteLine("修改部门信息" + department.Id.ToString() + "成功");

                    return true;
                }
                else
                {
                    Console.WriteLine("修改部门名称" + department.Id.ToString() + "失败");

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
    

        #region 为当前部门增加界河项目的权限
        private const String SET_SDEPARTMENT_BOUNDARY_STR = @"UPDATE `department` SET `canboundary` = @Value";

        public static bool SetSDepartmentBoundary(SDepartment department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count;

            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = SET_SDEPARTMENT_BOUNDARY_STR;
                cmd.Parameters.AddWithValue("@CanBoundary", department.CanBoundary);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("修改部门的界河项目权限成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("修改部门的界河项目权限失败");
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


        #region 为当前部门增加内河项目的权限
        private const String SET_SDEPARTMENT_INLAND_STR = @"UPDATE `department` SET `caninland` = @CanInland";

        public static bool SetSDepartmentInland(SDepartment department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count;

            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = SET_SDEPARTMENT_INLAND_STR;
                cmd.Parameters.AddWithValue("@CanInland", department.CanInland);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("修改部门的界河项目权限成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("修改部门的界河项目权限失败");
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


        #region 为当前部门增加应急项目的权限
        private const String SET_DEPARTMENT_EMERGENCY_STR = @"UPDATE `department` SET `canemergency` = @CanEmergency";

        public static bool SetSDepartmentEmergency(SDepartment department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count;

            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = SET_DEPARTMENT_EMERGENCY_STR;
                cmd.Parameters.AddWithValue("@CanEmergency", department.CanEmergency);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("修改部门的界河项目权限成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("修改部门的界河项目权限失败");
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


        #region 为当前部门增加例会项目的权限
        private const String SET_SDEPARTMENT_REGULAR_STR = @"UPDATE `department` SET `canregular` = @CanRegular";

        public static bool SetSDepartmentRegular(SDepartment department)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;
            int count;

            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = SET_SDEPARTMENT_REGULAR_STR;
                cmd.Parameters.AddWithValue("@CanRegular", department.CanRegular);

                count = cmd.ExecuteNonQuery();
                cmd.Dispose();

                con.Close();
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("修改部门的界河项目权限成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("修改部门的界河项目权限失败");
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
    #endregion


    #region 带上级部门和申请权限的部门信息HSDepartment的数据库操作接口
    /// <summary>
    /// 带上级部门和申请权限的部门信息HSDepartment的数据库操作接口
    /// </summary>
    public class DALHSDepartment : DALSDepartment
    {


        #region 查询带上级部门信息信息的部门的信息项目信息        /// <summary>
        /// 查询部门信息的信息串
        /// </summary>
        private const String QUERY_HSDEPARTMENT_STR = @"SELECT id, name, shortcall, canboundary, caninland, canemergency, canregular, highdepid FROM `department` ORDER BY id";

        public static List<HSDepartment> QueryHSDepartment()
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<HSDepartment> departments = new List<HSDepartment>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_HSDEPARTMENT_STR;

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    HSDepartment department = new HSDepartment();

                    ///  部门基本信息
                    department.Id = int.Parse(sqlRead["id"].ToString());
                    department.Name = sqlRead["name"].ToString();
                    department.ShortCall = sqlRead["shortcall"].ToString();

                    ///  申请权限信息
                    department.CanBoundary = int.Parse(sqlRead["canboundary"].ToString());
                    department.CanInland = int.Parse(sqlRead["caninland"].ToString());
                    department.CanEmergency = int.Parse(sqlRead["canemergency"].ToString());
                    department.CanRegular = int.Parse(sqlRead["canregular"].ToString());

                    ///  上级部门信息
                    department.HigherDepartmentId = int.Parse(sqlRead["highdepid"].ToString());

                    departments.Add(department);
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
            return departments;
        }
        #endregion


        #region 获取编号为departmentId的部门信息
        private const String GET_HSDEPARTMENT_BY_ID_STR = @"SELECT id, name, shortcall, canboundary, caninland, canemergency, canregular, highdepid FROM `department` WHERE (id = @departmentId)";
        /// <summary>
        /// 获取编号为departmentId的部门信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public HSDepartment GetHSDepartment(int departmentId)
        { 
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            HSDepartment department = new HSDepartment();
            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_HSDEPARTMENT_STR;
                cmd.Parameters.AddWithValue("@departmentId", departmentId);

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    

                    ///  部门基本信息
                    department.Id = int.Parse(sqlRead["id"].ToString());
                    department.Name = sqlRead["name"].ToString();
                    department.ShortCall = sqlRead["shortcall"].ToString();

                    ///  申请权限信息
                    department.CanBoundary = int.Parse(sqlRead["canboundary"].ToString());
                    department.CanInland = int.Parse(sqlRead["caninland"].ToString());
                    department.CanEmergency = int.Parse(sqlRead["canemergency"].ToString());
                    department.CanRegular = int.Parse(sqlRead["canregular"].ToString());

                    ///  上级部门信息
                    department.HigherDepartmentId = int.Parse(sqlRead["highdepid"].ToString());

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
            return department;

        }
        #endregion

               
        #region  查询当前一级部门的下级部门信息(不包含此一级部门)
        private static String QUERY_SUB_HSDEPARTMENT_STR = @"SELECT id, name, shortcall, canboundary, caninland, canemergency, canregular, highdepid FROM `department` WHERE (highdepid = @HigherDepartmentId) AND (highdepid != id)";
        /// <summary>
        /// 查询当前部门的二级部门信息
        /// </summary>
        /// <param name="higherDepartmentId"></param>
        /// <returns></returns>
        public static List<HSDepartment> QuerySubHSDepartment(int higherDepartmentId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            List<HSDepartment> departments = new List<HSDepartment>();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = QUERY_SUB_HSDEPARTMENT_STR;
                cmd.Parameters.AddWithValue("HigherDepartmentId", higherDepartmentId);

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {
                    HSDepartment department = new HSDepartment();

                    ///  部门基本信息
                    department.Id = int.Parse(sqlRead["id"].ToString());
                    department.Name = sqlRead["name"].ToString();
                    department.ShortCall = sqlRead["shortcall"].ToString();

                    ///  申请权限信息
                    department.CanBoundary = int.Parse(sqlRead["canboundary"].ToString());
                    department.CanInland = int.Parse(sqlRead["caninland"].ToString());
                    department.CanEmergency = int.Parse(sqlRead["canemergency"].ToString());
                    department.CanRegular = int.Parse(sqlRead["canregular"].ToString());

                    ///  上级部门信息
                    department.HigherDepartmentId = int.Parse(sqlRead["highdepid"].ToString());


                    departments.Add(department);
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
            return departments;
        }
        #endregion




    }

    #endregion



}