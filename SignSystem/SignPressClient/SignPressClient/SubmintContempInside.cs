using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SignPressClient.SignSocket;
using SignPressClient.Model;
using SignPressClient.SignLogging;
using SignPressClient.Tools;

namespace SignPressClient
{
    public partial class SubmintContempInside : Form
    {
        SignSocketClient _sc;
        int ContempId;
        int _type;
        Templete temp = new Templete();
        public SubmintContempInside()
        {
            InitializeComponent();
        }

         public SubmintContempInside(int contempId,SignSocketClient sc,int type)
             :this()
        {
            _sc = sc;
            //模版id
            ContempId = contempId;
            _type = type;
             try
            {
               // Templete temp = new Templete();
                //SOCKET查询
                temp = _sc.GetContractTemplateInside(ContempId);

                if (temp != null)
                   {
                     UserHelper.SelectedTemp = temp;
                  }


                this.ConTempName.Text = temp.Name.ToString();
                this.Column1.Text = temp.ColumnNames[0].ToString();
                this.Column2.Text = temp.ColumnNames[1].ToString();
                this.Column3.Text = temp.ColumnNames[2].ToString();
                this.Column4.Text = temp.ColumnNames[3].ToString();
                this.Column5.Text = temp.ColumnNames[4].ToString();
                this.Column6.Text = temp.ColumnNames[5].ToString();

                this.Sign1.Text = temp.SignDatas[0].SignInfo.ToString();
                this.Sign2.Text = temp.SignDatas[1].SignInfo.ToString();
                this.Sign3.Text = temp.SignDatas[2].SignInfo.ToString();
                this.Sign4.Text = temp.SignDatas[3].SignInfo.ToString();
                this.Sign5.Text = temp.SignDatas[4].SignInfo.ToString();
                this.Sign6.Text = temp.SignDatas[5].SignInfo.ToString();
                this.Sign7.Text = temp.SignDatas[6].SignInfo.ToString();
                this.Sign8.Text = temp.SignDatas[7].SignInfo.ToString();
                

                this.SignPer1.Text = temp.SignDatas[0].SignEmployee.Name.ToString();
                this.SignPer2.Text = temp.SignDatas[1].SignEmployee.Name.ToString();
                this.SignPer3.Text = temp.SignDatas[2].SignEmployee.Name.ToString();
                this.SignPer4.Text = temp.SignDatas[3].SignEmployee.Name.ToString();
                this.SignPer5.Text = temp.SignDatas[4].SignEmployee.Name.ToString();
                this.SignPer6.Text = temp.SignDatas[5].SignEmployee.Name.ToString();
                this.SignPer7.Text = temp.SignDatas[6].SignEmployee.Name.ToString();
                this.SignPer8.Text = temp.SignDatas[7].SignEmployee.Name.ToString();

                if (_type==10)
                {
                    this.SignPer4.Text = "无需签字";
                }
               
            }
            catch
            {
                MessageBox.Show("加载数据失败!");
                Logging.AddLog("提交界河专项模板详细信息失败!");

                if (MessageBox.Show("好吧,我们的程序出现点问题需要重新启动\n点击\"确定\"重启\n点击\"取消\"退出程序？",
                    "提示",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
           
        }

        private void SubmintContempInside_Load(object sender, EventArgs e)
        {
            //  绑定编号前缀信息
            this.BindIdDepartment(false);
            this.BindCategory(false);
            this.BindProject(false);
            this.BindIdYear();
            this.BindIdFlag();
            this.BindContractCategoryCount(true);
            this.BindDepartmentYearCategoryCount();
        }

        #region 绑定编号前缀信息

        /// <summary>
        /// 绑定会签单编号:部门简称信息
        /// </summary>
        private void BindIdDepartment(bool isFlush)
        {
            //窗体加载时位datagridview绑定数据源
            if (UserHelper.DepList == null || isFlush == true)
            {
                List<Department> list = _sc.QueryDepartment();

                this.IdDepartShortCall.ValueMember = "Id";
                this.IdDepartShortCall.DisplayMember = "ShortCall";
                this.IdDepartShortCall.DataSource = list;

                UserHelper.DepList = list;
            }

            if (UserHelper.DepList != null)
            {
                this.IdDepartShortCall.ValueMember = "Id";
                this.IdDepartShortCall.DisplayMember = "ShortCall";
                this.IdDepartShortCall.DataSource = UserHelper.DepList;

            }
        }

        //绑定编号中的工程简称  和   栏目一的工程名
        private void BindCategory(bool isFlush)
        {
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
            if (_type==10)
            {
                this.IdCategory.SelectedIndex = 2;
                //栏目一的工程名
                this.pName.ValueMember = "Id";
                this.pName.DisplayMember = "Category";
                this.pName.DataSource = UserHelper.ContractCategoryList;
                this.pName.SelectedIndex = 2;
            }
            if (_type == 11)
            {
                this.IdCategory.SelectedIndex = 3;
                //栏目一的工程名
                this.pName.ValueMember = "Id";
                this.pName.DisplayMember = "Category";
                this.pName.DataSource = UserHelper.ContractCategoryList;
                this.pName.SelectedIndex = 3;
            }
           

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
                //获取当前会签单是第几单
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

        #endregion



        #region 栏目一到栏目五的信息

        //绑定栏目二的项目名
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

        //绑定栏目三工程量
        //根据项目名称类型去获取相应的工程量item
        private void AddItem_Click(object sender, EventArgs e)           //添加主要项目和工程量
        {
            //项目id
            int projectid = Convert.ToInt32(this.xmName.SelectedValue);

            List<ContractItem> list = new List<ContractItem>();
            list = _sc.QueryContractItem(projectid);
            UserHelper.ContractItemList = list;

            int num = (this.ProjectPanel.Controls.Count - 1) / 6;
            ComboBox cmb = new ComboBox();
            cmb.Size = new Size(120, 29);
            cmb.Location = new Point(10, 10 + 34 * num);
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
            //增加按钮事件
            b.Click += new EventHandler(b_Click);
            this.ProjectPanel.Controls.Add(b);
            this.ProjectPanel.Height = this.ProjectPanel.Height + 34;
        }

        //工程量中删除按钮事件
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
        //栏目四 栏目五     本次申请金额带动栏目五  累计申请金额的改变
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
                //找到部门、工程名、年份  来确定本年的使用金额是多少
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
                throw ex;
            }
        }

        ////  处理textbox只能输入数字
        ////  
        ////  1.  在Winform(C#)中要实现限制Textbox只能输入数字，一般的做法就是在按键事件中处理，
        ////      判断keychar的值。限制只能输入数字，小数点，Backspace，del这几个键。数字0～9所
        ////      对应的keychar为48～57，小数点是46，Backspace是8，小数点是46。 
        ////
        ////  2.  输入小数点。输入的小数要符合数字的格式，类似9.9.9这样的是不能够输入的。做法就是用float.TryParse来转换Textbox中之前和之后的值，然后比较两者的转换结果。

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

        //将栏目四的本次申请金额 转化为大写形式
        private void ButtonColumn4InfoAmountInWords_Click(object sender, EventArgs e)
        {
            string money = this.Column4Info.Text.ToString();
            string amount = DigitToAmountInWords.GetCnString(money);
            this.Column4InfoAmountInWords.Text = amount;
        }
        //栏目五的大写按钮事件
        private void ButtonColumn5InfoAmountInWords_Click(object sender, EventArgs e)
        {
            string money = this.Column5Info.Text.ToString();
            string amount = DigitToAmountInWords.GetCnString(money);
            this.Column5InfoAmountInWords.Text = amount;
        }

        #endregion



        #region 编号中控件的事件
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

        private void BindProject()
        {

            if (this.pName.SelectedValue == null
            || this.IdCategory.SelectedValue == null)
            {
                return;
            }


            int categoryId = Convert.ToInt32(this.pName.SelectedValue);

            List<ContractProject> list = new List<ContractProject>();
            list = _sc.QueryContractProject(categoryId);
            UserHelper.ContractProjectList = list;


            this.xmName.ValueMember = "Id";
            this.xmName.DisplayMember = "Project";
            this.xmName.DataSource = UserHelper.ContractProjectList;

        }

        private void IdYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            ///this.ClearWorkloadProjectPane();
            ///

            this.BindDepartmentYearCategoryCount();
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



        #region 栏目中的事件
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

        private void xmName_SelectionChangeCommitted(object sender, EventArgs e)
        {

            /// modify by gatieme @ 2016-01-23 
            /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
            this.ClearWorkloadProjectPane();


            ///  FOR DEBUG
            //MessageBox.Show("Idcategory" + this.IdCategory.SelectedValue + ", Pname" + this.pName.SelectedValue + ", Xmname " + this.xmName.SelectedValue);

        }
        #endregion


        #region 提交事件
        //提交按钮
        private void btn_Submit_Click(object sender, EventArgs e)                   //提交会签单信息
        {
            //全部空不为空
            if (this.pName.Text.Trim() != "" && this.xmName.Text.Trim() != ""
            && this.Column4Info.Text.Trim() != "" && this.Column5Info.Text.Trim() != ""
            && this.Column4InfoAmountInWords.Text.Trim() != "" && this.Column5InfoAmountInWords.Text.Trim() != ""
            && this.label3.Text.Trim() != ""
            && this.IdNo.Text.Trim() != "")
            {
                if (MessageBox.Show("您确定要提交所填方案吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    HDJContractWithWorkload hdj = new HDJContractWithWorkload();
                    hdj.Name = this.ConTempName.Text;
                    hdj.SubmitEmployee = UserHelper.UserInfo;
                    Templete temp = new Templete();

                    temp.TempId = UserHelper.SelectedTemp.TempId;

                    hdj.ConTemp = temp;
                    hdj.Id = this.IdDepartShortCall.Text.ToString() + this.IdCategory.Text.ToString() +
                             this.IdYear.Text.ToString() + this.IdFlag.Text.ToString() + this.IdNo.Text.Trim();



                    string workloadStr = "";
                    List<ContractWorkload> worklist = new List<ContractWorkload>();

                    //工作量的控件数目
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

                        //一个会签单多个item的话就通过最后以为判别   如：省界专2016001（1、2、3）  就是这单有3项item
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

                    string currMoney = this.Column4Info.Text.ToString() + "    " + this.Column4InfoAmountInWords.Text.ToString();
                    string totalMoney = this.Column5Info.Text.ToString() + "    " + this.Column5InfoAmountInWords.Text.ToString();
                    list.Add(currMoney);
                    list.Add(totalMoney);

                    hdj.ColumnDatas = list;

                    //存入数据库
                    string result = _sc.InsertHDJContract(hdj);
                    //成功就清空
                    if (result == Response.INSERT_HDJCONTRACT_SUCCESS.ToString())
                    {
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


                        // 2015-07-03 11:25  提交成功后应该刷新一下待签字结构体
                        if (HDJContract.GetIsOnlineFromContractId(hdj.Id) == false)
                        {
                            MessageBox.Show("您提交了一份离线审批单子, 将直接完成签字，且无法修改!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //返回ok进行刷新
                        this.DialogResult = DialogResult.OK;

                    }
                    //提交失败
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


        #endregion

       

       

    }
}
