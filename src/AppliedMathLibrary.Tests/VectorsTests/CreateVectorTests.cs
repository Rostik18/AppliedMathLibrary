using AppliedMathLibrary.Objects;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace AppliedMathLibrary.Tests.VectorsTests
{
    public class CreateVectorTests
    {
        #region Positive scenarios

        [Theory]
        [InlineData(1, new double[] { 1 })]
        [InlineData(2, new double[] { 1, 2 })]
        public void CreateVector_ProperDimensionAndValues_VectorCreated(int n, double[] values)
        {
            var vector = new Vector(values);

            vector.Should().NotBeNull();
            vector.Dimension.Should().Be(n);

            for (var i = 0; i < n; i++)
            {
                vector[i].Should().Be(values[i]);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CreateVector_WithProperDimension_VectorCreatedWithZeroValues(int n)
        {
            var vector = new Vector(n);

            vector.Should().NotBeNull();
            vector.Dimension.Should().Be(n);

            vector.All(x => x == 0).Should().BeTrue();
        }

        [Fact]
        public void CreateVectorWithCopyConstructor_VectorCreated()
        {
            var vector = new Vector(1, 2);

            var copiedVector = new Vector(vector);

            copiedVector.Should().NotBeNull();
            copiedVector.Dimension.Should().Be(2);
            copiedVector[0].Should().Be(1);
            copiedVector[1].Should().Be(2);
        }

        [Fact]
        public void CreateVectorFromPoint_VectorCreated()
        {
            var point = new Point(1, 2);

            var vector = new Point(point);

            vector.Should().NotBeNull();
            vector.Dimension.Should().Be(2);
            vector[0].Should().Be(1);
            vector[1].Should().Be(2);
        }

        #endregion

        #region Negative scenarios

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateEmptyVector_ExceptionThrown(int n)
        {
            Assert.Throws<ArgumentException>(() => new Vector(n));
        }

        #endregion
    }
}
