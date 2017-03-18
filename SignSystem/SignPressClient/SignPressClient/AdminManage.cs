using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SignPressClient.SignSocket;
using SignPressClient.Model;

namespace SignPressClient
{
    public partial class AdminManage : Form
    {
        SignSocketClient _sc;

        public AdminManage()
        {
            InitializeComponent();
        }

        public AdminManage(int selectedIndex, SignSocketClient sc)
            : this()
        {
            this.Admin.SelectedIndex = selectedIndex;
            _sc = sc;
        }

        #region 窗体加载
        private void DepartmentManage_Load(object sender, EventArgs e)            //窗体加载事件
        {
            //窗体加载时位datagridview绑定数据源
            if (UserHelper.SDepList == null || UserHelper.DepList  == null || UserHelper.MDepList == null)                   //绑定部门信息
            {
                BindGridViewDataSourece();
            }
            else
            {
                this.dataGridView1.DataSource = UserHelper.MDepList;

                this.SelectedDepartment.ValueMember = "Id";
                this.SelectedDepartment.DisplayMember = "Name";
                this.SelectedDepartment.DataSource = UserHelper.DepList;
            }

            if (UserHelper.sEmpList == null)                   //绑定人员信息
            {
                int departmentID = 0;
                BindEmployee(departmentID);
            }
            else
            {
                this.dataGridView3.AutoGenerateColumns = false;
                this.dataGridView3.DataSource = UserHelper.sEmpList;
            }


//2016-4-21将全部信息汇总到dataGridView2中
//2016-4-23  改为在contemp查询全部信息
            if (UserHelper.TempList == null)                  //绑定模板信息
            {
                BindGroupViewData2Source();
            }
            else
            {
                this.dataGridView2.AutoGenerateColumns = false;
                this.dataGridView2.DataSource = UserHelper.TempList;
            }
//2016-04-18
//2016-4-21修改原来的inside为绑定全部模版
            //if (UserHelper.TempAllList == null)                  //绑定内河模板信息
            //{
            //    BindGroupViewData4Source();
            //}
            //else
            //{
            //    this.dataGridView4.AutoGenerateColumns = false;
            //    this.dataGridView4.DataSource = UserHelper.TempAllList;
            //}

            BindDataGridView1Column();            //部门列表添加编辑列
            BindDataGridView3Column();            //人员列表添加编辑列
            BindDataGridView2Column();            //模板列表添加编辑列
//内河编辑列
           // BindDataGridView4Column();            //内河模板列表添加编辑列
        }

        #endregion


        #region 绑定信息
        private void BindEmployee(int departmentID)           //绑定部门员工
        {
            //List<Employee> emp = new List<Employee>();
            //emp = await _sc.QueryEmployeeByDepartmentID(departmentID); 

            //departmentID为0时返回所有信息
            List<Employee> emp = _sc.QueryEmployeeByDepartmentID(departmentID);

            if (emp != null)
            {
                UserHelper.EmpList = emp;
            }
            List<sEmployee> semp = new List<sEmployee>();
            if (emp != null)
            {
                foreach (Employee e in emp)
                {
                    sEmployee se = new sEmployee();
                    se.Id = e.Id;
                    se.Name = e.Name;
                    se.Position = e.Position;
                    se.UserName = e.User.Username;
                    se.Department = e.Department.Name;
                    se.IsAdmin = e.IsAdmin == 0 ? "否" : "是";
                    se.CanSubmit = e.CanSubmit == 0 ? "否" : "是";
                    se.CanSign = e.CanSign == 0 ? "否" : "是";
                    se.CanStatistic = e.CanStatistic == 0 ? "否" : "是";
                    semp.Add(se);
                }
                UserHelper.sEmpList = semp;
                this.dataGridView3.AutoGenerateColumns = false;
                this.dataGridView3.DataSource = semp;
            }
        }


        public void BindGridViewDataSourece()              //异步获取部门列表数据并绑定
        {
            // Modify by gatieme at 2015-08-26 14:12
            //List<Department> list = new List<Department>();
            List<SDepartment> sdepList = _sc.QuerySDepartment();

            if (sdepList != null)
            {
                List<Department> depList = new List<Department>();
                List<MDepartment> mdepList = new List<MDepartment>();

                //  
                //SDepartment department = null;
                foreach (SDepartment department in sdepList)
                {
                    depList.Add(department.ToDepartment());
                    mdepList.Add(department.ToMDepartment());
                }
                //原来这里有断点
                UserHelper.DepList = depList;
                UserHelper.SDepList = sdepList;
                UserHelper.MDepList = mdepList;

                this.dataGridView1.DataSource = mdepList;

                this.SelectedDepartment.ValueMember = "Id";
                this.SelectedDepartment.DisplayMember = "Name";
                this.SelectedDepartment.DataSource = depList;
            }
        }


        //2016-4-23  在数据库contemp大表中查询所有模版信息
        private async void BindGroupViewData2Source()                        //获取所以模板信息
        {
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

        #endregion


        #region 增加修改 删除
        //人员中的增删
        private void BindDataGridView3Column()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewLinkColumn modify = new DataGridViewLinkColumn();
            modify.Text = "修改";
            modify.Name = "LinkModify";
            modify.HeaderText = "修改";
            modify.UseColumnTextForLinkValue = true;
            this.dataGridView3.Columns.Add(modify);

            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewLinkColumn resetPwd = new DataGridViewLinkColumn();
            resetPwd.Text = "重置密码";
            resetPwd.Name = "LinkResetPwd";
            resetPwd.HeaderText = "重置密码";
            resetPwd.UseColumnTextForLinkValue = true;
            this.dataGridView3.Columns.Add(resetPwd);

            this.dataGridView1.AutoGenerateColumns = false;
            DataGridViewLinkColumn delete = new DataGridViewLinkColumn();
            delete.Text = "删除";
            delete.Name = "LinkDelete";
            delete.HeaderText = "删除";
            delete.UseColumnTextForLinkValue = true;
            this.dataGridView3.Columns.Add(delete);
        }

        //部门中的增删
        private void BindDataGridView1Column()
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

        //模版中的增删
        private void BindDataGridView2Column()
        {
            //  添加修改模版信息
            this.dataGridView2.AutoGenerateColumns = false;
            DataGridViewLinkColumn edit = new DataGridViewLinkColumn();
            edit.Text = "修改";
            edit.Name = "LinkEdit";
            edit.HeaderText = "修改";
            edit.UseColumnTextForLinkValue = true;
            this.dataGridView2.Columns.Add(edit);

            //  删除修改模版信息
            this.dataGridView2.AutoGenerateColumns = false;
            DataGridViewLinkColumn delete = new DataGridViewLinkColumn();
            delete.Text = "删除";
            delete.Name = "LinkDelete";
            delete.HeaderText = "删除";
            delete.UseColumnTextForLinkValue = true;
            this.dataGridView2.Columns.Add(delete);

        }

        #endregion


        #region 各种按钮事件  

        #region 模版管理
        /********************
        * 2016-4-17        *
        * 添加内河模例行版按钮      就要把type 设置为10   2016-4-23 success
        ********************/
        private void btn_AddInsideContemp_Click(object sender, EventArgs e)
        {
            AddInsideContemp add = new AddInsideContemp(10,_sc);
            add.ShowDialog();
            BindGroupViewData2Source();
        }

        /********************
       * 2016-4-23        *
       * 添加内河专项模例行版按钮      就要把type 设置为11    与内河例行的模版是一样的   2016-4-23 success
       ********************/
        private void btn_AddContempInsideZX_Click(object sender, EventArgs e)
        {
            AddInsideContemp add = new AddInsideContemp(11,_sc);
            add.ShowDialog();
            BindGroupViewData2Source();
        }
        /********************
     * 2016-4-21        *
     * 添加界河例行模版按钮      type 20
     ********************/

        private void btn_AddJHContemp_Click(object sender, EventArgs e)
        {
            //添加界河例行模版事件
            AddContempJHLX add = new AddContempJHLX(20,_sc);
            add.ShowDialog();
            BindGroupViewData2Source();
        }
  

        /// <summary>
        /// 添加会签单模版的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        /// 这个是界河专项的模版   type 21
        private void AddTemp_Click_1(object sender, EventArgs e)
        {
            AddConTemp add = new AddConTemp(21,_sc);
            add.ShowDialog();
            //创建完模版后重新刷新管理模版界面的模版信息
            BindGroupViewData2Source();
        }
        #endregion

        #region 人员管理

        #region 添加人员
        private async void button1_Click(object sender, EventArgs e)                //添加人员
        {
            //  首先进行容错
            if (this.Position.Text.Trim() == ""
            || this.UserName.Text.Trim() == ""
            || this.UserPassword.Text.Trim() == "")
            {
                MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.CanSign.Checked
            && this.pictureBox1.ImageLocation == null)
            {
                MessageBox.Show("签字人需要提交签字图片！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (UserHelper.EmpList.Where(o => o.User.Username == this.UserName.Text.Trim()).ToList().Count > 0)
            {
                MessageBox.Show("员工信息已经存在，无法再次插入\n请检查用户名是否与他人重复！！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Employee emp = new Employee();
            emp.Name = this.eName.Text.Trim();

            Department d = new Department();
            d.Id = Convert.ToInt32(this.SelectedDepartment.SelectedValue);
            d.Name = this.SelectedDepartment.SelectedItem.ToString();
            emp.Department = d;

            emp.Position = this.Position.Text.Trim();
            User u = new User();
            u.Username = this.UserName.Text.Trim();
            u.Password = this.UserPassword.Text.Trim();
            emp.User = u;

            if (this.CanSubmit.Checked)
            {
                emp.CanSubmit = 1;
            }
            else
            {
                emp.CanSubmit = 0;
            }

            if (this.CanSign.Checked)
            {
                emp.CanSign = 1;
            }
            else
            {
                emp.CanSign = 0;
            }

            if (this.CanAdmin.Checked)
            {
                emp.IsAdmin = 1;
            }
            else
            {
                emp.IsAdmin = 0;
            }

            if (this.CanStatistic.Checked)
            {
                emp.CanStatistic = 1;
            }
            else
            {
                emp.CanStatistic = 0;
            }
            //发送添加人员信息    添加人员成功之后才将图片信息通过以文件的形式发给服务器
            int id = _sc.InsertEmployee(emp);

            if (id == -2)
            {
                MessageBox.Show("员工信息已经存在，无法再次插入\n请检查用户名是否与他人重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (id == -1)
            {
                MessageBox.Show("添加人员失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (id == 0)
            {
                MessageBox.Show("服务器连接中断！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("添加人员成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (this.CanSign.Checked && this.pictureBox1.ImageLocation != null)
                {
                    //将图片信息发送给服务器
                    await _sc.UploadPicture(id, this.pictureBox1.ImageLocation);
                }
                //情况数据
                ClearEmployeeInfo();
            }
            BindEmployee(0);
        }

        #endregion

        private void ClearEmployeeInfo()
        {
            this.eName.Clear();
            this.Position.Clear();
            this.UserName.Text = "";
            this.UserPassword.Text = "";
            this.CanSubmit.Checked = false;
            if (this.CanSign.Checked)
            {
                this.PicturrName.Clear();
                this.PicturrName.Visible = false;
                this.pictureBox1.Visible = false;
            }
            this.CanSign.Checked = false;
            this.CanAdmin.Checked = false;
            this.CanStatistic.Checked = false;
            
        }
        //选择图片
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请上传签名图片";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofd.FileName;
                int position = filepath.LastIndexOf("\\");
                string fileName = filepath.Substring(position + 1);
                this.PicturrName.Text = fileName;
                //显示图片
                this.pictureBox1.ImageLocation = filepath;
                this.pictureBox1.Visible = true;
            }
        }

        private void CanSign_Click(object sender, EventArgs e)
        {
            if (this.CanSign.Checked)
            {
                this.PicturrName.Visible = true;
                this.UploadButton.Visible = true;
            }
            else
            {
                this.PicturrName.Visible = false;
                this.UploadButton.Visible = false;
            }
        }
        #endregion

        #region  添加部门
        private void AddDepartment_Click(object sender, EventArgs e)                  //添加部门
        {
            //  modify by gatieme at 2015-08-08 16:20
            //  为部门添加部门简称
            string departmentName = this.DepartmentName.Text.Trim();
            string departmentShortCall = this.textBoxDepartmentShortCall.Text.Trim();

            if (departmentName == "")
            {
                MessageBox.Show("请填写部门名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (departmentShortCall == "")
            {
                MessageBox.Show("请填写部门简称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UserHelper.DepList.Where(o => o.Name == departmentName).ToList().Count > 0)
            {
                MessageBox.Show("该部门的部门名称与其他部门重复!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (UserHelper.DepList.Where(o => o.ShortCall == departmentShortCall).ToList().Count > 0)
            {
                MessageBox.Show("该部门的部门简称与其他部门重复!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Department department = new Department { Id = -1, Name = departmentName, ShortCall = departmentShortCall };
            SDepartment sdepartment = new SDepartment();
            sdepartment.Id = -1;
            sdepartment.Name = departmentName;
            sdepartment.ShortCall = departmentShortCall;
            if (this.CanBoundary.Checked)
            {
                sdepartment.CanBoundary = 1;
            }
            else
            {
                sdepartment.CanBoundary = 0;
            }

            if (this.CanInLand.Checked)
            {
                sdepartment.CanInland = 1;
            }
            else
            {
                sdepartment.CanInland = 0;
            }


            //string result = _sc.InsertDepartment(department);
            string result = _sc.InsertSDepartment(sdepartment);

            //if (result == Response.INSERT_DEPARTMENT_SUCCESS.ToString())
            //{
            if (result == Response.INSERT_SDEPARTMENT_SUCCESS.ToString())
            {
                MessageBox.Show("添加" + departmentName + "部门成功!");
                ///////
                BindGridViewDataSourece();
                ///////
                ClearDepartmentInfo();
            }
            //}
            else
            {
                MessageBox.Show("添加" + departmentName + "部门失败!");
            }

        }

        private void ClearDepartmentInfo()
        {
            this.DepartmentName.Text= "";
            this.textBoxDepartmentShortCall.Text = "";
            this.CanBoundary.Checked = false;
            this.CanInLand.Checked = false;
        }
        #endregion

      

        #endregion


        #region 表格单击事件

        #region 模版中的事件
        /*2016-4-20 在DataGridView中加入单元格点击事件
 *2016-4-21 修改inside 为All 的功能
 * 通过Id判断是什么类型的模版
                 * 1是内河模版 contempinside
                 * 2是界河例行 contempLi
                 * 3是界河专项 contempZhuan(就是现在的contemp)
                 */
//2016-4-23修改  通过type来判断模版的类型
        /// <summary>
        /// 模版管理操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)    //模板列表表格中的点击事件
        {
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
                if (Type/10 == 1)
                {
                    //显示内河模版   内河模版是一样的所以判断一次就行  之后就通过id号来给显示的窗口填充数据
                    //弹出显示已创建模版信息窗口    2016-4-23 success
                    ViewContempInside vcp = new ViewContempInside(Id, _sc,Type);
                    vcp.ShowDialog();
                }

                if (Type == 20)
                {
                    //显示界河例行模
                    ViewContempJHLX vcp = new ViewContempJHLX(Id, _sc);
                    vcp.ShowDialog();

                }
                if (Type == 21)
                {
                    //显示界河专项模版      2016-4-23 success
                    ViewConTemp vcp = new ViewConTemp(Id, _sc);
                    vcp.Show();
                }
            }
            else if (e.ColumnIndex == 4)            //  修改模版
            {
                if (MessageBox.Show("确定要修改此模板信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Id = Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                    int Type = Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[2].Value);

                    if (Type / 10 == 1)
                    {
                        //MessageBox.Show("修改内河模版");
                        //修改内河
                        EditConTempInside ecp = new EditConTempInside(Id, _sc,Type);
                        ecp.ShowDialog();
                        if (ecp.DialogResult == DialogResult.OK)
                        {
                            //MessageBox.Show("修改模版信息成功!");
                            ecp.Close();
                            BindGroupViewData2Source();
                        }
                    }

                    if (Type == 20)
                    {
                        //修改界河例行模
                        EditConTempJHLX ecp = new EditConTempJHLX(Id, _sc);
                        ecp.ShowDialog();
                        if (ecp.DialogResult == DialogResult.OK)
                        {
                            //MessageBox.Show("修改模版信息成功!");
                            ecp.Close();
                            BindGroupViewData2Source();
                        }

                    }
                    if (Type == 21)
                    {
                        //修改界河专项模版      
                        EditConTemp ecp = new EditConTemp(Id, _sc);
                        ecp.ShowDialog();
                        if (ecp.DialogResult == DialogResult.OK)
                        {
                            ecp.Close();
                            BindGroupViewData2Source();
                        }
                    }
                    

                    
                }
            }
            else if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("确定要删除此模板信息？\n请谨慎选择\n删除模版会导致以次模版为基础的所有的会签单全部被删除请谨慎选择", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (MessageBox.Show("重复确认？您正在进行一项危险操作，请谨慎选择", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int Id = Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                        //通过Id来进行删除
                        string result = _sc.DeleteContractTemplate(Id);
                        if (result == Response.DELETE_CONTRACT_TEMPLATE_SUCCESS.ToString())
                        {
                            MessageBox.Show("删除模板成功!", "提示", MessageBoxButtons.OK);
                        }
                        else if (result == "服务器连接中断")
                        {
                            MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (result == Response.DELETE_CONTRACT_TEMPLATE_EXIST_CONTRACT.ToString())
                        {
                            MessageBox.Show("此模板已经有会签单使用，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("删除模板失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        BindGroupViewData2Source();
                    }
                }
            }
        }
        #endregion

        #region 部门中的事件
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 5)         //  修改当前部门信息
            {
                if (MessageBox.Show("确定要修改当前部门信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SDepartment sdepartment = UserHelper.SDepList[e.RowIndex];
                    EditDepartment ed = new EditDepartment(sdepartment, _sc);
                    ed.ShowDialog();
                    if (ed.DialogResult == DialogResult.OK)
                    {
                        BindGridViewDataSourece();          //  绑定部门源
                        
                        BindEmployee(0);                    //  绑定员工信息
                    }
                }
            }
            else if (e.ColumnIndex == 6)         //  删除当前部门信息
            {
                if (MessageBox.Show("确定要删除此部门？\n危险操作，请谨慎进行\n由于部门下面可能有员工，因此您的删除操作会将部门下的所有员工全部被删除，由此将引入很多不安全问题，请问您是否继续删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    string result = _sc.DeleteDepartment(Id);

                    if (result == Response.DELETE_DEPARTMENT_SUCCESS.ToString())
                    {
                        MessageBox.Show("删除部门成功!", "提示", MessageBoxButtons.OK);
                        BindGridViewDataSourece();
                        BindEmployee(0);                    //  绑定员工信息

                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == Response.DELETE_DEPARTMENT_EXIST_EMPLOYEE.ToString())
                    {
                        MessageBox.Show("该部门下有人员存在，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("删除部门失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        #endregion

        #region 人员中的事件
        //原来此处有断点
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 9)         // 修改员工
            {
                if (MessageBox.Show("确定要修改此员工信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //int Id = Convert.ToInt32(this.dataGridView3.Rows[e.RowIndex].Cells[0].Value);
                    //string result = await _sc.ModifyEmployee(Id);
                    int row = Convert.ToInt32(e.RowIndex);              // 获取当前的信息行
                    Employee employee = UserHelper.EmpList[row];        // 获取数据源中第row个员工的信息
                    EditEmployee editEmployee = new EditEmployee(employee, _sc);
                    editEmployee.ShowDialog();


                    if (editEmployee.DialogResult == DialogResult.OK)
                    {
                        editEmployee.Close();

                        BindEmployee(0);
                    }
                }
            }
            else if (e.ColumnIndex == 10)  // 重置密码
            {
                String Name = this.dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show("确定重置员工" + Name + "的密码？员工密码将被重置为111", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Id = Convert.ToInt32(this.dataGridView3.Rows[e.RowIndex].Cells[0].Value);
                    String Username = this.dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                    String Password = "111";
                    User user = new User { Username = Username, Password = Password };
                    string result = _sc.ModifyEmployeePassword(user);      //  重置员工密码

                    if (result == Response.MODIFY_EMP_PWD_SUCCESS.ToString())
                    {
                        MessageBox.Show("重置密码成功!", "提示", MessageBoxButtons.OK);
                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,重置密码失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("重置密码成功失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    BindEmployee(0);
                }
            }
            else if (e.ColumnIndex == 11)         // 删除员工
            {
                if (MessageBox.Show("确定要删除此人员？\n危险操作，谨慎进行\n由于该员工可能关联了很多会签单模版以及会签单信息，请谨慎操作", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Id = Convert.ToInt32(this.dataGridView3.Rows[e.RowIndex].Cells[0].Value);
                    string result = _sc.DeleteEmployee(Id);

                    if (result == Response.DELETE_EMPLOYEE_SUCCESS.ToString())
                    {
                        MessageBox.Show("删除人员成功!", "提示", MessageBoxButtons.OK);
                    }
                    else if (result == "服务器连接中断")
                    {
                        MessageBox.Show("服务器连接中断,删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == Response.DELETE_EMPLOYEE_EXIST_CONTRACT.ToString())
                    {
                        MessageBox.Show("该人员存在会签单信息中，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (result == Response.DELETE_EMPLOYEE_EXIST_CONTEMP.ToString())
                    {
                        MessageBox.Show("该人员存在会签单模版信息中，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("删除人员失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    BindEmployee(0);
                }
            }
        }
        #endregion

        #endregion










    }
}
