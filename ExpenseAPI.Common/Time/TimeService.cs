using System;

namespace ExpenseAPI.Common
{
    internal class TimeService : ITimeService
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
