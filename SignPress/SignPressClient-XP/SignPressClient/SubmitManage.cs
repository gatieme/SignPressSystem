using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SignPressClient.Model;
using SignPressClient.SignSocket;
using SignPressClient.Tools;

namespace SignPressClient
{
    public partial class SubmitManage : Form
    {
        SignSocketClient _sc;


        private OpaqueCommand cmd = new OpaqueCommand();

        ///  modify by gatieme @ 2016-01-22
        ///  单击了增加进行添加工作量后, 如果还能修改其他的combobox信息
        ///  如果修改了工程类别或者工作类别, 这时候工作量集合却未经修改,
        ///  会导致出错--BUG...
        //private bool m_isAddWorkload;
        //public bool IsAddWorkload
        //{
        //    get { return this.m_isAddWorkload;  }
        //    set { this.m_isAddWorkload = value; }
        //}

        public SubmitManage()
        {
            InitializeComponent();
        }

        public SubmitManage(int SelectedIndex, SignSocketClient sc)
            : this()
        {
            this.Submit.SelectedIndex = SelectedIndex;
            _sc = sc;

            ///  modify by gatieme @ 2016-01-22
            //  初始化, 未添加工作量集合
            //this.m_isAddWorkload = false;
        }

        private void SubmitManage_Load(object sender, EventArgs e)                 //窗体加载事件
        {
            /*
             * combox的SelectedIndexChanged事件,在datasouce指定的时候就被触发了,这时候数据还没有绑定好,自然会报错.
             * 
             * 我认为这是不合理的.SelectedIndexChanged不应该在绑定数据的中间被触发.
             * 
             * 我最后解决办法是设置了一个标志符isLoaded,bool类型,在填充方法完毕后,设为true.允许SelectedIndexChanged被触发.
            */


            /*
             * combox的SelectedIndexChanged事件,在datasouce指定的时候就被触发了,这时候数据还没有绑定好,自然会报错.
             * 
             * 我认为这是不合理的.SelectedIndexChanged不应该在绑定数据的中间被触发.
             * 
             * 我最后解决办法是设置了一个标志符isLoaded,bool类型,在填充方法完毕后,设为true.允许SelectedIndexChanged被触发.
            */
            //this.IdDepartShortCall.SelectedIndex = 1;
            this.BindContractTemplate();
            this.ConTempInfo.Visible = false;

                                               //218.7.0.37
            //  DateTime timer = new DateTime();
            //  int month=DateTime.Now.Month;
            //  int year=DateTime.Now.Year;
            //  初始化年第一天
            //  dateTimePicker1.Text=year+"/1/1";
            //初始化月份第一天
            // dateTimePicker1.Text=year+"/"+month+"/1";
            this.AgreeStartDate.Format = DateTimePickerFormat.Custom;
            this.AgreeStartDate.CustomFormat = "yyyy-MM-dd";

            this.AgreeEndDate.Format = DateTimePickerFormat.Custom;
            this.AgreeEndDate.CustomFormat = "yyyy-MM-dd";



            this.BindPenddingList(false);
            //// 同意列表可能很多，数据量太大，取消自动刷新
            ///  bug 2015-07-06 10:59
            ///  修改为查询已通过的但是未曾下载过的会签单信息
            this.BindAgreeUndownloadList(false);
            //this.BindAgreeList(false);
            this.BindRefuseList(false);                                //加载已拒绝的方案

            //  绑定编号前缀信息
            this.BindIdDepartment(false);
            this.BindCategory(false);
            this.BindProject(false);
            this.BindIdYear();
            this.BindIdFlag();
            //this.BindContractCategoryCount(true);
            this.BindDepartmentYearCategoryCount();

            BindSignRefuseAndAgreeOpera();                     //绑定已拒绝以及已通过方案操作列
            BindRefuseOper();
        }

        /// <summary>
        /// 绑定会前单那编号部门简称信息
        /// </summary>
        /// modify by gatieme at 2015-08-10 09:58
        /// version 
        private void BindIdDepartment(bool isFlush)
        {
            //窗体加载时位datagridview绑定数据源
            if (UserHelper.DepList == null || isFlush == true)
            {
                List<Department> list = _sc.QueryDepartment();
                UserHelper.DepList = list;
            }

            if (UserHelper.DepList != null)
            {
                this.IdDepartShortCall.ValueMember = "Id";
                this.IdDepartShortCall.DisplayMember = "ShortCall";
                this.IdDepartShortCall.DataSource = UserHelper.DepList;

                ///////////////////BUG
                //this.IdDepartShortCall.SelectedIndex = 1;

            }
        }


        private void BindCategory(bool isFlush)
        {
   //         System.NullReferenceException: 未将对象引用设置到对象的实例。
   //在 SignPressClient.SubmitManage.BindCategory(Boolean isFlush) 位置 f:\[O]GitHub\SignPress\SignPressClient\SignPressClient\SubmitManage.cs:行号 141
   //在 SignPressClient.SubmitManage.SubmitManage_Load(Object sender, EventArgs e) 位置 f:\[O]GitHub\SignPress\SignPressClient\SignPressClient\SubmitManage.cs:行号 98
            //窗体加载时位datagridview绑定数据源
            if (UserHelper.ContractCategoryList == null || isFlush == true)
            {
                int departmentId = UserHelper.DepList[this.IdDepartShortCall.SelectedIndex].Id;
                List<ContractCategory> categorys = _sc.QuerySDepartmentContractCategory(departmentId);
                UserHelper.ContractCategoryList = categorys;
            }
            this.IdCategory.ValueMember = "Id";
            this.IdCategory.DisplayMember = "CategoryShortCall";
            this.IdCategory.DataSource = UserHelper.ContractCategoryList;
            this.IdCategory.SelectedIndex = 0;


            this.pName.ValueMember = "Id";
            this.pName.DisplayMember = "Category";
            this.pName.DataSource = UserHelper.ContractCategoryList;
            this.pName.SelectedIndex = 0;

        }

        //private void pName_SelectedIndexChanged(object sender, EventArgs e)           //根据工程名称获取项目名称
        private void BindProject(bool isFlush)
        {
            if (UserHelper.ContractProjectList == null || isFlush == true)
            {
                int categoryId = Convert.ToInt32(this.IdCategory.SelectedValue);

                List<ContractProject> list = new List<ContractProject>();
                list = _sc.QueryContractProject(categoryId);
                UserHelper.ContractProjectList = list;

            }

            this.xmName.DataSource = UserHelper.ContractProjectList;
            this.xmName.ValueMember = "Id";
            this.xmName.DisplayMember = "Project";
            this.xmName.SelectedIndex = 0;
            
            // FOR DEBUG
            //MessageBox.Show("Idcategory" + this.IdCategory.SelectedValue + ", Pname" + this.pName.SelectedValue + ", Xmname " + this.xmName.SelectedValue);

        }


        private void BindIdYear()
        {

            int year = System.DateTime.Now.Year;

            // 默认绑定前一年，当年 和 下一年
            //  也就是默认只能当年会签单，可以补钱前一年和预签下一年的会签单
            ComboboxItem[] item =
            {
               new ComboboxItem((year - 1).ToString(), (year - 1).ToString()),
               new ComboboxItem(year.ToString(), year.ToString()),
               new ComboboxItem((year + 1).ToString(), (year + 1).ToString()),
            };
            this.IdYear.Items.AddRange(item);
            this.IdYear.SelectedIndex = 1;

        }
        private void BindIdFlag()
        {
            this.IdFlag.SelectedIndex = 0;
        }


        private void BindContractCategoryCount(bool isFlush)
        {
            if (UserHelper.ContractProjectList == null || isFlush == true)
            {
                //MessageBox.Show(this.IdDepartShortCall.Text);
                Search search = new Search
                {
                    CategoryShortCall = this.IdDepartShortCall.Text,
                    Year = int.Parse(this.IdYear.Text),
                };

                int count = _sc.GetCategoryYearContractCount(search);
                this.IdNo.Text = count.ToString();

            }

        }
        private void BindDepartmentYearCategoryCount()
        {

            if (this.IdDepartShortCall.SelectedValue == null
            || this.IdCategory.SelectedValue == null
            || (ComboboxItem)this.IdYear.SelectedItem == null
            || (ComboboxItem)this.IdYear.SelectedItem == null)
            {
                //MessageBox.Show("请将编号填写完整!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //MessageBox.Show(this.IdDepartShortCall.Text);
            Search search = new Search
            {
                SDepartmentShortlCall = this.IdDepartShortCall.Text,
                CategoryShortCall = this.IdCategory.Text,
                Year = int.Parse(this.IdYear.Text),
            };

            int count = _sc.GetDepartmentCategoryYearContractCount(search);

            //  modigy by gatieme @2016-02-26 
            //  编号应该是2位，因此最后两位都是编号的信息
            if (count + 1 >= 100)       //  编号超出3位
            {
                if (UserHelper.UserInfo.CanStatistic == 0)
                {
                    MessageBox.Show("系统中编号限制为两位, 当前编号的会签单数目已达上限\n如有需要, 请找管理员\n当然您也可以换个部门审批", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.IdNo.Text = "";
                    return;
                }
            }
            else if (count + 1 < 10)
            {
                this.IdNo.Text = "0" + (count + 1).ToString();
            }
            else
            {
                this.IdNo.Text = (count + 1).ToString();
            }

        }

        private async void BindContractTemplate()
        {
            if (UserHelper.TempList == null)
            {
                //List<Templete> list = null;
                // modify by gatieme @ 2016-03-18
                //while (list == null)
                //{
                    //list = await _sc.QueryContractTemplate();
                //}
                List<Templete> list = await _sc.QueryContractTemplate();

                this.SelecteConTemplate.ValueMember = "TempId";
                this.SelecteConTemplate.DisplayMember = "Name";

                this.SelecteConTemplate.DataSource = list;

                UserHelper.TempList = list;
            }
            else
            {
                this.SelecteConTemplate.ValueMember = "TempId";
                this.SelecteConTemplate.DisplayMember = "Name";

                this.SelecteConTemplate.DataSource = UserHelper.TempList;
            }
        }

        /// <summary>
        /// 绑定审核中的数据信息
        /// </summary>
        /// <param name="isFlush">强制刷新标识
        /// 默认情况下，是直接读取UserHelper中的信息结构，不会强制刷新，但是某些情况下，比如用户刚提交了一个信息的时候，是期望进行强制刷新的
        /// 希望强制刷新的时候，将isFlush置为true</param>
        private async void BindPenddingList(bool isFlush)
        {
            /// 当且仅当缓存数据UserHelpwer为空，或者用户期望强制刷新时，强制进行数据获取
            if (UserHelper.PenddingList == null
            || isFlush == true)
            {

                //List<SHDJContract> penddingList = new List<SHDJContract>();
                List<SHDJContract> penddingList = null;
                //  modify by gatieme @ 2016-03-18
                //while (penddingList == null)
                //{
                penddingList = await _sc.QuerySignPend(UserHelper.UserInfo.Id);
                //}
                this.SignPendding.AutoGenerateColumns = false;
                this.SignPendding.DataSource = penddingList;

                UserHelper.PenddingList = penddingList;
            }
            else
            {
                this.SignPendding.AutoGenerateColumns = false;
                this.SignPendding.DataSource = UserHelper.PenddingList;
            }
        }

        private void BindRefuseList(bool isFlush)
        {
            if (UserHelper.RefuseList == null
             || isFlush == true)
            {
                //List<SHDJContract> hdj3 = new List<SHDJContract>();                   

                List<SHDJContract> refuseList = null;
                //  modify by gatieme @ 2016-03-18

                //while (refuseList == null)
                //{
                    refuseList = _sc.QuerySignRefuse(UserHelper.UserInfo.Id);
                //}
                this.SignRefuse.AutoGenerateColumns = false;
                this.SignRefuse.DataSource = refuseList;

                UserHelper.RefuseList = refuseList;
            }
            else
            {
                this.SignRefuse.AutoGenerateColumns = false;
                this.SignRefuse.DataSource = UserHelper.RefuseList;
            }
        }


        private void BindAgreeUndownloadList(bool isFlush)
        {
            if (UserHelper.AgreeUndownList == null
            || isFlush == true)
            {
                List<SHDJContract> hdj2 = new List<SHDJContract>();                   //加载已通过的方案
                hdj2 = _sc.QuerySignAgreeUndownload(UserHelper.UserInfo.Id);
                this.SignAgree.AutoGenerateColumns = false;
                this.SignAgree.DataSource = hdj2;

                UserHelper.AgreeUndownList = hdj2;
            }
            else
            {
                this.SignAgree.AutoGenerateColumns = false;
                this.SignAgree.DataSource = UserHelper.AgreeUndownList;
            }
        }

        private void BindAgreeList(bool isFlush)
        {
            if (UserHelper.AgreeList == null
             || isFlush == true)
            {
                List<SHDJContract> agreeList = null;                  //加载已通过的方案
                //  modify by gatieme @ 2016-03-18

                //while (agreeList == null)
                //{
                    agreeList = _sc.QuerySignAgree(UserHelper.UserInfo.Id);
                //}


                this.SignAgree.AutoGenerateColumns = false;
                this.SignAgree.DataSource = agreeList;
            }
            else
            {
                this.SignAgree.AutoGenerateColumns = false;
                this.SignAgree.DataSource = UserHelper.AgreeList;
            }
        }

        /// <summary>
        /// 绑定后台操作项，重新提交
        /// </summary>
        private void BindRefuseOper()
        {
            this.SignRefuse.AutoGenerateColumns = false;
            DataGridViewLinkColumn rs = new DataGridViewLinkColumn();
            rs.Text = "重新提交";
            rs.Name = "LinkReSubmit";
            rs.HeaderText = "重新提交";
            rs.UseColumnTextForLinkValue = true;
            this.SignRefuse.Columns.Add(rs);


            // 2015-03-09 11:23  modify by gatieme for 删除已经拒绝的单子, 使编号可以被重用
            this.SignRefuse.AutoGenerateColumns = false;
            DataGridViewLinkColumn del = new DataGridViewLinkColumn();
            del.Text = "删除";
            del.Name = "LinkDelete";
            del.HeaderText = "删除";
            del.UseColumnTextForLinkValue = true;
            this.SignRefuse.Columns.Add(del);
        }

        private void BindSignRefuseAndAgreeOpera()
        {
            this.SignAgree.AutoGenerateColumns = false;
            DataGridViewLinkColumn download = new DataGridViewLinkColumn();
            download.Text = "下载签单附件";
            download.Name = "LinkDownLoad";
            download.HeaderText = "下载签单附件";
            download.Width = 150;
            download.UseColumnTextForLinkValue = true;
            this.SignAgree.Columns.Add(download);
        }

        private void SelecteConTemplate_SelectedIndexChanged(object sender, EventArgs e)         //选择会签单模板
        {
            ///  BUG[2015-07-05 13:50]
            ///  每次点击提交管理的时候，都会调用次函数进行一次会签单模版查询，
            ///  这是个BUG，并且在当一个员工从普通员工提升为管理员后，出现加载数据失败
            /// 
            int TemplateId = -1;

            try
            {
                this.ConTempInfo.Visible = true;

                TemplateId = Convert.ToInt32(this.SelecteConTemplate.SelectedValue.ToString());

                if (UserHelper.SelectedTemp == null
                || UserHelper.SelectedTemp.TempId != TemplateId)
                {
                    Templete temp = _sc.GetContractTemplate(TemplateId);
                    if (temp != null)
                    {
                        UserHelper.SelectedTemp = temp;
                    }
                }
                this.ConName.Text = UserHelper.SelectedTemp.Name.ToString().Replace("模版", null);
                this.Column1.Text = UserHelper.SelectedTemp.ColumnNames[0].ToString();
                this.Column2.Text = UserHelper.SelectedTemp.ColumnNames[1].ToString();
                this.Column3.Text = UserHelper.SelectedTemp.ColumnNames[2].ToString();
                this.Column4.Text = UserHelper.SelectedTemp.ColumnNames[3].ToString();
                this.Column5.Text = UserHelper.SelectedTemp.ColumnNames[4].ToString();

                this.Sign1.Text = UserHelper.SelectedTemp.SignDatas[0].SignInfo.ToString();
                this.Sign2.Text = UserHelper.SelectedTemp.SignDatas[1].SignInfo.ToString();
                this.Sign3.Text = UserHelper.SelectedTemp.SignDatas[2].SignInfo.ToString();
                this.Sign4.Text = UserHelper.SelectedTemp.SignDatas[3].SignInfo.ToString();
                this.Sign5.Text = UserHelper.SelectedTemp.SignDatas[4].SignInfo.ToString();
                this.Sign6.Text = UserHelper.SelectedTemp.SignDatas[5].SignInfo.ToString();
                this.Sign7.Text = UserHelper.SelectedTemp.SignDatas[6].SignInfo.ToString();
                this.Sign8.Text = UserHelper.SelectedTemp.SignDatas[7].SignInfo.ToString();

                this.SignPer1.Text = UserHelper.SelectedTemp.SignDatas[0].SignEmployee.Name.ToString();
                this.SignPer2.Text = UserHelper.SelectedTemp.SignDatas[1].SignEmployee.Name.ToString();
                this.SignPer3.Text = UserHelper.SelectedTemp.SignDatas[2].SignEmployee.Name.ToString();
                this.SignPer4.Text = UserHelper.SelectedTemp.SignDatas[3].SignEmployee.Name.ToString();
                this.SignPer5.Text = UserHelper.SelectedTemp.SignDatas[4].SignEmployee.Name.ToString();
                this.SignPer6.Text = UserHelper.SelectedTemp.SignDatas[5].SignEmployee.Name.ToString();
                this.SignPer7.Text = UserHelper.SelectedTemp.SignDatas[6].SignEmployee.Name.ToString();
                this.SignPer8.Text = UserHelper.SelectedTemp.SignDatas[7].SignEmployee.Name.ToString();
            }
            catch
            {
                MessageBox.Show("加载会签单模版" + TemplateId + "数据失败!");

            }
        }


        private void button1_Click(object sender, EventArgs e)                   //提交会签单信息
        {
            if (this.pName.Text.Trim() != "" && this.xmName.Text.Trim() != ""
            && this.Column4Info.Text.Trim() != "" && this.Column5Info.Text.Trim() != ""
            && this.Column4InfoAmountInWords.Text.Trim() != "" && this.Column5InfoAmountInWords.Text.Trim() != ""
            && this.label3.Text.Trim() != ""
            && this.IdNo.Text.Trim() != "")
            {
                if (MessageBox.Show("您确定要提交所填方案吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    HDJContractWithWorkload hdj = new HDJContractWithWorkload();
                    hdj.Name = this.ConName.Text;
                    hdj.SubmitEmployee = UserHelper.UserInfo;
                    Templete temp = new Templete();
                    temp.TempId = UserHelper.SelectedTemp.TempId;
                    hdj.ConTemp = temp;
                    hdj.Id = this.IdDepartShortCall.Text.ToString() + this.IdCategory.Text.ToString() +
                             this.IdYear.Text.ToString() + this.IdFlag.Text.ToString() + this.IdNo.Text.Trim();



                    string workloadStr = "";
                    List<ContractWorkload> worklist = new List<ContractWorkload>();
                    int num = (this.ProjectPanel.Controls.Count - 1) / 6;

                    //  modify by gatieme @ 2016-01-22 23:48
                    //  修复了无工作量集合也能提交会签单的BG...
                    if (num <= 0)
                    {
                        MessageBox.Show("会签单的工作量集合为空, 无法提交!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    for (int i = 1; i <= num; i++)
                    {


                        ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
                        TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
                        TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];

                        ContractWorkload workload = new ContractWorkload();
                        workload.ContractId = hdj.Id;
                        ContractItem it = new ContractItem();
                        it.ProjectId = Convert.ToInt32(this.xmName.SelectedValue);
                        it.Id = Convert.ToInt32(item.SelectedValue);
                        it.Item = item.Text.ToString();

                        workload.Item = it;
                        if (work.Text.Trim() == "" || expense.Text.Trim() == "")
                        {
                            MessageBox.Show("请将工作量填写完整!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //  判断工作量中是否有重复的数据



                        workload.Work = Convert.ToDouble(work.Text.Trim());
                        workload.Expense = Convert.ToDouble(expense.Text.Trim());
                        workload.Id = hdj.Id + it.Id.ToString();




                        //  modify by gatieme @ 2016-01-22
                        //  修复了工作量可以重复的BUG...   
                        if (worklist.Where(o => o.Item.Id == it.Id).ToList().Count > 0)
                        {
                            MessageBox.Show("工作量有重复的!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        worklist.Add(workload);
                        workloadStr += workload.Item.Item + "   工作量 : " + workload.Work.ToString() + "   投资额 : " + workload.Expense.ToString() + "\r\n";


                    }
                    hdj.WorkLoads = worklist;


                    List<String> list = new List<string>();
                    list.Add(this.pName.Text.ToString());  //  工程名称
                    list.Add(this.xmName.Text.ToString()); //  项目名称
                    list.Add(workloadStr);
                    
                    string currMoney  = this.Column4Info.Text.ToString() + "    " + this.Column4InfoAmountInWords.Text.ToString();
                    string totalMoney = this.Column5Info.Text.ToString() + "    " + this.Column5InfoAmountInWords.Text.ToString();
                    list.Add(currMoney);
                    list.Add(totalMoney);

                    hdj.ColumnDatas = list;


                    string result = _sc.InsertHDJContract(hdj);
                    if (result == Response.INSERT_HDJCONTRACT_SUCCESS.ToString())
                    {
                        this.ConTempInfo.Visible = false;
                        this.pName.Text = "";
                        this.xmName.Text = "";
                        this.Column4Info.Text = "";
                        this.Column5Info.Text = "";
                        this.IdNo.Text = "";

                        for (int i = 1; i <= num; i++)
                        {
                            ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
                            TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
                            Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + i.ToString()];
                            TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];
                            Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + i.ToString()];
                            Button b = (Button)this.ProjectPanel.Controls["Delete_" + i.ToString()];

                            this.ProjectPanel.Controls.Remove(item);
                            this.ProjectPanel.Controls.Remove(work);
                            this.ProjectPanel.Controls.Remove(workdsc);
                            this.ProjectPanel.Controls.Remove(expense);
                            this.ProjectPanel.Controls.Remove(expensedsc);
                            this.ProjectPanel.Controls.Remove(b);

                            item.Dispose();
                            work.Dispose();
                            workdsc.Dispose();
                            expense.Dispose();
                            expensedsc.Dispose();
                            b.Dispose();
                        }

                        this.ProjectPanel.Height = this.ProjectPanel.Height - 34 * num;

                        MessageBox.Show("提交成功!", "提示", MessageBoxButtons.OK);

                        //if(hdj.ConTemp.SignDatas.Where())
                        // 2015-07-03 11:25  提交成功后应该刷新一下待签字结构体
                        if (HDJContract.GetIsOnlineFromContractId(hdj.Id) == false)
                        {
                            MessageBox.Show("您提交了一份离线审批单子, 将直接完成签字，且无法修改!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        this.BindPenddingList(true);        //  强制刷新待审核的数据

                        /////////////////////////////////////////
                        /// 每次提交一个新的单子之后，待审核数目增加 1///
                        /////////////////////////////////////////
                        MainWindow mw = (MainWindow)this.MdiParent;
                        foreach (TreeNode t in mw.treeView1.Nodes)
                        {
                            if (t.Text.Contains("提交管理("))
                            {
                                int count = Convert.ToInt32(t.Text.Split('(')[1].Split(')')[0]);    // 提交管理

                                t.Text = "提交管理(" + (count + 1) + ")";

                                //t.Nodes[0]  -=>  提交方案
                                //t.Nodes[1]  -=>  审核中
                                //t.Nodes[2]  -=>  已拒绝
                                //t.Nodes[3]  -=>  已通过
                                if (t.Nodes[1].Text.Contains("审核中("))
                                {
                                    int childcount = Convert.ToInt32(t.Nodes[1].Text.Split('(')[1].Split(')')[0]);
                                    t.Nodes[1].Text = "审核中(" + (childcount + 1) + ")";
                                }
                            }
                        }
                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == Response.INSERT_HDJCONTRACT_EXIST.ToString())
                    {
                        MessageBox.Show("该会签单编号已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("请将所有空白处填完!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SignPendding_CellContentClick(object sender, DataGridViewCellEventArgs e)           //查看正在审批方案的详细信息
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {

                string Id = this.SignPendding.Rows[e.RowIndex].Cells[0].Value.ToString();
                SignConTemp sct = new SignConTemp(_sc, Id, 1);
                sct.ShowDialog();
            }
        }

        private void SignRefuse_CellContentClick(object sender, DataGridViewCellEventArgs e)              //已拒绝列表操作功能
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {
                string Id = this.SignRefuse.Rows[e.RowIndex].Cells[0].Value.ToString();
                SignConTemp sct = new SignConTemp(_sc, Id, 2);
                sct.ShowDialog();
            }
            else if (e.ColumnIndex == 4)
            {
                string Id = this.SignRefuse.Rows[e.RowIndex].Cells[0].Value.ToString();
                ReSubmitConTemp rsct = new ReSubmitConTemp(_sc, Id);
                rsct.ShowDialog();
                if (rsct.DialogResult == DialogResult.OK)
                {
                    rsct.Close();
                    ////
                    BindRefuseList(true);      // 重新提交后，强制是刷新拒绝列表
                    BindPenddingList(true);

                    MainWindow mw = (MainWindow)this.MdiParent;
                    foreach (TreeNode t in mw.treeView1.Nodes)
                    {
                        if (t.Text.Contains("提交管理("))   //  提交管理列表的数据减1
                        {
                            //t.Nodes[0]  -=>  提交方案
                            //t.Nodes[1]  -=>  审核中
                            //t.Nodes[2]  -=>  已拒绝
                            //t.Nodes[3]  -=>  已通过
                            int count = Convert.ToInt32(t.Text.Split('(')[1].Split(')')[0]);
                            if (count - 1 == 0)
                            {
                                t.Text = "提交管理";
                                t.Nodes[2].Text = "已拒绝";
                            }
                            else
                            {
                                t.Text = "提交管理(" + (count - 1) + ")";

                                if (t.Nodes[2].Text.Contains("已拒绝("))
                                {
                                    int childcount = Convert.ToInt32(t.Nodes[2].Text.Split('(')[1].Split(')')[0]);
                                    if (childcount - 1 == 0)
                                    {
                                        t.Nodes[2].Text = "已拒绝";
                                    }
                                    else
                                    {
                                        t.Nodes[2].Text = "已拒绝(" + (childcount - 1) + ")";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else if (e.ColumnIndex == 5)
            {   // 2015-03-09 11:23  modify by gatieme for 删除已经拒绝的单子, 使编号可以被重用
                if (MessageBox.Show("确定要删除此单子？\n危险操作，请谨慎进行\n会签单是本系统中重要的数据，随意删除可能将引入很多不安全问题，请问您是否继续删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Id = this.SignRefuse.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string result = _sc.DeleteHDJContract(Id);

                    if (result == Response.DELETE_HDJCONTRACT_SUCCESS.ToString())
                    {
                        MessageBox.Show("删除会签单成功!", "提示", MessageBoxButtons.OK);

                        ////
                        BindRefuseList(true);      // 重新提交后，强制是刷新拒绝列表

                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("删除会签单失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
        }

        private async void SignAgree_CellContentClick(object sender, DataGridViewCellEventArgs e)        //已通过列表操作列表
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {
                string Id = this.SignAgree.Rows[e.RowIndex].Cells[0].Value.ToString();
                SignConTemp sct = new SignConTemp(_sc, Id, 3);
                sct.ShowDialog();
            }
            if (e.ColumnIndex == 4)
            {
                string Id = this.SignAgree.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (HDJContract.GetIsOnlineFromContractId(Id) == false)
                {
                    MessageBox.Show(@"该会签单是离线审批的会签单, 无法通过本系统进行下载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show(@"温馨提示
如果您是首次下载附件，系统将为您生成会签单文件，这个过程比较费时间，希望您能耐心等待!
生成过程中我们会在磁盘上为您生成缓存文件，打开缓存可能会导致文件损毁！
为防止用户误操作造成的损毁，我们已经为您做了备份。
如果下载完成后提示您文件损坏无法打开，您只需要点击重新下载即可，系统会立即调用缓存为您重新生成（这个速度是很快的）
由此给您带来的不便，我们表示真诚的歉意", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = this.SignAgree.Rows[e.RowIndex].Cells[0].Value.ToString();
                sfd.Filter = "*.pdf | *.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    MainWindow mw = (MainWindow)this.MdiParent;
                    cmd.ShowOpaqueLayer(this.groupBox2, 125, true, true, "正在下载中，请稍候");
                    mw.treeView1.Enabled = false;

                    string filepath = sfd.FileName.ToString() + ".pdf";
                    await _sc.DownloadHDJContract(Id, filepath);
                    //if(cmd.)
                    //{
                    cmd.HideOpaqueLayer();
                    //}
                    MessageBox.Show("下载完成！");
                    mw.treeView1.Enabled = true;

                    foreach (TreeNode t in mw.treeView1.Nodes)
                    {
                        if (t.Text.Contains("提交管理("))
                        {
                            //t.Nodes[0]  -=>  提交方案
                            //t.Nodes[1]  -=>  审核中
                            //t.Nodes[2]  -=>  已拒绝
                            //t.Nodes[3]  -=>  已通过
                            int count = Convert.ToInt32(t.Text.Split('(')[1].Split(')')[0]);
                            if (count - 1 == 0)
                            {
                                t.Text = "提交管理";
                                t.Nodes[3].Text = "已通过";
                            }
                            else
                            {
                                t.Text = "提交管理(" + (count - 1) + ")";

                                if(t.Nodes[3].Text.Contains("已通过("))
                                {
                                    int childcount = Convert.ToInt32(t.Nodes[3].Text.Split('(')[1].Split(')')[0]);
                                    if (childcount - 1 == 0)
                                    {
                                        t.Nodes[3].Text = "已通过";
                                    }
                                    else
                                    {
                                        t.Nodes[3].Text = "已通过(" + (childcount - 1) + ")";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ContractId = this.ContractID.Text;
            string projectName = this.ProgramName.Text;
            DateTime start = this.AgreeStartDate.Value;
            DateTime end = this.AgreeEndDate.Value;
            if (start > end)
            {
                MessageBox.Show("开始日期必须小于结束日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Search s = new Search();
                s.EmployeeId = UserHelper.UserInfo.Id;
                s.ConId = ContractId;
                s.ProjectName = projectName;
                s.DateBegin = start;
                s.DateEnd = end;

                List<SHDJContract> list = new List<SHDJContract>();
                list = _sc.SearchAgreeHDJConstract(s);

                this.SignAgree.DataSource = list;
            }
        }

        private async void SignPendding_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = await _sc.QuerySignPend(UserHelper.UserInfo.Id);
            this.SignPendding.DataSource = list;
            UserHelper.PenddingList = list;
        }

        private void SignRefuse_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = _sc.QuerySignRefuse(UserHelper.UserInfo.Id);
            this.SignRefuse.DataSource = list;
            UserHelper.RefuseList = list;
        }

        private void SignAgree_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = _sc.QuerySignAgree(UserHelper.UserInfo.Id);
            this.SignAgree.DataSource = list;
            UserHelper.AgreeList = list;
        }

        private void RefreshRefuselist_Click(object sender, EventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            //  modify by gatieme @ 2016-03-18

            //list = null;
            //while (list == null)
            //{
                list = _sc.QuerySignRefuse(UserHelper.UserInfo.Id);
            //}
            this.SignRefuse.DataSource = list;
            UserHelper.RefuseList = list;
        }

        private async void RefreshPendinglist_Click(object sender, EventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            //list = null;
            //while (list == null)
            //{
                list = await _sc.QuerySignPend(UserHelper.UserInfo.Id);
            //}
            this.SignPendding.DataSource = list;
            UserHelper.PenddingList = list;
        }




        private void AddItem_Click(object sender, EventArgs e)                  //添加主要项目和工程量
        {
            int projectid = Convert.ToInt32(this.xmName.SelectedValue);

            List<ContractItem> list = new List<ContractItem>();
            list = _sc.QueryContractItem(projectid);
            UserHelper.ContractItemList = list;

            int num = (this.ProjectPanel.Controls.Count - 1) / 6;
            ComboBox cmb = new ComboBox();
            cmb.Size = new Size(120, 29);
            cmb.Location = new Point(3, 10 + 34 * num);
            cmb.Name = "Item_" + (1 * num + 1).ToString();
            cmb.ValueMember = "Id";
            cmb.DisplayMember = "Item";
            cmb.DataSource = UserHelper.ContractItemList;
            //cmb.SelectedIndexChanged += cmb_SelectedIndexChanged;    //用于绑定工作量和投资额
            this.ProjectPanel.Controls.Add(cmb);
            Label l = new Label();
            l.Size = new Size(70, 29);
            l.Location = new Point(130, 10 + 34 * num);
            l.Text = "工作量:";
            l.Name = "WorkDsc_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(l);
            TextBox t = new TextBox();
            t.Size = new Size(60, 29);
            t.Location = new Point(202, 10 + 34 * num);
            t.Name = "WorkNum_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(t);
            Label l1 = new Label();
            l1.Size = new Size(70, 29);
            l1.Location = new Point(269, 10 + 34 * num);
            l1.Text = "投资额:";
            l1.Name = "ExpenseDsc_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(l1);
            TextBox t1 = new TextBox();
            t1.Size = new Size(60, 29);
            t1.Location = new Point(341, 10 + 34 * num);
            t1.Name = "Expense_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(t1);
            Button b = new Button();
            b.Size = new Size(60, 29);
            b.Location = new Point(411, 10 + 34 * num);
            b.Name = "Delete_" + (1 * num + 1).ToString();
            b.Text = "删除";
            b.Click += new EventHandler(b_Click);
            this.ProjectPanel.Controls.Add(b);
            this.ProjectPanel.Height = this.ProjectPanel.Height + 34;
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int num = Convert.ToInt32(b.Name.Substring(7));
            int total = (this.ProjectPanel.Controls.Count - 1) / 6;

            ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + num.ToString()];
            TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + num.ToString()];
            Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + num.ToString()];
            TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + num.ToString()];
            Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + num.ToString()];

            this.ProjectPanel.Controls.Remove(item);
            this.ProjectPanel.Controls.Remove(work);
            this.ProjectPanel.Controls.Remove(workdsc);
            this.ProjectPanel.Controls.Remove(expense);
            this.ProjectPanel.Controls.Remove(expensedsc);
            this.ProjectPanel.Controls.Remove(b);

            item.Dispose();
            work.Dispose();
            workdsc.Dispose();
            expense.Dispose();
            expensedsc.Dispose();
            b.Dispose();

            if (num == total)
            {
                this.ProjectPanel.Height = this.ProjectPanel.Height - 34;
            }
            else
            {
                for (int i = num + 1; i <= total; i++)
                {
                    ComboBox item1 = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
                    TextBox work1 = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
                    Label workdsc1 = (Label)this.ProjectPanel.Controls["WorkDsc_" + i.ToString()];
                    TextBox expense1 = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];
                    Label expensedsc1 = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + i.ToString()];
                    Button delete = (Button)this.ProjectPanel.Controls["Delete_" + i.ToString()];

                    item1.Location = new Point(item1.Location.X, item1.Location.Y - 34);
                    work1.Location = new Point(work1.Location.X, work1.Location.Y - 34);
                    workdsc1.Location = new Point(workdsc1.Location.X, workdsc1.Location.Y - 34);
                    expense1.Location = new Point(expense1.Location.X, expense1.Location.Y - 34);
                    expensedsc1.Location = new Point(expensedsc1.Location.X, expensedsc1.Location.Y - 34);
                    delete.Location = new Point(delete.Location.X, delete.Location.Y - 34);

                    item1.Name = "Item_" + (i - 1).ToString();
                    work1.Name = "WorkNum_" + (i - 1).ToString();
                    workdsc1.Name = "WorkDsc_" + (i - 1).ToString();
                    expense1.Name = "Expense_" + (i - 1).ToString();
                    expensedsc1.Name = "ExpenseDsc_" + (i - 1).ToString();
                    delete.Name = "Delete_" + (i - 1).ToString();


                }
                this.ProjectPanel.Height = this.ProjectPanel.Height - 34;
            }

        }







        private void Column4Info_TextChanged(object sender, EventArgs e)
        {
            if (this.Column4Info.Text.Trim() == "")
            {
                this.Column5Info.Text = "";

                return;
            }
            //MessageBox.Show(this.Column4Info.Text.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            ///  modify by gatieme @2016-04-05 21:16
            ///  需求变动
            ///  会签单中累计申请额度，
            ///  从原来 当前部门当前小项目Project的累计申请额度
            ///  -=>
            ///  变更为 当前部门当前大类别Category的累计申请额度
            try
            {
                double currExpense = double.Parse(this.Column4Info.Text.ToString());

                Search search = new Search
                {
                    SDepartmentShortlCall = this.IdDepartShortCall.Text,
                    //ProjectId = Convert.ToInt32(this.xmName.SelectedValue),
                    CategoryShortCall = this.IdCategory.Text,
                    Year = int.Parse(this.IdYear.Text),
                };


                //double totlaExpense = _sc.StatisticDepartmentYearProjectExpense(search);
                double totlaExpense = _sc.StatisticDepartmentYearCategoryExpense(search);

                this.Column5Info.Text = (totlaExpense + currExpense).ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //  处理textbox只能输入数字
        //  
        //  1.  在Winform(C#)中要实现限制Textbox只能输入数字，一般的做法就是在按键事件中处理，
        //      判断keychar的值。限制只能输入数字，小数点，Backspace，del这几个键。数字0～9所
        //      对应的keychar为48～57，小数点是46，Backspace是8，小数点是46。 
        //
        //  2.  输入小数点。输入的小数要符合数字的格式，类似9.9.9这样的是不能够输入的。做法就是用float.TryParse来转换Textbox中之前和之后的值，然后比较两者的转换结果。
        private void Column4Info_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57)
             && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (this.Column4Info.Text.Length <= 0)
                {
                    e.Handled = true;   //小数点不能在第一位
                }
                else                                             //处理不规则的小数点
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(this.Column4Info.Text, out oldf);
                    b2 = float.TryParse(this.Column4Info.Text + e.KeyChar.ToString(), out f);

                    if (b2 == false)
                    {
                        if (b1 == true)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                }
            }
        }




        /// <summary>
        /// modify by gatrieme @2016-01-22 
        /// 修复了一处性能损失的BUG，
        /// 修改SelectedIndexChanged事件 -=> SelectionChangeCommitted事件
        /// 之前每次点击主窗体左侧列表的时候,combox都会被初始化, 这样 SelectedIndexChanged事件就会被触发，
        /// 这样对服务器的负载影响还是很大的，修改成SlectionChangeCommitted后只有在用户选择了combox的条目后才会被触发

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdDepartShortCall_SelectionChangeCommitted(object sender, EventArgs e)
        {

            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            this.ClearWorkloadProjectPane();
            ///



            int departmentId = UserHelper.DepList[this.IdDepartShortCall.SelectedIndex].Id;
            List<ContractCategory> categorys = _sc.QuerySDepartmentContractCategory(departmentId);
            UserHelper.ContractCategoryList = categorys;

            //if (categorys.Count != 0)
            {
                this.IdCategory.ValueMember = "Id";
                this.IdCategory.DisplayMember = "CategoryShortCall";
                this.IdCategory.DataSource = UserHelper.ContractCategoryList;

                this.pName.ValueMember = "Id";
                this.pName.DisplayMember = "Category";
                this.pName.DataSource = UserHelper.ContractCategoryList;
            }

            //  modify by gatieme @ 2016-03-10 11:36
            //  竟然引入了BUG
            this.BindDepartmentYearCategoryCount();


        }

        private void IdCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            this.ClearWorkloadProjectPane();

            //
            this.pName.SelectedValue = this.IdCategory.SelectedValue;
            this.BindProject();

            //  modify by gatieme @ 2016-01-20 23:19
            //  竟然引入了BUG
            this.BindDepartmentYearCategoryCount();
        }

        private void IdYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            ///this.ClearWorkloadProjectPane();
            ///

            this.BindDepartmentYearCategoryCount();
        }


        #region  文本框只限输入数字

        // http://blog.sina.com.cn/s/blog_a9091a33010162iv.html
        // http://blog.csdn.net/hjingtao/article/details/7302448
        // http://blog.163.com/shanghai_xo/blog/static/120131617201091312136777/
        private void IdFlag_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.IdFlag.Text.Trim() == "0")
            {
                this.labelIdFlag.Text = "在线";
            }
            else
            {
                ///  无需添加, 此处不影响
                ///// modify by gatieme @ 2016-01-23 
                ///// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
                //this.ClearWorkloadProjectPane();
                /////

                if (UserHelper.UserInfo.CanStatistic == 0)
                {
                    MessageBox.Show("您没有离线提交的权限!\n如有需要, 请找管理员申请", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.IdFlag.SelectedIndex = 0;
                    return;
                }
                this.labelIdFlag.Text = "离线";
            }

        }


        private void Column5Info_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57)
             && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (this.Column4Info.Text.Length <= 0)
                {
                    e.Handled = true;   //小数点不能在第一位
                }
                else                                             //处理不规则的小数点
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(this.Column4Info.Text, out oldf);
                    b2 = float.TryParse(this.Column4Info.Text + e.KeyChar.ToString(), out f);

                    if (b2 == false)
                    {
                        if (b1 == true)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                }
            }
        }


        private void IdNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57)
             && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (this.Column4Info.Text.Length <= 0)
                {
                    e.Handled = true;   //小数点不能在第一位
                }
                else                                             //处理不规则的小数点
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(this.Column4Info.Text, out oldf);
                    b2 = float.TryParse(this.Column4Info.Text + e.KeyChar.ToString(), out f);

                    if (b2 == false)
                    {
                        if (b1 == true)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                }
            }
        }


        #endregion
        private void BindProject()
        {

            if (this.pName.SelectedValue == null
            || this.IdCategory.SelectedValue == null)
            {
                return ;
            }


            int categoryId = Convert.ToInt32(this.pName.SelectedValue);

            List<ContractProject> list = new List<ContractProject>();
            list = _sc.QueryContractProject(categoryId);
            UserHelper.ContractProjectList = list;


            this.xmName.ValueMember = "Id";
            this.xmName.DisplayMember = "Project";
            this.xmName.DataSource = UserHelper.ContractProjectList;

        }

        private void pName_SelectionChangeCommitted(object sender, EventArgs e)
        {

            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            this.ClearWorkloadProjectPane();
           
            /////
            //this.IdCategory.SelectedValue = this.pName.SelectedValue;
            //int categoryId = Convert.ToInt32(this.pName.SelectedValue);

            //List<ContractProject> list = new List<ContractProject>();
            //list = _sc.QueryContractProject(categoryId);
            //UserHelper.ContractProjectList = list;


            //this.xmName.ValueMember = "Id";
            //this.xmName.DisplayMember = "Project";
            //this.xmName.DataSource = UserHelper.ContractProjectList;
            
            this.IdCategory.SelectedValue = this.pName.SelectedValue;
            this.BindProject();

            //  modify by gatieme @ 2016-01-20 23:19
            //  竟然引入了BUG
            this.BindDepartmentYearCategoryCount();

            ///  FOR DEBUG
            //MessageBox.Show("Idcategory" + this.IdCategory.SelectedValue + ", Pname" + this.pName.SelectedValue + ", Xmname " + this.xmName.SelectedValue);

   
        }
        

        /// <summary>
        /// 清空所有的wokload的列表
        /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        /// </summary>
        private void ClearWorkloadProjectPane()
        {

            int workloadNum = (this.ProjectPanel.Controls.Count - 1) / 6;
            if (workloadNum <= 0)
            {
                return;
            }

            if (MessageBox.Show("您已经添加了工作量的信息, 请问您执意要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                for (int num = 1; num <= workloadNum; num++)
                {
                    ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + num.ToString()];
                    TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + num.ToString()];
                    Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + num.ToString()];
                    TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + num.ToString()];
                    Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + num.ToString()];
                    Button delete = (Button)this.ProjectPanel.Controls["Delete_" + num.ToString()];

                    this.ProjectPanel.Controls.Remove(item);
                    this.ProjectPanel.Controls.Remove(work);
                    this.ProjectPanel.Controls.Remove(workdsc);
                    this.ProjectPanel.Controls.Remove(expense);
                    this.ProjectPanel.Controls.Remove(expensedsc);
                    this.ProjectPanel.Controls.Remove(delete);

                    item.Dispose();
                    work.Dispose();
                    workdsc.Dispose();
                    expense.Dispose();
                    expensedsc.Dispose();
                    delete.Dispose();

                }
            }
        }

        private void xmName_SelectionChangeCommitted(object sender, EventArgs e)
        {

            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            this.ClearWorkloadProjectPane();
            
            
            ///  FOR DEBUG
            //MessageBox.Show("Idcategory" + this.IdCategory.SelectedValue + ", Pname" + this.pName.SelectedValue + ", Xmname " + this.xmName.SelectedValue);

        }

        private void ButtonColumn4InfoAmountInWords_Click(object sender, EventArgs e)
        {
            string money = this.Column4Info.Text.ToString();
            string amount = DigitToAmountInWords.GetCnString(money);
            this.Column4InfoAmountInWords.Text = amount;
        }

        private void ButtonColumn5InfoAmountInWords_Click(object sender, EventArgs e)
        {
            string money = this.Column5Info.Text.ToString();
            string amount = DigitToAmountInWords.GetCnString(money);
            this.Column5InfoAmountInWords.Text = amount;
        }





        /*
        private void xmName_SelectedIndexChanged(object sender, EventArgs e)
        {
        } */
    }
}
