using System;

namespace ExpenseAPI.Common
{
    public interface ITimeService
    {
        DateTime UtcNow { get; }

        DateTime FirstDayOfCurrentMonth { get; }
    }
}
