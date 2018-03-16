using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class CannedDrivingData
	{
		private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

        // BONUS: What's so great about IReadOnlyCollections?
        // It will provide a strongly typed read-only collection of item that is not allowed to add a new item or replace the existing with the other one during runtime.

        public static readonly IReadOnlyCollection<Period> History = new[]
		{
			new Period
			{
				Start = _day + new TimeSpan(0, 0, 0),
				End = _day + new TimeSpan(8, 54, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(8, 54, 0),
				End = _day + new TimeSpan(9, 28, 0),
				AverageSpeed = 28m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 28, 0),
				End = _day + new TimeSpan(9, 35, 0),
				AverageSpeed = 33m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 50, 0),
				End = _day + new TimeSpan(12, 35, 0),
				AverageSpeed = 25m
			},
			new Period
			{
				Start = _day + new TimeSpan(12, 35, 0),
				End = _day + new TimeSpan(13, 30, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(13, 30, 0),
				End = _day + new TimeSpan(19, 12, 0),
				AverageSpeed = 29m
			},
			new Period
			{
				Start = _day + new TimeSpan(19, 12, 0),
				End = _day + new TimeSpan(24, 0, 0),
				AverageSpeed = 0m
			}
		};

        public static readonly IReadOnlyCollection<Period> EmptyHistory = new Period[] { };

        public static readonly IReadOnlyCollection<Period> HistoryWithOutsideAnalyserTimeRange = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 54, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(19, 12, 0),
                End = _day + new TimeSpan(24, 0, 0),
                AverageSpeed = 0m
            }
        };

        public static readonly IReadOnlyCollection<Period> HistoryWithExceedingSpeedLimit = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 54, 0),
                AverageSpeed = 31m
            },
            new Period
            {
                Start = _day + new TimeSpan(8, 54, 0),
                End = _day + new TimeSpan(9, 28, 0),
                AverageSpeed = 32m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 28, 0),
                End = _day + new TimeSpan(9, 35, 0),
                AverageSpeed = 33m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 50, 0),
                End = _day + new TimeSpan(12, 35, 0),
                AverageSpeed = 34m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 35, 0),
                End = _day + new TimeSpan(13, 30, 0),
                AverageSpeed = 35m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 30, 0),
                End = _day + new TimeSpan(19, 12, 0),
                AverageSpeed = 36m
            },
            new Period
            {
                Start = _day + new TimeSpan(19, 12, 0),
                End = _day + new TimeSpan(24, 0, 0),
                AverageSpeed = 37m
            }
        };
    }
}
