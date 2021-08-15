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
    public class MemoryCacheServiceTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetCurrentCache_ReturnsExistingCache(bool addAdvertisement)
        {
            IMemoryCacheService memoryCacheService = new MemoryCacheService();
            Assert.NotNull(memoryCacheService.GetCurrentCache());

            if (addAdvertisement)
            {
                memoryCacheService.Set("Key1", new Advertisement() { WebId = "Web1" }, DateTime.Now.AddMinutes(1));
                Assert.NotNull(memoryCacheService.Get("Key1"));
            }

        }
    }
}
