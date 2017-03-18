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



/***************************************************************
 * 2016-4-23将提交管理的下拉列表改为表格datagrideview显示 这样跟方便操作*
 ***************************************************************/

namespace SignPressClient
{
    public partial class SubmitManage : Form
    {
        SignSocketClient _sc;

       // int conbox_selectcontemp;


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

        #region  数据加载
        private void SubmitManage_Load(object sender, EventArgs e)                 //窗体加载事件
        {
            /*
             * combox的SelectedIndexChanged事件,在datasouce指定的时候就被触发了,这时候数据还没有绑定好,自然会报错.
             * 
             * 我认为这是不合理的.SelectedIndexChanged不应该在绑定数据的中间被触发.
             * 
             * 我最后解决办法是设置了一个标志符isLoaded,bool类型,在填充方法完毕后,设为true.允许SelectedIndexChanged被触发.
            */
            //this.IdDepartShortCall.SelectedIndex = 1;

            //将Panel容器先不显示
            //this.ConTempInfo.Visible = false;

                                               //218.7.0.37
            //  DateTime timer = new DateTime();
            //  int month=DateTime.Now.Month;
            //  int year=DateTime.Now.Year;
            //  初始化年第一天
            //  dateTimePicker1.Text=year+"/1/1";
            //初始化月份第一天
            // dateTimePicker1.Text=year+"/"+month+"/1";
//2016-4-24修改为在表格中显示要提交的模版
            this.BindContractTemplate();

            //已通过中的开始时间
            this.AgreeStartDate.Format = DateTimePickerFormat.Custom;
            this.AgreeStartDate.CustomFormat = "yyyy-MM-dd";
            //已通过中的结束时间
            this.AgreeEndDate.Format = DateTimePickerFormat.Custom;
            this.AgreeEndDate.CustomFormat = "yyyy-MM-dd";


            //审核中
            this.BindPenddingList(false);


            //// 同意列表可能很多，数据量太大，取消自动刷新
            ///  bug 2015-07-06 10:59
            ///  修改为查询已通过的但是未曾下载过的会签单信息
            this.BindAgreeUndownloadList(false);
            //this.BindAgreeList(false);


             this.BindRefuseList(false);                                //加载已拒绝的方案


           BindSignRefuseAndAgreeOpera();                     //绑定已拒绝以及已通过方案操作列
           BindRefuseOper();

        }
        #endregion

        //2016-4-24 修改为在表格中显示
        #region 绑定提交会签单数据以及点击事件
        //绑定下拉列表的模版信息
        private async void BindContractTemplate()
        {
            //if (UserHelper.TempAllList == null)
            //{
            //    List<Templete> list = await _sc.QueryContractTemplateInside();

               
            //    this.SelecteConTemplate.ValueMember = "TempId";
            //    this.SelecteConTemplate.DisplayMember = "Name";
            //    this.SelecteConTemplate.DataSource = list;
            //    UserHelper.TempList = list;
            //}
            //else
            //{
            //    this.SelecteConTemplate.ValueMember = "TempId";
            //    this.SelecteConTemplate.DisplayMember = "Name";
            //    this.SelecteConTemplate.DataSource = UserHelper.TempAllList;
            //}

            ///***********************************************************************************
            //   2016-4-22 修复关于conbox控件和selectIndexChanged事件的BUG  
            //   设置DataSource的时候会调用SelecteConTemplate_SelectedIndexChanged 这时候还没有绑定好数据源所以
            //   还不能让selectindex为-1会出现未实例化对象，在datasource之后才能让之为-1,并且此时打开标志位conbox_selecttemp为1 
            //   让SelecteConTemplate_SelectedIndexChanged函数生效
            // *******************************************************************************************/
            //this.SelecteConTemplate.SelectedIndex = -1;
            //conbox_selectcontemp = 1;
            List<Templete> list = new List<Templete>();
            list = await _sc.QueryContractTemplateInside();

            if (list != null)
            {
                UserHelper.TempList = list;
                this.dataGridView2.AutoGenerateColumns = false;
                this.dataGridView2.DataSource = list;
            }
            else
            {
                MessageBox.Show("数据库中还没有模版，请新建模版");
            }

        }
//表格点击事件
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)    //模板列表表格中的点击事件
        {
            bool ifsuccess = false;
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {
                int Id = Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                int Type = Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[2].Value);
                /*
                 *通过type来判断   10是内例  11是内专  20是界例  21是界专 
                 */
                if (Type / 10 == 1)
                {
                    //弹出要提交相应模版的窗口    
                    SubmintContempInside contemp = new SubmintContempInside(Id, _sc,Type);
                    DialogResult result = contemp.ShowDialog();
                    if (result==DialogResult.OK)
                    {
                        ifsuccess = true;
                    }
                }

                if (Type == 20)
                {
                    //提交界河例行模版
                    SubmintContempJHLX contemp = new SubmintContempJHLX(Id, _sc);
                    DialogResult result = contemp.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ifsuccess = true;
                    }
                }
                if (Type == 21)
                {
                    //提交界河专项模版      
                    SubmintContemp contemp = new SubmintContemp(Id, _sc);
                    DialogResult result = contemp.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ifsuccess = true;
                    }
                }

                if (ifsuccess)
                {
                    this.BindPenddingList(true);

                    //更新提交管理数据
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

            }
        }

        #endregion


        #region 绑定审核中数据以及相关事件操作
        //查询signaturetatus和hdjcontract两张表 查出会签单的编号  名称  工程名  提交时间  提交人 当前节点 最大节点
        /// <summary>
        /// 绑定审核中的数据信息
        /// </summary>
        /// <param name="isFlush">强制刷新标识
        /// 默认情况下，是直接读取UserHelper中的信息结构，不会强制刷新，但是某些情况下，比如用户刚提交了一个信息的时候，是期望进行强制刷新的
        /// 希望强制刷新的时候，将isFlush置为true</param>
        private  async void BindPenddingList(bool isFlush)
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
                if (penddingList==null)
                {
                    MessageBox.Show("当前没有你的审核会签单");
                }
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


        private void SignPendding_CellContentClick(object sender, DataGridViewCellEventArgs e)           //查看正在审批方案的详细信息
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {

                string Id = this.SignPendding.Rows[e.RowIndex].Cells[0].Value.ToString();
                string Type = Id[1].ToString() + Id[2].ToString();
                /*
                 *通过type来判断   10是内例  11是内专  20是界例  21是界专 
                 */
                if (Type == "内例"||Type=="内专")
                {
                    //弹出要提交相应模版的窗口    
                    //MessageBox.Show("我要看内河模版");
                    SignConTempInside sct = new SignConTempInside(_sc, Id, 1);
                    sct.ShowDialog();
                }

                if (Type == "界例")
                {
                    //提交界河例行模版
                   // MessageBox.Show("我要看界河例行模版");
                    SignConTempJHLX sct = new SignConTempJHLX(_sc, Id, 1);
                    sct.ShowDialog();
                }
                if (Type == "界专")
                {
                    //看界河专项模版   
                    SignConTemp sct = new SignConTemp(_sc, Id, 1);
                    sct.ShowDialog();
                }
                
            }
        }

        private async void SignPendding_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = await _sc.QuerySignPend(UserHelper.UserInfo.Id);
            this.SignPendding.DataSource = list;
            UserHelper.PenddingList = list;
        }
        #endregion


        #region 已拒绝的会签单已经相关事件
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

        private void SignRefuse_CellContentClick(object sender, DataGridViewCellEventArgs e)              //已拒绝列表操作功能
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {
                //会签单的编号
                string Id = this.SignRefuse.Rows[e.RowIndex].Cells[0].Value.ToString();
                string Type = Id[1].ToString() + Id[2].ToString();
                if (Type=="内例"||Type=="内专")
                {
                    SignConTempInside sct = new SignConTempInside(_sc, Id, 2);
                    sct.ShowDialog();
                }
                if (Type == "界例")
                {
                    SignConTempJHLX sct = new SignConTempJHLX(_sc, Id, 2);
                    sct.ShowDialog();
                }
                if (Type == "界专")
                {
                    SignConTemp sct = new SignConTemp(_sc, Id, 2);
                    sct.ShowDialog();
                } 
            }
                //重新提交
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

        private void SignRefuse_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = _sc.QuerySignRefuse(UserHelper.UserInfo.Id);
            this.SignRefuse.DataSource = list;
            UserHelper.RefuseList = list;
        }

        #region 绑定已拒绝的重新提交和删除  和已通过的下载会签单 事件链接
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
        #endregion

        //刷新已拒绝
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
        //刷新在审核中
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

        #endregion


        #region 绑定已通过的会签单信息以及相关事件
        //绑定已通过但是未被下载的会签单信息
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

        private void SignAgree_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = _sc.QuerySignAgree(UserHelper.UserInfo.Id);
            this.SignAgree.DataSource = list;
            UserHelper.AgreeList = list;
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
                string type = Id[1].ToString() + Id[2].ToString();
                if (type=="内例" || type=="内专")
                {
                    SignConTempInside sct = new SignConTempInside(_sc, Id, 3);
                    sct.ShowDialog();
                }
                if (type == "界例")
                {
                    SignConTempJHLX sct = new SignConTempJHLX(_sc, Id, 3);
                    sct.ShowDialog();
                }
                if (type == "界专")
                {
                    SignConTemp sct = new SignConTemp(_sc, Id, 3);
                    sct.ShowDialog();
                }
                
            }
            //下载
            if (e.ColumnIndex == 4)
            {
                //获取会签单编号
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
                    //下载会签单
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

                                if (t.Nodes[3].Text.Contains("已通过("))
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

        //搜索按钮
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
        #endregion


       


//2016-4-24  改为在表格中显示  start
        //private void SelecteConTemplate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //当为1时函数功能开放
        //        if (conbox_selectcontemp==-1)
        //        {
        //            return;
        //        }
        //        conbox_selectcontemp = -1;

        //         int TemplateId = Convert.ToInt32(this.SelecteConTemplate.SelectedValue.ToString());
        //         //MessageBox.Show("当前选中Id是"+TemplateId);
        //         if (TemplateId/10000000==1)
        //         {
        //             MessageBox.Show("我是内河提交单");

        //         }
        //         if (TemplateId / 10000000 == 2)
        //         {
        //             MessageBox.Show("我是界河例行提交单");
        //         }
        //         if (TemplateId / 10000000 == 3)
        //         {
        //             SubmintContemp contemp = new SubmintContemp(TemplateId,_sc);
        //             //setForm(contemp);
        //             DialogResult result = contemp.ShowDialog();

        //             if (result==DialogResult.OK)
        //             {
        //                 this.BindPenddingList(true);
        //                 //更新提交管理数据
        //                 MainWindow mw = (MainWindow)this.MdiParent;
        //                 foreach (TreeNode t in mw.treeView1.Nodes)
        //                 {
        //                     if (t.Text.Contains("提交管理("))
        //                     {
        //                         int count = Convert.ToInt32(t.Text.Split('(')[1].Split(')')[0]);    // 提交管理

        //                         t.Text = "提交管理(" + (count + 1) + ")";
        //                         //t.Nodes[0]  -=>  提交方案
        //                         //t.Nodes[1]  -=>  审核中
        //                         //t.Nodes[2]  -=>  已拒绝
        //                         //t.Nodes[3]  -=>  已通过
        //                         if (t.Nodes[1].Text.Contains("审核中("))
        //                         {
        //                             int childcount = Convert.ToInt32(t.Nodes[1].Text.Split('(')[1].Split(')')[0]);
        //                             t.Nodes[1].Text = "审核中(" + (childcount + 1) + ")";
        //                         }
        //                     }
        //                 }

        //             }
        //         }

        //         this.SelecteConTemplate.SelectedIndex = -1;
        //         conbox_selectcontemp = 1;
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }
        //}
        ////嵌入模版时先判断原来是否存在
        //private void ClosePreForm()
        //{
        //    //首先要判断该功能窗体对象是否在父窗体splitContainer1.Panel2已经存在了
        //    if (this.splitContainer1.Panel2.Controls.Count!=0)
        //    {
        //        foreach (Control  item in this.splitContainer1.Panel2.Controls)
        //        {
        //            if (item is Form)
        //            {
        //                Form objControl = (Form)item;
        //                objControl.Close();
        //            }
        //        }
        //    }
        //}
        ////点击提交管理时显示选中的提交模版
        //private void setForm(Form objForm)
        //{
        //    //关闭已经嵌入的窗体
        //    ClosePreForm();
        //    //【2】将子窗体设置为非顶级控件
        //    objForm.TopLevel = false;
        //    //【3】让窗体最大化显示
        //    objForm.WindowState = FormWindowState.Maximized;
        //    //【4】去掉窗体边框
        //    objForm.FormBorderStyle = FormBorderStyle.None;
        //    //【5】指定子窗体显示的容器
        //    objForm.Parent = this.splitContainer1.Panel2;
        //    //【6】显示窗体并嵌入  
        //    objForm.Show();
        //}
//2016-4-24  改为在表格中显示  end

//先注释掉    因为加了splitContainer1来分割
        //private void button1_Click(object sender, EventArgs e)                   //提交会签单信息
        //{
        //    if (this.pName.Text.Trim() != "" && this.xmName.Text.Trim() != ""
        //    && this.Column4Info.Text.Trim() != "" && this.Column5Info.Text.Trim() != ""
        //    && this.Column4InfoAmountInWords.Text.Trim() != "" && this.Column5InfoAmountInWords.Text.Trim() != ""
        //    && this.label3.Text.Trim() != ""
        //    && this.IdNo.Text.Trim() != "")
        //    {
        //        if (MessageBox.Show("您确定要提交所填方案吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            HDJContractWithWorkload hdj = new HDJContractWithWorkload();
        //            hdj.Name = this.ConName.Text;
        //            hdj.SubmitEmployee = UserHelper.UserInfo;
        //            Templete temp = new Templete();
        //            temp.TempId = UserHelper.SelectedTemp.TempId;
        //            hdj.ConTemp = temp;
        //            hdj.Id = this.IdDepartShortCall.Text.ToString() + this.IdCategory.Text.ToString() +
        //                     this.IdYear.Text.ToString() + this.IdFlag.Text.ToString() + this.IdNo.Text.Trim();



        //            string workloadStr = "";
        //            List<ContractWorkload> worklist = new List<ContractWorkload>();
        //            int num = (this.ProjectPanel.Controls.Count - 1) / 6;

        //            //  modify by gatieme @ 2016-01-22 23:48
        //            //  修复了无工作量集合也能提交会签单的BG...
        //            if (num <= 0)
        //            {
        //                MessageBox.Show("会签单的工作量集合为空, 无法提交!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            for (int i = 1; i <= num; i++)
        //            {


        //                ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
        //                TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
        //                TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];

        //                ContractWorkload workload = new ContractWorkload();
        //                workload.ContractId = hdj.Id;
        //                ContractItem it = new ContractItem();
        //                it.ProjectId = Convert.ToInt32(this.xmName.SelectedValue);
        //                it.Id = Convert.ToInt32(item.SelectedValue);
        //                it.Item = item.Text.ToString();

        //                workload.Item = it;
        //                if (work.Text.Trim() == "" || expense.Text.Trim() == "")
        //                {
        //                    MessageBox.Show("请将工作量填写完整!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                //  判断工作量中是否有重复的数据



        //                workload.Work = Convert.ToDouble(work.Text.Trim());
        //                workload.Expense = Convert.ToDouble(expense.Text.Trim());
        //                workload.Id = hdj.Id + it.Id.ToString();




        //                //  modify by gatieme @ 2016-01-22
        //                //  修复了工作量可以重复的BUG...   
        //                if (worklist.Where(o => o.Item.Id == it.Id).ToList().Count > 0)
        //                {
        //                    MessageBox.Show("工作量有重复的!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }

        //                worklist.Add(workload);
        //                workloadStr += workload.Item.Item + "   工作量 : " + workload.Work.ToString() + "   投资额 : " + workload.Expense.ToString() + "\r\n";


        //            }
        //            hdj.WorkLoads = worklist;


        //            List<String> list = new List<string>();
        //            list.Add(this.pName.Text.ToString());  //  工程名称
        //            list.Add(this.xmName.Text.ToString()); //  项目名称
        //            list.Add(workloadStr);
                    
        //            string currMoney  = this.Column4Info.Text.ToString() + "    " + this.Column4InfoAmountInWords.Text.ToString();
        //            string totalMoney = this.Column5Info.Text.ToString() + "    " + this.Column5InfoAmountInWords.Text.ToString();
        //            list.Add(currMoney);
        //            list.Add(totalMoney);

        //            hdj.ColumnDatas = list;


        //            string result = _sc.InsertHDJContract(hdj);
        //            if (result == Response.INSERT_HDJCONTRACT_SUCCESS.ToString())
        //            {
        //                this.ConTempInfo.Visible = false;
        //                this.pName.Text = "";
        //                this.xmName.Text = "";
        //                this.Column4Info.Text = "";
        //                this.Column5Info.Text = "";
        //                this.IdNo.Text = "";

        //                for (int i = 1; i <= num; i++)
        //                {
        //                    ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
        //                    TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
        //                    Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + i.ToString()];
        //                    TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];
        //                    Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + i.ToString()];
        //                    Button b = (Button)this.ProjectPanel.Controls["Delete_" + i.ToString()];

        //                    this.ProjectPanel.Controls.Remove(item);
        //                    this.ProjectPanel.Controls.Remove(work);
        //                    this.ProjectPanel.Controls.Remove(workdsc);
        //                    this.ProjectPanel.Controls.Remove(expense);
        //                    this.ProjectPanel.Controls.Remove(expensedsc);
        //                    this.ProjectPanel.Controls.Remove(b);

        //                    item.Dispose();
        //                    work.Dispose();
        //                    workdsc.Dispose();
        //                    expense.Dispose();
        //                    expensedsc.Dispose();
        //                    b.Dispose();
        //                }

        //                this.ProjectPanel.Height = this.ProjectPanel.Height - 34 * num;

        //                MessageBox.Show("提交成功!", "提示", MessageBoxButtons.OK);

        //                //if(hdj.ConTemp.SignDatas.Where())
        //                // 2015-07-03 11:25  提交成功后应该刷新一下待签字结构体
        //                if (HDJContract.GetIsOnlineFromContractId(hdj.Id) == false)
        //                {
        //                    MessageBox.Show("您提交了一份离线审批单子, 将直接完成签字，且无法修改!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                this.BindPenddingList(true);        //  强制刷新待审核的数据

        //                /////////////////////////////////////////
        //                /// 每次提交一个新的单子之后，待审核数目增加 1///
        //                /////////////////////////////////////////
        //                MainWindow mw = (MainWindow)this.MdiParent;
        //                foreach (TreeNode t in mw.treeView1.Nodes)
        //                {
        //                    if (t.Text.Contains("提交管理("))
        //                    {
        //                        int count = Convert.ToInt32(t.Text.Split('(')[1].Split(')')[0]);    // 提交管理

        //                        t.Text = "提交管理(" + (count + 1) + ")";

        //                        //t.Nodes[0]  -=>  提交方案
        //                        //t.Nodes[1]  -=>  审核中
        //                        //t.Nodes[2]  -=>  已拒绝
        //                        //t.Nodes[3]  -=>  已通过
        //                        if (t.Nodes[1].Text.Contains("审核中("))
        //                        {
        //                            int childcount = Convert.ToInt32(t.Nodes[1].Text.Split('(')[1].Split(')')[0]);
        //                            t.Nodes[1].Text = "审核中(" + (childcount + 1) + ")";
        //                        }
        //                    }
        //                }
        //            }
        //            else if (result == "服务器连接中断")
        //            {
        //                MessageBox.Show("服务器连接中断,提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            else if (result == Response.INSERT_HDJCONTRACT_EXIST.ToString())
        //            {
        //                MessageBox.Show("该会签单编号已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            else
        //            {
        //                MessageBox.Show("提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("请将所有空白处填完!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}



      



       


      

     
       

       
        

        
       



//2016-4-21加splitContainer1后先注释下
        //private void AddItem_Click(object sender, EventArgs e)                  //添加主要项目和工程量
        //{
        //    int projectid = Convert.ToInt32(this.xmName.SelectedValue);

        //    List<ContractItem> list = new List<ContractItem>();
        //    list = _sc.QueryContractItem(projectid);
        //    UserHelper.ContractItemList = list;

        //    int num = (this.ProjectPanel.Controls.Count - 1) / 6;
        //    ComboBox cmb = new ComboBox();
        //    cmb.Size = new Size(120, 29);
        //    cmb.Location = new Point(3, 10 + 34 * num);
        //    cmb.Name = "Item_" + (1 * num + 1).ToString();
        //    cmb.ValueMember = "Id";
        //    cmb.DisplayMember = "Item";
        //    cmb.DataSource = UserHelper.ContractItemList;
        //    //cmb.SelectedIndexChanged += cmb_SelectedIndexChanged;    //用于绑定工作量和投资额
        //    this.ProjectPanel.Controls.Add(cmb);
        //    Label l = new Label();
        //    l.Size = new Size(70, 29);
        //    l.Location = new Point(130, 10 + 34 * num);
        //    l.Text = "工作量:";
        //    l.Name = "WorkDsc_" + (1 * num + 1).ToString();
        //    this.ProjectPanel.Controls.Add(l);
        //    TextBox t = new TextBox();
        //    t.Size = new Size(60, 29);
        //    t.Location = new Point(202, 10 + 34 * num);
        //    t.Name = "WorkNum_" + (1 * num + 1).ToString();
        //    this.ProjectPanel.Controls.Add(t);
        //    Label l1 = new Label();
        //    l1.Size = new Size(70, 29);
        //    l1.Location = new Point(269, 10 + 34 * num);
        //    l1.Text = "投资额:";
        //    l1.Name = "ExpenseDsc_" + (1 * num + 1).ToString();
        //    this.ProjectPanel.Controls.Add(l1);
        //    TextBox t1 = new TextBox();
        //    t1.Size = new Size(60, 29);
        //    t1.Location = new Point(341, 10 + 34 * num);
        //    t1.Name = "Expense_" + (1 * num + 1).ToString();
        //    this.ProjectPanel.Controls.Add(t1);
        //    Button b = new Button();
        //    b.Size = new Size(60, 29);
        //    b.Location = new Point(411, 10 + 34 * num);
        //    b.Name = "Delete_" + (1 * num + 1).ToString();
        //    b.Text = "删除";
        //    b.Click += new EventHandler(b_Click);
        //    this.ProjectPanel.Controls.Add(b);
        //    this.ProjectPanel.Height = this.ProjectPanel.Height + 34;
        //}

        //private void b_Click(object sender, EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    int num = Convert.ToInt32(b.Name.Substring(7));
        //    int total = (this.ProjectPanel.Controls.Count - 1) / 6;

        //    ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + num.ToString()];
        //    TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + num.ToString()];
        //    Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + num.ToString()];
        //    TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + num.ToString()];
        //    Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + num.ToString()];

        //    this.ProjectPanel.Controls.Remove(item);
        //    this.ProjectPanel.Controls.Remove(work);
        //    this.ProjectPanel.Controls.Remove(workdsc);
        //    this.ProjectPanel.Controls.Remove(expense);
        //    this.ProjectPanel.Controls.Remove(expensedsc);
        //    this.ProjectPanel.Controls.Remove(b);

        //    item.Dispose();
        //    work.Dispose();
        //    workdsc.Dispose();
        //    expense.Dispose();
        //    expensedsc.Dispose();
        //    b.Dispose();

        //    if (num == total)
        //    {
        //        this.ProjectPanel.Height = this.ProjectPanel.Height - 34;
        //    }
        //    else
        //    {
        //        for (int i = num + 1; i <= total; i++)
        //        {
        //            ComboBox item1 = (ComboBox)this.ProjectPanel.Controls["Item_" + i.ToString()];
        //            TextBox work1 = (TextBox)this.ProjectPanel.Controls["WorkNum_" + i.ToString()];
        //            Label workdsc1 = (Label)this.ProjectPanel.Controls["WorkDsc_" + i.ToString()];
        //            TextBox expense1 = (TextBox)this.ProjectPanel.Controls["Expense_" + i.ToString()];
        //            Label expensedsc1 = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + i.ToString()];
        //            Button delete = (Button)this.ProjectPanel.Controls["Delete_" + i.ToString()];

        //            item1.Location = new Point(item1.Location.X, item1.Location.Y - 34);
        //            work1.Location = new Point(work1.Location.X, work1.Location.Y - 34);
        //            workdsc1.Location = new Point(workdsc1.Location.X, workdsc1.Location.Y - 34);
        //            expense1.Location = new Point(expense1.Location.X, expense1.Location.Y - 34);
        //            expensedsc1.Location = new Point(expensedsc1.Location.X, expensedsc1.Location.Y - 34);
        //            delete.Location = new Point(delete.Location.X, delete.Location.Y - 34);

        //            item1.Name = "Item_" + (i - 1).ToString();
        //            work1.Name = "WorkNum_" + (i - 1).ToString();
        //            workdsc1.Name = "WorkDsc_" + (i - 1).ToString();
        //            expense1.Name = "Expense_" + (i - 1).ToString();
        //            expensedsc1.Name = "ExpenseDsc_" + (i - 1).ToString();
        //            delete.Name = "Delete_" + (i - 1).ToString();


        //        }
        //        this.ProjectPanel.Height = this.ProjectPanel.Height - 34;
        //    }

        //}






        //private void Column4Info_TextChanged(object sender, EventArgs e)
        //{
        //    if (this.Column4Info.Text.Trim() == "")
        //    {
        //        this.Column5Info.Text = "";

        //        return;
        //    }
        //    //MessageBox.Show(this.Column4Info.Text.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        //    ///  modify by gatieme @2016-04-05 21:16
        //    ///  需求变动
        //    ///  会签单中累计申请额度，
        //    ///  从原来 当前部门当前小项目Project的累计申请额度
        //    ///  -=>
        //    ///  变更为 当前部门当前大类别Category的累计申请额度
        //    try
        //    {
        //        double currExpense = double.Parse(this.Column4Info.Text.ToString());

        //        Search search = new Search
        //        {
        //            SDepartmentShortlCall = this.IdDepartShortCall.Text,
        //            //ProjectId = Convert.ToInt32(this.xmName.SelectedValue),
        //            CategoryShortCall = this.IdCategory.Text,
        //            Year = int.Parse(this.IdYear.Text),
        //        };


        //        //double totlaExpense = _sc.StatisticDepartmentYearProjectExpense(search);
        //        double totlaExpense = _sc.StatisticDepartmentYearCategoryExpense(search);

        //        this.Column5Info.Text = (totlaExpense + currExpense).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        ////  处理textbox只能输入数字
        ////  
        ////  1.  在Winform(C#)中要实现限制Textbox只能输入数字，一般的做法就是在按键事件中处理，
        ////      判断keychar的值。限制只能输入数字，小数点，Backspace，del这几个键。数字0～9所
        ////      对应的keychar为48～57，小数点是46，Backspace是8，小数点是46。 
        ////
        ////  2.  输入小数点。输入的小数要符合数字的格式，类似9.9.9这样的是不能够输入的。做法就是用float.TryParse来转换Textbox中之前和之后的值，然后比较两者的转换结果。
        //private void Column4Info_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // 判断按键是不是要输入的类型。
        //    if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57)
        //     && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
        //    {
        //        e.Handled = true;
        //    }

        //    //小数点的处理。
        //    if ((int)e.KeyChar == 46)                           //小数点
        //    {
        //        if (this.Column4Info.Text.Length <= 0)
        //        {
        //            e.Handled = true;   //小数点不能在第一位
        //        }
        //        else                                             //处理不规则的小数点
        //        {
        //            float f;
        //            float oldf;
        //            bool b1 = false, b2 = false;
        //            b1 = float.TryParse(this.Column4Info.Text, out oldf);
        //            b2 = float.TryParse(this.Column4Info.Text + e.KeyChar.ToString(), out f);

        //            if (b2 == false)
        //            {
        //                if (b1 == true)
        //                {
        //                    e.Handled = true;
        //                }
        //                else
        //                {
        //                    e.Handled = false;
        //                }
        //            }
        //        }
        //    }
        //}

//2016-4-21加splitContainer1后先注释下            end




//2016-4-21加splitContainer1后先注释下 start

        /// <summary>
        /// modify by gatrieme @2016-01-22 
        /// 修复了一处性能损失的BUG，
        /// 修改SelectedIndexChanged事件 -=> SelectionChangeCommitted事件
        /// 之前每次点击主窗体左侧列表的时候,combox都会被初始化, 这样 SelectedIndexChanged事件就会被触发，
        /// 这样对服务器的负载影响还是很大的，修改成SlectionChangeCommitted后只有在用户选择了combox的条目后才会被触发

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void IdDepartShortCall_SelectionChangeCommitted(object sender, EventArgs e)
        //{

        //    /// modify by gatieme @ 2016-01-23 
        //    /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        //    this.ClearWorkloadProjectPane();
        //    ///



        //    int departmentId = UserHelper.DepList[this.IdDepartShortCall.SelectedIndex].Id;
        //    List<ContractCategory> categorys = _sc.QuerySDepartmentContractCategory(departmentId);
        //    UserHelper.ContractCategoryList = categorys;

        //    //if (categorys.Count != 0)
        //    {
        //        this.IdCategory.ValueMember = "Id";
        //        this.IdCategory.DisplayMember = "CategoryShortCall";
        //        this.IdCategory.DataSource = UserHelper.ContractCategoryList;

        //        this.pName.ValueMember = "Id";
        //        this.pName.DisplayMember = "Category";
        //        this.pName.DataSource = UserHelper.ContractCategoryList;
        //    }

        //    //  modify by gatieme @ 2016-03-10 11:36
        //    //  竟然引入了BUG
        //    this.BindDepartmentYearCategoryCount();


        //}

        //private void IdCategory_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    /// modify by gatieme @ 2016-01-23 
        //    /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        //    this.ClearWorkloadProjectPane();

        //    //
        //    this.pName.SelectedValue = this.IdCategory.SelectedValue;
        //    this.BindProject();

        //    //  modify by gatieme @ 2016-01-20 23:19
        //    //  竟然引入了BUG
        //    this.BindDepartmentYearCategoryCount();
        //}
        //private void IdYear_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    /// modify by gatieme @ 2016-01-23 
        //    /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        //    ///this.ClearWorkloadProjectPane();
        //    ///

        //    this.BindDepartmentYearCategoryCount();
        //}


        //#region  文本框只限输入数字

        //// http://blog.sina.com.cn/s/blog_a9091a33010162iv.html
        //// http://blog.csdn.net/hjingtao/article/details/7302448
        //// http://blog.163.com/shanghai_xo/blog/static/120131617201091312136777/
        //private void IdFlag_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (this.IdFlag.Text.Trim() == "0")
        //    {
        //        this.labelIdFlag.Text = "在线";
        //    }
        //    else
        //    {
        //        ///  无需添加, 此处不影响
        //        ///// modify by gatieme @ 2016-01-23 
        //        ///// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        //        //this.ClearWorkloadProjectPane();
        //        /////

        //        if (UserHelper.UserInfo.CanStatistic == 0)
        //        {
        //            MessageBox.Show("您没有离线提交的权限!\n如有需要, 请找管理员申请", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            this.IdFlag.SelectedIndex = 0;
        //            return;
        //        }
        //        this.labelIdFlag.Text = "离线";
        //    }

        //}


        //private void Column5Info_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // 判断按键是不是要输入的类型。
        //    if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57)
        //     && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
        //    {
        //        e.Handled = true;
        //    }

        //    //小数点的处理。
        //    if ((int)e.KeyChar == 46)                           //小数点
        //    {
        //        if (this.Column4Info.Text.Length <= 0)
        //        {
        //            e.Handled = true;   //小数点不能在第一位
        //        }
        //        else                                             //处理不规则的小数点
        //        {
        //            float f;
        //            float oldf;
        //            bool b1 = false, b2 = false;
        //            b1 = float.TryParse(this.Column4Info.Text, out oldf);
        //            b2 = float.TryParse(this.Column4Info.Text + e.KeyChar.ToString(), out f);

        //            if (b2 == false)
        //            {
        //                if (b1 == true)
        //                {
        //                    e.Handled = true;
        //                }
        //                else
        //                {
        //                    e.Handled = false;
        //                }
        //            }
        //        }
        //    }
        //}


        //private void IdNo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // 判断按键是不是要输入的类型。
        //    if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57)
        //     && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
        //    {
        //        e.Handled = true;
        //    }

        //    //小数点的处理。
        //    if ((int)e.KeyChar == 46)                           //小数点
        //    {
        //        if (this.Column4Info.Text.Length <= 0)
        //        {
        //            e.Handled = true;   //小数点不能在第一位
        //        }
        //        else                                             //处理不规则的小数点
        //        {
        //            float f;
        //            float oldf;
        //            bool b1 = false, b2 = false;
        //            b1 = float.TryParse(this.Column4Info.Text, out oldf);
        //            b2 = float.TryParse(this.Column4Info.Text + e.KeyChar.ToString(), out f);

        //            if (b2 == false)
        //            {
        //                if (b1 == true)
        //                {
        //                    e.Handled = true;
        //                }
        //                else
        //                {
        //                    e.Handled = false;
        //                }
        //            }
        //        }
        //    }
        //}


        //#endregion
        //private void BindProject()
        //{

        //    if (this.pName.SelectedValue == null
        //    || this.IdCategory.SelectedValue == null)
        //    {
        //        return ;
        //    }


        //    int categoryId = Convert.ToInt32(this.pName.SelectedValue);

        //    List<ContractProject> list = new List<ContractProject>();
        //    list = _sc.QueryContractProject(categoryId);
        //    UserHelper.ContractProjectList = list;


        //    this.xmName.ValueMember = "Id";
        //    this.xmName.DisplayMember = "Project";
        //    this.xmName.DataSource = UserHelper.ContractProjectList;

        //}

        //private void pName_SelectionChangeCommitted(object sender, EventArgs e)
        //{

        //    /// modify by gatieme @ 2016-01-23 
        //    /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        //    this.ClearWorkloadProjectPane();
           
        //    /////
        //    //this.IdCategory.SelectedValue = this.pName.SelectedValue;
        //    //int categoryId = Convert.ToInt32(this.pName.SelectedValue);

        //    //List<ContractProject> list = new List<ContractProject>();
        //    //list = _sc.QueryContractProject(categoryId);
        //    //UserHelper.ContractProjectList = list;


        //    //this.xmName.ValueMember = "Id";
        //    //this.xmName.DisplayMember = "Project";
        //    //this.xmName.DataSource = UserHelper.ContractProjectList;
            
        //    this.IdCategory.SelectedValue = this.pName.SelectedValue;
        //    this.BindProject();

        //    //  modify by gatieme @ 2016-01-20 23:19
        //    //  竟然引入了BUG
        //    this.BindDepartmentYearCategoryCount();

        //    ///  FOR DEBUG
        //    //MessageBox.Show("Idcategory" + this.IdCategory.SelectedValue + ", Pname" + this.pName.SelectedValue + ", Xmname " + this.xmName.SelectedValue);

   
        //}
        

        ///// <summary>
        ///// 清空所有的wokload的列表
        ///// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        ///// </summary>
        //private void ClearWorkloadProjectPane()
        //{

        //    int workloadNum = (this.ProjectPanel.Controls.Count - 1) / 6;
        //    if (workloadNum <= 0)
        //    {
        //        return;
        //    }

        //    if (MessageBox.Show("您已经添加了工作量的信息, 请问您执意要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {

        //        for (int num = 1; num <= workloadNum; num++)
        //        {
        //            ComboBox item = (ComboBox)this.ProjectPanel.Controls["Item_" + num.ToString()];
        //            TextBox work = (TextBox)this.ProjectPanel.Controls["WorkNum_" + num.ToString()];
        //            Label workdsc = (Label)this.ProjectPanel.Controls["WorkDsc_" + num.ToString()];
        //            TextBox expense = (TextBox)this.ProjectPanel.Controls["Expense_" + num.ToString()];
        //            Label expensedsc = (Label)this.ProjectPanel.Controls["ExpenseDsc_" + num.ToString()];
        //            Button delete = (Button)this.ProjectPanel.Controls["Delete_" + num.ToString()];

        //            this.ProjectPanel.Controls.Remove(item);
        //            this.ProjectPanel.Controls.Remove(work);
        //            this.ProjectPanel.Controls.Remove(workdsc);
        //            this.ProjectPanel.Controls.Remove(expense);
        //            this.ProjectPanel.Controls.Remove(expensedsc);
        //            this.ProjectPanel.Controls.Remove(delete);

        //            item.Dispose();
        //            work.Dispose();
        //            workdsc.Dispose();
        //            expense.Dispose();
        //            expensedsc.Dispose();
        //            delete.Dispose();

        //        }
        //    }
        //}

        //private void xmName_SelectionChangeCommitted(object sender, EventArgs e)
        //{

        //    /// modify by gatieme @ 2016-01-23 
        //    /// 用于在添加workload列表后仍要修改会签单其他信息时候, 清空workload的列表
        //    this.ClearWorkloadProjectPane();
            
            
        //    ///  FOR DEBUG
        //    //MessageBox.Show("Idcategory" + this.IdCategory.SelectedValue + ", Pname" + this.pName.SelectedValue + ", Xmname " + this.xmName.SelectedValue);

        //}




        //private void ButtonColumn4InfoAmountInWords_Click(object sender, EventArgs e)
        //{
        //    string money = this.Column4Info.Text.ToString();
        //    string amount = DigitToAmountInWords.GetCnString(money);
        //    this.Column4InfoAmountInWords.Text = amount;
        //}

        //private void ButtonColumn5InfoAmountInWords_Click(object sender, EventArgs e)
        //{
        //    string money = this.Column5Info.Text.ToString();
        //    string amount = DigitToAmountInWords.GetCnString(money);
        //    this.Column5InfoAmountInWords.Text = amount;
        //}



 //2016-4-21加splitContainer1后先注释下  end

        /*
        private void xmName_SelectedIndexChanged(object sender, EventArgs e)
        {
        } */
    }
}
