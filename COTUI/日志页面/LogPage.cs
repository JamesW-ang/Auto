using CCWin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BrightIdeasSoftware;
using COTUI.通用功能类;
using COTUI.数据库.Services;
using COTUI.数据库.Models;

namespace COTUI.日志页面
{
    public partial class LogPage : Form
    {
        // 使用全局变量 Gvar.Logger 访问日志服务
        private LogService logService;
        private System.Windows.Forms.Timer autoRefreshTimer;
        private string currentStation = "全部工站";
        private ObjectListView olvLogs;

        public LogPage()
        {
            InitializeComponent();
            
            try
            {
                logService = new LogService();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化日志服务失败：{ex.Message}\n\n程序将继续运行，但日志查询功能可能不可用。", 
                    "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Gvar.Logger.ErrorException(ex, "初始化LogService失败");
            }
        }

        public void SetStation(string station)
        {
            try
            {
                currentStation = station;
                if (cmbStation.Items.Contains(station))
                {
                    cmbStation.SelectedItem = station;
                }
                LoadLogsAsync();
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "设置工站失败");
            }
        }

        private void LogPage_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeStationSelector();
                InitializeLogLevelFilter();
                InitializeAutoRefresh();
                InitializeObjectListView();
                
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Now;
                
                lblTotalLogs.Text = "0 (请点击查询按钮)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"日志页面加载失败：{ex.Message}\n\n{ex.StackTrace}", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Gvar.Logger.ErrorException(ex, "LogPage加载失败");
            }
        }

        private async void LoadLogsAsync()
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(LoadLogsAsync));
                    return;
                }

                if (olvLogs == null)
                {
                    return;
                }

                lblTotalLogs.Text = "加载中...";
                olvLogs.Enabled = false;
                Cursor = Cursors.WaitCursor;

                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);
                string selectedStation = cmbStation.SelectedItem?.ToString();
                int maxLogs = (int)numMaxLogs.Value;
                List<string> selectedLevels = GetSelectedLogLevels();

                if (maxLogs > 2000)
                {
                    var result = MessageBox.Show(
                        $"查询数量 {maxLogs} 过大,可能导致界面卡顿。\n\n建议设置为 500-1000 之间。\n\n是否继续?",
                        "性能警告",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.No)
                    {
                        lblTotalLogs.Text = "0 (已取消)";
                        olvLogs.Enabled = true;
                        Cursor = Cursors.Default;
                        return;
                    }
                }

                if (selectedLevels.Count == 0)
                {
                    lblTotalLogs.Text = "0 (未选择日志级别)";
                    olvLogs.Enabled = true;
                    Cursor = Cursors.Default;
                    return;
                }

                if (logService == null)
                {
                    lblTotalLogs.Text = "0 (服务不可用)";
                    olvLogs.Enabled = true;
                    Cursor = Cursors.Default;
                    return;
                }

                var startTime = DateTime.Now;
                
                List<LogModel> logs = await Task.Run(() => 
                {
                    List<LogModel> allLogs = new List<LogModel>();
                    
                    foreach (var level in selectedLevels)
                    {
                        try
                        {
                            var levelLogs = logService.GetLogs(
                                startTime: startDate,
                                endTime: endDate,
                                level: level,
                                station: selectedStation,
                                maxRecords: maxLogs
                            );
                            
                            if (levelLogs != null)
                            {
                                allLogs.AddRange(levelLogs);
                            }
                        }
                        catch (Exception ex)
                        {
                            Gvar.Logger.ErrorException(ex, $"查询日志级别 {level} 失败");
                        }
                    }
                    
                    return allLogs.OrderByDescending(l => l.LogTime).Take(maxLogs).ToList();
                });

                var queryTime = (DateTime.Now - startTime).TotalSeconds;

                var uiStartTime = DateTime.Now;
                UpdateLogDisplay(logs);
                var uiTime = (DateTime.Now - uiStartTime).TotalSeconds;

                lblTotalLogs.Text = $"{logs.Count} 条 (查询:{queryTime:F1}s, 显示:{uiTime:F1}s)";
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载日志失败");
                
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show($"加载日志失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblTotalLogs.Text = "0 (加载失败)";
                    }));
                }
                else
                {
                    MessageBox.Show($"加载日志失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblTotalLogs.Text = "0 (加载失败)";
                }
            }
            finally
            {
                if (olvLogs != null)
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            olvLogs.Enabled = true;
                            Cursor = Cursors.Default;
                        }));
                    }
                    else
                    {
                        olvLogs.Enabled = true;
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void UpdateLogDisplay(List<LogModel> logs)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdateLogDisplay(logs)));
                    return;
                }

                if (olvLogs == null)
                {
                    return;
                }

                if (logs == null || logs.Count == 0)
                {
                    olvLogs.ClearObjects();
                    return;
                }

                olvLogs.SetObjects(logs);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "UpdateLogDisplay 异常");
            }
        }

        private List<string> GetSelectedLogLevels()
        {
            List<string> levels = new List<string>();
            
            try
            {
                if (chkTrace.Checked) levels.Add("Trace");
                if (chkDebug.Checked) levels.Add("Debug");
                if (chkInfo.Checked) levels.Add("Info");
                if (chkWarn.Checked) levels.Add("Warn");
                if (chkError.Checked) levels.Add("Error");
                if (chkFatal.Checked) levels.Add("Fatal");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "获取日志级别失败");
            }
            
            return levels;
        }

        private Color GetLogLevelColor(string level)
        {
            switch (level)
            {
                case "Trace": return Color.Gray;
                case "Debug": return Color.LightBlue;
                case "Info": return Color.Green;
                case "Warn": return Color.Orange;
                case "Error": return Color.Red;
                case "Fatal": return Color.DarkRed;
                default: return Color.Black;
            }
        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnQuery.Enabled)
                {
                    return;
                }

                Cursor = Cursors.WaitCursor;
                btnQuery.Enabled = false;
                lblTotalLogs.Text = "查询中...";
                
                await Task.Run(() =>
                {
                    try
                    {
                        DatabaseLoggerExtension.Instance.FlushLogs();
                        System.Threading.Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                        Gvar.Logger.ErrorException(ex, "刷新日志队列失败");
                    }
                });
                
                LoadLogsAsync();
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "查询日志失败");
                MessageBox.Show($"查询日志失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTotalLogs.Text = "0 (查询失败)";
            }
            finally
            {
                await Task.Delay(500);
                btnQuery.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (olvLogs != null)
                {
                    olvLogs.ClearObjects();
                }
                lblTotalLogs.Text = "0";
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "清空日志显示失败");
                MessageBox.Show($"清空失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (olvLogs == null || olvLogs.GetItemCount() == 0)
                {
                    MessageBox.Show("没有可导出的日志数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "CSV文件|*.csv|文本文件|*.txt";
                saveDialog.FileName = $"日志导出_{DateTime.Now:yyyyMMddHHmmss}.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        writer.WriteLine("时间,级别,消息,来源,工站");

                        foreach (LogModel log in olvLogs.Objects)
                        {
                            var line = string.Join(",",
                                EscapeCSV(log.LogTime.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                                EscapeCSV(log.Level ?? ""),
                                EscapeCSV(log.Message ?? ""),
                                EscapeCSV(log.Source ?? ""),
                                EscapeCSV(log.Station ?? "未知工站")
                            );
                            writer.WriteLine(line);
                        }
                    }

                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Gvar.Logger.ErrorException(ex, "导出日志失败");
            }
        }

        private string EscapeCSV(string text)
        {
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
            {
                text = text.Replace("\"", "\"\"");
                return $"\"{text}\"";
            }
            return text;
        }

        private void InitializeStationSelector()
        {
            cmbStation.Items.Add("全部工站");
            cmbStation.Items.Add("工站1");
            cmbStation.Items.Add("工站2");
            cmbStation.Items.Add("工站3");
            cmbStation.Items.Add("工站4");
            cmbStation.Items.Add("工站5");
            cmbStation.SelectedIndex = 0;
        }

        private void InitializeLogLevelFilter()
        {
            chkTrace.Checked = false;
            chkDebug.Checked = false;
            chkInfo.Checked = true;
            chkWarn.Checked = true;
            chkError.Checked = true;
            chkFatal.Checked = true;
        }

        private void InitializeAutoRefresh()
        {
            autoRefreshTimer = new System.Windows.Forms.Timer();
            autoRefreshTimer.Interval = 5000;
            autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
            chkAutoRefresh.Checked = false;
        }

        private void InitializeObjectListView()
        {
            try
            {
                olvLogs = new ObjectListView
                {
                    Dock = DockStyle.Fill,
                    View = View.Details,
                    FullRowSelect = true,
                    GridLines = true,
                    ShowGroups = false,
                    VirtualMode = false,
                    UseAlternatingBackColors = true,
                    AlternateRowBackColor = Color.WhiteSmoke,
                    Font = new Font("微软雅黑", 9F),
                    RowHeight = 22
                };

                var colTime = new OLVColumn("时间", "LogTime")
                {
                    Width = 180,
                    AspectName = "LogTime",
                    AspectToStringFormat = "{0:yyyy-MM-dd HH:mm:ss.fff}"
                };

                var colLevel = new OLVColumn("级别", "Level")
                {
                    Width = 80,
                    AspectName = "Level"
                };

                var colMessage = new OLVColumn("消息", "Message")
                {
                    Width = 400,
                    FillsFreeSpace = true,
                    AspectName = "Message"
                };

                var colSource = new OLVColumn("来源", "Source")
                {
                    Width = 120,
                    AspectName = "Source"
                };

                var colStation = new OLVColumn("工站", "Station")
                {
                    Width = 100,
                    AspectName = "Station"
                };

                olvLogs.AllColumns.AddRange(new[] { colTime, colLevel, colMessage, colSource, colStation });
                olvLogs.Columns.AddRange(new ColumnHeader[] { colTime, colLevel, colMessage, colSource, colStation });

                olvLogs.RowFormatter = delegate(OLVListItem item)
                {
                    if (item.RowObject is LogModel log)
                    {
                        try
                        {
                            if (item.SubItems.Count > 1)
                            {
                                Color levelColor = GetLogLevelColor(log.Level);
                                item.SubItems[1].ForeColor = levelColor;
                                item.SubItems[1].Font = new Font(olvLogs.Font, FontStyle.Bold);
                            }

                            if (log.Level == "Error" || log.Level == "Fatal")
                            {
                                item.BackColor = Color.MistyRose;
                            }
                        }
                        catch
                        {
                            // 忽略格式化异常
                        }
                    }
                };

                var parent = panel2;
                parent.Controls.Clear();
                parent.Controls.Add(olvLogs);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "InitializeObjectListView 失败");
                throw;
            }
        }

        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadLogsAsync();
        }

        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRefresh.Checked)
            {
                autoRefreshTimer.Start();
            }
            else
            {
                autoRefreshTimer.Stop();
            }
        }

        private void cmbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAutoRefresh.Checked)
            {
                LoadLogsAsync();
            }
        }

        private void LogLevelFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRefresh.Checked)
            {
                LoadLogsAsync();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (autoRefreshTimer != null)
            {
                autoRefreshTimer.Stop();
                autoRefreshTimer.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}
