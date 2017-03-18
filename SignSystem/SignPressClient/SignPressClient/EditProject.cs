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

namespace SignPressClient
{
    public partial class EditProject : Form
    {
        private SignSocketClient _sc;


        private string m_categoryName;
        public String CategoryName
        {
            get { return this.m_categoryName; }
            set { this.m_categoryName = value; }
        }

        private ContractProject m_project;
        public ContractProject Project
        {
            get { return this.m_project; }
            set { this.m_project = value; }
        }
        public EditProject()
        {
            InitializeComponent();
        }

        public EditProject(string categoryName, ContractProject project, SignSocketClient sc)
        : this()
        {
            this.m_categoryName = categoryName;
            this.m_project = project;
            _sc = sc;
        }

        private void EditProject_Load(object sender, EventArgs e)
        {
            this.textBoxCategoryName.Text = this.m_categoryName;

            this.textBoxProjectId.Text = this.m_project.Id.ToString();
            this.textBoxProjectName.Text = this.m_project.Project.ToString();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            string projectName = this.textBoxProjectName.Text.Trim();
            if (projectName == "")
            {
                MessageBox.Show("请将工作量信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (projectName == this.m_project.Project)
            {
                MessageBox.Show("你未修改任何信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.m_project.Project = projectName;

            try
            {
                string result = _sc.ModifyProject(this.m_project);
                if (result == Response.MODIFY_PROJECT_SUCCESS.ToString())
                {
                    MessageBox.Show("修改工作量信息成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (result == "服务器连接中断")
                {
                    MessageBox.Show("服务器连接中断,修改员工信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("修改工作量信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
