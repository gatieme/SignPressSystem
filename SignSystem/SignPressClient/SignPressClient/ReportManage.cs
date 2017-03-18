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
    public partial class ReportManage : Form
    {
        SignSocketClient _sc;
        private OpaqueCommand cmd = new OpaqueCommand();



        public ReportManage()
        {
            InitializeComponent();
        }

        public ReportManage(int selectedIndex, SignSocketClient sc)
            : this()
        {
            this.ReportAnalysis.SelectedIndex = selectedIndex;
            _sc = sc;
        }

        private void ReportManage_Load(object sender, EventArgs e)                //窗口加载事件
        {
            this.BindCategory();
            this.BindIdYear();
            BindDataGridView3();        //项目类别列表添加编辑列
            BindDataGridView1();        //工作量类别列表添加编辑列
        }



        #region 下载/上传配额表
        private async void DownLoad_Click(object sender, EventArgs e)        //下载配额表
        {
            if(this.CategoryId.SelectedItem == null)
            {
                MessageBox.Show("请选择希望下载的会签单类别信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Search search = new Search
            {
                Year = int.Parse(this.IdYear.Text),
                CategoryId = int.Parse(((ComboboxItem)this.CategoryId.SelectedItem).Value),
            };
            SaveFileDialog sfd = new SaveFileDialog();

            //设置文件类型 
            sfd.Filter = "Microsoft Excel(*.xls) | *.xls";

            // 设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            sfd.FileName = this.CategoryId.Text + "计划费用分配表";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MainWindow mw = (MainWindow)this.MdiParent;
                cmd.ShowOpaqueLayer(this.ReportAnalysis, 125, true, true, "正在下载中，请稍候");
                mw.treeView1.Enabled = false;

                string filepath = sfd.FileName.ToString();
                await _sc.DownloadRegularload(search, filepath);

                cmd.HideOpaqueLayer();
                MessageBox.Show("下载完成！");
                mw.treeView1.Enabled = true;
            }

            this.UpLoad.Visible = true;
        }

        private async void UpLoad_Click(object sender, EventArgs e)             //上传配额表
        {
            Search search = new Search
            {
                Year = int.Parse(this.IdYear.Text),
                CategoryId = int.Parse(((ComboboxItem)this.CategoryId.SelectedItem).Value),
            };

            OpenFileDialog ofd = new OpenFileDialog();
            //设置文件类型 
            ofd.Filter = "Microsoft Excel(*.xls) | *.xls";

            // 设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MainWindow mw = (MainWindow)this.MdiParent;
                cmd.ShowOpaqueLayer(this.ReportAnalysis, 125, true, true, "正在上传中，请稍候");
                mw.treeView1.Enabled = false;

                string filepath = ofd.FileName.ToString();
                var result = await _sc.UploadRegularload(search, filepath);

                cmd.HideOpaqueLayer();
                mw.treeView1.Enabled = true;
                if (result)
                {
                    MessageBox.Show("上传成功！");
                }
                else
                {
                    MessageBox.Show("上传失败, 请重新上传！");
                }
            }
            this.UpLoad.Visible = false;
        }
        #endregion


        #region 项目 和 工作量的修改和删除 绑定
        private void BindDataGridView1()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewLinkColumn edit = new DataGridViewLinkColumn();
            edit.Text = "修改";
            edit.Name = "LinkEdit";
            edit.HeaderText = "修改";
            edit.UseColumnTextForLinkValue = true;
            this.dataGridView1.Columns.Add(edit);

            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewLinkColumn delete = new DataGridViewLinkColumn();
            delete.Text = "删除";
            delete.Name = "LinkDelete";
            delete.HeaderText = "删除";
            delete.UseColumnTextForLinkValue = true;
            this.dataGridView1.Columns.Add(delete);
        }

        private void BindDataGridView3()
        {
            this.dataGridView3.AutoGenerateColumns = false;
            DataGridViewLinkColumn edit = new DataGridViewLinkColumn();
            edit.Text = "修改";
            edit.Name = "LinkEdit";
            edit.HeaderText = "修改";
            edit.UseColumnTextForLinkValue = true;
            this.dataGridView3.Columns.Add(edit);

            this.dataGridView3.AutoGenerateColumns = false;
            DataGridViewLinkColumn delete = new DataGridViewLinkColumn();
            delete.Text = "删除";
            delete.Name = "LinkDelete";
            delete.HeaderText = "删除";
            delete.UseColumnTextForLinkValue = true;
            this.dataGridView3.Columns.Add(delete);
        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 2)         //  修改当前部门信息
            {
                if (MessageBox.Show("确定要修改当前项目类别信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // BUG
                    string categoryName = ((ComboboxItem)this.SelectedCategory.SelectedItem).Text;
                    ContractProject project = UserHelper.ContractProjectList[e.RowIndex];
                    EditProject ep = new EditProject(categoryName, project, _sc);
                    ep.ShowDialog();
                    if (ep.DialogResult == DialogResult.OK)
                    {
                        this.BindProject(true);
                    }
                }
            }
            else if (e.ColumnIndex == 3)         //  删除当前部门信息
            {
                if (MessageBox.Show("确定要删除此项目类别信息？\n危险操作，请谨慎进行\n由于此项目类别下面可能有工作量集合，因此您的删除操作会将此项目下的所有工作量全部被删除，由此将引入很多不安全问题，请问您是否继续删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //MessageBox.Show(this.dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int projectId = Convert.ToInt32(this.dataGridView3.Rows[e.RowIndex].Cells[0].Value);
                    string result = _sc.DeleteProject(projectId);

                    if (result == Response.DELETE_PROJECT_SUCCESS.ToString())
                    {
                        MessageBox.Show("删除项目类别成功!", "提示", MessageBoxButtons.OK);
                        BindProject(true);

                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //else if (result == Response.DELETE_DEPARTMENT_EXIST_EMPLOYEE.ToString())
                    //{
                    //    MessageBox.Show("该部门下有人员存在，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    else
                    {
                        MessageBox.Show("删除项目类别失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 2)         //  修改当前部门信息
            {
                if (MessageBox.Show("确定要修改当前工作量类别信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // BUG
                    string categoryName = ((ComboboxItem)this.CateId.SelectedItem).Text;
                    string projectName = this.SelectedProject.Text;
                    ContractItem item = UserHelper.ContractItemList[e.RowIndex];
                    EditItem ei = new EditItem(categoryName, projectName, item, _sc);
                    ei.ShowDialog();
                    if (ei.DialogResult == DialogResult.OK)
                    {
                        this.BindItem(true);
                    }
                }
            }
            else if (e.ColumnIndex == 3)         //  删除当前部门信息
            {
                if (MessageBox.Show("确定要删除此工作量类别信息？\n危险操作，请谨慎进行\n请问您是否继续删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //MessageBox.Show(this.dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int itemId = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    string result = _sc.DeleteItem(itemId);

                    if (result == Response.DELETE_ITEM_SUCCESS.ToString())
                    {
                        MessageBox.Show("删除工作量成功!", "提示", MessageBoxButtons.OK);
                        this.BindItem(true);

                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //else if (result == Response.DELETE_DEPARTMENT_EXIST_EMPLOYEE.ToString())
                    //{
                    //    MessageBox.Show("该部门下有人员存在，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    else
                    {
                        MessageBox.Show("删除工作量失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

        }
        #endregion


        #region 工程、下载年份、项目、item的数据绑定
        private void BindCategory()
        {
            ComboboxItem[] item ={
               new ComboboxItem("界河航道例行养护工程","1"),
               new ComboboxItem("界河航道专项养护工程","2"),
               new ComboboxItem("内河航道例行养护工程","3"),
               new ComboboxItem("内河航道专项养护工程","4"),
                                };
            this.CategoryId.Items.AddRange(item);
            this.Category.Items.AddRange(item);
            //给项目管理 和工作量管理加载工程名
            this.SelectedCategory.Items.AddRange(item);
            this.CateId.Items.AddRange(item);

            
            


        }
        //费用 和报表要用
        private void BindIdYear()
        {
            int year = System.DateTime.Now.Year;

            // 默认绑定前一年，当年 和 下一年
            //  也就是默认只能当年会签单，可以补钱前一年和预签下一年的会签单
            this.IdYear.Items.Add((year - 1).ToString());
            this.IdYear.Items.Add(year.ToString());
            this.IdYear.Items.Add((year + 1).ToString());
            this.IdYear.SelectedIndex = 1;

            this.Year.Items.Add((year - 1).ToString());
            this.Year.Items.Add(year.ToString());
            this.Year.Items.Add((year + 1).ToString());
            this.Year.SelectedIndex = 1;
        }



        private void BindProject(bool isFlush)
        {
            if (UserHelper.ContractProjectList == null || isFlush == true)
            {
                //int categoryId = Convert.ToInt32(this.SelectedCategory.SelectedValue);
                int categoryId = int.Parse(((ComboboxItem)this.SelectedCategory.SelectedItem).Value);

                List<ContractProject> list = new List<ContractProject>();
                list = _sc.QueryContractProject(categoryId);
                UserHelper.ContractProjectList = list;

            }

            /// dataGridView3是Project的List
            this.dataGridView3.DataSource = UserHelper.ContractProjectList;

            ///  绑定
            this.SelectedProject.ValueMember = "Id";
            this.SelectedProject.DisplayMember = "Project";
            this.SelectedProject.DataSource = UserHelper.ContractProjectList;

        }

        private void BindItem(bool isFlush)
        {
            if (UserHelper.ContractItemList == null || isFlush == true)
            {

                int projectId = Convert.ToInt32(this.SelectedProject.SelectedValue);

                List<ContractItem> list = null;
                list = _sc.QueryContractItem(projectId);
                UserHelper.ContractItemList = list;
            }
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = UserHelper.ContractItemList;
        }

        #endregion


        #region 各类下拉框改变的事件
        private void SelectedCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboboxItem)this.SelectedCategory.SelectedItem == null)
            {
                MessageBox.Show("您没有选择任何条目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int categoryId = int.Parse(((ComboboxItem)this.SelectedCategory.SelectedItem).Value);

            List<ContractProject> list = new List<ContractProject>();
            list = _sc.QueryContractProject(categoryId);
            UserHelper.ContractProjectList = list;

            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.DataSource = UserHelper.ContractProjectList;
        }

        private void CateId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ComboboxItem)this.CateId.SelectedItem == null)
            {
                return;
            }
            int categoryId = int.Parse(((ComboboxItem)this.CateId.SelectedItem).Value);

            List<ContractProject> list = new List<ContractProject>();
            list = _sc.QueryContractProject(categoryId);
            UserHelper.ContractProjectList = list;


            this.SelectedProject.ValueMember = "Id";
            this.SelectedProject.DisplayMember = "Project";
            this.SelectedProject.DataSource = UserHelper.ContractProjectList;

        }

        private void SelectedProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedProject.SelectedValue == null)
            {
                return;
            }
            int projectId = Convert.ToInt32(this.SelectedProject.SelectedValue);

            List<ContractItem> list = null;
            list = _sc.QueryContractItem(projectId);
            UserHelper.ContractItemList = list;

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = UserHelper.ContractItemList;

        }

        #endregion

        #region 下载报表
        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.Category.SelectedItem == null)
            {
                MessageBox.Show("请选择希望下载的会签单类别信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Search search = new Search
            {
                Year = int.Parse(this.IdYear.Text),
                CategoryId = int.Parse(((ComboboxItem)this.Category.SelectedItem).Value),
            };

            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "Microsoft Excel(*.xls) | *.xls";

            // 设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            sfd.FileName = this.Category.Text + "年度费用分配表";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MainWindow mw = (MainWindow)this.MdiParent;
                cmd.ShowOpaqueLayer(this.ReportAnalysis, 125, true, true, "正在下载中，请稍候");
                mw.treeView1.Enabled = false;

                string filepath = sfd.FileName.ToString();
                await _sc.DownloadStatistic(search, filepath);

                cmd.HideOpaqueLayer();
                MessageBox.Show("下载完成！");
                mw.treeView1.Enabled = true;
            }
        }

        #endregion


        #region 添加工程 中的项目
        private void buttonAddProject_Click(object sender, EventArgs e)
        {
            if (this.SelectedCategory.SelectedItem == null)
            {
                MessageBox.Show("请填写完整的工作量名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            string projectname = this.pName.Text;
            //int categoryId = Convert.ToInt32(this.SelectedCategory.SelectedValue);
            int categoryId = int.Parse(((ComboboxItem)this.SelectedCategory.SelectedItem).Value);


            if (projectname == "")
            {
                MessageBox.Show("请填写工作量名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContractProject project = new ContractProject
            {
                CategoryId = categoryId,
                Project = projectname,
            };

            string result = _sc.InsertProject(project);

            if (result == Response.INSERT_PROJECT_SUCCESS.ToString())
            {
                MessageBox.Show("添加项目" + project.Project + "成功!");

                /////////////////////////////////////////
                BindProject(true);
                ///////////////////////////////////////
            }
            else
            {
                MessageBox.Show("添加项目" + project.Project + "失败!");
            }
        }
        #endregion

        #region 添加项目中的工作量 item
        private void AddItem_Click(object sender, EventArgs e)
        {
            if (this.SelectedProject.SelectedValue == null)
            {
                MessageBox.Show("请将信息填写完整!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string itemname = this.ItemName.Text;
            int projectid = Convert.ToInt32(this.SelectedProject.SelectedValue);

            if (this.ItemName.Text == "")
            {
                MessageBox.Show("请填写工作量名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContractItem item = new ContractItem
            {
                ProjectId = projectid,
                Item = itemname
            };

            string result = _sc.InsertItem(item);

            if (result == Response.INSERT_ITEM_SUCCESS.ToString())
            {
                MessageBox.Show("添加工作量" + itemname + "成功!");

                ///////
                BindItem(true);
                ///////
            }
            else
            {
                MessageBox.Show("添加工作量" + itemname + "失败!");
            }
        }

        #endregion

    }


}
