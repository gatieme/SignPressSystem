namespace SignPressClient
{
    partial class ReportManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ReportAnalysis = new System.Windows.Forms.TabControl();
            this.CostAllocate = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.CategoryId = new System.Windows.Forms.ComboBox();
            this.DownLoad = new System.Windows.Forms.Button();
            this.IdYear = new System.Windows.Forms.ComboBox();
            this.UpLoad = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Report = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Category = new System.Windows.Forms.ComboBox();
            this.Year = new System.Windows.Forms.ComboBox();
            this.ProjectManage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SelectedCategory = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAddProject = new System.Windows.Forms.Button();
            this.ItemManage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ItemName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SelectedProject = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CateId = new System.Windows.Forms.ComboBox();
            this.labelDepartmentShortCall = new System.Windows.Forms.Label();
            this.AddItem = new System.Windows.Forms.Button();
            this.ReportAnalysis.SuspendLayout();
            this.CostAllocate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.Report.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.ProjectManage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel3.SuspendLayout();
            this.ItemManage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "报表及业务管理";
            // 
            // ReportAnalysis
            // 
            this.ReportAnalysis.Controls.Add(this.CostAllocate);
            this.ReportAnalysis.Controls.Add(this.Report);
            this.ReportAnalysis.Controls.Add(this.ProjectManage);
            this.ReportAnalysis.Controls.Add(this.ItemManage);
            this.ReportAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportAnalysis.Location = new System.Drawing.Point(0, 25);
            this.ReportAnalysis.Name = "ReportAnalysis";
            this.ReportAnalysis.SelectedIndex = 0;
            this.ReportAnalysis.Size = new System.Drawing.Size(774, 506);
            this.ReportAnalysis.TabIndex = 2;
            // 
            // CostAllocate
            // 
            this.CostAllocate.Controls.Add(this.panel1);
            this.CostAllocate.Location = new System.Drawing.Point(4, 22);
            this.CostAllocate.Name = "CostAllocate";
            this.CostAllocate.Padding = new System.Windows.Forms.Padding(3);
            this.CostAllocate.Size = new System.Drawing.Size(766, 480);
            this.CostAllocate.TabIndex = 0;
            this.CostAllocate.Text = "费用分配";
            this.CostAllocate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 474);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 474);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划费用分配";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Aqua;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.CategoryId);
            this.panel4.Controls.Add(this.DownLoad);
            this.panel4.Controls.Add(this.IdYear);
            this.panel4.Controls.Add(this.UpLoad);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(754, 160);
            this.panel4.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(586, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "注：先下载分配表模板表，填写完整后上传！（如果希望修改请下载后重新上传）";
            // 
            // CategoryId
            // 
            this.CategoryId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoryId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CategoryId.FormattingEnabled = true;
            this.CategoryId.Location = new System.Drawing.Point(122, 43);
            this.CategoryId.Name = "CategoryId";
            this.CategoryId.Size = new System.Drawing.Size(169, 29);
            this.CategoryId.TabIndex = 10;
            // 
            // DownLoad
            // 
            this.DownLoad.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DownLoad.Location = new System.Drawing.Point(172, 80);
            this.DownLoad.Name = "DownLoad";
            this.DownLoad.Size = new System.Drawing.Size(75, 32);
            this.DownLoad.TabIndex = 5;
            this.DownLoad.Text = "下   载";
            this.DownLoad.UseVisualStyleBackColor = true;
            this.DownLoad.Click += new System.EventHandler(this.DownLoad_Click);
            // 
            // IdYear
            // 
            this.IdYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IdYear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IdYear.FormattingEnabled = true;
            this.IdYear.Location = new System.Drawing.Point(122, 8);
            this.IdYear.Name = "IdYear";
            this.IdYear.Size = new System.Drawing.Size(81, 29);
            this.IdYear.TabIndex = 9;
            // 
            // UpLoad
            // 
            this.UpLoad.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpLoad.Location = new System.Drawing.Point(362, 80);
            this.UpLoad.Name = "UpLoad";
            this.UpLoad.Size = new System.Drawing.Size(75, 32);
            this.UpLoad.TabIndex = 6;
            this.UpLoad.Text = "上   传";
            this.UpLoad.UseVisualStyleBackColor = true;
            this.UpLoad.Visible = false;
            this.UpLoad.Click += new System.EventHandler(this.UpLoad_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "会签单类别：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "年         份：";
            // 
            // Report
            // 
            this.Report.Controls.Add(this.groupBox2);
            this.Report.Location = new System.Drawing.Point(4, 22);
            this.Report.Name = "Report";
            this.Report.Padding = new System.Windows.Forms.Padding(3);
            this.Report.Size = new System.Drawing.Size(766, 480);
            this.Report.TabIndex = 1;
            this.Report.Text = "报表分析";
            this.Report.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 474);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "各统计表下载";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Aqua;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.Category);
            this.panel5.Controls.Add(this.Year);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 25);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(754, 140);
            this.panel5.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "年   份：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(185, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 12;
            this.button1.Text = "下   载";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 21);
            this.label6.TabIndex = 9;
            this.label6.Text = "会签单类别：";
            // 
            // Category
            // 
            this.Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Category.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Category.FormattingEnabled = true;
            this.Category.Location = new System.Drawing.Point(135, 43);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(169, 29);
            this.Category.TabIndex = 11;
            // 
            // Year
            // 
            this.Year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Year.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Year.FormattingEnabled = true;
            this.Year.Location = new System.Drawing.Point(135, 7);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(81, 29);
            this.Year.TabIndex = 10;
            // 
            // ProjectManage
            // 
            this.ProjectManage.Controls.Add(this.groupBox3);
            this.ProjectManage.Controls.Add(this.panel3);
            this.ProjectManage.Location = new System.Drawing.Point(4, 22);
            this.ProjectManage.Name = "ProjectManage";
            this.ProjectManage.Padding = new System.Windows.Forms.Padding(3);
            this.ProjectManage.Size = new System.Drawing.Size(766, 480);
            this.ProjectManage.TabIndex = 3;
            this.ProjectManage.Text = "项目类别管理";
            this.ProjectManage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(3, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(760, 381);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "项目类别信息";
            // 
            // dataGridView3
            // 
            this.dataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProjectName});
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 25);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(754, 353);
            this.dataGridView3.TabIndex = 0;
            this.dataGridView3.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "项目类别编号";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 130;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "Project";
            this.ProjectName.HeaderText = "项目类别名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 150;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Aqua;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pName);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.SelectedCategory);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.buttonAddProject);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(760, 93);
            this.panel3.TabIndex = 2;
            // 
            // pName
            // 
            this.pName.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pName.Location = new System.Drawing.Point(430, 17);
            this.pName.Name = "pName";
            this.pName.Size = new System.Drawing.Size(132, 25);
            this.pName.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(337, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 19);
            this.label9.TabIndex = 6;
            this.label9.Text = "项目类别名称";
            // 
            // SelectedCategory
            // 
            this.SelectedCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedCategory.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectedCategory.FormattingEnabled = true;
            this.SelectedCategory.Location = new System.Drawing.Point(143, 15);
            this.SelectedCategory.Name = "SelectedCategory";
            this.SelectedCategory.Size = new System.Drawing.Size(121, 29);
            this.SelectedCategory.TabIndex = 5;
            this.SelectedCategory.SelectedIndexChanged += new System.EventHandler(this.SelectedCategory_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(50, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 19);
            this.label8.TabIndex = 4;
            this.label8.Text = "所属工程类别";
            // 
            // buttonAddProject
            // 
            this.buttonAddProject.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddProject.Location = new System.Drawing.Point(615, 59);
            this.buttonAddProject.Name = "buttonAddProject";
            this.buttonAddProject.Size = new System.Drawing.Size(87, 27);
            this.buttonAddProject.TabIndex = 3;
            this.buttonAddProject.Text = "添    加";
            this.buttonAddProject.UseVisualStyleBackColor = true;
            this.buttonAddProject.Click += new System.EventHandler(this.buttonAddProject_Click);
            // 
            // ItemManage
            // 
            this.ItemManage.Controls.Add(this.groupBox4);
            this.ItemManage.Controls.Add(this.panel2);
            this.ItemManage.Location = new System.Drawing.Point(4, 22);
            this.ItemManage.Name = "ItemManage";
            this.ItemManage.Padding = new System.Windows.Forms.Padding(3);
            this.ItemManage.Size = new System.Drawing.Size(766, 480);
            this.ItemManage.TabIndex = 2;
            this.ItemManage.Text = "工作量类别管理";
            this.ItemManage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(3, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(760, 381);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "工作量类别信息";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Item});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(754, 353);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "工作量类别编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "工作量类别名称";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 170;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aqua;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ItemName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.SelectedProject);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.CateId);
            this.panel2.Controls.Add(this.labelDepartmentShortCall);
            this.panel2.Controls.Add(this.AddItem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 93);
            this.panel2.TabIndex = 1;
            // 
            // ItemName
            // 
            this.ItemName.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ItemName.Location = new System.Drawing.Point(598, 12);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(132, 25);
            this.ItemName.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(518, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 19);
            this.label7.TabIndex = 9;
            this.label7.Text = "工作量名称";
            // 
            // SelectedProject
            // 
            this.SelectedProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedProject.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectedProject.FormattingEnabled = true;
            this.SelectedProject.Location = new System.Drawing.Point(365, 10);
            this.SelectedProject.Name = "SelectedProject";
            this.SelectedProject.Size = new System.Drawing.Size(121, 29);
            this.SelectedProject.TabIndex = 8;
            this.SelectedProject.SelectedIndexChanged += new System.EventHandler(this.SelectedProject_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(272, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 19);
            this.label10.TabIndex = 7;
            this.label10.Text = "所属项目类别";
            // 
            // CateId
            // 
            this.CateId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CateId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CateId.FormattingEnabled = true;
            this.CateId.Location = new System.Drawing.Point(121, 10);
            this.CateId.Name = "CateId";
            this.CateId.Size = new System.Drawing.Size(121, 29);
            this.CateId.TabIndex = 6;
            this.CateId.SelectedIndexChanged += new System.EventHandler(this.CateId_SelectedIndexChanged);
            // 
            // labelDepartmentShortCall
            // 
            this.labelDepartmentShortCall.AutoSize = true;
            this.labelDepartmentShortCall.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDepartmentShortCall.Location = new System.Drawing.Point(28, 15);
            this.labelDepartmentShortCall.Name = "labelDepartmentShortCall";
            this.labelDepartmentShortCall.Size = new System.Drawing.Size(87, 19);
            this.labelDepartmentShortCall.TabIndex = 4;
            this.labelDepartmentShortCall.Text = "所属工程类别";
            // 
            // AddItem
            // 
            this.AddItem.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddItem.Location = new System.Drawing.Point(656, 59);
            this.AddItem.Name = "AddItem";
            this.AddItem.Size = new System.Drawing.Size(87, 27);
            this.AddItem.TabIndex = 3;
            this.AddItem.Text = "添    加";
            this.AddItem.UseVisualStyleBackColor = true;
            this.AddItem.Click += new System.EventHandler(this.AddItem_Click);
            // 
            // ReportManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 531);
            this.Controls.Add(this.ReportAnalysis);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(202, 0);
            this.Name = "ReportManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ReportManage";
            this.Load += new System.EventHandler(this.ReportManage_Load);
            this.ReportAnalysis.ResumeLayout(false);
            this.CostAllocate.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.Report.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ProjectManage.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ItemManage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl ReportAnalysis;
        private System.Windows.Forms.TabPage CostAllocate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage Report;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Category;
        private System.Windows.Forms.ComboBox Year;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage ItemManage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelDepartmentShortCall;
        private System.Windows.Forms.Button AddItem;
        private System.Windows.Forms.TabPage ProjectManage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonAddProject;
        private System.Windows.Forms.ComboBox SelectedCategory;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox SelectedProject;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CateId;
        private System.Windows.Forms.TextBox pName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.TextBox ItemName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox CategoryId;
        private System.Windows.Forms.Button DownLoad;
        private System.Windows.Forms.ComboBox IdYear;
        private System.Windows.Forms.Button UpLoad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
    }
}