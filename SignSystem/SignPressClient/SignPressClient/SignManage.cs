using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using SignPressClient.Model;
using SignPressClient.SignSocket;
using SignPressClient.Tools;

namespace SignPressClient
{
    public partial class SignManage : Form
    {
        SignSocketClient _sc;
        private OpaqueCommand cmd = new OpaqueCommand();


        public SignManage()
        {
            InitializeComponent();
        }

        public SignManage(int selectedIndex, SignSocketClient sc)
            : this()
        {
            this.Sign.SelectedIndex = selectedIndex;
            _sc = sc;
        }

        private void SignManage_Load(object sender, EventArgs e)
        {
            //绑定待办信息  已办的就不自动绑定了，有需要人工刷新
            this.BindUnsignData(false);
        }
        //QUERY_UNSIGN_CONTRACT_REQUEST;1;1QUERY_SIGN_REFUSE_REQUEST;1;1,



        #region 待办中的刷新以及其他事件

        #region 绑定数据信息
        /// <summary>
        /// 代办列表的数据量较小，可以自动刷新
        /// </summary>
        /// <param name="isFlush"></param>
        private  void BindUnsignData(bool isFlush)
        {
            if (UserHelper.ToDoList == null
            || isFlush == true)
            {
                //通过用户id查询
                List<SHDJContract> unsignList =  _sc.QueryUnsignContract(UserHelper.UserInfo.Id);

                //List<SHDJContract> unsignList = null;

                //while (unsignList == null)
                //{
                //    unsignList =  _sc.QueryUnsignContract(UserHelper.UserInfo.Id);
                //}

                this.ToDoListView.AutoGenerateColumns = false;
                this.ToDoListView.DataSource = unsignList;

                UserHelper.ToDoList = unsignList;
            }
            else
            {
                this.ToDoListView.AutoGenerateColumns = false;
                this.ToDoListView.DataSource = UserHelper.ToDoList;
            }
        }
        #endregion

        #region 待办中的列表点击事件
        // 待签字列表的单机事件
        private void ToDoListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //用于判断是否签单同意  用于刷新列表
            bool ifok = false; ;
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 1)
            {
                string Id = this.ToDoListView.Rows[e.RowIndex].Cells[0].Value.ToString();
                //这里也要区分模版类型
                string type = Id[1].ToString() + Id[2].ToString();
                if (type == "内例" || type == "内专")
                {
                    //显示内河的模版
                    SignConTempInside sct = new SignConTempInside(_sc, Id, 4);
                    sct.ShowDialog();
                    if (sct.DialogResult == DialogResult.OK)
                    {
                        sct.Close();
                        ifok = true;
                    }
                }

                if (type == "界例")
                {
                    //显示界河例行的模版
                    SignConTempJHLX sct = new SignConTempJHLX(_sc, Id, 4);
                    sct.ShowDialog();
                    if (sct.DialogResult == DialogResult.OK)
                    {
                        sct.Close();
                        ifok = true;
                    }
                }

                if (type == "界专")
                {
                    SignConTemp sct = new SignConTemp(_sc, Id, 4);
                    sct.ShowDialog();

                    if (sct.DialogResult == DialogResult.OK)
                    {
                        sct.Close();
                        ifok = true;
                    }
                }

                if (ifok == true)
                {
                    //刷新数据
                    ///[BUG NO.2015-07-05 17:52]
                    BindUnsignData(true);

                    // 用户签字后，待办列表数目减少1，已办列表数目增加1
                    MainWindow mw = (MainWindow)this.MdiParent;
                    foreach (TreeNode t in mw.treeView1.Nodes)
                    {
                        if (t.Text.Contains("签字管理("))
                        {
                            int count = Convert.ToInt32(t.Text.Split('(')[1].Split(')')[0]);
                            if (count - 1 == 0)
                            {
                                t.Text = "签字管理";
                                t.Nodes[0].Text = "待办列表";
                            }
                            else
                            {
                                t.Text = "签字管理(" + (count - 1) + ")";
                                t.Nodes[0].Text = "待办列表(" + (count - 1) + ")";
                            }
                        }
                    }
                    //BindSignedData(true);
                }
                //SignConTemp sct = new SignConTemp(_sc, Id, 4);
                //sct.ShowDialog();

            }
        }
        #endregion 

        #region 刷新单击事件
        //刷新待办
        private void RefreshPendinglist_Click(object sender, EventArgs e)
        {
            List<SHDJContract> list = new List<SHDJContract>();
            list = null;
            while (list == null)
            {
                list = _sc.QueryUnsignContract(UserHelper.UserInfo.Id);
            }
            this.ToDoListView.DataSource = list;
            UserHelper.ToDoList = list;
        }
        #endregion

        #endregion

        #region 已办中的刷新以及其他事件

        #region 数据绑定
        /// <summary>
        /// 已办列表的数据量较大，自动刷新不是很划算
        /// </summary>
        /// <param name="isFlush"></param>
        private  void BindSignedData(bool isFlush)
        {
            /// 当且仅当缓存数据UserHelpwer为空，或者用户期望强制刷新时，强制进行数据获取
            if (UserHelper.DoneList == null
            || isFlush == true)
            {
                List<SHDJContract> signedList =  _sc.QuerySignedContract(UserHelper.UserInfo.Id);
                //List<SHDJContract> signedList = null;
                //while (signedList == null)
                //{
                //    signedList = await _sc.QuerySignedContract(UserHelper.UserInfo.Id);
                //}
                this.DoneListView.AutoGenerateColumns = false;
                this.DoneListView.DataSource = signedList;

                UserHelper.DoneList = signedList;
            }
            else
            {
                this.DoneListView.AutoGenerateColumns = false;
                this.DoneListView.DataSource = UserHelper.DoneList;

            }
        }
        #endregion

        #region 已办列表中的事件
        // 已办列表的单击事件
        private async void DoneListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 1)
            {

                string id = this.DoneListView.Rows[e.RowIndex].Cells[0].Value.ToString();
                //这里也要区分模版类型
                string type = id[1].ToString() + id[2].ToString();
                if (type == "内例" || type == "内专")
                {
                    //显示内河的模版
                    SignConTempInside sct = new SignConTempInside(_sc, id, 5);
                    sct.ShowDialog();
                }

                if (type == "界例")
                {
                    //显示界河例行的模版
                    SignConTempJHLX sct = new SignConTempJHLX(_sc, id, 5);
                    sct.ShowDialog();
                }

                if (type == "界专")
                {
                    SignConTemp sct = new SignConTemp(_sc, id, 5);
                    sct.ShowDialog();
                }
            }

            //下载
            if (e.ColumnIndex == 8)
            {
                //获取会签单编号
                string Id = this.DoneListView.Rows[e.RowIndex].Cells[0].Value.ToString();
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
                sfd.FileName = this.DoneListView.Rows[e.RowIndex].Cells[0].Value.ToString();
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

                   
                }
            }
        }
        //搜索
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
                if (this.Downloadable.Checked == true)
                {
                    Search s = new Search();
                    s.EmployeeId = UserHelper.UserInfo.Id;
                    s.ConId = ContractId;
                    s.ProjectName = projectName;
                    s.DateBegin = start;
                    s.DateEnd = end;
                    s.Downloadable = 1;

                    List<SHDJContract> list = new List<SHDJContract>();
                    list = _sc.SearchSignedHDJConstract(s);


                    this.DoneListView.AutoGenerateColumns = false;
                    this.DoneListView.DataSource = list;

                    DataGridViewLinkColumn download = new DataGridViewLinkColumn();
                    download.Text = "下载签单附件";
                    download.Name = "LinkDownLoad";
                    download.HeaderText = "下载签单附件";
                    download.Width = 150;
                    download.UseColumnTextForLinkValue = true;
                    this.DoneListView.Columns.Add(download);
                }
                else
                {
                    Search s = new Search();
                    s.EmployeeId = UserHelper.UserInfo.Id;
                    s.ConId = ContractId;
                    s.ProjectName = projectName;
                    s.DateBegin = start;
                    s.DateEnd = end;
                    s.Downloadable = 0;

                    List<SHDJContract> list = new List<SHDJContract>();
                    list = _sc.SearchSignedHDJConstract(s);


                    this.DoneListView.AutoGenerateColumns = false;
                    this.DoneListView.DataSource = list;
                }
            }


        }
        #endregion 

        #endregion

       


       

       
    }
}
