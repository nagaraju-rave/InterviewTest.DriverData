using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
    [TestFixture]
    public class DeliveryDriverAnalyserTests
    {
        private AnalyserConfiguration analyserConfiguration;
        private RatingCalculator ratingCalculator;

        [SetUp]
        public void Initialize()
        {
            analyserConfiguration = new AnalyserConfiguration
            {
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                MaxSpeed = 30m
            };
            ratingCalculator = new RatingCalculator();
        }

        [Test]
        public void ShouldYieldCorrectValues()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            // Act
            var actualResult = new DeliveryDriverAnalyser(analyserConfiguration, ratingCalculator).Analyse(CannedDrivingData.History);


            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void EmptyHistory_ShouldThrowArgumentNullException()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            //Act
            //Assert
            Assert.Throws(typeof(ArgumentNullException), delegate { new DeliveryDriverAnalyser(analyserConfiguration, ratingCalculator).Analyse(CannedDrivingData.EmptyHistory); });
        }

        [Test]
        public void HistoryWithOutsideAnalyserTimeRange_ShouldReturnZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new DeliveryDriverAnalyser(analyserConfiguration, ratingCalculator).Analyse(CannedDrivingData.HistoryWithOutsideAnalyserTimeRange);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void HistoryWithExceedingSpeedLimit_ShouldReturnZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new DeliveryDriverAnalyser(analyserConfiguration, ratingCalculator).Analyse(CannedDrivingData.HistoryWithExceedingSpeedLimit);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
