using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
        // BONUS: What's the difference between DateTime and DateTimeOffset?
        // A DateTime value defines a particular date and time and provides limited information about the time zone. 
        // The DateTimeOffset structure represents a date and time value, together with an offset that indicates how much that value differs from UTC. 
        public DateTimeOffset Start;
		public DateTimeOffset End;

        // BONUS: What's the difference between decimal and double?
        // The Decimal and Double are different in the way that they store the values. Major difference is their Precision.
        // Double has double precision(64 bit) floating type datatype while decimal has (128 bit) floating type datatype.
        public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}
