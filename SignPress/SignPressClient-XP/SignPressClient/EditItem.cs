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
    public partial class EditItem : Form
    {
        private SignSocketClient _sc;

        private string m_categoryName;
        public String CategoryName
        {
            get { return this.m_categoryName; }
            set { this.m_categoryName = value; }
        }

        private string m_projectName;
        public String ProjectName
        {
            get { return this.m_projectName; }
            set { this.m_projectName = value; }
        }

        private ContractItem m_item;
        public ContractItem Item
        {
            get { return this.m_item; }
            set { this.m_item = value; }
        }


        public EditItem()
        {
            InitializeComponent();
        }

        public EditItem(String categoryName, String projectName, ContractItem item, SignSocketClient sc)
        :this()
        {   
            this.m_categoryName = categoryName;
            this.m_projectName = projectName;
            
            this.m_item = item;

            _sc = sc;
        }

        private void EditItem_Load(object sender, EventArgs e)
        {

            this.textBoxProjectName.Text = this.m_projectName.ToString();
            this.textBoxCategoryName.Text = this.m_categoryName.ToString();

            this.textBoxItemId.Text = this.m_item.Id.ToString();
            this.textBoxItemName.Text = this.m_item.Item.ToString();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            string itemName = this.textBoxItemName.Text.Trim();
            if (itemName == "")
            {
                MessageBox.Show("请将工作量信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (itemName == this.m_item.Item)
            {
                MessageBox.Show("你未修改任何信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.m_item.Item = itemName;
           
            try
            {

                string result = _sc.ModifyItem(this.m_item);
                if (result == Response.MODIFY_ITEM_SUCCESS.ToString())
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
