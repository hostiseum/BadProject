using Configuration.Interface;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;

namespace BetterProject
{
    public class ConfigurationService : IConfigurationService
    {
        NameValueCollection _appSettings;

        public ConfigurationService()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        public NameValueCollection AppSettings
        {
            get
            {
                return _appSettings;
            }

            set
            {
                _appSettings = value;
            }
        }



        public T GetSetting<T>(string key)
        {
           return (T) Convert.ChangeType(_appSettings[key], typeof(T));
        }
    }
}
