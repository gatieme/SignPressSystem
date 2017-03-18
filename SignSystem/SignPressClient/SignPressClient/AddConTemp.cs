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
    public partial class AddConTemp : Form
    {
        SignSocketClient _sc;
        int Type;
        public AddConTemp()
        {
            InitializeComponent();
        }

        public AddConTemp(int type,SignSocketClient sc)
            : this()
        {
            _sc = sc;
            Type = type;
        }
        #region 数据加载
        //界河例行有5 个栏目  12个签字人
        private void AddConTemp_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AutoScroll = true;
            this.HScroll = true;
            this.VScroll = true;

            this.Sign1Level.SelectedIndex = 1;
            this.Sign2Level.SelectedIndex = 2;
            this.Sign3Level.SelectedIndex = 3;
            this.Sign4Level.SelectedIndex = 4;
            this.Sign5Level.SelectedIndex = 5;
            this.Sign6Level.SelectedIndex = 6;
            this.Sign7Level.SelectedIndex = 7;
            this.Sign8Level.SelectedIndex = 8;
            this.Sign9Level.SelectedIndex = 9;
            this.Sign10Level.SelectedIndex = 10;
            this.Sign11Level.SelectedIndex = 11;
            this.Sign12Level.SelectedIndex = 12;

            this.View1.SelectedIndex = 0;
            this.View2.SelectedIndex = 0;
            this.View3.SelectedIndex = 0;
            this.View4.SelectedIndex = 0;
            this.View5.SelectedIndex = 0;
            this.View6.SelectedIndex = 0;
            this.View7.SelectedIndex = 0;
            this.View8.SelectedIndex = 0;
            this.View9.SelectedIndex = 0;
            this.View10.SelectedIndex = 0;
            this.View11.SelectedIndex = 0;
            this.View12.SelectedIndex = 0;

            this.Download1.SelectedIndex = 0;
            this.Download2.SelectedIndex = 0;
            this.Download3.SelectedIndex = 0;
            this.Download4.SelectedIndex = 0;
            this.Download5.SelectedIndex = 0;
            this.Download6.SelectedIndex = 0;
            this.Download7.SelectedIndex = 0;
            this.Download8.SelectedIndex = 0;
            this.Download9.SelectedIndex = 0;
            this.Download10.SelectedIndex = 0;
            this.Download11.SelectedIndex = 0;
            this.Download12.SelectedIndex = 0;


            BindDepartment(UserHelper.DepList);               //绑定部门信息
        }

        private void BindDepartment(List<Department> list)
        {
            if (list != null)
            {
                this.SignDep1.ValueMember = "Id";
                this.SignDep1.DisplayMember = "Name";
                this.SignDep1.DataSource = new List<Department>(list);

                this.SignDep2.ValueMember = "Id";
                this.SignDep2.DisplayMember = "Name";
                this.SignDep2.DataSource = new List<Department>(list);
               
                this.SignDep3.ValueMember = "Id";
                this.SignDep3.DisplayMember = "Name";
                this.SignDep3.DataSource = new List<Department>(list);
                
                this.SignDep4.ValueMember = "Id";
                this.SignDep4.DisplayMember = "Name";
                this.SignDep4.DataSource = new List<Department>(list);
               
                this.SignDep5.ValueMember = "Id";
                this.SignDep5.DisplayMember = "Name";
                this.SignDep5.DataSource = new List<Department>(list);
                
                this.SignDep6.ValueMember = "Id";
                this.SignDep6.DisplayMember = "Name";
                this.SignDep6.DataSource = new List<Department>(list);
               
                this.SignDep7.ValueMember = "Id";
                this.SignDep7.DisplayMember = "Name";
                this.SignDep7.DataSource = new List<Department>(list);
                
                this.SignDep8.ValueMember = "Id";
                this.SignDep8.DisplayMember = "Name";
                this.SignDep8.DataSource = new List<Department>(list);

                this.SignDep9.ValueMember = "Id";
                this.SignDep9.DisplayMember = "Name";
                this.SignDep9.DataSource = new List<Department>(list);

                this.SignDep10.ValueMember = "Id";
                this.SignDep10.DisplayMember = "Name";
                this.SignDep10.DataSource = new List<Department>(list);

                this.SignDep11.ValueMember = "Id";
                this.SignDep11.DisplayMember = "Name";
                this.SignDep11.DataSource = new List<Department>(list);

                this.SignDep12.ValueMember = "Id";
                this.SignDep12.DisplayMember = "Name";
                this.SignDep12.DataSource = new List<Department>(list);

            }
        }

        private List<Employee> BindEmployee(int departmentID)            //绑定员工信息
        {
            List<Employee> emp = new List<Employee>();
            emp = UserHelper.EmpList.Where(o => o.Department.Id == departmentID).ToList();

            return emp;
        }

        private void BindEmployeeControl(ComboBox comboBox, List<Employee> list)     //绑定控件人员信息
        {
            comboBox.ValueMember = "Id";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = list;

        }
        #endregion


        #region  各文本框获得焦点与失去焦点的事件
        private void Column1_TextChanged(object sender, EventArgs e)
        {
            this.label4.Visible = false;
        }

        private void Column1_Validated(object sender, EventArgs e)
        {
            if (this.Column1.Text.Trim() == "")
            {
                this.label4.Visible = true;
            }
            else
            {
                this.label4.Visible = false;
            }
        }

        private void Column2_TextChanged(object sender, EventArgs e)
        {
            this.label5.Visible = false;
        }

        private void Column2_Validated(object sender, EventArgs e)
        {
            if (this.Column2.Text.Trim() == "")
            {
                this.label5.Visible = true;
            }
            else
            {
                this.label5.Visible = false;
            }
        }

        private void Column3_TextChanged(object sender, EventArgs e)
        {
            this.label6.Visible = false;
        }

        private void Column3_Validated(object sender, EventArgs e)
        {
            if (this.Column3.Text.Trim() == "")
            {
                this.label6.Visible = true;
            }
            else
            {
                this.label6.Visible = false;
            }
        }

        private void Column4_TextChanged(object sender, EventArgs e)
        {
            this.label7.Visible = false;
        }

        private void Column4_Validated(object sender, EventArgs e)
        {
            if (this.Column4.Text.Trim() == "")
            {
                this.label7.Visible = true;
            }
            else
            {
                this.label7.Visible = false;
            }
        }

        private void Column5_TextChanged(object sender, EventArgs e)
        {
            this.label8.Visible = false;
        }

        private void Column5_Validated(object sender, EventArgs e)
        {
            if (this.Column5.Text.Trim() == "")
            {
                this.label8.Visible = true;
            }
            else
            {
                this.label8.Visible = false;
            }
        }

        private void Sign1_TextChanged(object sender, EventArgs e)
        {
            this.label9.Visible = false;
        }

        private void Sign1_Validated(object sender, EventArgs e)
        {
            if (this.Sign1.Text.Trim() == "")
            {
                this.label9.Visible = true;
            }
            else
            {
                this.label9.Visible = false;
            }
        }

        private void Sign2_TextChanged(object sender, EventArgs e)
        {
            this.label10.Visible = false;
        }

        private void Sign2_Validated(object sender, EventArgs e)
        {
            if (this.Sign2.Text.Trim() == "")
            {
                this.label10.Visible = true;
            }
            else
            {
                this.label10.Visible = false;
            }
        }

        private void Sign3_TextChanged(object sender, EventArgs e)
        {
            this.label11.Visible = false;
        }

        private void Sign3_Validated(object sender, EventArgs e)
        {
            if (this.Sign3.Text.Trim() == "")
            {
                this.label11.Visible = true;
            }
            else
            {
                this.label11.Visible = false;
            }
        }

        private void Sign4_TextChanged(object sender, EventArgs e)
        {
            this.label12.Visible = false;
        }

        private void Sign4_Validated(object sender, EventArgs e)
        {
            if (this.Sign4.Text.Trim() == "")
            {
                this.label12.Visible = true;
            }
            else
            {
                this.label12.Visible = false;
            }
        }

        private void Sign5_TextChanged(object sender, EventArgs e)
        {
            this.label13.Visible = false;
        }

        private void Sign5_Validated(object sender, EventArgs e)
        {
            if (this.Sign5.Text.Trim() == "")
            {
                this.label13.Visible = true;
            }
            else
            {
                this.label13.Visible = false;
            }
        }

        private void Sign6_TextChanged(object sender, EventArgs e)
        {
            this.label14.Visible = false;
        }

        private void Sign6_Validated(object sender, EventArgs e)
        {
            if (this.Sign6.Text.Trim() == "")
            {
                this.label14.Visible = true;
            }
            else
            {
                this.label14.Visible = false;
            }
        }

        private void Sign7_TextChanged(object sender, EventArgs e)
        {
            this.label15.Visible = false;
        }

        private void Sign7_Validated(object sender, EventArgs e)
        {
            if (this.Sign7.Text.Trim() == "")
            {
                this.label15.Visible = true;
            }
            else
            {
                this.label15.Visible = false;
            }
        }

        private void Sign8_TextChanged(object sender, EventArgs e)
        {
            this.label16.Visible = false;
        }

        private void Sign8_Validated(object sender, EventArgs e)
        {
            if (this.Sign8.Text.Trim() == "")
            {
                this.label16.Visible = true;
            }
            else
            {
                this.label16.Visible = false;
            }
        }

        private void Sign9_TextChanged(object sender, EventArgs e)
        {
            this.label2.Visible = false;
        }

        private void Sign9_Validated(object sender, EventArgs e)
        {
            if (this.Sign9.Text.Trim() == "")
            {
                this.label2.Visible = true;
            }
            else
            {
                this.label2.Visible = false;
            }
        }

        private void Sign10_TextChanged(object sender, EventArgs e)
        {
            this.label3.Visible = false;
        }

        private void Sign10_Validated(object sender, EventArgs e)
        {
            if (this.Sign10.Text.Trim() == "")
            {
                this.label3.Visible = true;
            }
            else
            {
                this.label3.Visible = false;
            }
        }

        private void Sign11_TextChanged(object sender, EventArgs e)
        {
            this.label17.Visible = false;
        }

        private void Sign11_Validated(object sender, EventArgs e)
        {
            if (this.Sign11.Text.Trim() == "")
            {
                this.label17.Visible = true;
            }
            else
            {
                this.label17.Visible = false;
            }
        }

        private void Sign12_TextChanged(object sender, EventArgs e)
        {
            this.label18.Visible = false;
        }

        private void Sign12_Validated(object sender, EventArgs e)
        {
            if (this.Sign12.Text.Trim() == "")
            {
                this.label18.Visible = true;
            }
            else
            {
                this.label18.Visible = false;
            }
        }




        #endregion


        #region 各部门控件选择值发生改变事件
        private void SignDep1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep1.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer1, list);
        }

        private void SignDep2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep2.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer2, list);
        }

        private void SignDep3_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep3.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer3, list);
        }

        private void SignDep4_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep4.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer4, list);
        }

        private void SignDep5_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep5.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer5, list);
        }

        private void SignDep6_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep6.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer6, list);
        }

        private void SignDep7_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep7.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer7, list);
        }

        private void SignDep8_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep8.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer8, list);
        }

        private void SignDep9_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep9.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer9, list);
        }

        private void SignDep10_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep10.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer10, list);
        }

        private void SignDep11_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep11.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer11, list);
        }

        private void SignDep12_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Employee> list = new List<Employee>();
            int departmentID = Convert.ToInt32(this.SignDep12.SelectedValue.ToString());
            list = BindEmployee(departmentID);
            BindEmployeeControl(this.SignPer12, list);
        }
        #endregion


        #region 单击保存按钮
        
        private  void button1_Click(object sender, EventArgs e)                    
        {
            if (this.CheckIntegrity() == false)
            {
                return;
            }

            Templete temp = new Templete();
            SignatureTemplate signtemp1 = new SignatureTemplate();
            SignatureTemplate signtemp2 = new SignatureTemplate();
            SignatureTemplate signtemp3 = new SignatureTemplate();
            SignatureTemplate signtemp4 = new SignatureTemplate();
            SignatureTemplate signtemp5 = new SignatureTemplate();
            SignatureTemplate signtemp6 = new SignatureTemplate();
            SignatureTemplate signtemp7 = new SignatureTemplate();
            SignatureTemplate signtemp8 = new SignatureTemplate();
            SignatureTemplate signtemp9 = new SignatureTemplate();
            SignatureTemplate signtemp10 = new SignatureTemplate();
            SignatureTemplate signtemp11 = new SignatureTemplate();
            SignatureTemplate signtemp12 = new SignatureTemplate();
            List<string> list = new List<string>();
            List<SignatureTemplate> tlist = new List<SignatureTemplate>();

//原来此处有断点
            // 会签单的名称
            temp.Name = this.ConTempName.Text.ToString();
            temp.Type = Type;
            
            // 添加前5行的会签单基本信息
            list.Add(this.Column1.Text.ToString());
            list.Add(this.Column2.Text.ToString());
            list.Add(this.Column3.Text.ToString());
            list.Add(this.Column4.Text.ToString());
            list.Add(this.Column5.Text.ToString());
            temp.ColumnNames = list;

            ///  添加后12个签字人的信息基本信息
            signtemp1.SignInfo = this.Sign1.Text.ToString();
            Employee emp1 = new Employee();
            emp1.Name = this.SignPer1.SelectedText;
            emp1.Id = Convert.ToInt32(this.SignPer1.SelectedValue.ToString());
            Department dep1 = new Department();
            dep1.Id = Convert.ToInt32(this.SignDep1.SelectedValue.ToString());
            dep1.Name = this.SignDep1.SelectedText;
            emp1.Department = dep1;
            signtemp1.SignEmployee = emp1;
            signtemp1.SignLevel = Convert.ToInt32(this.Sign1Level.SelectedItem.ToString());
            signtemp1.CanView = this.View1.SelectedIndex;
            signtemp1.CanDownload = this.Download1.SelectedIndex;
            tlist.Add(signtemp1);

            signtemp2.SignInfo = this.Sign2.Text.ToString();
            Employee emp2 = new Employee();
            emp2.Name = this.SignPer2.SelectedText;
            emp2.Id = Convert.ToInt32(this.SignPer2.SelectedValue.ToString());
            Department dep2 = new Department();
            dep2.Id = Convert.ToInt32(this.SignDep2.SelectedValue.ToString());
            dep2.Name = this.SignDep2.SelectedText;
            emp2.Department = dep2;
            signtemp2.SignEmployee = emp2;
            signtemp2.SignLevel = Convert.ToInt32(this.Sign2Level.SelectedItem.ToString());
            signtemp2.CanView = this.View2.SelectedIndex;
            signtemp2.CanDownload = this.Download2.SelectedIndex;
            tlist.Add(signtemp2);

            signtemp3.SignInfo = this.Sign3.Text.ToString();
            Employee emp3 = new Employee();
            emp3.Name = this.SignPer3.SelectedText;
            emp3.Id = Convert.ToInt32(this.SignPer3.SelectedValue.ToString());
            Department dep3 = new Department();
            dep3.Id = Convert.ToInt32(this.SignDep3.SelectedValue.ToString());
            dep3.Name = this.SignDep3.SelectedText;
            emp3.Department = dep3;
            signtemp3.SignEmployee = emp3;
            signtemp3.SignLevel = Convert.ToInt32(this.Sign3Level.SelectedItem.ToString());
            signtemp3.CanView = this.View3.SelectedIndex;
            signtemp3.CanDownload = this.Download3.SelectedIndex;
            tlist.Add(signtemp3);

            signtemp4.SignInfo = this.Sign4.Text.ToString();
            Employee emp4 = new Employee();
            emp4.Name = this.SignPer4.SelectedText;
            emp4.Id = Convert.ToInt32(this.SignPer4.SelectedValue.ToString());
            Department dep4 = new Department();
            dep4.Id = Convert.ToInt32(this.SignDep4.SelectedValue.ToString());
            dep4.Name = this.SignDep4.SelectedText;
            emp4.Department = dep4;
            signtemp4.SignEmployee = emp4;
            signtemp4.SignLevel = Convert.ToInt32(this.Sign4Level.SelectedItem.ToString());
            signtemp4.CanView = this.View4.SelectedIndex;
            signtemp4.CanDownload = this.Download4.SelectedIndex;
            tlist.Add(signtemp4);

            signtemp5.SignInfo = this.Sign5.Text.ToString();
            Employee emp5 = new Employee();
            emp5.Name = this.SignPer5.SelectedText;
            emp5.Id = Convert.ToInt32(this.SignPer5.SelectedValue.ToString());
            Department dep5 = new Department();
            dep5.Id = Convert.ToInt32(this.SignDep5.SelectedValue.ToString());
            dep5.Name = this.SignDep5.SelectedText;
            emp5.Department = dep5;
            signtemp5.SignEmployee = emp5;
            signtemp5.SignLevel = Convert.ToInt32(this.Sign5Level.SelectedItem.ToString());
            signtemp5.CanView = this.View5.SelectedIndex;
            signtemp5.CanDownload = this.Download5.SelectedIndex;
            tlist.Add(signtemp5);

            signtemp6.SignInfo = this.Sign6.Text.ToString();
            Employee emp6 = new Employee();
            emp6.Name = this.SignPer6.SelectedText;
            emp6.Id = Convert.ToInt32(this.SignPer6.SelectedValue.ToString());
            Department dep6 = new Department();
            dep6.Id = Convert.ToInt32(this.SignDep6.SelectedValue.ToString());
            dep6.Name = this.SignDep6.SelectedText;
            emp6.Department = dep6;
            signtemp6.SignEmployee = emp6;
            signtemp6.SignLevel = Convert.ToInt32(this.Sign6Level.SelectedItem.ToString());
            signtemp6.CanView = this.View6.SelectedIndex;
            signtemp6.CanDownload = this.Download6.SelectedIndex;
            tlist.Add(signtemp6);

            signtemp7.SignInfo = this.Sign7.Text.ToString();
            Employee emp7 = new Employee();
            emp7.Name = this.SignPer7.SelectedText;
            emp7.Id = Convert.ToInt32(this.SignPer7.SelectedValue.ToString());
            Department dep7 = new Department();
            dep7.Id = Convert.ToInt32(this.SignDep7.SelectedValue.ToString());
            dep7.Name = this.SignDep7.SelectedText;
            emp7.Department = dep7;
            signtemp7.SignEmployee = emp7;
            signtemp7.SignLevel = Convert.ToInt32(this.Sign7Level.SelectedItem.ToString());
            signtemp7.CanView = this.View7.SelectedIndex;
            signtemp7.CanDownload = this.Download7.SelectedIndex;
            tlist.Add(signtemp7);

            signtemp8.SignInfo = this.Sign8.Text.ToString();
            Employee emp8 = new Employee();
            emp8.Name = this.SignPer8.SelectedText;
            emp8.Id = Convert.ToInt32(this.SignPer8.SelectedValue.ToString());
            Department dep8 = new Department();
            dep8.Id = Convert.ToInt32(this.SignDep8.SelectedValue.ToString());
            dep8.Name = this.SignDep8.SelectedText;
            emp8.Department = dep8;
            signtemp8.SignEmployee = emp8;
            signtemp8.SignLevel = Convert.ToInt32(this.Sign8Level.SelectedItem.ToString());
            signtemp8.CanView = this.View8.SelectedIndex;
            signtemp8.CanDownload = this.Download8.SelectedIndex;
            tlist.Add(signtemp8);

            signtemp9.SignInfo = this.Sign9.Text.ToString();
            Employee emp9 = new Employee();
            emp9.Name = this.SignPer9.SelectedText;
            emp9.Id = Convert.ToInt32(this.SignPer9.SelectedValue.ToString());
            Department dep9 = new Department();
            dep9.Id = Convert.ToInt32(this.SignDep9.SelectedValue.ToString());
            dep9.Name = this.SignDep9.SelectedText;
            emp9.Department = dep9;
            signtemp9.SignEmployee = emp9;
            signtemp9.SignLevel = Convert.ToInt32(this.Sign9Level.SelectedItem.ToString());
            signtemp9.CanView = this.View9.SelectedIndex;
            signtemp9.CanDownload = this.Download9.SelectedIndex;
            tlist.Add(signtemp9);


            signtemp10.SignInfo = this.Sign10.Text.ToString();
            Employee emp10 = new Employee();
            emp10.Name = this.SignPer10.SelectedText;
            emp10.Id = Convert.ToInt32(this.SignPer10.SelectedValue.ToString());
            Department dep10 = new Department();
            dep10.Id = Convert.ToInt32(this.SignDep10.SelectedValue.ToString());
            dep10.Name = this.SignDep10.SelectedText;
            emp10.Department = dep10;
            signtemp10.SignEmployee = emp10;
            signtemp10.SignLevel = Convert.ToInt32(this.Sign10Level.SelectedItem.ToString());
            signtemp10.CanView = this.View10.SelectedIndex;
            signtemp10.CanDownload = this.Download10.SelectedIndex;
            tlist.Add(signtemp10);

            signtemp11.SignInfo = this.Sign11.Text.ToString();
            Employee emp11 = new Employee();
            emp11.Name = this.SignPer11.SelectedText;
            emp11.Id = Convert.ToInt32(this.SignPer11.SelectedValue.ToString());
            Department dep11 = new Department();
            dep11.Id = Convert.ToInt32(this.SignDep11.SelectedValue.ToString());
            dep11.Name = this.SignDep11.SelectedText;
            emp11.Department = dep11;
            signtemp11.SignEmployee = emp11;
            signtemp11.SignLevel = Convert.ToInt32(this.Sign11Level.SelectedItem.ToString());
            signtemp11.CanView = this.View11.SelectedIndex;
            signtemp11.CanDownload = this.Download11.SelectedIndex;
            tlist.Add(signtemp11);

            signtemp12.SignInfo = this.Sign12.Text.ToString();
            Employee emp12 = new Employee();
            emp12.Name = this.SignPer12.SelectedText;
            emp12.Id = Convert.ToInt32(this.SignPer12.SelectedValue.ToString());
            Department dep12 = new Department();
            dep12.Id = Convert.ToInt32(this.SignDep12.SelectedValue.ToString());
            dep12.Name = this.SignDep12.SelectedText;
            emp12.Department = dep12;
            signtemp12.SignEmployee = emp12;
            signtemp12.SignLevel = Convert.ToInt32(this.Sign12Level.SelectedItem.ToString());
            signtemp12.CanView = this.View12.SelectedIndex;
            signtemp12.CanDownload = this.Download12.SelectedIndex;
            tlist.Add(signtemp12);

            temp.SignDatas = tlist;
//原来此处有断点
            string result =  _sc.AddConTemplete(temp);

            if (result == Response.INSERT_CONTRACT_TEMPLATE_SUCCESS.ToString())
            {
                MessageBox.Show("添加模板" + this.ConTempName.Text + "成功！");
               
            }
            else
            {
                MessageBox.Show("添加模板失败！ 请重新添加");
      
            }
            this.Close();
        }

        #endregion

        #region 检查输入信息的完整性和合法性
        private bool CheckIntegrity()
        {
            //MessageBox.Show("请仔细检查是否有两处签字栏让同一个人签字,", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (this.ConTempName.Text.ToString() == "")
            {
                MessageBox.Show("请输入会前单模版的名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 否则检查前5个栏目的信息
            if (this.Column1.Text.ToString() == ""
             || this.Column2.Text.ToString() == ""
             || this.Column3.Text.ToString() == ""
             || this.Column4.Text.ToString() == ""
             || this.Column5.Text.ToString() == "")
            {
                MessageBox.Show("请将栏目填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign1.Text.ToString() == ""
            || this.SignPer1.SelectedValue.ToString() == ""
            || this.SignDep1.SelectedValue.ToString() == ""
            || this.Sign1Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人1的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign2.Text.ToString() == ""
            || this.SignPer2.SelectedValue.ToString() == ""
            || this.SignDep2.SelectedValue.ToString() == ""
            || this.Sign2Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人2的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign3.Text.ToString() == ""
            || this.SignPer3.SelectedValue.ToString() == ""
            || this.SignDep3.SelectedValue.ToString() == ""
            || this.Sign3Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人3的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign4.Text.ToString() == ""
            || this.SignPer4.SelectedValue.ToString() == ""
            || this.SignDep4.SelectedValue.ToString() == ""
            || this.Sign4Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人4的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign5.Text.ToString() == ""
            || this.SignPer5.SelectedValue.ToString() == ""
            || this.SignDep5.SelectedValue.ToString() == ""
            || this.Sign5Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人5的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign6.Text.ToString() == ""
            || this.SignPer6.SelectedValue.ToString() == ""
            || this.SignDep6.SelectedValue.ToString() == ""
            || this.Sign6Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人2的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.Sign7.Text.ToString() == ""
            || this.SignPer7.SelectedValue.ToString() == ""
            || this.SignDep7.SelectedValue.ToString() == ""
            || this.Sign7Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人7的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign8.Text.ToString() == ""
            || this.SignPer8.SelectedValue.ToString() == ""
            || this.SignDep8.SelectedValue.ToString() == ""
            || this.Sign8Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人8的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign9.Text.ToString() == ""
           || this.SignPer9.SelectedValue.ToString() == ""
           || this.SignDep9.SelectedValue.ToString() == ""
           || this.Sign9Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人9的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign10.Text.ToString() == ""
           || this.SignPer10.SelectedValue.ToString() == ""
           || this.SignDep10.SelectedValue.ToString() == ""
           || this.Sign10Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人9的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign11.Text.ToString() == ""
           || this.SignPer11.SelectedValue.ToString() == ""
           || this.SignDep11.SelectedValue.ToString() == ""
           || this.Sign11Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人11的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Sign12.Text.ToString() == ""
          || this.SignPer12.SelectedValue.ToString() == ""
          || this.SignDep12.SelectedValue.ToString() == ""
          || this.Sign12Level.SelectedItem.ToString() == "请选择签字顺序")
            {
                MessageBox.Show("请将签字人12的信息填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;

        }
        #endregion

       

       
    }
}
