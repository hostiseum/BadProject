using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BetterProject
{
    public class ConfigurationService : IConfigurationService
    {
        public T GetSetting<T>(string key)
        {
           return (T) Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }
    }
}
