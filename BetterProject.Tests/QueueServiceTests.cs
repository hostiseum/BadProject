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
    public class QueueServiceTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetCurrentQueue_ReturnsExistingQueue(bool addError)
        {
            IQueueService queueService = new QueueService();
            Assert.Empty(queueService.GetCurrentQueue());

            if (addError)
            {
                queueService.Enqueue(It.IsAny<DateTime>());
                Assert.NotEmpty(queueService.GetCurrentQueue());
            }
        }

        [Theory]
        [InlineData(1, 10)]
        public void GetErrorsCount_ReturnsCount(int durationInHours, int maxErrorsTolerance)
        {
            IQueueService queueService = new QueueService();

            //Add Errors that were added more than the duration that we want to check
            for (int i = 0; i < maxErrorsTolerance; i++)
            {
                //Setup some errors that are outside the duration that we are interested in
                queueService.Enqueue(DateTime.Now.AddHours(durationInHours * -2));
            }

            for (int i = 0; i < maxErrorsTolerance; i++)
            {
                //Setup some errors that are inside the duration that we are interested in
                queueService.Enqueue(DateTime.Now.AddHours(durationInHours * -0.5));
            }

            var errorCount = queueService.GetCurrentQueue().Count();
            Assert.Equal(maxErrorsTolerance * 2, errorCount);
            Assert.Equal(maxErrorsTolerance, queueService.GetErrorsCount(durationInHours, maxErrorsTolerance));
            var errorCountAfterMethodCall = queueService.GetCurrentQueue().Count();

            Assert.Equal(maxErrorsTolerance, errorCountAfterMethodCall);

        }
    }
}
