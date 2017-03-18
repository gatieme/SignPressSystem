﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SignPressServer.SignTools;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using SignPressServer.SignData;

namespace SignPressServer.SignDAL
{
	/*
	 * 航道局基本会签单的模版信息
	 */
	public class DALHDJContract
	{
		/// <summary>
		/// 添加航道局会签单的信息串
		/// </summary>
		private const String INSERT_HDJCONTRACT_STR = @"INSERT INTO `hdjcontract` (`id`, `name`, `contempid`, `subempid`, `submitdate`, `columndata1`, `columndata2`, `columndata3`, `columndata4`, `columndata5`) 
														VALUES (@Id, @Name, @ConTempId, @SubEmpId, @SubmitDate, @ColumnData_1, @ColumnData_2, @ColumnData_3, @ColumnData_4, @ColumnData_5)";

		/// <summary>
		/// 删除航道局会签单的信息串
		/// </summary>
		private const String DELETE_HDJCONTRACT_ID_STR = @"DELETE FROM `hdjcontract` WHERE (`id` = @Id)";

		/// <summary>
		/// 修改航道局会签单的信息串
		/// </summary>
		private const String MODIFY_HDJCONTRACT_STR = @"UPDATE `hdjcontract`
														SET `submitdate` = @SubmitDate,
															`columndata1` = @ColumnData_1, `columndata2` = @ColumnData_2, `columndata3` = @ColumnData_3, `columndata4` = @ColumnData_4, `columndata5` = @ColumnData_5        
														WHERE (`id` = @Id)";

		/// <summary>
		/// 查询航道局会签单的信息串
		/// </summary>
		private const String QUERY_HDJCONTRACT_STR = @"SELECT `id`, `name`, `subempid`, `subempname`, `submitdate` FROM `hdjcontract`";


		private const String GET_HDJCONTRACT_STR = @"SELECT h.id id, h.name name, c.id contempid, c.name contempname, 
c.column1 columnname1, c.column2 columnname2, c.column3 columnname3, c.column4 columnname4, c.column5 columnname5,
h.columndata1 columndata1, h.columndata2 columndata2, h.columndata3 columndata3, h.columndata4 columndata4, h.columndata5 columndata5, 
c.signinfo1 signinfo1, c.signinfo2 signinfo2, c.signinfo3 signinfo3, c.signinfo4 signinfo4, c.signinfo5 signinfo5, c.signinfo5 signinfo5, c.signinfo6 signinfo6, c.signinfo7 signinfo7, c.signinfo8 signinfo8,                                                                  
e1.id signid1, e2.id signid2, e3.id signid3, e4.id signid4, e5.id signid5, e6.id signid6, e7.id signid7, e8.id signid8,
e1.name signname1, e2.name signname2, e3.name signname3, e4.name signname4, e5.name signname5, e6.name signname6, e7.name signname7, e8.name signname8,          
d1.id departmentid1, d2.id departmentid2, d3.id departmentid3, d4.id departmentid4, d5.id departmentid5, d6.id departmentid6, d7.id departmentid7, d8.id departmentid8,
d1.name departmentname1, d2.name departmentname2, d3.name departmentname3, d4.name departmentname4, d5.name departmentname5, d6.name departmentname6, d7.name departmentname7, d8.name departmentname8,
signlevel1 signlevel1, c.signlevel2, c.signlevel2, c.signlevel3, signlevel3, c.signlevel4 signlevel4, c.signlevel5 signlevel5, c.signlevel6 signlevel6, c.signlevel7 signlevel7, c.signlevel8 signlevel8,
s.result1 result1, s.result2 result2, s.result3 result3, s.result4 result4, s.result5 result5, s.result6 result6, s.result7 result7, s.result8 result8
FROM hdjcontract h, contemp c, signaturestatus s,
employee e1, employee e2, employee e3, employee e4, employee e5, employee e6, employee e7, employee e8,
department d1, department d2, department d3, department d4, department d5, department d6, department d7, department d8
WHERE (h.id = @Id and h.contempid = c.id  
and c.signid1 = e1.id  and c.signid2 = e2.id and c.signid3 = e3.id and c.signid4 = e4.id and c.signid5 = e5.id and c.signid6 = e6.id and c.signid7 = e7.id and c.signid8 = e8.id
and d1.id = e1.departmentid and d2.id = e2.departmentid and d3.id = e3.departmentid and d4.id = e4.departmentid and d5.id = e5.departmentid and d6.id = e6.departmentid and d7.id = e7.departmentid and d8.id = e8.departmentid
and h.id = s.conid);";

		#region 判断当前会签单是否存在
		private const string IS_HDJCONTRACT_EXIST_STR = @"SELECT Count(id) count FROM `hdjcontract` WHERE (id = @Id)";

		public static bool IsHDJContractExist(string contractId)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = -1;

			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = IS_HDJCONTRACT_EXIST_STR;
				cmd.Parameters.AddWithValue("@Id", contractId);

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{
					count = int.Parse(sqlRead["count"].ToString());
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
			if (count == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

        #region  插入会签单
		public static bool InsertHDJContract(HDJContract contract)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;
			
			int count = -1;         // 受影响行数

			// 当天的会签单的数目
			int currDayConCount = DALHDJContract.GetDayHDJContractCount(System.DateTime.Now.Date);
			
			try
			{
				con.Open();

				cmd = con.CreateCommand();
				cmd.CommandText = INSERT_HDJCONTRACT_STR;


				/// 修改编号的设置
				/// 目前编号设置[前缀串 年4位 + 月2位 + 日2位 + 编号6位]
				//////////////////////////////////////////////////////////////////////
				// modify by gatieme 2015-07-10 14Label2
				///  修改ID为手动填写
				if (contract.Id == "")
				{
					contract.Id += System.DateTime.Now.ToString("yyyyMMdd") + (currDayConCount + 1).ToString().PadLeft(6, '0');
				}
				//////////////////////////////////////////////////////////////////////

				cmd.Parameters.AddWithValue("@Id", contract.Id);
				cmd.Parameters.AddWithValue("@Name", contract.Name);
				cmd.Parameters.AddWithValue("@ConTempId", contract.ConTemp.TempId);
				cmd.Parameters.AddWithValue("@SubEmpId", contract.SubmitEmployee.Id);
				cmd.Parameters.AddWithValue("@SubmitDate", System.DateTime.Now);
				
				///  5个栏目信息
				for (int cnt = 0; cnt < 5; cnt++)
				{
					String strColumn = "@ColumnData_" + (cnt + 1).ToString();
					cmd.Parameters.AddWithValue(strColumn, contract.ColumnDatas[cnt]);
				}



				count = cmd.ExecuteNonQuery();
				cmd.Dispose();

				con.Close();
				con.Dispose();
				if (count == 1)     //  插入成功后的受影响行数为1
				{
					Console.WriteLine("插入会签单成功");
					///////////////////////////////////////////////////////////
					//  此处应该判断如果提交人和申请人的第一个人的同一个个人的话，直接同意
					//  但是暂时不予考虑，应该自己的确需要签字审核
					///////////////////////////////////////////////////////////
					return true;
				}
				else
				{
					Console.WriteLine("插入会签单失败");
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


		#region 删除会签单模版信息
		/// <summary>
		/// 删除编号为conTempId的会签单模版信息
		/// </summary>
		/// <param name="conTempId"></param>
		/// <returns></returns>
		public static bool DeleteHDJContact(String contractId)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = -1;
			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = DELETE_HDJCONTRACT_ID_STR;
                cmd.Parameters.AddWithValue("@Id", contractId);                        // 会签单模版姓名


				count = cmd.ExecuteNonQuery();
				cmd.Dispose();

				con.Close();
				con.Dispose();

				if (count == 1)
				{
                    Console.WriteLine("删除会签单" + contractId.ToString() + "成功");

					return true;
				}
				else
				{
                    Console.WriteLine("删除会签单" + contractId.ToString() + "失败");

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


		#region 修改会签单模版的信息
		public static bool ModifyHDJContract(HDJContract contract)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = -1;
			try
			{
				con.Open();

				cmd = con.CreateCommand();
				cmd.CommandText = MODIFY_HDJCONTRACT_STR;

				cmd.Parameters.AddWithValue("@Id", contract.Id);
				//cmd.Parameters.AddWithValue("@Name", contract.Name);
				//cmd.Parameters.AddWithValue("@TempId", contract.ConTemp.TempId);
				//cmd.Parameters.AddWithValue("@SubEmpId", contract.SubmitEmployee.Id);
				cmd.Parameters.AddWithValue("@SubmitDate", System.DateTime.Now);
				///  5个栏目信息
				for (int cnt = 0; cnt < 5; cnt++)
				{
					String strColumn = "@ColumnData_" + (cnt + 1).ToString();
					cmd.Parameters.AddWithValue(strColumn, contract.ColumnDatas[cnt]);
				}

				count = cmd.ExecuteNonQuery();
				cmd.Dispose();

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
			if (count == 1)
			{
				Console.WriteLine("修改会签单信息" + contract.Id.ToString() + "成功");

				return true;
			}
			else
			{
				Console.WriteLine("修改会签单信息" + contract.Id.ToString() + "失败");

				return false;
			}
		}
		#endregion


		#region 查询会签单的信息
		public static List<HDJContract> QueryHDJContract()
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			List<HDJContract> contracts = new List<HDJContract>();

			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = QUERY_HDJCONTRACT_STR;


				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{
					HDJContract contract = new HDJContract();


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
			return contracts;
		}
		#endregion


		#region 查询会签单的信息
		public static HDJContract GetHDJContract(String contractId)
		{
			return GetHDJContactRefuse(contractId);
		}
		#endregion



		#region 获取到会签单更加详细的信息(特使用在查询被拒绝的单子)

		private const String GET_HDJCONTRACT_REFUSE_STR = @"SELECT h.id id, h.name name, c.id contempid, c.name contempname, 
c.column1 columnname1, c.column2 columnname2, c.column3 columnname3, c.column4 columnname4, c.column5 columnname5,
h.columndata1 columndata1, h.columndata2 columndata2, h.columndata3 columndata3, h.columndata4 columndata4, h.columndata5 columndata5, 
c.signinfo1 signinfo1, c.signinfo2 signinfo2, c.signinfo3 signinfo3, c.signinfo4 signinfo4, c.signinfo5 signinfo5, c.signinfo5 signinfo5, c.signinfo6 signinfo6, c.signinfo7 signinfo7, c.signinfo8 signinfo8,                                                                  
e1.id signid1, e2.id signid2, e3.id signid3, e4.id signid4, e5.id signid5, e6.id signid6, e7.id signid7, e8.id signid8,
e1.name signname1, e2.name signname2, e3.name signname3, e4.name signname4, e5.name signname5, e6.name signname6, e7.name signname7, e8.name signname8,          
d1.id departmentid1, d2.id departmentid2, d3.id departmentid3, d4.id departmentid4, d5.id departmentid5, d6.id departmentid6, d7.id departmentid7, d8.id departmentid8,
d1.name departmentname1, d2.name departmentname2, d3.name departmentname3, d4.name departmentname4, d5.name departmentname5, d6.name departmentname6, d7.name departmentname7, d8.name departmentname8,
c.signlevel1 signlevel1, c.signlevel2, c.signlevel2, c.signlevel3, signlevel3, c.signlevel4 signlevel4, c.signlevel5 signlevel5, c.signlevel6 signlevel6, c.signlevel7 signlevel7, c.signlevel8 signlevel8,
c.canview1 canview1, c.canview2 canview2, c.canview3 canview3, c.canview4 canview4, c.canview5 canview5, c.canview6 canview6, c.canview7 canview7, c.canview8 canview8,
c.candownload1 candownload1, c.candownload2 candownload2, c.candownload3 candownload3, c.candownload4 candownload4, c.candownload5 candownload5, c.candownload6 candownload6, c.candownload7 candownload7, c.candownload8 candownload8,
s.result1 result1, s.result2 result2, s.result3 result3, s.result4 result4, s.result5 result5, s.result6 result6, s.result7 result7, s.result8 result8,

(SELECT sd.remark remark1
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '1' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark1,

(SELECT sd.remark remark2
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '2' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark2,

  (SELECT sd.remark remark3
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '3' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark3,
  (SELECT sd.remark remark4
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '4' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark4,
  (SELECT sd.remark remark5
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '5' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark5,
  (SELECT sd.remark remark6
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '6' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark6,

  (SELECT sd.remark remark7
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '7' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark7,
	
  (SELECT sd.remark remark8
FROM signaturedetail sd, hdjcontract hc, signaturelevel sl, signaturestatus st
WHERE hc.id = @Id and hc.id = sd.conid and st.conid = hc.id
  and sl.contempid = hc.contempid and sl.signnum = '8' and sd.empid = sl.empid
  and sd.updatecount = st.updatecount) remark8

FROM 

hdjcontract h, 
contemp c, 
signaturestatus s,
employee e1, employee e2, employee e3, employee e4, employee e5, employee e6, employee e7, employee e8,
department d1, department d2, department d3, department d4, department d5, department d6, department d7, department d8

WHERE (h.id = @Id and h.contempid = c.id
and c.signid1 = e1.id  and c.signid2 = e2.id and c.signid3 = e3.id and c.signid4 = e4.id and c.signid5 = e5.id and c.signid6 = e6.id and c.signid7 = e7.id and c.signid8 = e8.id
and d1.id = e1.departmentid and d2.id = e2.departmentid and d3.id = e3.departmentid and d4.id = e4.departmentid and d5.id = e5.departmentid and d6.id = e6.departmentid and d7.id = e7.departmentid and d8.id = e8.departmentid
and h.id = s.conid);
";

		//  keyi
		//public static HDJContract ViewHDJContract(String contractId)
		//{
		//    DALHDJContract.GetHDJContactRefuse(contractId);
		//}
		
		public static HDJContract GetHDJContactRefuse(String contractId)
		{ 
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			HDJContract contract = new HDJContract();

			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = GET_HDJCONTRACT_REFUSE_STR;
				cmd.Parameters.AddWithValue("@Id", contractId);

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{

					contract.Id = sqlRead["id"].ToString();
					contract.Name = sqlRead["name"].ToString();
					ContractTemplate conTemp = new ContractTemplate();
					conTemp.TempId = int.Parse(sqlRead["contempid"].ToString());
					conTemp.Name = sqlRead["contempname"].ToString();
					// 5个栏目信息
					// conTemp.ColumnCount = 5;
					List<String> columnnames = new List<String>();
					List<String> columndatas = new List<String>();
					for (int cnt = 1; cnt <= 5; cnt++)
					{
						String strColumnname = "columnname" + cnt.ToString();
						String strColumnData = "columndata" + cnt.ToString();
						
						columnnames.Add(sqlRead[strColumnname].ToString());
						columndatas.Add(sqlRead[strColumnData].ToString());
					}
					conTemp.ColumnNames = columnnames;
					contract.ColumnDatas = columndatas;

					// 8个签字人信息
					// conTemp.SignCount = 8;
					List<SignatureTemplate> signatures = new List<SignatureTemplate>();
					List<int> signResults = new List<int>();
					List<String > signRemarks = new List<String>();
					for (int cnt = 1; cnt <= 8; cnt++)
					{
						String strSignInfo = "signinfo" + cnt.ToString();
						String strSignId = "signid" + cnt.ToString();
						String strSignName = "signname" + cnt.ToString();
						String strDepartmentId = "departmentid" + cnt.ToString();
						String strDepartmentName = "departmentname" + cnt.ToString();
						String strSignLevel = "signlevel" + cnt.ToString();
						String strCanView = "canview" + cnt.ToString();
						String strCanDownload = "candownload" + cnt.ToString();
						String strSignResult = "result" + cnt.ToString();
						String strSignRemark = "remark" + cnt.ToString();


						SignatureTemplate signDatas = new SignatureTemplate();
						signDatas.SignInfo = sqlRead[strSignInfo].ToString();
						signDatas.SignLevel = int.Parse(sqlRead[strSignLevel].ToString());
						signDatas.CanView = int.Parse(sqlRead[strCanView].ToString());
						signDatas.CanDownload = int.Parse(sqlRead[strCanDownload].ToString());

						Employee employee = new Employee();
						employee.Id = int.Parse(sqlRead[strSignId].ToString());
						employee.Name = sqlRead[strSignName].ToString();
						Department department = new Department();
						department.Id = int.Parse(sqlRead[strDepartmentId].ToString());
						department.Name = sqlRead[strDepartmentName].ToString();
						employee.Department = department;
						signDatas.SignEmployee = employee;
						
						
						
						// 8个人的签字结果
						signResults.Add(int.Parse(sqlRead[strSignResult].ToString()));
						signRemarks.Add(sqlRead[strSignRemark].ToString());

						signatures.Add(signDatas);
					}
					conTemp.SignDatas = signatures;
					contract.ConTemp = conTemp;

					contract.SignResults = signResults;
					contract.SignRemarks = signRemarks;

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
			return contract;
		}
		#endregion


		#region 获取已经完成签字的会签单的信息
		private const String GET_HDJCONTRACT_AGREE_STR = @"SELECT h.id id, h.name name, c.id contempid, c.name contempname, 
c.column1 columnname1, c.column2 columnname2, c.column3 columnname3, c.column4 columnname4, c.column5 columnname5,
h.columndata1 columndata1, h.columndata2 columndata2, h.columndata3 columndata3, h.columndata4 columndata4, h.columndata5 columndata5, 
c.signinfo1 signinfo1, c.signinfo2 signinfo2, c.signinfo3 signinfo3, c.signinfo4 signinfo4, c.signinfo5 signinfo5, c.signinfo5 signinfo5, c.signinfo6 signinfo6, c.signinfo7 signinfo7, c.signinfo8 signinfo8,                                                                  
e1.id signid1, e2.id signid2, e3.id signid3, e4.id signid4, e5.id signid5, e6.id signid6, e7.id signid7, e8.id signid8,
e1.name signname1, e2.name signname2, e3.name signname3, e4.name signname4, e5.name signname5, e6.name signname6, e7.name signname7, e8.name signname8,          
d1.id departmentid1, d2.id departmentid2, d3.id departmentid3, d4.id departmentid4, d5.id departmentid5, d6.id departmentid6, d7.id departmentid7, d8.id departmentid8,
d1.name departmentname1, d2.name departmentname2, d3.name departmentname3, d4.name departmentname4, d5.name departmentname5, d6.name departmentname6, d7.name departmentname7, d8.name departmentname8,
signlevel1 signlevel1, c.signlevel2, c.signlevel2, c.signlevel3, signlevel3, c.signlevel4 signlevel4, c.signlevel5 signlevel5, c.signlevel6 signlevel6, c.signlevel7 signlevel7, c.signlevel8 signlevel8,
s.result1 result1, s.result2 result2, s.result3 result3, s.result4 result4, s.result5 result5, s.result6 result6, s.result7 result7, s.result8 result8,
s1.remark remark1, s2.remark remark2, s3.remark remark3, s4.remark remark4, s5.remark remark5, s6.remark remark6, s7.remark remark7, s8.remark remark8
FROM 

hdjcontract h, 
contemp c, 
signaturestatus s,
employee e1, employee e2, employee e3, employee e4, employee e5, employee e6, employee e7, employee e8,
department d1, department d2, department d3, department d4, department d5, department d6, department d7, department d8,
signaturedetail s1, signaturedetail s2, signaturedetail s3, signaturedetail s4,
signaturedetail s5, signaturedetail s6, signaturedetail s7, signaturedetail s8

WHERE (h.id = @Id and h.contempid = c.id and h.id = s.conid
and c.signid1 = e1.id  and c.signid2 = e2.id and c.signid3 = e3.id and c.signid4 = e4.id and c.signid5 = e5.id and c.signid6 = e6.id and c.signid7 = e7.id and c.signid8 = e8.id
and d1.id = e1.departmentid and d2.id = e2.departmentid and d3.id = e3.departmentid and d4.id = e4.departmentid and d5.id = e5.departmentid and d6.id = e6.departmentid and d7.id = e7.departmentid and d8.id = e8.departmentid
 and s1.empid = e1.id and s1.conid = h.id and s1.updatecount = s.updatecount
 and s2.empid = e2.id and s2.conid = h.id and s2.updatecount = s.updatecount
 and s3.empid = e3.id and s3.conid = h.id and s3.updatecount = s.updatecount
 and s4.empid = e4.id and s4.conid = h.id and s4.updatecount = s.updatecount
 and s5.empid = e5.id and s5.conid = h.id and s5.updatecount = s.updatecount
 and s6.empid = e6.id and s6.conid = h.id and s6.updatecount = s.updatecount
 and s7.empid = e7.id and s7.conid = h.id and s7.updatecount = s.updatecount
 and s8.empid = e8.id and s8.conid = h.id and s8.updatecount = s.updatecount)";
		public static HDJContract GetHDJContactAgree(String contractId)
		{ 
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			HDJContract contract = new HDJContract();

			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = GET_HDJCONTRACT_AGREE_STR;
				cmd.Parameters.AddWithValue("@Id", contractId);

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{

					contract.Id = sqlRead["id"].ToString();
					contract.Name = sqlRead["name"].ToString();
					ContractTemplate conTemp = new ContractTemplate();
					conTemp.TempId = int.Parse(sqlRead["contempid"].ToString());
					conTemp.Name = sqlRead["contempname"].ToString();
					// 5个栏目信息
					// conTemp.ColumnCount = 5;
					List<String> columnnames = new List<String>();
					List<String> columndatas = new List<String>();
					for (int cnt = 1; cnt <= 5; cnt++)
					{
						String strColumnname = "columnname" + cnt.ToString();
						String strColumnData = "columndata" + cnt.ToString();
						columnnames.Add(sqlRead[strColumnname].ToString());
						columndatas.Add(sqlRead[strColumnData].ToString());
					}
					conTemp.ColumnNames = columnnames;
					contract.ColumnDatas = columndatas;

					// 8个签字人信息
					// conTemp.SignCount = 8;
					List<SignatureTemplate> signatures = new List<SignatureTemplate>();
					List<int> signResults = new List<int>();
					List<String > signRemarks = new List<String>();
					for (int cnt = 1; cnt <= 8; cnt++)
					{
						String strSignInfo = "signinfo" + cnt.ToString();
						String strSignId = "signid" + cnt.ToString();
						String strSignName = "signname" + cnt.ToString();
						String strDepartmentId = "departmentid" + cnt.ToString();
						String strDepartmentName = "departmentname" + cnt.ToString();
						String strSignLevel = "signlevel" + cnt.ToString();
						String strSignResult = "result" + cnt.ToString();
						String strSignRemark = "remark" + cnt.ToString();


						SignatureTemplate signDatas = new SignatureTemplate();
						signDatas.SignInfo = sqlRead[strSignInfo].ToString();
						signDatas.SignLevel = int.Parse(sqlRead[strSignLevel].ToString());
						Employee employee = new Employee();
						employee.Id = int.Parse(sqlRead[strSignId].ToString());
						employee.Name = sqlRead[strSignName].ToString();
						Department department = new Department();
						department.Id = int.Parse(sqlRead[strDepartmentId].ToString());
						department.Name = sqlRead[strDepartmentName].ToString();
						employee.Department = department;
						signDatas.SignEmployee = employee;
						
						
						
						// 8个人的签字结果
						signResults.Add(int.Parse(sqlRead[strSignResult].ToString()));
					   // Console.WriteLine(1111);
						signRemarks.Add(sqlRead[strSignRemark].ToString());

						signatures.Add(signDatas);
					}
					conTemp.SignDatas = signatures;
					contract.ConTemp = conTemp;

					contract.SignResults = signResults;
					contract.SignRemarks = signRemarks;

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
			return contract;
		}
		#endregion

        #region 获取已经完成签字的会签单的信息
        private const String GET_HDJCONTRACT_OFFLINE_STR = @"SELECT h.id id, h.name name, c.id contempid, c.name contempname, 
c.column1 columnname1, c.column2 columnname2, c.column3 columnname3, c.column4 columnname4, c.column5 columnname5,
h.columndata1 columndata1, h.columndata2 columndata2, h.columndata3 columndata3, h.columndata4 columndata4, h.columndata5 columndata5, 
c.signinfo1 signinfo1, c.signinfo2 signinfo2, c.signinfo3 signinfo3, c.signinfo4 signinfo4, c.signinfo5 signinfo5, c.signinfo5 signinfo5, c.signinfo6 signinfo6, c.signinfo7 signinfo7, c.signinfo8 signinfo8,                                                                  
e1.id signid1, e2.id signid2, e3.id signid3, e4.id signid4, e5.id signid5, e6.id signid6, e7.id signid7, e8.id signid8,
e1.name signname1, e2.name signname2, e3.name signname3, e4.name signname4, e5.name signname5, e6.name signname6, e7.name signname7, e8.name signname8,          
d1.id departmentid1, d2.id departmentid2, d3.id departmentid3, d4.id departmentid4, d5.id departmentid5, d6.id departmentid6, d7.id departmentid7, d8.id departmentid8,
d1.name departmentname1, d2.name departmentname2, d3.name departmentname3, d4.name departmentname4, d5.name departmentname5, d6.name departmentname6, d7.name departmentname7, d8.name departmentname8,
signlevel1 signlevel1, c.signlevel2, c.signlevel2, c.signlevel3, signlevel3, c.signlevel4 signlevel4, c.signlevel5 signlevel5, c.signlevel6 signlevel6, c.signlevel7 signlevel7, c.signlevel8 signlevel8

FROM 

hdjcontract h, 
contemp c, 
signaturestatus s,
employee e1, employee e2, employee e3, employee e4, employee e5, employee e6, employee e7, employee e8,
department d1, department d2, department d3, department d4, department d5, department d6, department d7, department d8


WHERE (h.id = @Id and h.contempid = c.id and h.id = s.conid
and c.signid1 = e1.id  and c.signid2 = e2.id and c.signid3 = e3.id and c.signid4 = e4.id and c.signid5 = e5.id and c.signid6 = e6.id and c.signid7 = e7.id and c.signid8 = e8.id
and d1.id = e1.departmentid and d2.id = e2.departmentid and d3.id = e3.departmentid and d4.id = e4.departmentid and d5.id = e5.departmentid and d6.id = e6.departmentid and d7.id = e7.departmentid and d8.id = e8.departmentid)";
        public static HDJContract GetHDJContactOffline(String contractId)
        {
            MySqlConnection con = DBTools.GetMySqlConnection();
            MySqlCommand cmd;

            HDJContract contract = new HDJContract();

            try
            {
                con.Open();

                cmd = con.CreateCommand();

                cmd.CommandText = GET_HDJCONTRACT_OFFLINE_STR;
                cmd.Parameters.AddWithValue("@Id", contractId);

                MySqlDataReader sqlRead = cmd.ExecuteReader();
                cmd.Dispose();

                while (sqlRead.Read())
                {

                    contract.Id = sqlRead["id"].ToString();
                    contract.Name = sqlRead["name"].ToString();
                    ContractTemplate conTemp = new ContractTemplate();
                    conTemp.TempId = int.Parse(sqlRead["contempid"].ToString());
                    conTemp.Name = sqlRead["contempname"].ToString();
                    // 5个栏目信息
                    // conTemp.ColumnCount = 5;
                    List<String> columnnames = new List<String>();
                    List<String> columndatas = new List<String>();
                    for (int cnt = 1; cnt <= 5; cnt++)
                    {
                        String strColumnname = "columnname" + cnt.ToString();
                        String strColumnData = "columndata" + cnt.ToString();
                        columnnames.Add(sqlRead[strColumnname].ToString());
                        columndatas.Add(sqlRead[strColumnData].ToString());
                    }
                    conTemp.ColumnNames = columnnames;
                    contract.ColumnDatas = columndatas;

                    // 8个签字人信息
                    // conTemp.SignCount = 8;
                    List<SignatureTemplate> signatures = new List<SignatureTemplate>();
                    List<int> signResults = new List<int>();
                    List<String> signRemarks = new List<String>();
                    for (int cnt = 1; cnt <= 8; cnt++)
                    {
                        String strSignInfo = "signinfo" + cnt.ToString();
                        String strSignId = "signid" + cnt.ToString();
                        String strSignName = "signname" + cnt.ToString();
                        String strDepartmentId = "departmentid" + cnt.ToString();
                        String strDepartmentName = "departmentname" + cnt.ToString();
                        String strSignLevel = "signlevel" + cnt.ToString();
                        //String strSignResult = "result" + cnt.ToString();
                        //String strSignRemark = "remark" + cnt.ToString();


                        SignatureTemplate signDatas = new SignatureTemplate();
                        signDatas.SignInfo = sqlRead[strSignInfo].ToString();
                        signDatas.SignLevel = int.Parse(sqlRead[strSignLevel].ToString());
                        Employee employee = new Employee();
                        employee.Id = int.Parse(sqlRead[strSignId].ToString());
                        employee.Name = sqlRead[strSignName].ToString();
                        Department department = new Department();
                        department.Id = int.Parse(sqlRead[strDepartmentId].ToString());
                        department.Name = sqlRead[strDepartmentName].ToString();
                        employee.Department = department;
                        signDatas.SignEmployee = employee;



                        // 8个人的签字结果
                        //signResults.Add(int.Parse(sqlRead[strSignResult].ToString()));
                        // Console.WriteLine(1111);
                        //signRemarks.Add(sqlRead[strSignRemark].ToString());

                        signatures.Add(signDatas);
                    }
                    conTemp.SignDatas = signatures;
                    contract.ConTemp = conTemp;

                    contract.SignResults = signResults;
                    contract.SignRemarks = signRemarks;

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
            return contract;
        }
        #endregion


		#region 获取当天会签单的数目
		public static String GET_DAY_HDJCONTRACT_COUNT_STR = @"SELECT Count(id) dayconcount FROM `hdjcontract` WHERE DATE(submitdate) = @Date";
		/// <summary>
		/// 获取当天会签单的数目
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static int GetDayHDJContractCount(DateTime date)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = 0;

			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = GET_DAY_HDJCONTRACT_COUNT_STR;
				cmd.Parameters.AddWithValue("@Date", date);

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{

					count = int.Parse(sqlRead["dayconcount"].ToString());
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
			return count;
		}
			 
		#endregion


		#region 获取当前员工提交的会签单数目
		private const String GET_EMPLOYEE_SUBMIT_HDJCONTRACT_COUNT_STR = @"SELECT Count(id) count FROM `hdjcontract` WHERE (subempid = @EmployeeId)";
		public static int GetEmployeeSubmitedHDJContractCount(int employeeId)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = 0;

			try
			{
				con.Open();

				cmd = con.CreateCommand();

				cmd.CommandText = GET_EMPLOYEE_SUBMIT_HDJCONTRACT_COUNT_STR;
				cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{

					count = int.Parse(sqlRead["count"].ToString());
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
			return count;
		}
		#endregion




		#region 统计当前类别Category的会签单的数目
		///  编号共5位。
		///  第一位代表是单位名称简称；
		///  第二位代表类别简称；类别最多有四类，即界、内、应和例。具体的含义是为界河（简称为界）航道养护工程、内河（简称为内）航道养护工程、应急抢通（简称为应）工程、例会项目（简称为例）工程。
		///  第三位如果为0，表示该会签审批单是在线（通过我们的系统）审批完成的；否则该位为1，表示离线审批（通过传统方式，没有通过系统，而是领导手工签字完成）；
		///  第四位、第五位表示本年度的分类编号，要求年度内实现自加功能，年初重新初始化为0（可人工、或系统自动完成）。
		///  该系统的使用对象有黑河航道局、佳木斯航道局、哈总段、一中心、二中心、三中心、测绘中心、省航道局八个科室，具体明细如下：
        private static String GET_CATEGORY_YEAR_HDJCONTRACT_COUNT_STR = "SELECT Count(id) count FROM `hdjcontract` WHERE id like (@CategoryYear)";
		//public static int GetCategoryHDJContractCount(ContractCategory category)
        public static int GetCategoryYearHDJContractCount(Search search)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = -1;

			try
			{
				con.Open();

				cmd = con.CreateCommand();

                cmd.CommandText = GET_CATEGORY_YEAR_HDJCONTRACT_COUNT_STR;
                cmd.Parameters.AddWithValue("@CategoryYear", "_" + search.CategoryShortCall + search.Year.ToString() + "%");

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{

					count = int.Parse(sqlRead["count"].ToString());
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
			return count;

		}
		
		#endregion


		#region 统计当前部门申请的会签单的数目
		///  编号共5位。
		///  第一位代表是单位名称简称；
		///  第二位代表类别简称；类别最多有四类，即界、内、应和例。具体的含义是为界河（简称为界）航道养护工程、内河（简称为内）航道养护工程、应急抢通（简称为应）工程、例会项目（简称为例）工程。
		///  第三位如果为0，表示该会签审批单是在线（通过我们的系统）审批完成的；否则该位为1，表示离线审批（通过传统方式，没有通过系统，而是领导手工签字完成）；
		///  第四位、第五位表示本年度的分类编号，要求年度内实现自加功能，年初重新初始化为0（可人工、或系统自动完成）。
		///  该系统的使用对象有黑河航道局、佳木斯航道局、哈总段、一中心、二中心、三中心、测绘中心、省航道局八个科室，具体明细如下：
        private static String GET_SDEPARTMENT_YEAR_HDJCONTRACT_COUNT_STR = "SELECT Count(id) count FROM `hdjcontract` WHERE id like (@SDepartmentYear)";
		//public static int GetSDepartmentHDJContractCount(SDepartment department)
		public static int GetSDepartmentYearHDJContractCount(Search search)

		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = 0;

			try
			{
				con.Open();

				cmd = con.CreateCommand();

                cmd.CommandText = GET_SDEPARTMENT_YEAR_HDJCONTRACT_COUNT_STR;
				cmd.Parameters.AddWithValue("@SDepartmentYear", search.SDepartmentShortlCall + "_" + search.Year.ToString() + "%");

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{

					count = int.Parse(sqlRead["count"].ToString());
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
			return count;

		}

		#endregion


		#region 统计当前部门签署的类别为category的会签单的数目
        //  
        private static String GET_SDEPARTMENT_CATEGORY_YEAR_HDJCONTRACT_COUNT_STR = @"SELECT Count(id) count FROM `hdjcontract` WHERE id like @SDepartmentCategoryYear";
		//
        //private static String GET_SDEPARTMENT_CATEGORY_YEAR_REGULAR_HDJCONTRACT_COUNT_STR = @"S";
        //public static int GetSDepartmentHDJContractCount(SDepartment department)
        public static int GetSDepartmentCategoryYearHDJContractCount(Search search)
		{
			MySqlConnection con = DBTools.GetMySqlConnection();
			MySqlCommand cmd;

			int count = 0;

			try
			{
				con.Open();

				cmd = con.CreateCommand();

                cmd.CommandText = GET_SDEPARTMENT_CATEGORY_YEAR_HDJCONTRACT_COUNT_STR;
				cmd.Parameters.AddWithValue("@SDepartmentCategoryYear", search.SDepartmentShortlCall + search.CategoryShortCall + search.Year.ToString() +"%");

				MySqlDataReader sqlRead = cmd.ExecuteReader();
				cmd.Dispose();

				while (sqlRead.Read())
				{
					count = int.Parse(sqlRead["count"].ToString());
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
			return count;

		}
		#endregion



        #region   通过编号获取出会签单的一些信息
        // 佳内2015001
        //private static int DepartmentIndexFromConId = 1;           //  编号的第1位标识了单子所属的部门
        //private static int CatgoryIndexFromConId = 2;              //  编号的第2位标识了单子的类别
        //private static int IsOnlineIndexFromConId = 3;             //  编号的第3~6位标识了单子是否是被在线签署的
        //private static int NumIndexFromConId = 4;                  //  编号的第7~9位
        public static char GetDepartmentShortCallFromContractId(string contractId)
        {
            return contractId[0];
        }
        public static char GetCatgoryShortCallFromContractId(string contractId)
        {
            return contractId[1];
        }
        public static int GetYearFromContractId(string contractId)
        {
            return int.Parse(contractId.Substring(2, 4));
        }
        public static bool GetIsOnlineFromContractId(string contractId)
        {
            return (contractId[6] == '0');
        }
        public static int GetNumFromContractId(string contractId)
        {
            return int.Parse(contractId.Substring(7, 2));
        }



        #endregion




    }


    public class DALHDJContractWithWorkload : DALHDJContract
    {
        #region 插入会签单信息
        public static bool InsertHDJContractWithWorkload(HDJContractWithWorkload contract)
        {
            //  首先插入会签单的基本信息
            if (DALHDJContractWithWorkload.InsertHDJContract(contract) == false)
            {
                return false;
            }

            //  接着插入会签单的统计
            foreach (ContractWorkload workload in contract.WorkLoads)
            {
                if (DALContractWorkload.InsertWorkload(workload) == false)
                {
                    return false;
                }    
            }

            ///  离线会签单的处理, 更改为在插入会签单的服务器处理逻辑中进行处理
            //if(DALHDJContract.GetIsOnlineFromContractId(contract.Id) == false)
            //{
            //    if (DALHDJContractWithWorkload.DealOfflineHDJContract(contract.Id) == false)
            //    {
            //        return false;
            //    }
            //    /// 生成会签单的信息
            //    MSOfficeThread
            //    /// 生成统计信息 
            //    //  一种方式是这里不实现, 但是让重新生成, 然后才能下载
            //    //  另外一种方式是这里实现, 但是由于需要向MSOfficeThread中添加数据,　因此需要共享MSOfficeThread

            //}
            return true;
        }
        #endregion

        //  修改会签单的基本信息
        public static bool ModifyHDJContractWithWorkload(HDJContractWithWorkload contract)
        {
            if (DALHDJContract.ModifyHDJContract(contract) == false)
            {
                return false;
            }

            //  接着插入会签单的统计
            foreach (ContractWorkload workload in contract.WorkLoads)
            {
                if (DALContractWorkload.ModifyWorkload(workload) == false)
                {
                    return false;
                }
            }
            return true;
        }


        public static bool DeleteHDJContractWithWorkload(string contractId)
        {
            // # 2016-03-09 11:44  版本1.0.2
            //  删除会签单的接口实现
            //  单子被拒绝时，可以删除会签单，从而使编号可以被重用

            //数据库表
            //  hdjcontract中 id作为参数
            //  signaturedetail 存储了编号id的会签单的签字信息  需要一并删除
            //  signaturestatus 存储了编号id的会签单的当前签字状态 需要一并删除
            //  workloa 存储了编号id的会签单对的工作量信息需要一并删除

            //  12:00更新了数据库，在对应的表中增加了外键约束和删除时CASECADE
            //  
            if (DALHDJContract.DeleteHDJContact(contractId) == false)
            {
                return false;
            }
                                           /*
            if (DALContractWorkload.DeleteWorkload(contractId) == false)
            {
                return false;
            }                                */
            return true;
        }


        public static HDJContractWithWorkload GetHDJContractWithWorkload(string contractId)
        {
            HDJContract con = DALHDJContract.GetHDJContactRefuse(contractId);
            HDJContractWithWorkload contract = new HDJContractWithWorkload(con);
            List<ContractWorkload> workloads = DALContractWorkload.QureyContractWorkLoad(contractId);
            contract.WorkLoads = workloads;

            return contract;

        }

        #region  处理离线的会签单(自动生成签字信息和生成会签单)
        private static string SET_TOTAL_RESULT_1_STR = @"UPDATE `signaturestatus` SET `totalresult` = 1 WHERE (`conid` = @Id)";

        //public static bool DealOfflineHDJContract(HDJContractWithWorkload contract)
        public static bool DealOfflineHDJContract(String contractId)
        {
            //if (DALHDJContract.GetIsOnlineFromContractId(contractId) == true)
            //{
            //    return false;
            //}
            ///  方案一
            //  首先获取到会签单的签字列表
            //  自动为所有签字人生成签字信息
            //  
            //  这样的好处是会签单会自动进入签字流程.

            ///  方案二
            ///  直接设置会签单的totalresult = 1;
            MySqlConnection con = DBTools.GetMySqlConnection( );

            MySqlCommand cmd;
            int count = -1;                      // 受影响行数
            try
            {
                con.Open( );

                cmd = con.CreateCommand( );
                cmd.CommandText = SET_TOTAL_RESULT_1_STR;
                //cmd.Parameters.AddWithValue("@Id", System.DateTime.Now.ToString("yyyyMMddHHmmss"));
                cmd.Parameters.AddWithValue("@Id", contractId);
                count = cmd.ExecuteNonQuery( );
                cmd.Dispose( );

                con.Close( );
                con.Dispose();
                if (count == 1)     //  插入成功后的受影响行数为1
                {
                    Console.WriteLine("设置totalresult = 1成功");
                    return true;
                }
                else
                {
                    Console.WriteLine("设置totalresult = 1失败");
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
