using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using COTUI.通用功能类;
using COTUI.数据库;

namespace COTUI
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bCanRun = false;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, "anlogTestOne", out bCanRun);
            if (!bCanRun)
            {
                MessageBox.Show("程序已在运行中");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                // 1. 初始化数据库（通过访问单例自动初始化）
                var db = Gvar.DB;
                
                // 2. 初始化 Logger（文件日志）
                var logger = Gvar.Logger;
                
                // 3. 初始化并注册数据库日志扩展
                var dbLogger = DatabaseLoggerExtension.Instance;
                dbLogger.Initialize();
                
                Gvar.Logger.Info(LogLevel.Debug, "[Program] 数据库日志系统已启动");
                Gvar.Logger.Log(LogLevel.Info, "程序启动");
                
                // 4. 运行主窗体（设置全局引用）
                Application.Run(Gvar.UI.MainForm = new Form1());
                
                // 5. 程序退出时清理
                Gvar.Logger.Log(LogLevel.Info, "程序退出");
                dbLogger.Shutdown();
                Gvar.Logger.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"程序启动失败：{ex.Message}\n\n{ex.StackTrace}", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
