namespace SignPressClient
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("部门管理");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("人员管理");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("模板管理");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("管理员", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("提交方案");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("审核中");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("已拒绝");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("已通过");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("提交管理", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("待办列表");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("已办列表");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("签字管理", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("查看个人信息");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("修改密码");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("用户管理", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("费用分配");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("报表分析");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("项目类别管理");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("工作量管理");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("业务及报表管理", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19});
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainnotifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMaximize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.treeView1 = new SignPressClient.BaseTreeView();
            this.panel2.SuspendLayout();
            this.mainnotifyContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(803, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(583, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 24);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.ContextMenuStrip = this.mainnotifyContextMenuStrip;
            this.mainNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotifyIcon.Icon")));
            this.mainNotifyIcon.Text = "黑龙江省航道局会签单";
            this.mainNotifyIcon.Visible = true;
            this.mainNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainNotifyIcon_MouseDoubleClick);
            // 
            // mainnotifyContextMenuStrip
            // 
            this.mainnotifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMinimize,
            this.toolStripMenuItemMaximize,
            this.toolStripMenuItemMNormal,
            this.toolStripMenuItemQuit});
            this.mainnotifyContextMenuStrip.Name = "mainnotifyContextMenuStrip";
            this.mainnotifyContextMenuStrip.Size = new System.Drawing.Size(113, 92);
            // 
            // toolStripMenuItemMinimize
            // 
            this.toolStripMenuItemMinimize.Name = "toolStripMenuItemMinimize";
            this.toolStripMenuItemMinimize.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemMinimize.Text = "最小化";
            this.toolStripMenuItemMinimize.Click += new System.EventHandler(this.toolStripMenuItemMinimize_Click);
            // 
            // toolStripMenuItemMaximize
            // 
            this.toolStripMenuItemMaximize.Name = "toolStripMenuItemMaximize";
            this.toolStripMenuItemMaximize.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemMaximize.Text = "最大化";
            this.toolStripMenuItemMaximize.Click += new System.EventHandler(this.toolStripMenuItemMaximize_Click);
            // 
            // toolStripMenuItemMNormal
            // 
            this.toolStripMenuItemMNormal.Name = "toolStripMenuItemMNormal";
            this.toolStripMenuItemMNormal.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemMNormal.Text = "还原";
            this.toolStripMenuItemMNormal.Click += new System.EventHandler(this.toolStripMenuItemNormal_Click);
            // 
            // toolStripMenuItemQuit
            // 
            this.toolStripMenuItemQuit.Name = "toolStripMenuItemQuit";
            this.toolStripMenuItemQuit.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemQuit.Text = "退出";
            this.toolStripMenuItemQuit.Click += new System.EventHandler(this.toolStripMenuItemQuit_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("华文彩云", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(256, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(742, 50);
            this.label2.TabIndex = 5;
            this.label2.Text = "欢迎使用黑龙江省航道局会签系统";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.HotTracking = true;
            this.treeView1.ItemHeight = 23;
            this.treeView1.Location = new System.Drawing.Point(0, 24);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Department";
            treeNode1.Text = "部门管理";
            treeNode2.Name = "Employee";
            treeNode2.Text = "人员管理";
            treeNode3.Name = "Templete";
            treeNode3.Text = "模板管理";
            treeNode4.Name = "Admin";
            treeNode4.Text = "管理员";
            treeNode5.Name = "SubmitPlan";
            treeNode5.Text = "提交方案";
            treeNode6.Name = "Pendding";
            treeNode6.Text = "审核中";
            treeNode7.Name = "Rejected";
            treeNode7.Text = "已拒绝";
            treeNode8.Name = "Passed";
            treeNode8.Text = "已通过";
            treeNode9.Name = "SubmitManage";
            treeNode9.Text = "提交管理";
            treeNode10.Name = "TodoList";
            treeNode10.Text = "待办列表";
            treeNode11.Name = "DoneList";
            treeNode11.Text = "已办列表";
            treeNode12.Name = "SignPressManage";
            treeNode12.Text = "签字管理";
            treeNode13.Name = "ViewPersonal";
            treeNode13.Text = "查看个人信息";
            treeNode14.Name = "ChangePassWord";
            treeNode14.Text = "修改密码";
            treeNode15.Name = "UserManage";
            treeNode15.Text = "用户管理";
            treeNode16.Name = "CostAllocate";
            treeNode16.Text = "费用分配";
            treeNode17.Name = "ReportAnalysis";
            treeNode17.Text = "报表分析";
            treeNode18.Name = "ProjectManage";
            treeNode18.Text = "项目类别管理";
            treeNode19.Name = "ItemManage";
            treeNode19.Text = "工作量管理";
            treeNode20.Name = "ReportManage";
            treeNode20.Text = "业务及报表管理";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode9,
            treeNode12,
            treeNode15,
            treeNode20});
            this.treeView1.Size = new System.Drawing.Size(237, 495);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(803, 519);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主界面";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.mainnotifyContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public BaseTreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip mainnotifyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMinimize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMaximize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMNormal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemQuit;
        private System.Windows.Forms.Label label2;
    }
}