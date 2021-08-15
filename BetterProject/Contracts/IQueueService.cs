using System;

namespace BetterProject
{
    public interface IQueueService
    {
        int GetErrors(int durationInHours, int noOfErrors);
        void Enqueue(DateTime now);
    }
}