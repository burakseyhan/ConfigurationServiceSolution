using System;
using System.Configuration;
using System.Linq;

namespace ConfigurationService.Library
{
    public class ConfigurationHelper<T>
    {
        private string FilePath { get; set; }

        public ConfigurationHelper(string filePath)
        {
            FilePath = filePath;

        }

        public T GetValue(string key)
        {
            ExeConfigurationFileMap fileMap = fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = FilePath + ".config";

            Configuration otherConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (otherConfig.AppSettings.Settings.Count > 0)
            {
                if (otherConfig.AppSettings.Settings.AllKeys.Contains(key))
                {
                    object value = otherConfig.AppSettings.Settings[key].Value;

                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }

            return (T)Convert.ChangeType(default(T), typeof(T));
        }

        public void SetValue(string key, string value)
        {
            ExeConfigurationFileMap fileMap = fileMap = new ExeConfigurationFileMap();

            fileMap.ExeConfigFilename = FilePath + ".config";

            Configuration otherConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            
            otherConfig.AppSettings.Settings.Add(key, value);

            otherConfig.Save(ConfigurationSaveMode.Modified);
        }
    }
}
