using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace COTUI.控件类库
{
    public class INIDLLHelper
    {
        public string Path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, byte[] retVal, int size, string filePath);

        public INIDLLHelper(string iniPath)
        {
            Path = iniPath;
        }

        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        public string IniReadValue(string section, string key)
        {
            StringBuilder stringBuilder = new StringBuilder(200);
            int privateProfileString = GetPrivateProfileString(section, key, "", stringBuilder, 200, Path);
            return stringBuilder.ToString();
        }

        public List<string> ReadSections()
        {
            List<string> list = new List<string>();
            byte[] array = new byte[65536];
            uint privateProfileStringA = GetPrivateProfileStringA(null, null, null, array, array.Length, Path);
            int num = 0;
            for (int i = 0; i < privateProfileStringA; i++)
            {
                if (array[i] == 0)
                {
                    list.Add(Encoding.Default.GetString(array, num, i - num));
                    num = i + 1;
                }
            }

            return list;
        }

        public List<string> ReadKeys(string section)
        {
            List<string> list = new List<string>();
            byte[] array = new byte[65536];
            uint privateProfileStringA = GetPrivateProfileStringA(section, null, null, array, array.Length, Path);
            int num = 0;
            for (int i = 0; i < privateProfileStringA; i++)
            {
                if (array[i] == 0)
                {
                    list.Add(Encoding.Default.GetString(array, num, i - num));
                    num = i + 1;
                }
            }

            return list;
        }
    }
   
}
