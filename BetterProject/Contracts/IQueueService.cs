using System;
using System.Collections.Concurrent;

namespace BetterProject
{
    public interface IQueueService
    {
        int GetErrorsCount(int durationInHours, int maxErrorsTolerance);
        void Enqueue(DateTime now);
        ConcurrentQueue<DateTime> GetCurrentQueue();
    }
}