using AppliedMathLibrary.Points;
using AppliedMathLibrary.Vectors;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace AppliedMathLibrary.Tests.PointsTests
{
    public class CreatePointTests
    {
        #region Positive scenarios

        [Theory]
        [InlineData(1, new double[] { 1 })]
        [InlineData(2, new double[] { 1, 2 })]
        public void CreatePoint_ProperDimensionAndValues_PointCreated(int n, double[] values)
        {
            var point = new Point(values);

            point.Should().NotBeNull();
            point.Dimension.Should().Be(n);

            for (var i = 0; i < n; i++)
            {
                point[i].Should().Be(values[i]);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CreatePoint_WithProperDimension_PointCreatedWithZeroValues(int n)
        {
            var point = new Point(n);

            point.Should().NotBeNull();
            point.Dimension.Should().Be(n);

            point.All(x => x == 0).Should().BeTrue();
        }

        [Fact]
        public void CreatePointWithCopyConstructor_PointCreated()
        {
            var point = new Point(1, 2);

            var copiedPoint = new Point(point);

            copiedPoint.Should().NotBeNull();
            copiedPoint.Dimension.Should().Be(2);
            copiedPoint[0].Should().Be(1);
            copiedPoint[1].Should().Be(2);
        }

        [Fact]
        public void CreatePointFromVector_PointCreated()
        {
            var vector = new Vector(1, 2);

            var point = new Point(vector);

            point.Should().NotBeNull();
            point.Dimension.Should().Be(2);
            point[0].Should().Be(1);
            point[1].Should().Be(2);
        }

        #endregion

        #region Negative scenarios

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateEmptyPoint_ExceptionThrown(int n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var point = new Point(n);
            });
        }

        #endregion
    }
}
