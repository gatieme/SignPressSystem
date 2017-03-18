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


///  modify by gatieme for @ 2016-01-24 14:47
///  修改了重新提交时, 由于原来会签单中workload工作量的SelectedValue无法赋值，
///  导致重新提交会签单后, 工作量集合没有修改的BUG
///  BUG修复历程
///  会签单内部存储当前的会签单数据        HDJContractWithWorkload hdj;
///  然后每次当会签单重新提交时，如果删除了工作条目, 首先判断当前工作量是不是属于原来的hdj会签单内还是新添加的工作量, 
///  如果是原来有的, 那么直接在原来的条目中remove掉, 
///  然后再重新提交时再次绑定数据时候, 对于原来就有的条目, 只是简单的修改工作量和金额就行, 不涉及类别的修改， 这样自然就不用根据SelectedValue获取类别
///  对于新增加的数据，由于类别SelectedValue存在, 那么就直接根据SelectedValue获取到具体的信息即可
///  

namespace SignPressClient
{
    public partial class ReSubmitConTemp : Form
    {
        string              Id;                  // 待处理的会签单编号
        SignSocketClient    _sc;
        HDJContractWithWorkload hdj;
        int projectid;

        public ReSubmitConTemp()
        {
            InitializeComponent();
        }

        public ReSubmitConTemp(SignSocketClient sc, string ID)
            : this()
        {
            _sc = sc;
            Id = ID;
        }

        public ReSubmitConTemp(SignSocketClient sc, HDJContractWithWorkload contract)
            : this()
        {
            this._sc = sc;
            this.Id = contract.Id;
            this.hdj = contract;

        }

        private async void ReSubmitConTemp_Load(object sender, EventArgs e)
        {
            try
            {
                //HDJContractWithWorkload hdj = new HDJContractWithWorkload();
                this.hdj = await _sc.GetHDJContractWithWorkload(Id);

                this.ConTempName.Text = hdj.Name;
                this.ConTempId.Text = hdj.Id;

                // 显示5个栏目的信息
                List<string> columnlist = new List<string>();
                columnlist = hdj.ConTemp.ColumnNames;
                this.Column1.Text = columnlist[0].ToString();
                this.Column2.Text = columnlist[1].ToString();
                this.Column3.Text = columnlist[2].ToString();
                this.Column4.Text = columnlist[3].ToString();
                this.Column5.Text = columnlist[4].ToString();

                // 2015-07-01 Modify by gatieme
                // 显示项目的信息
                List<String> columnInfo = hdj.ColumnDatas;
                this.Column1Info.Text = columnInfo[0].ToString();
                this.Column2Info.Text = columnInfo[1].ToString();
                //this.Column3Info.Text = columnInfo[2].ToString();
                this.Column4Info.Text = columnInfo[3].ToString();
                this.Column5Info.Text = columnInfo[4].ToString();

                // 显示8个签字人的信息
                this.Sign1.Text = hdj.ConTemp.SignDatas[0].SignInfo.ToString();
                this.Sign2.Text = hdj.ConTemp.SignDatas[1].SignInfo.ToString();
                this.Sign3.Text = hdj.ConTemp.SignDatas[2].SignInfo.ToString();
                this.Sign4.Text = hdj.ConTemp.SignDatas[3].SignInfo.ToString();
                this.Sign5.Text = hdj.ConTemp.SignDatas[4].SignInfo.ToString();
                this.Sign6.Text = hdj.ConTemp.SignDatas[5].SignInfo.ToString();
                this.Sign7.Text = hdj.ConTemp.SignDatas[6].SignInfo.ToString();
                this.Sign8.Text = hdj.ConTemp.SignDatas[7].SignInfo.ToString();

                this.SignPer1.Text = hdj.ConTemp.SignDatas[0].SignEmployee.Name.ToString();
                this.SignPer2.Text = hdj.ConTemp.SignDatas[1].SignEmployee.Name.ToString();
                this.SignPer3.Text = hdj.ConTemp.SignDatas[2].SignEmployee.Name.ToString();
                this.SignPer4.Text = hdj.ConTemp.SignDatas[3].SignEmployee.Name.ToString();
                this.SignPer5.Text = hdj.ConTemp.SignDatas[4].SignEmployee.Name.ToString();
                this.SignPer6.Text = hdj.ConTemp.SignDatas[5].SignEmployee.Name.ToString();
                this.SignPer7.Text = hdj.ConTemp.SignDatas[6].SignEmployee.Name.ToString();
                this.SignPer8.Text = hdj.ConTemp.SignDatas[7].SignEmployee.Name.ToString();

                //  ==Happy!!!==
                //  根据工程名称和项目名称获取子类集合
                //  modify by gatieme @ 2016-01-23 
                //  修复了修重新提交会签单时, 工作量集合显示不出来的问题
                //  修复BUG历程说明, 老八你这代码写的没谁了, 你这神奇的命名方式我就不说拉
                //  但是我QueryContractItemByName根据项目和工程名称来获取会签单工作量集合的皆苦
                //  愣是让你写成了获取projectId的接口
                //  我和我的小伙伴都惊呆了
                //this.BindContractItem(true);
                
                //修改代码如下
                //string categoryName = columnInfo[0].ToString();
                //string projectname = columnInfo[1].ToString();
                //Search search = new Search
                //{
                //    CategoryName = categoryName,
                //    ProjectName = projectname,
                //};
                //List<ContractItem> list = _sc.QueryContractItemByName(search);
                // BUG代码如下
                //int projectid =   _sc.QueryContractItemByName(search);
                //List<ContractItem> list = new List<ContractItem>();
                //list = _sc.QueryContractItem(projectid);
                //UserHelper.ContractItemList = list;

                //  显示工作量和投资额
                int n = hdj.WorkLoads.Count;
                for (int pos = 0; pos < n; pos++)
                {
                    int num = pos;//(this.ProjectPanel.Controls.Count - 1) / 6;
                    this.BindContractItem(true);


                    ComboBox cmb = new ComboBox();
                    cmb.Size = new Size(100, 29);
                    cmb.Location = new Point(3, 10 + 34 * pos);
                    cmb.Name = "Item_" + (pos + 1).ToString();
                    cmb.ValueMember = "Id";
                    cmb.DisplayMember = "Item";
                    //cmb.DropDownStyle = ComboBoxStyle.DropDownList;  //  设置combobox只能通过下拉选取，不能输入
                    cmb.Enabled = false;                               //  设置combobox为只读

                    //  BUG,添加数据源之后SelectedText--SelectedValue等无法初始化
                    //  感觉是控件自己的问题
                    //cmb.DataSource = UserHelper.ContractItemList;
                    //  添加了上一行代码后绑定了数据源, 使得后面的代码完全不起作用
                    //  Text--SelectedText--SelectedValue等无法通过正常方式赋值
                    //cmb.Text = hdj.WorkLoads[pos].Item.Item.ToString();
                    cmb.SelectedText = hdj.WorkLoads[pos].Item.Item.ToString(); 
                    //cmb.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);    //用于绑定工作量和投资额
                    
                    //cmb.SelectedValue = hdj.WorkLoads[pos].Item.Id;
                    //cmb.SelectedIndex = 1;
                    this.ProjectPanel.Controls.Add(cmb);
                    // 调试用弹窗
                    //MessageBox.Show(cmb.Name + cmb.SelectedValue + hdj.WorkLoads[pos].Item.Id.ToString() + cmb.Text + hdj.WorkLoads[pos].Item.Item.ToString());
                    //MessageBox.Show(cmb.Name + ", " + cmb.SelectedValue + hdj.WorkLoads[pos].Item.Id.ToString());
                    //[60, 110] -=> [60, 170] -=> [60, 230] -=> [90, 290]  [60, 380]  [50, 440]

                    Label l = new Label();
                    l.Size = new Size(60, 29);
                    l.Location = new Point(110, 10 + 34 * pos);
                    l.Text = "工作量";
                    l.Name = "WorkDsc_" + (pos + 1).ToString();
                    this.ProjectPanel.Controls.Add(l);
                    
                    TextBox t = new TextBox();
                    t.Size = new Size(60, 29);
                    t.Location = new Point(170, 10 + 34 * pos);
                    t.Name = "WorkNum_" + (pos + 1).ToString();
                    t.Text = hdj.WorkLoads[pos].Work.ToString();
                    this.ProjectPanel.Controls.Add(t);

                    TextBox info = new TextBox();
                    info.Size = new Size(60, 29);
                    info.Location = new Point(230, 10 + 34 * pos);
                    info.Name = "WorkInfo_" + (pos + 1).ToString();
                    info.Text = hdj.WorkLoads[pos].WorkInfo.ToString();
                    this.ProjectPanel.Controls.Add(info);

                    Label l1 = new Label();
                    l1.Size = new Size(90, 29);
                    l1.Location = new Point(290, 10 + 34 * pos);
                    l1.Text = "投资额(元)";
                    l1.Name = "ExpenseDsc_" + (pos + 1).ToString();
                    this.ProjectPanel.Controls.Add(l1);
                    
                    TextBox t1 = new TextBox();
                    t1.Size = new Size(60, 29);
                    t1.Location = new Point(380, 10 + 34 * pos);
                    t1.Name = "Expense_" + (pos + 1).ToString();
                    t1.Text = hdj.WorkLoads[pos].Expense.ToString();
                    this.ProjectPanel.Controls.Add(t1);
                    
                    Button b = new Button();
                    b.Size = new Size(50, 29);
                    b.Location = new Point(440, 10 + 34 * pos);
                    b.Name = "Delete_" + (pos + 1).ToString();
                    b.Text = "删除";
                    b.Click += new EventHandler(b_Click);
                    this.ProjectPanel.Controls.Add(b);
                    
                    this.ProjectPanel.Height = this.ProjectPanel.Height + 34;
                }
            }
            catch
            {
                MessageBox.Show("加载数据失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Logging.AddLog("重新提交时加载数据失败！");

                if (MessageBox.Show("好吧,我们的程序出现点问题需要重新启动\n点击\"确定\"重启\n点击\"取消\"退出程序？",
                    "程序异常",
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

        private void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int num = Convert.ToInt32(b.Name.Substring(7));
            int total = (this.ProjectPanel.Controls.Count - 1) / 6;

            ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + num.ToString()];
            Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + num.ToString()];
            TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + num.ToString()];
            TextBox workInfo = (TextBox)this.ProjectPanel.Controls["WorkInfo_" + num.ToString()];
            Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + num.ToString()];
            TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + num.ToString()];

            this.ProjectPanel.Controls.Remove(item);
            this.ProjectPanel.Controls.Remove(work);
            this.ProjectPanel.Controls.Remove(workInfo);
            this.ProjectPanel.Controls.Remove(workdsc);
            this.ProjectPanel.Controls.Remove(expense);
            this.ProjectPanel.Controls.Remove(expensedsc);
            this.ProjectPanel.Controls.Remove(b);

            item.Dispose();
            work.Dispose();
            workInfo.Dispose();
            workdsc.Dispose();
            expense.Dispose();
            expensedsc.Dispose();
            b.Dispose();
            
            // modify by gatieme @ 2016-01-24 修复提交时候
            //  modify by gatieme for @ 2016-01-24 14:47
            //  修改了重新提交时, 由于原来会签单中workload工作量的SelectedValue无法赋值，
            //  导致重新提交会签单后, 工作量集合没有修改的BUG
            /// BUG...
            //  每次删除后直接将数据从会签单源中删除
            if (num <= hdj.WorkLoads.Count)
            {
                MessageBox.Show("删除了源数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                hdj.WorkLoads.RemoveAt(num - 1);
            }
            else
            {
                MessageBox.Show("删除了新数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
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


        // 重新提交按钮单机以后，应该进行一次刷新
        // modify by gatieme 2015-07-02 9:47 修改提交方案wield异步
        private  void button1_Click(object sender, EventArgs e)
        {
            //if (this.Column1Info.Text.Trim() != "" && this.Column2Info.Text.Trim() != "" &&
            //    this.ProjectPanel.Text.Trim() != "" && this.Column4Info.Text.Trim() != "" && this.Column5Info.Text.Trim() != "")
            if (this.Column1Info.Text.Trim() != "" && this.Column2Info.Text.Trim() != "" &&
                this.Column4Info.Text.Trim() != "" && this.Column5Info.Text.Trim() != "")
            {
                if (MessageBox.Show("您确定要提交所填方案吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //HDJContractWithWorkload hdj = new HDJContractWithWorkload();
                    hdj.Id = this.ConTempId.Text;



                    string workloadStr = "";
                    //List<ContractWorkload> worklist = new List<ContractWorkload>();
                    int num = (this.ProjectPanel.Controls.Count - 1) / 6;
                    for (int i = 1; i <= num; i++)
                    {

                        ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
                        TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
                        TextBox workInfo = (TextBox)this.ProjectPanel.Controls["WorkInfo_" + i.ToString()];
                        TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];
                        
                        //  modify by gatieme for @ 2016-01-24 14:47
                        //  修改了重新提交时, 由于原来会签单中workload工作量的SelectedValue无法赋值，
                        //  导致重新提交会签单后, 工作量集合没有修改的BUG
                        /// BUG...
                        if (i <= hdj.WorkLoads.Count)           //  这些工作量是原来会签单中已经有的, 只需要修改其工作量的大小和金额即可
                        {
                            hdj.WorkLoads[i - 1].Work = Convert.ToDouble(work.Text.Trim());
                            hdj.WorkLoads[i - 1].Expense = Convert.ToDouble(expense.Text.Trim());
                            workloadStr += hdj.WorkLoads[i - 1].Item.Item + "   工作量 : " + hdj.WorkLoads[i - 1].Work.ToString() + "   投资额 : " + hdj.WorkLoads[i - 1].Expense.ToString() + "\r\n";

                        }
                        else
                        {

                            ContractWorkload workload = new ContractWorkload();
                            workload.ContractId = hdj.Id;
                            ContractItem it = new ContractItem();
                            it.ProjectId = projectid;
                            it.Id = Convert.ToInt32(item.SelectedValue);
                            it.Item = item.Text.ToString();
                            workload.Item = it;
                            if (work.Text.Trim() == "" || expense.Text.Trim() == "")
                            {
                                MessageBox.Show("请将工作量填写完整!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            workload.Work = Convert.ToDouble(work.Text.Trim());
                            workload.WorkInfo = workInfo.Text.Trim();
                            workload.Expense = Convert.ToDouble(expense.Text.Trim());
                            workload.Id = hdj.Id + it.Id.ToString();

                            workloadStr += workload.Item.Item + "   工作量 : " + workload.Work.ToString( ) + workload.WorkInfo.ToString( ) + "   投资额 : " + workload.Expense.ToString() + "\r\n";
                            hdj.WorkLoads.Add(workload);
                        }


                        //worklist.Add(workload);
                    }
                    //hdj.WorkLoads = worklist;

                    List<String> list = new List<string>();
                    list.Add(this.Column1Info.Text.ToString());
                    list.Add(this.Column2Info.Text.ToString());
                    list.Add(workloadStr);
                    list.Add(this.Column4Info.Text.ToString());
                    list.Add(this.Column5Info.Text.ToString());
                    hdj.ColumnDatas = list;


                    string result =  _sc.ModifyContractTemplate(hdj);
                    if (result == Response.MODIFY_HDJCONTRACT_SUCCESS.ToString())
                    {
                        MessageBox.Show("提交成功!", "提示", MessageBoxButtons.OK);

                        //  2015-07-01 14:58 modify by gatieme
                        //  重新提交后，应该设置OK
                        //this.DialogResult = DialogResult.OK;
                        //this.Close();         // 不能关闭，否则会异常
                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        //  add by gatieme @2016-01-23  
        //  修复重新提提交会签单时, 无法添加工作量集合的BUG...
        private void AddItem_Click(object sender, EventArgs e)
        {

            // 首先先绑定item的数据信息
            this.BindContractItem(true);

            int num = (this.ProjectPanel.Controls.Count - 1) / 6;
            ComboBox cmb = new ComboBox();
            cmb.Size = new Size(100, 29);
            cmb.Location = new Point(3, 10 + 34 * num);
            cmb.Name = "Item_" + (1 * num + 1).ToString();
            cmb.ValueMember = "Id";
            cmb.DisplayMember = "Item";
            List<ContractItem> items = UserHelper.ContractItemList;
            cmb.DataSource = items;//UserHelper.ContractItemList;

            ////cmb.SelectedIndexChanged += cmb_SelectedIndexChanged;    //用于绑定工作量和投资额
            //this.ProjectPanel.Controls.Add(cmb);
            //Label l = new Label();
            //l.Size = new Size(70, 29);
            //l.Location = new Point(130, 10 + 34 * num);
            //l.Text = "工作量:";
            //l.Name = "WorkDsc_" + (1 * num + 1).ToString();
            //this.ProjectPanel.Controls.Add(l);
            //TextBox t = new TextBox();
            //t.Size = new Size(60, 29);
            //t.Location = new Point(202, 10 + 34 * num);
            //t.Name = "WorkNum_" + (1 * num + 1).ToString();
            //this.ProjectPanel.Controls.Add(t);
            //Label l1 = new Label();
            //l1.Size = new Size(70, 29);
            //l1.Location = new Point(269, 10 + 34 * num);
            //l1.Text = "投资额:";
            //l1.Name = "ExpenseDsc_" + (1 * num + 1).ToString();
            //this.ProjectPanel.Controls.Add(l1);
            //TextBox t1 = new TextBox();
            //t1.Size = new Size(60, 29);
            //t1.Location = new Point(341, 10 + 34 * num);
            //t1.Name = "Expense_" + (1 * num + 1).ToString();
            //this.ProjectPanel.Controls.Add(t1);
            //Button b = new Button();
            //b.Size = new Size(60, 29);
            //b.Location = new Point(411, 10 + 34 * num);
            //b.Name = "Delete_" + (1 * num + 1).ToString();
            //b.Text = "删除";
            //b.Click += new EventHandler(b_Click);
            //this.ProjectPanel.Controls.Add(b);
            //this.ProjectPanel.Height = this.ProjectPanel.Height + 34;


            this.ProjectPanel.Controls.Add(cmb);
            Label l = new Label();
            l.Size = new Size(60, 29);
            l.Location = new Point(110, 10 + 34 * num);
            l.Text = "工作量";
            l.Name = "WorkDsc_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(l);

            TextBox t = new TextBox();
            t.Size = new Size(60, 29);
            t.Location = new Point(170, 10 + 34 * num);
            t.Name = "WorkNum_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(t);


            TextBox info = new TextBox();
            info.Size = new Size(60, 29);
            info.Location = new Point(230, 10 + 34 * num);
            info.Name = "WorkInfo_" + (1 * num + 1).ToString();
            info.Text = "单位/备注";
            this.ProjectPanel.Controls.Add(info);

            Label l1 = new Label();
            l1.Size = new Size(90, 29);
            l1.Location = new Point(290, 10 + 34 * num);
            l1.Text = "投资额(元)";
            l1.Name = "ExpenseDsc_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(l1);

            TextBox t1 = new TextBox();
            t1.Size = new Size(60, 29);
            t1.Location = new Point(380, 10 + 34 * num);
            t1.Name = "Expense_" + (1 * num + 1).ToString();
            this.ProjectPanel.Controls.Add(t1);

            Button b = new Button();
            b.Size = new Size(50, 29);
            b.Location = new Point(440, 10 + 34 * num);
            b.Name = "Delete_" + (1 * num + 1).ToString();
            b.Text = "删除";
            b.Click += new EventHandler(b_Click);

            this.ProjectPanel.Controls.Add(b);
            this.ProjectPanel.Height = this.ProjectPanel.Height + 34;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFlush"></param>
        private void BindContractItem(bool isFlush)
        {
            if (UserHelper.ContractItemList == null || isFlush == true)
            {
                string categoryName = this.Column1Info.Text.ToString();
                string projectName = this.Column2Info.Text.ToString();
                Search search = new Search
                {
                    CategoryName = categoryName,
                    ProjectName = projectName,
                };

                List<ContractItem> list = _sc.QueryContractItemByName(search);
                UserHelper.ContractItemList = list;
            }
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("修改会签单时, 无能修改已有工作量的类别, 如果必须修改, 请删除后重新添加", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
