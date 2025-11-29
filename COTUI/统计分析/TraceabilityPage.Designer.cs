namespace COTUI.统计分析
{
    partial class TraceabilityPage
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
            this.gbQuery = new System.Windows.Forms.GroupBox();
            this.btnGenerateTestData = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.olvRecords = new BrightIdeasSoftware.ObjectListView();
            this.colSN = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colModel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResult = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colStation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colOperator = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailInfo = new System.Windows.Forms.Label();
            this.gbQuery.SuspendLayout();
            this.gbResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvRecords)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQuery
            // 
            this.gbQuery.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbQuery.Controls.Add(this.btnGenerateTestData);
            this.gbQuery.Controls.Add(this.btnQuery);
            this.gbQuery.Controls.Add(this.txtSN);
            this.gbQuery.Controls.Add(this.lblSN);
            this.gbQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQuery.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbQuery.Location = new System.Drawing.Point(0, 0);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Padding = new System.Windows.Forms.Padding(10);
            this.gbQuery.Size = new System.Drawing.Size(980, 80);
            this.gbQuery.TabIndex = 0;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "产品查询";
            // 
            // btnGenerateTestData
            // 
            this.btnGenerateTestData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnGenerateTestData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateTestData.FlatAppearance.BorderSize = 0;
            this.btnGenerateTestData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateTestData.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerateTestData.ForeColor = System.Drawing.Color.White;
            this.btnGenerateTestData.Location = new System.Drawing.Point(620, 30);
            this.btnGenerateTestData.Name = "btnGenerateTestData";
            this.btnGenerateTestData.Size = new System.Drawing.Size(150, 35);
            this.btnGenerateTestData.TabIndex = 3;
            this.btnGenerateTestData.Text = "生成测试数据";
            this.btnGenerateTestData.UseVisualStyleBackColor = false;
            this.btnGenerateTestData.Click += new System.EventHandler(this.btnGenerateTestData_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(480, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(120, 35);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = " 查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtSN
            // 
            this.txtSN.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtSN.Location = new System.Drawing.Point(120, 33);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(340, 29);
            this.txtSN.TabIndex = 1;
            // 
            // lblSN
            // 
            this.lblSN.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblSN.Location = new System.Drawing.Point(20, 30);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(100, 35);
            this.lblSN.TabIndex = 0;
            this.lblSN.Text = "产品SN:";
            this.lblSN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbResults
            // 
            this.gbResults.Controls.Add(this.olvRecords);
            this.gbResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbResults.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbResults.Location = new System.Drawing.Point(0, 80);
            this.gbResults.Name = "gbResults";
            this.gbResults.Padding = new System.Windows.Forms.Padding(10);
            this.gbResults.Size = new System.Drawing.Size(980, 280);
            this.gbResults.TabIndex = 1;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "查询结果";
            // 
            // olvRecords
            // 
            this.olvRecords.AllColumns.Add(this.colSN);
            this.olvRecords.AllColumns.Add(this.colTime);
            this.olvRecords.AllColumns.Add(this.colModel);
            this.olvRecords.AllColumns.Add(this.colResult);
            this.olvRecords.AllColumns.Add(this.colStation);
            this.olvRecords.AllColumns.Add(this.colOperator);
            this.olvRecords.BackColor = System.Drawing.Color.White;
            this.olvRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olvRecords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSN,
            this.colTime,
            this.colModel,
            this.colResult,
            this.colStation,
            this.colOperator});
            this.olvRecords.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvRecords.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.olvRecords.FullRowSelect = true;
            this.olvRecords.GridLines = true;
            this.olvRecords.HideSelection = false;
            this.olvRecords.Location = new System.Drawing.Point(10, 32);
            this.olvRecords.Name = "olvRecords";
            this.olvRecords.RowHeight = 30;
            this.olvRecords.ShowGroups = false;
            this.olvRecords.Size = new System.Drawing.Size(960, 238);
            this.olvRecords.TabIndex = 0;
            this.olvRecords.UseAlternatingBackColors = true;
            this.olvRecords.UseCompatibleStateImageBehavior = false;
            this.olvRecords.View = System.Windows.Forms.View.Details;
            this.olvRecords.SelectionChanged += new System.EventHandler(this.olvRecords_SelectionChanged);
            // 
            // colSN
            // 
            this.colSN.AspectName = "ProductSN";
            this.colSN.CellPadding = null;
            this.colSN.Text = "产品SN";
            this.colSN.Width = 160;
            // 
            // colTime
            // 
            this.colTime.AspectName = "ProductionTime";
            this.colTime.AspectToStringFormat = "{0:yyyy-MM-dd HH:mm:ss}";
            this.colTime.CellPadding = null;
            this.colTime.Text = "生产时间";
            this.colTime.Width = 160;
            // 
            // colModel
            // 
            this.colModel.AspectName = "ProductModel";
            this.colModel.CellPadding = null;
            this.colModel.Text = "产品型号";
            this.colModel.Width = 120;
            // 
            // colResult
            // 
            this.colResult.AspectName = "OverallResult";
            this.colResult.CellPadding = null;
            this.colResult.Text = "检测结果";
            this.colResult.Width = 100;
            // 
            // colStation
            // 
            this.colStation.AspectName = "Station";
            this.colStation.CellPadding = null;
            this.colStation.Text = "工站";
            this.colStation.Width = 100;
            // 
            // colOperator
            // 
            this.colOperator.AspectName = "Operator";
            this.colOperator.CellPadding = null;
            this.colOperator.FillsFreeSpace = true;
            this.colOperator.IsTileViewColumn = true;
            this.colOperator.Text = "操作员";
            this.colOperator.Width = 100;
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.panelDetails);
            this.gbDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetails.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbDetails.Location = new System.Drawing.Point(0, 360);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Padding = new System.Windows.Forms.Padding(10);
            this.gbDetails.Size = new System.Drawing.Size(980, 290);
            this.gbDetails.TabIndex = 2;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "详细信息";
            // 
            // panelDetails
            // 
            this.panelDetails.AutoScroll = true;
            this.panelDetails.BackColor = System.Drawing.Color.White;
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.lblDetailInfo);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(10, 32);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(960, 248);
            this.panelDetails.TabIndex = 0;
            // 
            // lblDetailInfo
            // 
            this.lblDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetailInfo.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblDetailInfo.Location = new System.Drawing.Point(0, 0);
            this.lblDetailInfo.Name = "lblDetailInfo";
            this.lblDetailInfo.Padding = new System.Windows.Forms.Padding(10);
            this.lblDetailInfo.Size = new System.Drawing.Size(958, 246);
            this.lblDetailInfo.TabIndex = 0;
            this.lblDetailInfo.Text = "选择一条记录查看详细信息...";
            // 
            // TraceabilityPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(980, 650);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbResults);
            this.Controls.Add(this.gbQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TraceabilityPage";
            this.Text = "产品追溯查询";
            this.Load += new System.EventHandler(this.TraceabilityPage_Load);
            this.gbQuery.ResumeLayout(false);
            this.gbQuery.PerformLayout();
            this.gbResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvRecords)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbQuery;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnGenerateTestData;
        private System.Windows.Forms.GroupBox gbResults;
        private BrightIdeasSoftware.ObjectListView olvRecords;
        private BrightIdeasSoftware.OLVColumn colSN;
        private BrightIdeasSoftware.OLVColumn colTime;
        private BrightIdeasSoftware.OLVColumn colModel;
        private BrightIdeasSoftware.OLVColumn colResult;
        private BrightIdeasSoftware.OLVColumn colStation;
        private BrightIdeasSoftware.OLVColumn colOperator;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailInfo;
    }
}
