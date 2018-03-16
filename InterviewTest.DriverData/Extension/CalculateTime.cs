using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Extension
{
    public static class CalculateTime
    {
        public static TimeSpan GetStartTime(this Period period, TimeSpan analyserStartTime)
        {
            // Set period start time as analyser start time if period start time is not in the analyser time range.;
            return period.Start.TimeOfDay < analyserStartTime ? analyserStartTime : period.Start.TimeOfDay;
        }

        public static TimeSpan GetEndTime(this Period period, TimeSpan analyserEndTime)
        {
            // Set period end time as analyser end time if period end time is not in the analyser time range.
            return period.End.TimeOfDay > analyserEndTime ? analyserEndTime : period.End.TimeOfDay;
        }
    }
}
