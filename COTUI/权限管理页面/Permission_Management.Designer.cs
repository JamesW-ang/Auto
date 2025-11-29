namespace COTUI.权限管理
{
    partial class Permission_Management
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Permission_Management));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cobx_User = new CCWin.SkinControl.SkinComboBox();
            this.txt_Password = new CCWin.SkinControl.SkinTextBox();
            this.btn_login = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select User:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Password:";
            // 
            // cobx_User
            // 
            this.cobx_User.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cobx_User.FormattingEnabled = true;
            this.cobx_User.Items.AddRange(new object[] {
            "admin",
            "user1",
            "user2"});
            this.cobx_User.Location = new System.Drawing.Point(140, 53);
            this.cobx_User.Name = "cobx_User";
            this.cobx_User.Size = new System.Drawing.Size(180, 22);
            this.cobx_User.TabIndex = 5;
            this.cobx_User.Text = "admin";
            this.cobx_User.WaterText = "";
            // 
            // txt_Password
            // 
            this.txt_Password.BackColor = System.Drawing.Color.Transparent;
            this.txt_Password.DownBack = null;
            this.txt_Password.Icon = null;
            this.txt_Password.IconIsButton = false;
            this.txt_Password.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_Password.IsPasswordChat = '●';
            this.txt_Password.IsSystemPasswordChar = true;
            this.txt_Password.Lines = new string[] {
        "skinTextBox1"};
            this.txt_Password.Location = new System.Drawing.Point(140, 94);
            this.txt_Password.Margin = new System.Windows.Forms.Padding(0);
            this.txt_Password.MaxLength = 32767;
            this.txt_Password.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_Password.MouseBack = null;
            this.txt_Password.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_Password.Multiline = false;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.NormlBack = null;
            this.txt_Password.Padding = new System.Windows.Forms.Padding(5);
            this.txt_Password.ReadOnly = false;
            this.txt_Password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_Password.Size = new System.Drawing.Size(180, 28);
            // 
            // 
            // 
            this.txt_Password.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Password.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Password.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt_Password.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_Password.SkinTxt.Name = "BaseText";
            this.txt_Password.SkinTxt.PasswordChar = '●';
            this.txt_Password.SkinTxt.Size = new System.Drawing.Size(170, 18);
            this.txt_Password.SkinTxt.TabIndex = 0;
            this.txt_Password.SkinTxt.Text = "skinTextBox1";
            this.txt_Password.SkinTxt.UseSystemPasswordChar = true;
            this.txt_Password.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_Password.SkinTxt.WaterText = "";
            this.txt_Password.TabIndex = 6;
            this.txt_Password.Text = "skinTextBox1";
            this.txt_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_Password.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_Password.WaterText = "";
            this.txt_Password.WordWrap = true;
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.Transparent;
            this.btn_login.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_login.DownBack = null;
            this.btn_login.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_login.Location = new System.Drawing.Point(140, 149);
            this.btn_login.MouseBack = null;
            this.btn_login.Name = "btn_login";
            this.btn_login.NormlBack = null;
            this.btn_login.Size = new System.Drawing.Size(180, 29);
            this.btn_login.TabIndex = 7;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // Permission_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 211);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.cobx_User);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Permission_Management";
            this.Text = "Permission_Management";
            this.Load += new System.EventHandler(this.Permission_Management_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CCWin.SkinControl.SkinComboBox cobx_User;
        private CCWin.SkinControl.SkinTextBox txt_Password;
        private CCWin.SkinControl.SkinButton btn_login;
    }
}