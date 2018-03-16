using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData.Extension;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // The internal class are accessible only inside the same assembly.
    // The public AnalyserLookup factory provides access to the required analyser. 
    // By not exposing all the different analysers as public we can keep the assembly's public API footprint small and clean
    internal class DeliveryDriverAnalyser : IAnalyser
    {
        private readonly AnalyserConfiguration _analyserConfiguration;
        private readonly RatingCalculator _ratingCalculator;

        public DeliveryDriverAnalyser(AnalyserConfiguration analyserConfiguration, RatingCalculator ratingCalculator)
        {
            _analyserConfiguration = analyserConfiguration;
            _ratingCalculator = ratingCalculator;
        }

        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
            ValidateHistory(history);

            HistoryAnalysis historyAnalysis = new HistoryAnalysis();

            IEnumerable<Period> documentedPeriods = GetDocumentedPeriods(history);

            IEnumerable<PeriodRating> undocumentedPeriods = GetUndocumentedPeriods(documentedPeriods);

            IEnumerable<PeriodRating> analysedPeriods = AnalysePeriods(documentedPeriods);

            historyAnalysis.AnalysedDuration = new TimeSpan(analysedPeriods.Select(x => (long)x.Duration).Sum());
            historyAnalysis.DriverRating = _ratingCalculator.CalculateOverallRating(analysedPeriods, undocumentedPeriods);

            return historyAnalysis;
        }

        /// <summary>
        /// Validate whether history contains data or not. 
        /// Throws argument null exception if data is null or empty list is passed.
        /// </summary>
        /// <param name="history"></param>
        private void ValidateHistory(IReadOnlyCollection<Period> history)
        {
            if (history == null || !history.Any())
            {
                throw new ArgumentNullException(nameof(history), "History data not passed.");
            }
        }

        /// <summary>
        /// Get the list of periods which falls between the analyser start and end time. 
        /// Ignore anything outside of Analyser timing.
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        private IEnumerable<Period> GetDocumentedPeriods(IReadOnlyCollection<Period> history)
        {
            return history.Where(p => (
                                    (ValidatePeriodTimeInAnalyserShiftTime(p.Start))
                                    || (ValidatePeriodTimeInAnalyserShiftTime(p.End))
                                )).ToList();
        }

        /// <summary>
        /// Validate whether the given period time is in the Analyser shift time.
        /// </summary>
        /// <param name="periodTime"></param>
        /// <returns></returns>
        private bool ValidatePeriodTimeInAnalyserShiftTime(DateTimeOffset periodTime)
        {
            return periodTime.TimeOfDay > _analyserConfiguration.StartTime && periodTime.TimeOfDay < _analyserConfiguration.EndTime;
        }

        /// <summary>
        /// Get the list of missing period durations and ratings between the analyser start and end time.
        /// </summary>
        /// <param name="documentedPeriods"></param>
        /// <returns></returns>
        private IEnumerable<PeriodRating> GetUndocumentedPeriods(IEnumerable<Period> documentedPeriods)
        {
            var undocumentedPeriods = new List<PeriodRating>();

            var firstDocumentedPeriod = documentedPeriods.FirstOrDefault();

            if (firstDocumentedPeriod == null)
            {
                return undocumentedPeriods;
            }

            DateTimeOffset previousPeriodEndTime = firstDocumentedPeriod.Start;
            foreach (Period period in documentedPeriods)
            {
                if (previousPeriodEndTime != period.Start)
                {
                    undocumentedPeriods.Add(new PeriodRating
                    {
                        Duration = period.Start.Subtract(previousPeriodEndTime).Ticks,
                        Rating = 0
                    });
                }
                previousPeriodEndTime = period.End;
            }

            return undocumentedPeriods;
        }

        /// <summary>
        /// Calculates the duration and rating for documented periods.
        /// </summary>
        /// <param name="documentedPeriods"></param>
        /// <returns></returns>
        private IEnumerable<PeriodRating> AnalysePeriods(IEnumerable<Period> documentedPeriods)
        {
            var analysedPeriods = new List<PeriodRating>();

            foreach (Period period in documentedPeriods)
            {
                TimeSpan Start = period.GetStartTime(_analyserConfiguration.StartTime);
                TimeSpan End = period.GetEndTime(_analyserConfiguration.EndTime);

                analysedPeriods.Add(new PeriodRating
                {
                    Duration = End.Subtract(Start).Ticks,
                    // Rate linearly if speed is between zero and analyser max speed, otherwise evaluate to zero.
                    Rating = period.AverageSpeed > 0 && period.AverageSpeed < _analyserConfiguration.MaxSpeed ? period.AverageSpeed / _analyserConfiguration.MaxSpeed : 0
                });
            }

            return analysedPeriods;
        }
    }
}