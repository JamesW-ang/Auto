using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;

namespace COTUI.通用功能类
{
    public class CW_File
    {
        private Thread m_thread;

        private bool m_threadSign = false;

        private ConcurrentQueue<string> m_dataqueue = new ConcurrentQueue<string>();

        private string m_filePath = "";

        private Mutex mu = new Mutex();

        private void ThreadData()
        {
            while (m_threadSign)
            {
                mu.WaitOne();
                if (m_dataqueue.Count > 0)
                {
                    string result = "";
                    if (m_dataqueue.TryDequeue(out result))
                    {
                        Stream stream = File.Open(m_filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default);
                        streamWriter.WriteLine(result);
                        streamWriter.Close();
                        streamWriter.Dispose();
                        streamWriter.Dispose();
                    }
                }

                Thread.Sleep(10);
                mu.ReleaseMutex();
            }
        }

        public int Open(string path, int openMode = 0)
        {
            try
            {
                m_filePath = path;
                if (!File.Exists(m_filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(m_filePath));
                }
                else if (openMode == 1)
                {
                    File.Delete(m_filePath);
                }
            }
            catch (ArgumentException)
            {
                return 1;
            }
            catch (PathTooLongException)
            {
                return 1;
            }
            catch (DirectoryNotFoundException)
            {
                return 1;
            }
            catch (NotSupportedException)
            {
                return 1;
            }
            catch (IOException)
            {
                return 2;
            }

            m_thread = new Thread(ThreadData);
            m_thread.Name = "CowainLogFileWiteThread";
            m_thread.IsBackground = true;
            m_threadSign = true;
            m_thread.Start();
            return 0;
        }

        public int WriteLine(string value)
        {
            mu.WaitOne();
            Thread.Sleep(5);
            m_dataqueue.Enqueue(value);
            mu.ReleaseMutex();
            return 0;
        }

        public void Close()
        {
            m_threadSign = false;
        }
    }

    public class BaseHelper
    {
        public enum LineColor
        {
            White,
            Red,
            Green,
            Blue,
            Black
        }

        private string m_path = "";

        private int m_saveMode = 0;

        private CW_File m_file = new CW_File();

        public event Action<ScrollLogItem> Log_Event_CallBack;

        public int Open(string path, int saveMode = 1)
        {
            m_path = path;
            m_saveMode = saveMode;
            if (saveMode == 0)
            {
                return m_file.Open(path);
            }

            string path2 = m_path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            m_file.Close();
            if (File.Exists(path2))
            {
                m_file.Open(path2);
            }

            return 0;
        }

        public int OpenCSV(string path, int saveMode = 1)
        {
            m_path = path;
            m_saveMode = saveMode;
            if (saveMode == 0)
            {
                return m_file.Open(path);
            }

            string path2 = m_path + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            m_file.Close();
            if (File.Exists(path2))
            {
                m_file.Open(path2);
            }

            return 0;
        }

        public void Write(string text, LineColor lineColor = LineColor.White)
        {
            if (m_saveMode == 1)
            {
                string path = m_path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!File.Exists(path))
                {
                    m_file.Close();
                    m_file.Open(path);
                }
            }

            string text2 = string.Format("{0},{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), text);
            m_file.WriteLine(text2);
            if (this.Log_Event_CallBack != null)
            {
                ScrollLogItem scrollLogItem = new ScrollLogItem();
                scrollLogItem.content = text2;
                scrollLogItem.lineColor = lineColor;
                this.Log_Event_CallBack(scrollLogItem);
            }
        }

        public void WriteCSV(string text)
        {
            if (m_saveMode == 1)
            {
                string path = m_path + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".csv";
                if (!File.Exists(path))
                {
                    m_file.Close();
                    m_file.Open(path);
                }
            }

            m_file.WriteLine(text);
        }

        public void Exit()
        {
            if (m_file != null)
            {
                m_file.Close();
            }
        }
    }

    public class ScrollLogItem
    {
        public string content;

        public BaseHelper.LineColor lineColor;
    }
}
