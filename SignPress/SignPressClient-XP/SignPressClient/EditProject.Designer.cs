namespace SignPressClient
{
    partial class EditProject
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
            this.buttonModify = new System.Windows.Forms.Button();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.textBoxProjectId = new System.Windows.Forms.TextBox();
            this.labelProjectId = new System.Windows.Forms.Label();
            this.labelCategoryName = new System.Windows.Forms.Label();
            this.textBoxCategoryName = new System.Windows.Forms.TextBox();
            this.buttonCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonModify
            // 
            this.buttonModify.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonModify.Location = new System.Drawing.Point(68, 198);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(87, 34);
            this.buttonModify.TabIndex = 33;
            this.buttonModify.Text = "修改";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProjectName.Location = new System.Drawing.Point(43, 138);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(90, 21);
            this.labelProjectName.TabIndex = 32;
            this.labelProjectName.Text = "项目名称：";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxProjectName.Location = new System.Drawing.Point(149, 135);
            this.textBoxProjectName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(171, 29);
            this.textBoxProjectName.TabIndex = 31;
            // 
            // textBoxProjectId
            // 
            this.textBoxProjectId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxProjectId.Location = new System.Drawing.Point(145, 30);
            this.textBoxProjectId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxProjectId.Name = "textBoxProjectId";
            this.textBoxProjectId.ReadOnly = true;
            this.textBoxProjectId.Size = new System.Drawing.Size(171, 29);
            this.textBoxProjectId.TabIndex = 30;
            // 
            // labelProjectId
            // 
            this.labelProjectId.AutoSize = true;
            this.labelProjectId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProjectId.Location = new System.Drawing.Point(42, 32);
            this.labelProjectId.Name = "labelProjectId";
            this.labelProjectId.Size = new System.Drawing.Size(90, 21);
            this.labelProjectId.TabIndex = 29;
            this.labelProjectId.Text = "项目编号：";
            // 
            // labelCategoryName
            // 
            this.labelCategoryName.AutoSize = true;
            this.labelCategoryName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCategoryName.Location = new System.Drawing.Point(46, 82);
            this.labelCategoryName.Name = "labelCategoryName";
            this.labelCategoryName.Size = new System.Drawing.Size(90, 21);
            this.labelCategoryName.TabIndex = 28;
            this.labelCategoryName.Text = "所属工程：";
            // 
            // textBoxCategoryName
            // 
            this.textBoxCategoryName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCategoryName.Location = new System.Drawing.Point(145, 79);
            this.textBoxCategoryName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCategoryName.Name = "textBoxCategoryName";
            this.textBoxCategoryName.ReadOnly = true;
            this.textBoxCategoryName.Size = new System.Drawing.Size(171, 29);
            this.textBoxCategoryName.TabIndex = 27;
            // 
            // buttonCancle
            // 
            this.buttonCancle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCancle.Location = new System.Drawing.Point(220, 198);
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.Size = new System.Drawing.Size(83, 35);
            this.buttonCancle.TabIndex = 34;
            this.buttonCancle.Text = "取消";
            this.buttonCancle.UseVisualStyleBackColor = true;
            this.buttonCancle.Click += new System.EventHandler(this.buttonCancle_Click);
            // 
            // EditProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 262);
            this.Controls.Add(this.buttonCancle);
            this.Controls.Add(this.buttonModify);
            this.Controls.Add(this.labelProjectName);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.textBoxProjectId);
            this.Controls.Add(this.labelProjectId);
            this.Controls.Add(this.labelCategoryName);
            this.Controls.Add(this.textBoxCategoryName);
            this.Name = "EditProject";
            this.Text = "项目信息";
            this.Load += new System.EventHandler(this.EditProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.TextBox textBoxProjectId;
        private System.Windows.Forms.Label labelProjectId;
        private System.Windows.Forms.Label labelCategoryName;
        private System.Windows.Forms.TextBox textBoxCategoryName;
        private System.Windows.Forms.Button buttonCancle;
    }
}