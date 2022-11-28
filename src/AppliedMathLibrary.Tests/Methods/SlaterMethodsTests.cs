using System;
using System.Linq;
using AppliedMathLibrary.Methods;
using AppliedMathLibrary.Objects;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.Methods
{
    public class SlaterMethodsTests
    {
        [Fact]
        public void BetterBySlaterThan_FirstIsBetter()
        {
            var vector1 = new Vector(2, 3, 4);
            var vector2 = new Vector(1, 2, 3);

            vector1.BetterBySlaterThan(vector2).Should().BeTrue();
            vector2.BetterBySlaterThan(vector1).Should().BeFalse();

            SlaterMethods.CompareBySlater(vector1, vector2).Should().BeTrue();
            SlaterMethods.CompareBySlater(vector2, vector1).Should().BeFalse();
        }

        [Fact]
        public void BetterBySlaterThan_VectorsEqual()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            vector1.BetterBySlaterThan(vector2).Should().BeFalse();
            vector2.BetterBySlaterThan(vector1).Should().BeFalse();

            SlaterMethods.CompareBySlater(vector1, vector2).Should().BeFalse();
            SlaterMethods.CompareBySlater(vector2, vector1).Should().BeFalse();
        }

        [Fact]
        public void BetterBySlaterThan_SecondIsBetter()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(2, 3, 4);

            vector1.BetterBySlaterThan(vector2).Should().BeFalse();
            vector2.BetterBySlaterThan(vector1).Should().BeTrue();

            SlaterMethods.CompareBySlater(vector1, vector2).Should().BeFalse();
            SlaterMethods.CompareBySlater(vector2, vector1).Should().BeTrue();
        }

        [Fact]
        public void BetterBySlaterThan_VectorsIncomparable()
        {
            var vector1 = new Vector(1, 3, 3);
            var vector2 = new Vector(2, 2, 4);

            vector1.BetterBySlaterThan(vector2).Should().BeFalse();
            vector2.BetterBySlaterThan(vector1).Should().BeFalse();

            SlaterMethods.CompareBySlater(vector1, vector2).Should().BeFalse();
            SlaterMethods.CompareBySlater(vector2, vector1).Should().BeFalse();
        }

        [Fact]
        public void BestBySlater_OneBestVectorFound()
        {
            var vector1 = new Vector(4, 4, 4);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 3, 2);

            var bestVectors = SlaterMethods.BestBySlater(new[] { vector1, vector2, vector3 });

            bestVectors.Count.Should().Be(1);
            bestVectors.First().Should().BeEquivalentTo(vector1);
        }

        [Fact]
        public void BestBySlater_TwoEqualBestVectorsFound()
        {
            var vector1 = new Vector(4, 4, 4);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 3, 2);
            var vector4 = new Vector(4, 4, 4);

            var bestVectors = SlaterMethods.BestBySlater(new[] { vector1, vector2, vector3, vector4 });

            bestVectors.Count.Should().Be(2);
            bestVectors.Any(x => x == vector1).Should().BeTrue();
            bestVectors.Any(x => x == vector4).Should().BeTrue();
        }

        [Fact]
        public void BestBySlater_TwoUnequalBestVectorsFound()
        {
            var vector1 = new Vector(4, 4, 3);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(4, 3, 4);
            var vector4 = new Vector(1, 3, 2);

            var bestVectors = SlaterMethods.BestBySlater(new[] { vector1, vector2, vector3, vector4 });

            bestVectors.Count.Should().Be(2);
            bestVectors.Any(x => x == vector1).Should().BeTrue();
            bestVectors.Any(x => x == vector3).Should().BeTrue();
        }

        #region Negative scenarios

        [Fact]
        public void BetterBySlaterThan_DifferentDimensionVectorsCompared_ExceptionThrown()
        {
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);

            Assert.Throws<ArgumentException>(() =>
            {
                vector1.BetterBySlaterThan(vector2);
            });
        }

        [Fact]
        public void BestBySlater_DifferentDimensionVectorsCompared_ExceptionThrown()
        {
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 2, 3, 4);

            Assert.Throws<ArgumentException>(() => SlaterMethods.BestBySlater(new[] { vector1, vector2, vector3 }));
        }

        #endregion
    }
}
