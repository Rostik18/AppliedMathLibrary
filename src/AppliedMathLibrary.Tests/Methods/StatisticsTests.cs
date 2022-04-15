using AppliedMathLibrary.Methods;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AppliedMathLibrary.Tests.Methods
{
    public class StatisticsTests
    {
        [Fact]
        public void StatisticsMean()
        {
            Statistics.Mean(new List<double> { 1, 2, 6 }).Should().Be(3.0);
            Statistics.Mean(new List<double> { -5, -1, -3, 6 }).Should().Be(-0.75);
        }

        [Fact]
        public void StatisticsMedian()
        {
            Statistics.Median(new List<double> { }).Should().Be(0);
            Statistics.Median(new List<double> { 2 }).Should().Be(2.0);
            Statistics.Median(new List<double> { 2, 4 }).Should().Be(3.0);
            Statistics.Median(new List<double> { 2, 3, 5 }).Should().Be(3.0);
            Statistics.Median(new List<double> { 2, 3, 5, 6 }).Should().Be(4.0);
            Statistics.Median(new List<double> { 2, 3, 5, 6, 8 }).Should().Be(5.0);
        }

        [Fact]
        public void StatisticsMode()
        {
            Statistics.Mode(new List<double> { 1, 1, 2, 6 }).Should().BeEquivalentTo(new double[] { 1 });
            Statistics.Mode(new List<double> { 1, 1, 2, 5, 6, 6, 9 }).Should().BeEquivalentTo(new double[] { 1, 6 });
        }

        [Fact]
        public void StatisticsGenerateRandomArray()
        {
            var randArray1 = Statistics.GenerateRandomArray(4);
            randArray1.Should().HaveCount(4);
            randArray1.All(x => x >= 0 && x <= 1).Should().BeTrue();

            var randArray2 = Statistics.GenerateRandomArray(5, -2, 3);
            randArray2.Should().HaveCount(5);
            randArray2.All(x => x >= -2 && x <= 3).Should().BeTrue();
        }

        [Fact]
        public void StatisticsRandomSampleByRule1()
        {
            Statistics.RandomSampleByRule1(new List<double> { 1, 2, 3, 4, 5, 6 }, 2).Count.Should().Be(2);
            Statistics.RandomSampleByRule1(new List<double> { 1, 2, 3, 4, 5, 6 }, 3).Count.Should().Be(3);
            Statistics.RandomSampleByRule1(new List<double> { 1, 2, 3, 4, 5, 6 }, 4).Count.Should().Be(4);
        }

        [Fact]
        public void StatisticsRandomSampleByRule2()
        {
            Statistics.RandomSampleByRule2(new List<double> { 1, 2, 3, 4, 5, 6 }, 2).Count.Should().Be(2);
            Statistics.RandomSampleByRule2(new List<double> { 1, 2, 3, 4, 5, 6 }, 3).Count.Should().Be(3);
            Statistics.RandomSampleByRule2(new List<double> { 1, 2, 3, 4, 5, 6 }, 4).Count.Should().Be(4);
        }

    }
}
