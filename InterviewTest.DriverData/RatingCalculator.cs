using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData
{
    internal class RatingCalculator
    {
        /// <summary>
        /// Returns the overall rating for the analyser.
        /// </summary>
        /// <param name="analysedPeriods"></param>
        /// <param name="undocumentedPeriods"></param>
        /// <returns></returns>
        public decimal CalculateOverallRating(IEnumerable<PeriodRating> analysedPeriods, IEnumerable<PeriodRating> undocumentedPeriods)
        {
            // Calculate the weighted sum by taking total of products of durations and ratings
            var weightedSum = analysedPeriods.Select(x => x.Duration * x.Rating).Sum();

            // Calculate total duration including undocumented periods
            var totalDuration = analysedPeriods.Sum(x => x.Duration) + undocumentedPeriods.Sum(x => x.Duration);

            //Calculate overall rating by dividing the weighted sum by total duration including undocumented periods.
            return totalDuration == 0 ? 0 : (weightedSum / totalDuration);
        }
    }
}
