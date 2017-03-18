﻿using System;
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
    public partial class EditDepartment : Form
    {
        ////   此处应该注意，这里的dpartment跟底层Uderhelper的数据是同一个数据域
        private Department m_department;

        private SDepartment m_sdepartment;

        private SignSocketClient _sc;

        public EditDepartment()
        {
            InitializeComponent();
        }


        public EditDepartment(SDepartment sdepartment, SignSocketClient sc)
            :this()
        {
            this.m_sdepartment = sdepartment;
            this._sc = sc;
        }



        private void EditDepartment_Load(object sender, EventArgs e)
        {
            this.textBoxId.Text = this.m_sdepartment.Id.ToString();
            this.textBoxName.Text = this.m_sdepartment.Name;
            this.textBoxShortCall.Text = this.m_sdepartment.ShortCall;

            if (this.m_sdepartment.CanBoundary == 1)
            {
                this.CanBoundary.Checked = true;
            }

            if (this.m_sdepartment.CanInland == 1)
            {
                this.CanInLand.Checked = true;
            }

          
        }

        private void ModifyDepartment_Click(object sender, EventArgs e)
        {
            if (this.CheckIntegrity() == true)
            { 

                ////   此处应该注意，这里的dpartment跟底层Uderhelper的数据是同一个数据域
                this.m_sdepartment.Name = this.textBoxName.Text.Trim();       // 
                this.m_sdepartment.ShortCall = this.textBoxShortCall.Text.Trim();
                if (this.CanBoundary.Checked)
                {
                    this.m_sdepartment.CanBoundary = 1;
                }
                else
                {
                    this.m_sdepartment.CanBoundary = 0;
                }

                if (this.CanInLand.Checked)
                {
                    this.m_sdepartment.CanInland = 1;
                }
                else
                {
                    this.m_sdepartment.CanInland = 0;
                }

               

                string result=_sc.ModifySDepartment(this.m_sdepartment);

                //if (result == Response.MODIFY_DEPARTMENT_SUCCESS.ToString())
               if (result == Response.MODIFY_SDEPARTMENT_SUCCESS.ToString())
               {
                 MessageBox.Show("修改部门成功!", "提示", MessageBoxButtons.OK);
                 //this.m_department.Name = departmentname;
                 this.DialogResult = DialogResult.OK;
               }
                else if (result == "服务器连接中断")
                {
                    MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("修改部门失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #region 检查输入信息的完整性和合法性
        private bool CheckIntegrity()
        {
            //this.m_department.Name = this.textBoxName.Text.Trim();
            //  modify by gatieme at 2015-08-08 16:09
            //  为部门添加部门简称
            string departmentName = this.textBoxName.Text.Trim();
            string departmentShortCall = this.textBoxShortCall.Text.Trim();

            if (departmentName == "")
            {
                MessageBox.Show("请填写部门名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (departmentShortCall == "")
            {
                MessageBox.Show("请填写部门简称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            /*if (departmentName == this.m_sdepartment.Name && departmentShortCall == this.m_sdepartment.ShortCall)
            {
                MessageBox.Show("您未对此部门做任何修改!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (UserHelper.SDepList.Where(o => (o.Name == departmentName && o.ShortCall == departmentShortCall)).ToList().Count > 0)
            {
                MessageBox.Show("该部门已经存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //  一下两种情况仅发生在修改只某个部门的名称或者简称时（只需改一项）
            else if (UserHelper.SDepList.Where(o => (o.Id != this.m_sdepartment.Id && o.Name == departmentName)).ToList().Count > 0)
            {
                MessageBox.Show("该部门名称与其他部门的部门名称重复!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (UserHelper.SDepList.Where(o => (o.Id != this.m_sdepartment.Id && o.ShortCall == departmentShortCall)).ToList().Count > 0)
            {
                MessageBox.Show("该部门简称与其他部门的部门简称重复!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
                    */
            return true;
        }
        #endregion
    }
}
