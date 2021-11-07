using AppliedMathLibrary.Points;
using AppliedMathLibrary.Vectors;
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

        [Theory]
        [InlineData(new double[] { 1, 2, 3 })]
        public void CreateVector3_ProperDimensionAndValues_VectorCreated(double[] values)
        {
            var vector = new Vector3(values);
            var emptyVector = new Vector3();
            var copyVector = new Vector3(vector);

            vector.Should().NotBeNull();
            vector.Dimension.Should().Be(3);

            for (var i = 0; i < vector.Dimension; i++)
            {
                vector[i].Should().Be(values[i]);
                emptyVector[i].Should().Be(0);
                copyVector[i].Should().Be(values[i]);
            }

            vector.GetHashCode().Should().NotBe(copyVector.GetHashCode());
        }

        [Theory]
        [InlineData(new double[] { 1, 2 })]
        public void CreateVector2_ProperDimensionAndValues_VectorCreated(double[] values)
        {
            var vector = new Vector2(values);
            var emptyVector = new Vector2();
            var copyVector = new Vector2(vector);

            vector.Should().NotBeNull();
            vector.Dimension.Should().Be(2);

            for (var i = 0; i < vector.Dimension; i++)
            {
                vector[i].Should().Be(values[i]);
                emptyVector[i].Should().Be(0);
                copyVector[i].Should().Be(values[i]);
            }

            vector.GetHashCode().Should().NotBe(copyVector.GetHashCode());
        }

        #endregion

        #region Negative scenarios

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateEmptyVector_ExceptionThrown(int n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var vector = new Vector(n);
            });
        }

        #endregion
    }
}
