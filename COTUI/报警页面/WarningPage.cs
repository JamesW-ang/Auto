using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using COTUI.通用功能类;

namespace COTUI.报警页面
{
    public partial class WarningPage : Form
    {
        // 使用全局变量 Gvar.Logger 访问日志服务
        private const double PRODUCTION_HOURS_PER_DAY = 24.0; // 每天生产时长（小时），可根据实际调整

        public WarningPage()
        {
            InitializeComponent();
        }

        private void WarningPage_Load(object sender, EventArgs e)
        {
            InitializeTimeSelectors();
            InitializeDataGridViews();
        }

        /// <summary>
        /// 初始化时间选择器
        /// </summary>
        private void InitializeTimeSelectors()
        {
            // 设置开始时间为今天的00:00:00
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker1.ShowUpDown = false;

            // 设置结束时间为当前时间
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker2.ShowUpDown = false;

            // 初始化统计页面的年份和月份下拉框
            InitializeStatsComboBoxes();
        }

        /// <summary>
        /// 初始化统计页面的下拉框
        /// </summary>
        private void InitializeStatsComboBoxes()
        {
            // 填充年份（最近5年）
            cmbStatsYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                cmbStatsYear.Items.Add(currentYear - i);
            }
            cmbStatsYear.SelectedIndex = 0;

            // 填充月份
            cmbStatsMonth.Items.Clear();
            cmbStatsMonth.Items.Add("全部");
            for (int i = 1; i <= 12; i++)
            {
                cmbStatsMonth.Items.Add($"{i}月");
            }
            cmbStatsMonth.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化数据表格样式
        /// </summary>
        private void InitializeDataGridViews()
        {
            // 设置报警日志表格样式
            dgvWarningLog.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvWarningLog.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dgvWarningLog.DefaultCellStyle.SelectionForeColor = Color.White;

            // 设置每日统计表格样式
            dgvDailyStats.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvDailyStats.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dgvDailyStats.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 此方法已废弃，保留以防止设计器错误
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证时间范围
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("开始时间不能晚于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                LoadWarningLogs();
                CalculateStatistics();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"查询报警日志失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Gvar.Logger.ErrorException(ex, "查询报警日志失败");
            }
        }

        /// <summary>
        /// 加载报警日志
        /// </summary>
        private void LoadWarningLogs()
        {
            dgvWarningLog.Rows.Clear();

            DateTime startTime = dateTimePicker1.Value;
            DateTime endTime = dateTimePicker2.Value;

            // 读取日志文件
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(logDirectory))
            {
                MessageBox.Show("日志目录不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var logFiles = Directory.GetFiles(logDirectory, "log_*.csv");
            List<LogEntry> logEntries = new List<LogEntry>();

            foreach (var logFile in logFiles)
            {
                try
                {
                    var lines = File.ReadAllLines(logFile, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        var entry = ParseLogEntry(line);
                        if (entry != null && IsMatchFilter(entry, startTime, endTime))
                        {
                            logEntries.Add(entry);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, $"读取日志文件失败：{logFile}");
                }
            }

            // 按时间排序并显示
            logEntries = logEntries.OrderByDescending(e => e.Time).ToList();
            foreach (var entry in logEntries)
            {
                int rowIndex = dgvWarningLog.Rows.Add(
                    entry.Time.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    entry.Level,
                    entry.Message,
                    entry.Source
                );

                // 根据日志级别设置颜色
                Color levelColor = GetLogLevelColor(entry.Level);
                dgvWarningLog.Rows[rowIndex].Cells[1].Style.ForeColor = levelColor;
                dgvWarningLog.Rows[rowIndex].Cells[1].Style.Font = new Font(dgvWarningLog.Font, FontStyle.Bold);
            }
        }

        /// <summary>
        /// 解析日志条目
        /// </summary>
        private LogEntry ParseLogEntry(string line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line)) return null;

                // CSV格式：时间,级别,消息,文件,方法:行号
                var parts = SplitCSVLine(line);
                if (parts.Length < 4) return null;

                DateTime time;
                if (!DateTime.TryParse(parts[0], out time)) return null;

                return new LogEntry
                {
                    Time = time,
                    Level = parts[1],
                    Message = parts[2],
                    Source = parts.Length > 3 ? parts[3] : ""
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 分割CSV行（处理引号内的逗号）
        /// </summary>
        private string[] SplitCSVLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            StringBuilder current = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            result.Add(current.ToString());
            return result.ToArray();
        }

        /// <summary>
        /// 检查日志条目是否匹配筛选条件
        /// </summary>
        private bool IsMatchFilter(LogEntry entry, DateTime startTime, DateTime endTime)
        {
            // 检查时间范围
            if (entry.Time < startTime || entry.Time > endTime)
                return false;

            // 只显示警告级别以上的日志（Warn, Error, Fatal）
            return entry.Level == "Warn" || entry.Level == "Error" || entry.Level == "Fatal";
        }

        /// <summary>
        /// 计算统计信息
        /// </summary>
        private void CalculateStatistics()
        {
            int totalWarnings = dgvWarningLog.Rows.Count;
            
            // 计算生产时长（根据筛选条件）
            double productionHours = CalculateProductionHours();
            
            // 估算报警时长（假设每个报警平均持续5分钟）
            double warningHours = totalWarnings * 5.0 / 60.0;
            
            // 计算报警占比
            double warningPercent = productionHours > 0 ? (warningHours / productionHours * 100) : 0;

            // 更新显示
            lblTotalWarnings.Text = totalWarnings.ToString();
            lblProductionTime.Text = $"{productionHours:F1} 小时";
            lblWarningDuration.Text = $"{warningHours:F1} 小时";
            lblWarningPercent.Text = $"{warningPercent:F2}%";

            // 根据百分比设置颜色
            if (warningPercent > 10)
                lblWarningPercent.ForeColor = Color.Red;
            else if (warningPercent > 5)
                lblWarningPercent.ForeColor = Color.Orange;
            else
                lblWarningPercent.ForeColor = Color.Green;
        }

        /// <summary>
        /// 计算生产时长
        /// </summary>
        private double CalculateProductionHours()
        {
            DateTime startTime = dateTimePicker1.Value;
            DateTime endTime = dateTimePicker2.Value;
            
            // 计算时间跨度
            TimeSpan timeSpan = endTime - startTime;
            return timeSpan.TotalHours;
        }

        /// <summary>
        /// 获取日志级别颜色
        /// </summary>
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

        /// <summary>
        /// 每日统计查询按钮点击事件
        /// </summary>
        private void btnQueryMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadDailyStatistics();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"查询每日统计失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Gvar.Logger.ErrorException(ex, "查询每日统计失败");
            }
        }

        /// <summary>
        /// 加载每日统计数据
        /// </summary>
        private void LoadDailyStatistics()
        {
            dgvDailyStats.Rows.Clear();

            if (cmbStatsYear.SelectedItem == null)
            {
                MessageBox.Show("请选择年份", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int year = Convert.ToInt32(cmbStatsYear.SelectedItem);
            int? month = cmbStatsMonth.SelectedIndex > 0 ? (int?)cmbStatsMonth.SelectedIndex : null;

            // 读取日志文件
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(logDirectory))
            {
                MessageBox.Show("日志目录不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 统计数据字典：日期 -> 报警次数
            Dictionary<DateTime, int> dailyWarnings = new Dictionary<DateTime, int>();

            var logFiles = Directory.GetFiles(logDirectory, "log_*.csv");
            foreach (var logFile in logFiles)
            {
                try
                {
                    var lines = File.ReadAllLines(logFile, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        var entry = ParseLogEntry(line);
                        if (entry != null && (entry.Level == "Warn" || entry.Level == "Error" || entry.Level == "Fatal"))
                        {
                            DateTime date = entry.Time.Date;
                            if (date.Year == year && (!month.HasValue || date.Month == month.Value))
                            {
                                if (!dailyWarnings.ContainsKey(date))
                                    dailyWarnings[date] = 0;
                                dailyWarnings[date]++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, $"读取日志文件失败：{logFile}");
                }
            }

            // 生成统计报表
            var sortedDates = dailyWarnings.Keys.OrderByDescending(d => d).ToList();
            foreach (var date in sortedDates)
            {
                int warnings = dailyWarnings[date];
                double productionTime = PRODUCTION_HOURS_PER_DAY;
                double warningTime = warnings * 5.0 / 60.0; // 假设每个报警5分钟
                double percent = (warningTime / productionTime) * 100;

                int rowIndex = dgvDailyStats.Rows.Add(
                    date.ToString("yyyy-MM-dd"),
                    warnings,
                    productionTime.ToString("F1"),
                    warningTime.ToString("F1"),
                    percent.ToString("F2")
                );

                // 根据百分比设置颜色
                if (percent > 10)
                    dgvDailyStats.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Red;
                else if (percent > 5)
                    dgvDailyStats.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Orange;
                else
                    dgvDailyStats.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Green;

                dgvDailyStats.Rows[rowIndex].Cells[4].Style.Font = new Font(dgvDailyStats.Font, FontStyle.Bold);
            }
        }

        /// <summary>
        /// 日志条目类
        /// </summary>
        private class LogEntry
        {
            public DateTime Time { get; set; }
            public string Level { get; set; }
            public string Message { get; set; }
            public string Source { get; set; }
        }
    }
}
