using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BetterProject
{
    public class QueueService : IQueueService 
    {
        private ConcurrentQueue<DateTime> errors = new ConcurrentQueue<DateTime>();

        public QueueService() { }

        public ConcurrentQueue<DateTime> GetCurrentQueue()
        {
            return errors;
        }

        public void Enqueue(DateTime now)
        {
            errors.Enqueue(now);
        }

        public int GetErrorsCount(int durationInHours, int maxErrorsTolerance)
        {
            Console.WriteLine($"Errors in the queue: {errors?.Count}");
            
            while (errors.Count > maxErrorsTolerance)
            {
                DateTime error;
                errors.TryDequeue(out error);
            }
            // Count HTTP error timestamps in the last hour
            int errorCount = 0;
            foreach (var err in errors)
            {
                if (err > DateTime.Now.AddHours(-1))
                {
                    errorCount++;
                }
            }

            return errorCount;
        }
    }
}
