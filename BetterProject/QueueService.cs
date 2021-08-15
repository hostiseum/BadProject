using System;
using System.Collections.Generic;
using System.Text;

namespace BetterProject
{
    public class QueueService : IQueueService 
    {
        private static Queue<DateTime> errors = new Queue<DateTime>();

        public QueueService(){
          
        }

        public void Enqueue(DateTime now)
        {
            errors.Enqueue(now);
        }

        public int GetErrors(int durationInHours, int noOfErrors)
        {
            // Count HTTP error timestamps in the last hour
            while (errors.Count > noOfErrors) 
                errors.Dequeue();
          
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
