using System;
using System.IO;
using System.Collections.Generic;

namespace COTUI.通用功能类
{
    /// <summary>
    /// 配置文件管理器（INI格式）
    /// 
    /// <para><b>使用方式：</b></para>
    /// 1. 在解决方案根目录手动创建 Config.ini
    /// 2. 编辑配置参数
    /// 3. ConfigManager 自动读取配置
    /// </summary>
    public class ConfigManager
    {
        private static readonly Lazy<ConfigManager> instance = new Lazy<ConfigManager>(() => new ConfigManager());
        public static ConfigManager Instance => instance.Value;

        private readonly string configFilePath;
        private Dictionary<string, Dictionary<string, string>> configData;

        private ConfigManager()
        {
            // 配置文件路径：解决方案根目录\Config.ini
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            
            // 如果是Debug/Release模式，向上找到解决方案目录
            // 例如：bin\Debug → 往上2层 → 解决方案目录
            DirectoryInfo directory = new DirectoryInfo(appPath);
            
            // 尝试向上查找Config.ini（最多向上3层）
            for (int i = 0; i < 3; i++)
            {
                string testPath = Path.Combine(directory.FullName, "Config.ini");
                if (File.Exists(testPath))
                {
                    configFilePath = testPath;
                    break;
                }
                
                if (directory.Parent != null)
                {
                    directory = directory.Parent;
                }
            }

            // 如果找不到，使用exe同目录
            if (string.IsNullOrEmpty(configFilePath))
            {
                configFilePath = Path.Combine(appPath, "Config.ini");
            }

            configData = new Dictionary<string, Dictionary<string, string>>();

            // 检查配置文件是否存在
            if (!File.Exists(configFilePath))
            {
                Gvar.Logger.Info($"? 配置文件不存在: {configFilePath}");
                Gvar.Logger.Info("请在解决方案根目录手动创建 Config.ini 文件！");
                
                // 不自动创建，而是提示用户手动创建
                throw new FileNotFoundException(
                    $"配置文件不存在！\n" +
                    $"请在以下路径手动创建 Config.ini 文件：\n" +
                    $"{configFilePath}\n\n" +
                    $"参考模板请查看项目文档.",
                    configFilePath
                );
            }

            // 加载配置
            LoadConfig();
            
            Gvar.Logger.Info($"? 配置文件已加载: {configFilePath}");
            Gvar.Logger.Info($"   分组数量: {configData.Count}");
            
            // 控制台输出（方便调试）
            System.Diagnostics.Debug.WriteLine($"配置文件路径: {configFilePath}");
            System.Diagnostics.Debug.WriteLine($"配置分组数: {configData.Count}");
        }

        private void LoadConfig()
        {
            try
            {
                if (!File.Exists(configFilePath))
                {
                    throw new FileNotFoundException($"配置文件不存在: {configFilePath}");
                }

                configData.Clear();
                string currentSection = "";
                int lineNumber = 0;

                foreach (string line in File.ReadAllLines(configFilePath))
                {
                    lineNumber++;
                    string trimmedLine = line.Trim();

                    // 跳过空行和注释
                    if (string.IsNullOrEmpty(trimmedLine) || 
                        trimmedLine.StartsWith("#") || 
                        trimmedLine.StartsWith(";"))
                    {
                        continue;
                    }

                    // 解析分组 [Section]
                    if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                    {
                        currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                        if (!configData.ContainsKey(currentSection))
                        {
                            configData[currentSection] = new Dictionary<string, string>();
                        }
                        continue;
                    }

                    // 解析键值对 Key=Value
                    int equalIndex = trimmedLine.IndexOf('=');
                    if (equalIndex > 0 && !string.IsNullOrEmpty(currentSection))
                    {
                        string key = trimmedLine.Substring(0, equalIndex).Trim();
                        string value = trimmedLine.Substring(equalIndex + 1).Trim();
                        
                        configData[currentSection][key] = value;
                        
                        Gvar.Logger.Info($"[{currentSection}] {key} = {value}");
                    }
                    else if (equalIndex < 0 && !string.IsNullOrEmpty(trimmedLine))
                    {
                        Gvar.Logger.Info($"配置文件第{lineNumber}行格式错误: {trimmedLine}");
                    }
                }

                Gvar.Logger.Info($"配置文件加载成功，共{configData.Count}个分组");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载配置文件失败");
                throw;
            }
        }

        public void SaveConfig()
        {
            try
            {
                // 备份原配置文件
                if (File.Exists(configFilePath))
                {
                    string backupPath = configFilePath + ".bak";
                    File.Copy(configFilePath, backupPath, true);
                    Gvar.Logger.Info($"配置文件已备份: {backupPath}");
                }

                using (StreamWriter writer = new StreamWriter(configFilePath, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("# 设备配置文件");
                    writer.WriteLine($"# 最后修改: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine();

                    foreach (var section in configData)
                    {
                        writer.WriteLine($"[{section.Key}]");
                        foreach (var kvp in section.Value)
                        {
                            writer.WriteLine($"{kvp.Key}={kvp.Value}");
                        }
                        writer.WriteLine();
                    }
                }

                Gvar.Logger.Info("配置文件已保存");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "保存配置文件失败");
                throw;
            }
        }

        public void ReloadConfig()
        {
            LoadConfig();
            Gvar.Logger.Info("配置文件已重新加载");
        }

        public string GetValue(string section, string key, string defaultValue = "")
        {
            try
            {
                if (configData.ContainsKey(section) && configData[section].ContainsKey(key))
                {
                    return configData[section][key];
                }
                else
                {
                    Gvar.Logger.Info($"配置项不存在: [{section}] {key}，使用默认值: {defaultValue}");
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.Info($"读取配置失败: [{section}] {key}, {ex.Message}");
            }

            return defaultValue;
        }

        public int GetIntValue(string section, string key, int defaultValue = 0)
        {
            string value = GetValue(section, key, defaultValue.ToString());
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            
            Gvar.Logger.Info($"配置值无法转换为整数: [{section}] {key} = {value}，使用默认值: {defaultValue}");
            return defaultValue;
        }

        public bool GetBoolValue(string section, string key, bool defaultValue = false)
        {
            string value = GetValue(section, key, defaultValue.ToString()).ToLower();
            if (value == "true" || value == "1" || value == "yes")
            {
                return true;
            }
            else if (value == "false" || value == "0" || value == "no")
            {
                return false;
            }
            
            Gvar.Logger.Info($"配置值无法转换为布尔值: [{section}] {key} = {value}，使用默认值: {defaultValue}");
            return defaultValue;
        }

        public double GetDoubleValue(string section, string key, double defaultValue = 0.0)
        {
            string value = GetValue(section, key, defaultValue.ToString());
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            
            Gvar.Logger.Info($"配置值无法转换为浮点数: [{section}] {key} = {value}，使用默认值: {defaultValue}");
            return defaultValue;
        }

        public void SetValue(string section, string key, string value)
        {
            try
            {
                if (!configData.ContainsKey(section))
                {
                    configData[section] = new Dictionary<string, string>();
                    Gvar.Logger.Info($"创建新的配置分组: [{section}]");
                }

                configData[section][key] = value;
                Gvar.Logger.Info($"配置已更新: [{section}] {key} = {value}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"设置配置值失败: [{section}] {key}={value}");
            }
        }

        public void SetValue(string section, string key, int value)
        {
            SetValue(section, key, value.ToString());
        }

        public void SetValue(string section, string key, bool value)
        {
            SetValue(section, key, value.ToString().ToLower());
        }

        public void SetValue(string section, string key, double value)
        {
            SetValue(section, key, value.ToString());
        }

        public bool Exists(string section, string key)
        {
            return configData.ContainsKey(section) && configData[section].ContainsKey(key);
        }

        public string GetConfigFilePath()
        {
            return configFilePath;
        }

        public string GetConfigInfo()
        {
            return $"配置文件路径: {configFilePath}\n" +
                   $"文件存在: {File.Exists(configFilePath)}\n" +
                   $"分组数量: {configData.Count}\n" +
                   $"文件大小: {(File.Exists(configFilePath) ? new FileInfo(configFilePath).Length : 0)} 字节\n" +
                   $"编码: UTF-8";
        }
    }
}
