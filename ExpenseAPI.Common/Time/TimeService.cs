using System;

namespace ExpenseAPI.Common
{
    internal class TimeService : ITimeService
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
        
        public DateTime FirstDayOfCurrentMonth
        {
            get
            {
                var utcNow = UtcNow;

                return new DateTime(utcNow.Year, utcNow.Month, 1);
            }
        }
    }
}
