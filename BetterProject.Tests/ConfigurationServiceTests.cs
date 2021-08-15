using Adv;
using BetterProject.Providers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirdParty;
using Xunit;

namespace BetterProject.Tests
{
    public class ConfigurationServiceTests
    {
        [Fact]
        public void GetSetting_Success()
        {
            ConfigurationService configurationService = new ConfigurationService();
            var appSettings = new System.Collections.Specialized.NameValueCollection();
            appSettings["RetryCount"] = "3";
            appSettings["DurationInHours"] = "1";
            appSettings["MaxErrorsTolerance"] = "10";
            appSettings["WrongDataType"] = "abcd";
            configurationService.AppSettings = appSettings;

            Assert.Equal(3, configurationService.GetSetting<int>("RetryCount"));
            Assert.Equal(1, configurationService.GetSetting<int>("DurationInHours"));
            Assert.Equal(10, configurationService.GetSetting<int>("MaxErrorsTolerance"));
            
        }

        [Fact]
        public void GetSetting_ThrowsInvalidCastException()
        {
            ConfigurationService configurationService = new ConfigurationService();
            var appSettings = new System.Collections.Specialized.NameValueCollection();
            appSettings["RetryCount"] = "3";
            appSettings["DurationInHours"] = "1";
            appSettings["MaxErrorsTolerance"] = "10";
            appSettings["WrongDataType"] = "abcd";
            configurationService.AppSettings = appSettings;
            Assert.Throws<InvalidCastException>(() => configurationService.GetSetting<int>("WrongKey"));
        }

        [Fact]
        public void GetSetting_ThrowsFormatException()
        {
            ConfigurationService configurationService = new ConfigurationService();
            var appSettings = new System.Collections.Specialized.NameValueCollection();
            appSettings["RetryCount"] = "3";
            appSettings["DurationInHours"] = "1";
            appSettings["MaxErrorsTolerance"] = "10";
            appSettings["WrongDataType"] = "abcd";
            configurationService.AppSettings = appSettings;

         
            Assert.Throws<FormatException>(() => configurationService.GetSetting<int>("WrongDataType"));
        }
    }
}
