using Adv;
using BetterProject.Providers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdParty;
using Xunit;

namespace BetterProject.Tests
{
    public class AdvertisementServiceTests
    {
        [Fact]
        public void AdvertisementService_CreatesInstance()
        {

            Mock<IConfigurationService> mockConfigurationService = new Mock<IConfigurationService>();
            Mock<IMemoryCacheService> mockMemoryCacheService = new Mock<IMemoryCacheService>();
            Mock<IQueueService> mockQueueService = new Mock<IQueueService>();
            Mock<INoSqlProvider> mockNoSqlProvider = new Mock<INoSqlProvider>();
            Mock<ISqlProvider> mockSqlProvider = new Mock<ISqlProvider>();

            AdvertisementService adv = new AdvertisementService(mockConfigurationService.Object, 
                                                                mockMemoryCacheService.Object,
                                                                mockQueueService.Object,
                                                                mockNoSqlProvider.Object,
                                                                mockSqlProvider.Object);

            

        }

        [Fact]
        public void AdvertisementService_ThrowsException()
        {
            Mock<IConfigurationService> mockConfigurationService = new Mock<IConfigurationService>();
            Mock<IMemoryCacheService> mockMemoryCacheService = new Mock<IMemoryCacheService>();
            Mock<IQueueService> mockQueueService = new Mock<IQueueService>();
            Mock<INoSqlProvider> mockNoSqlProvider = new Mock<INoSqlProvider>();
            Mock<ISqlProvider> mockSqlProvider = new Mock<ISqlProvider>();

            Assert.Throws<ArgumentNullException>(() => new AdvertisementService(null,
                                                                                mockMemoryCacheService.Object,
                                                                                mockQueueService.Object,
                                                                                mockNoSqlProvider.Object,
                                                                                mockSqlProvider.Object));
            Assert.Throws<ArgumentNullException>(() => new AdvertisementService(mockConfigurationService.Object,
                                                                                null,
                                                                                mockQueueService.Object,
                                                                                mockNoSqlProvider.Object,
                                                                                mockSqlProvider.Object));
            Assert.Throws<ArgumentNullException>(() => new AdvertisementService(mockConfigurationService.Object,
                                                                                mockMemoryCacheService.Object,
                                                                                null,
                                                                                mockNoSqlProvider.Object,
                                                                                mockSqlProvider.Object));
            Assert.Throws<ArgumentNullException>(() => new AdvertisementService(mockConfigurationService.Object,
                                                                                mockMemoryCacheService.Object,
                                                                                mockQueueService.Object,
                                                                                null,
                                                                                mockSqlProvider.Object));
            Assert.Throws<ArgumentNullException>(() => new AdvertisementService(mockConfigurationService.Object,
                                                                                mockMemoryCacheService.Object,
                                                                                mockQueueService.Object,
                                                                                mockNoSqlProvider.Object,
                                                                                null));
        }

        [Fact]
        public void MemoryCacheService_ReturnsAdvertisement()
        {
            Mock<IConfigurationService> mockConfigurationService = new Mock<IConfigurationService>();
            Mock<IMemoryCacheService> mockMemoryCacheService = new Mock<IMemoryCacheService>();
            Mock<IQueueService> mockQueueService = new Mock<IQueueService>();
            Mock<INoSqlProvider> mockNoSqlProvider = new Mock<INoSqlProvider>();
            Mock<ISqlProvider> mockSqlProvider = new Mock<ISqlProvider>();

            mockMemoryCacheService.Setup((m) => m.Get(It.IsAny<string>())).Returns(new Advertisement());

            AdvertisementService adv = new AdvertisementService(mockConfigurationService.Object,
                                                                mockMemoryCacheService.Object,
                                                                mockQueueService.Object,
                                                                mockNoSqlProvider.Object,
                                                                mockSqlProvider.Object);
            Assert.NotNull(adv.GetAdvertisement(It.IsAny<string>()));
        }

        [Theory]
        [InlineData(1,2)]
        [InlineData(11, 2)]
        public void MemoryCacheService_ReturnsNull_NoSqlProvider_ReturnsAdvertisement(int errors, int retryCount)
        {
            Mock<IConfigurationService> mockConfigurationService = new Mock<IConfigurationService>();
            Mock<IMemoryCacheService> mockMemoryCacheService = new Mock<IMemoryCacheService>();
            Mock<IQueueService> mockQueueService = new Mock<IQueueService>();
            Mock<INoSqlProvider> mockNoSqlProvider = new Mock<INoSqlProvider>();
            Mock<ISqlProvider> mockSqlProvider = new Mock<ISqlProvider>();
            
            mockConfigurationService.Setup((c) => c.GetSetting<int>("RetryCount")).Returns(() => retryCount);
            mockMemoryCacheService.Setup((m) => m.Get(It.IsAny<string>())).Returns(It.IsAny<Advertisement>());
            mockQueueService.Setup((q) => q.GetErrors(It.IsAny<int>(), It.IsAny<int>())).Returns(() => errors);
            mockNoSqlProvider.Setup((n) => n.GetAdv(It.IsAny<string>())).Returns(new Advertisement());
            
            AdvertisementService adv = new AdvertisementService(mockConfigurationService.Object,
                                                                mockMemoryCacheService.Object,
                                                                mockQueueService.Object,
                                                                mockNoSqlProvider.Object,
                                                                mockSqlProvider.Object);

            Assert.NotNull(adv.GetAdvertisement(It.IsAny<string>()));
        }
    }
}
