using System;

namespace BetterProject
{
    public interface IQueueService
    {
        public int GetErrors(int durationInHours, int noOfErrors);
        void Enqueue(DateTime now);
    }
}