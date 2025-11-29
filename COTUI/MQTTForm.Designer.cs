namespace COTUI
{
    partial class MQTTForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubscribeTopic = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSubscribeClear = new System.Windows.Forms.Button();
            this.btnSubscribeCancel = new System.Windows.Forms.Button();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.dgvSubscribed = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPublishTopic = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPublishClear = new System.Windows.Forms.Button();
            this.btnPublishCancel = new System.Windows.Forms.Button();
            this.btnPublish = new System.Windows.Forms.Button();
            this.dgvPublished = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRestartWorkReport = new System.Windows.Forms.Button();
            this.btnRefreshMES = new System.Windows.Forms.Button();
            this.btnDeserialize = new System.Windows.Forms.Button();
            this.btnFlowCard = new System.Windows.Forms.Button();
            this.btnQRCodeReport = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnEMAP = new System.Windows.Forms.Button();
            this.btnVersion = new System.Windows.Forms.Button();
            this.btnProcessReport = new System.Windows.Forms.Button();
            this.btnMachineReport = new System.Windows.Forms.Button();
            this.btnInOutStation = new System.Windows.Forms.Button();
            this.btnParameterReport = new System.Windows.Forms.Button();
            this.btnDataCorrection = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.chkNormalMode = new System.Windows.Forms.CheckBox();
            this.chkAbnormalMode = new System.Windows.Forms.CheckBox();
            this.txtQRCode = new System.Windows.Forms.TextBox();
            this.btnWorkReportTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearWorkOrder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWorkOrder = new System.Windows.Forms.TextBox();
            this.btnLoadWorkOrder = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubscribed)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublished)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1017, 715);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.dgvSubscribed, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(248, 709);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnExit, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtServer, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtPort, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtUsername, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtPassword, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.btnConnect, 0, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(242, 194);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Location = new System.Drawing.Point(124, 123);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(103, 54);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "user:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "password:";
            // 
            // txtServer
            // 
            this.txtServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServer.Location = new System.Drawing.Point(124, 3);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(115, 25);
            this.txtServer.TabIndex = 4;
            // 
            // txtPort
            // 
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.Location = new System.Drawing.Point(124, 33);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(115, 25);
            this.txtPort.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsername.Location = new System.Drawing.Point(124, 63);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(115, 25);
            this.txtUsername.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(124, 93);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(115, 25);
            this.txtPassword.TabIndex = 7;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnConnect.ForeColor = System.Drawing.SystemColors.Control;
            this.btnConnect.Location = new System.Drawing.Point(3, 123);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(103, 54);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtSubscribeTopic, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 203);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(242, 28);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "订阅主题：";
            // 
            // txtSubscribeTopic
            // 
            this.txtSubscribeTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubscribeTopic.Location = new System.Drawing.Point(97, 3);
            this.txtSubscribeTopic.Name = "txtSubscribeTopic";
            this.txtSubscribeTopic.Size = new System.Drawing.Size(142, 25);
            this.txtSubscribeTopic.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.Controls.Add(this.btnSubscribeClear, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnSubscribeCancel, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnSubscribe, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 237);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(242, 43);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // btnSubscribeClear
            // 
            this.btnSubscribeClear.Location = new System.Drawing.Point(163, 3);
            this.btnSubscribeClear.Name = "btnSubscribeClear";
            this.btnSubscribeClear.Size = new System.Drawing.Size(74, 37);
            this.btnSubscribeClear.TabIndex = 2;
            this.btnSubscribeClear.Text = "清空";
            this.btnSubscribeClear.UseVisualStyleBackColor = true;
            // 
            // btnSubscribeCancel
            // 
            this.btnSubscribeCancel.Location = new System.Drawing.Point(83, 3);
            this.btnSubscribeCancel.Name = "btnSubscribeCancel";
            this.btnSubscribeCancel.Size = new System.Drawing.Size(74, 37);
            this.btnSubscribeCancel.TabIndex = 1;
            this.btnSubscribeCancel.Text = "取消";
            this.btnSubscribeCancel.UseVisualStyleBackColor = true;
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(3, 3);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(74, 37);
            this.btnSubscribe.TabIndex = 0;
            this.btnSubscribe.Text = "订阅";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            // 
            // dgvSubscribed
            // 
            this.dgvSubscribed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubscribed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubscribed.Location = new System.Drawing.Point(3, 286);
            this.dgvSubscribed.Name = "dgvSubscribed";
            this.dgvSubscribed.RowHeadersWidth = 51;
            this.dgvSubscribed.Size = new System.Drawing.Size(242, 420);
            this.dgvSubscribed.TabIndex = 3;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.dgvPublished, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(257, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(248, 709);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.txtPublishTopic, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(242, 24);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "发布主题：";
            // 
            // txtPublishTopic
            // 
            this.txtPublishTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPublishTopic.Location = new System.Drawing.Point(103, 3);
            this.txtPublishTopic.Name = "txtPublishTopic";
            this.txtPublishTopic.Size = new System.Drawing.Size(136, 25);
            this.txtPublishTopic.TabIndex = 1;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.Controls.Add(this.btnPublishClear, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnPublishCancel, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnPublish, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(242, 54);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // btnPublishClear
            // 
            this.btnPublishClear.Location = new System.Drawing.Point(163, 3);
            this.btnPublishClear.Name = "btnPublishClear";
            this.btnPublishClear.Size = new System.Drawing.Size(76, 42);
            this.btnPublishClear.TabIndex = 2;
            this.btnPublishClear.Text = "清空";
            this.btnPublishClear.UseVisualStyleBackColor = true;
            // 
            // btnPublishCancel
            // 
            this.btnPublishCancel.Location = new System.Drawing.Point(83, 3);
            this.btnPublishCancel.Name = "btnPublishCancel";
            this.btnPublishCancel.Size = new System.Drawing.Size(74, 42);
            this.btnPublishCancel.TabIndex = 1;
            this.btnPublishCancel.Text = "取消";
            this.btnPublishCancel.UseVisualStyleBackColor = true;
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(3, 3);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(74, 42);
            this.btnPublish.TabIndex = 0;
            this.btnPublish.Text = "发布";
            this.btnPublish.UseVisualStyleBackColor = true;
            // 
            // dgvPublished
            // 
            this.dgvPublished.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPublished.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPublished.Location = new System.Drawing.Point(3, 93);
            this.dgvPublished.Name = "dgvPublished";
            this.dgvPublished.RowHeadersWidth = 51;
            this.dgvPublished.RowTemplate.Height = 27;
            this.dgvPublished.Size = new System.Drawing.Size(242, 613);
            this.dgvPublished.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.btnRestartWorkReport, 1, 8);
            this.tableLayoutPanel9.Controls.Add(this.btnRefreshMES, 0, 8);
            this.tableLayoutPanel9.Controls.Add(this.btnDeserialize, 1, 7);
            this.tableLayoutPanel9.Controls.Add(this.btnFlowCard, 0, 7);
            this.tableLayoutPanel9.Controls.Add(this.btnQRCodeReport, 1, 6);
            this.tableLayoutPanel9.Controls.Add(this.btnPost, 0, 6);
            this.tableLayoutPanel9.Controls.Add(this.btnEMAP, 1, 5);
            this.tableLayoutPanel9.Controls.Add(this.btnVersion, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.btnProcessReport, 1, 4);
            this.tableLayoutPanel9.Controls.Add(this.btnMachineReport, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.btnInOutStation, 1, 3);
            this.tableLayoutPanel9.Controls.Add(this.btnParameterReport, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.btnDataCorrection, 1, 2);
            this.tableLayoutPanel9.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.chkNormalMode, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.chkAbnormalMode, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.txtQRCode, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnWorkReportTest, 0, 2);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(511, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 9;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(248, 709);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // btnRestartWorkReport
            // 
            this.btnRestartWorkReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestartWorkReport.Location = new System.Drawing.Point(127, 617);
            this.btnRestartWorkReport.Name = "btnRestartWorkReport";
            this.btnRestartWorkReport.Size = new System.Drawing.Size(118, 89);
            this.btnRestartWorkReport.TabIndex = 17;
            this.btnRestartWorkReport.Text = "重启报工监视";
            this.btnRestartWorkReport.UseVisualStyleBackColor = true;
            // 
            // btnRefreshMES
            // 
            this.btnRefreshMES.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefreshMES.Location = new System.Drawing.Point(3, 617);
            this.btnRefreshMES.Name = "btnRefreshMES";
            this.btnRefreshMES.Size = new System.Drawing.Size(118, 89);
            this.btnRefreshMES.TabIndex = 16;
            this.btnRefreshMES.Text = "刷新MES";
            this.btnRefreshMES.UseVisualStyleBackColor = true;
            // 
            // btnDeserialize
            // 
            this.btnDeserialize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeserialize.Location = new System.Drawing.Point(127, 528);
            this.btnDeserialize.Name = "btnDeserialize";
            this.btnDeserialize.Size = new System.Drawing.Size(118, 83);
            this.btnDeserialize.TabIndex = 15;
            this.btnDeserialize.Text = "反序列化解析";
            this.btnDeserialize.UseVisualStyleBackColor = true;
            // 
            // btnFlowCard
            // 
            this.btnFlowCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFlowCard.Location = new System.Drawing.Point(3, 528);
            this.btnFlowCard.Name = "btnFlowCard";
            this.btnFlowCard.Size = new System.Drawing.Size(118, 83);
            this.btnFlowCard.TabIndex = 14;
            this.btnFlowCard.Text = "流程卡号";
            this.btnFlowCard.UseVisualStyleBackColor = true;
            // 
            // btnQRCodeReport
            // 
            this.btnQRCodeReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQRCodeReport.Location = new System.Drawing.Point(127, 439);
            this.btnQRCodeReport.Name = "btnQRCodeReport";
            this.btnQRCodeReport.Size = new System.Drawing.Size(118, 83);
            this.btnQRCodeReport.TabIndex = 13;
            this.btnQRCodeReport.Text = "二维码上报";
            this.btnQRCodeReport.UseVisualStyleBackColor = true;
            // 
            // btnPost
            // 
            this.btnPost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPost.Location = new System.Drawing.Point(3, 439);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(118, 83);
            this.btnPost.TabIndex = 12;
            this.btnPost.Text = "post";
            this.btnPost.UseVisualStyleBackColor = true;
            // 
            // btnEMAP
            // 
            this.btnEMAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEMAP.Location = new System.Drawing.Point(127, 350);
            this.btnEMAP.Name = "btnEMAP";
            this.btnEMAP.Size = new System.Drawing.Size(118, 83);
            this.btnEMAP.TabIndex = 11;
            this.btnEMAP.Text = "EMAP";
            this.btnEMAP.UseVisualStyleBackColor = true;
            // 
            // btnVersion
            // 
            this.btnVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVersion.Location = new System.Drawing.Point(3, 350);
            this.btnVersion.Name = "btnVersion";
            this.btnVersion.Size = new System.Drawing.Size(118, 83);
            this.btnVersion.TabIndex = 10;
            this.btnVersion.Text = "版本号";
            this.btnVersion.UseVisualStyleBackColor = true;
            // 
            // btnProcessReport
            // 
            this.btnProcessReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProcessReport.Location = new System.Drawing.Point(127, 261);
            this.btnProcessReport.Name = "btnProcessReport";
            this.btnProcessReport.Size = new System.Drawing.Size(118, 83);
            this.btnProcessReport.TabIndex = 9;
            this.btnProcessReport.Text = "工艺上报";
            this.btnProcessReport.UseVisualStyleBackColor = true;
            // 
            // btnMachineReport
            // 
            this.btnMachineReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMachineReport.Location = new System.Drawing.Point(3, 261);
            this.btnMachineReport.Name = "btnMachineReport";
            this.btnMachineReport.Size = new System.Drawing.Size(118, 83);
            this.btnMachineReport.TabIndex = 8;
            this.btnMachineReport.Text = "机况上报";
            this.btnMachineReport.UseVisualStyleBackColor = true;
            // 
            // btnInOutStation
            // 
            this.btnInOutStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInOutStation.Location = new System.Drawing.Point(127, 172);
            this.btnInOutStation.Name = "btnInOutStation";
            this.btnInOutStation.Size = new System.Drawing.Size(118, 83);
            this.btnInOutStation.TabIndex = 7;
            this.btnInOutStation.Text = "产品出入站";
            this.btnInOutStation.UseVisualStyleBackColor = true;
            // 
            // btnParameterReport
            // 
            this.btnParameterReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnParameterReport.Location = new System.Drawing.Point(3, 172);
            this.btnParameterReport.Name = "btnParameterReport";
            this.btnParameterReport.Size = new System.Drawing.Size(118, 83);
            this.btnParameterReport.TabIndex = 6;
            this.btnParameterReport.Text = "参数上报";
            this.btnParameterReport.UseVisualStyleBackColor = true;
            // 
            // btnDataCorrection
            // 
            this.btnDataCorrection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataCorrection.Location = new System.Drawing.Point(127, 83);
            this.btnDataCorrection.Name = "btnDataCorrection";
            this.btnDataCorrection.Size = new System.Drawing.Size(118, 83);
            this.btnDataCorrection.TabIndex = 5;
            this.btnDataCorrection.Text = "数据修改补报";
            this.btnDataCorrection.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "钢环二维码：";
            // 
            // chkNormalMode
            // 
            this.chkNormalMode.AutoSize = true;
            this.chkNormalMode.Location = new System.Drawing.Point(3, 43);
            this.chkNormalMode.Name = "chkNormalMode";
            this.chkNormalMode.Size = new System.Drawing.Size(89, 19);
            this.chkNormalMode.TabIndex = 1;
            this.chkNormalMode.Text = "正常模式";
            this.chkNormalMode.UseVisualStyleBackColor = true;
            // 
            // chkAbnormalMode
            // 
            this.chkAbnormalMode.AutoSize = true;
            this.chkAbnormalMode.Location = new System.Drawing.Point(127, 43);
            this.chkAbnormalMode.Name = "chkAbnormalMode";
            this.chkAbnormalMode.Size = new System.Drawing.Size(104, 19);
            this.chkAbnormalMode.TabIndex = 2;
            this.chkAbnormalMode.Text = "非正常模式";
            this.chkAbnormalMode.UseVisualStyleBackColor = true;
            // 
            // txtQRCode
            // 
            this.txtQRCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQRCode.Location = new System.Drawing.Point(127, 3);
            this.txtQRCode.Name = "txtQRCode";
            this.txtQRCode.Size = new System.Drawing.Size(118, 25);
            this.txtQRCode.TabIndex = 3;
            // 
            // btnWorkReportTest
            // 
            this.btnWorkReportTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorkReportTest.Location = new System.Drawing.Point(3, 83);
            this.btnWorkReportTest.Name = "btnWorkReportTest";
            this.btnWorkReportTest.Size = new System.Drawing.Size(118, 83);
            this.btnWorkReportTest.TabIndex = 4;
            this.btnWorkReportTest.Text = "报工测试";
            this.btnWorkReportTest.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.btnClearWorkOrder, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.txtWorkOrder, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnLoadWorkOrder, 0, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(765, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(249, 709);
            this.tableLayoutPanel10.TabIndex = 3;
            // 
            // btnClearWorkOrder
            // 
            this.btnClearWorkOrder.Location = new System.Drawing.Point(127, 53);
            this.btnClearWorkOrder.Name = "btnClearWorkOrder";
            this.btnClearWorkOrder.Size = new System.Drawing.Size(113, 38);
            this.btnClearWorkOrder.TabIndex = 7;
            this.btnClearWorkOrder.Text = "清空";
            this.btnClearWorkOrder.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "选择工单：";
            // 
            // txtWorkOrder
            // 
            this.txtWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorkOrder.Location = new System.Drawing.Point(127, 3);
            this.txtWorkOrder.Name = "txtWorkOrder";
            this.txtWorkOrder.Size = new System.Drawing.Size(119, 25);
            this.txtWorkOrder.TabIndex = 5;
            // 
            // btnLoadWorkOrder
            // 
            this.btnLoadWorkOrder.Location = new System.Drawing.Point(3, 53);
            this.btnLoadWorkOrder.Name = "btnLoadWorkOrder";
            this.btnLoadWorkOrder.Size = new System.Drawing.Size(113, 38);
            this.btnLoadWorkOrder.TabIndex = 6;
            this.btnLoadWorkOrder.Text = "加载补报工单";
            this.btnLoadWorkOrder.UseVisualStyleBackColor = true;
            // 
            // MQTTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 715);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MQTTForm";
            this.Load += new System.EventHandler(this.MQTTForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubscribed)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublished)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubscribeTopic;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnSubscribeClear;
        private System.Windows.Forms.Button btnSubscribeCancel;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.DataGridView dgvSubscribed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPublishTopic;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button btnPublishClear;
        private System.Windows.Forms.Button btnPublishCancel;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.DataGridView dgvPublished;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRestartWorkReport;
        private System.Windows.Forms.Button btnRefreshMES;
        private System.Windows.Forms.Button btnDeserialize;
        private System.Windows.Forms.Button btnFlowCard;
        private System.Windows.Forms.Button btnQRCodeReport;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnEMAP;
        private System.Windows.Forms.Button btnVersion;
        private System.Windows.Forms.Button btnProcessReport;
        private System.Windows.Forms.Button btnMachineReport;
        private System.Windows.Forms.Button btnInOutStation;
        private System.Windows.Forms.Button btnParameterReport;
        private System.Windows.Forms.Button btnDataCorrection;
        private System.Windows.Forms.Button btnWorkReportTest;
        private System.Windows.Forms.CheckBox chkNormalMode;
        private System.Windows.Forms.CheckBox chkAbnormalMode;
        private System.Windows.Forms.TextBox txtQRCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button btnClearWorkOrder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWorkOrder;
        private System.Windows.Forms.Button btnLoadWorkOrder;
    }
}
