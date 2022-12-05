using System;
using System.Linq;
using AppliedMathLibrary.Methods;
using AppliedMathLibrary.Objects;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.Methods
{
    public class ParetoEqualityTests
    {
        [Fact]
        public void BetterByParetoThan_FirstIsBetter()
        {
            var vector1 = new Vector(1, 2, 4);
            var vector2 = new Vector(1, 2, 3);

            vector1.BetterByParetoThan(vector2).Should().BeTrue();
            vector2.BetterByParetoThan(vector1).Should().BeFalse();

            ParetoMethods.CompareByPareto(vector1, vector2).Should().BeTrue();
            ParetoMethods.CompareByPareto(vector2, vector1).Should().BeFalse();
        }

        [Fact]
        public void BetterByParetoThan_VectorsEqual()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            vector1.BetterByParetoThan(vector2).Should().BeFalse();
            vector2.BetterByParetoThan(vector1).Should().BeFalse();

            ParetoMethods.CompareByPareto(vector1, vector2).Should().BeFalse();
            ParetoMethods.CompareByPareto(vector2, vector1).Should().BeFalse();
        }

        [Fact]
        public void BetterByParetoThan_SecondIsBetter()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 4);

            vector1.BetterByParetoThan(vector2).Should().BeFalse();
            vector2.BetterByParetoThan(vector1).Should().BeTrue();

            ParetoMethods.CompareByPareto(vector1, vector2).Should().BeFalse();
            ParetoMethods.CompareByPareto(vector2, vector1).Should().BeTrue();
        }

        [Fact]
        public void BetterByParetoThan_VectorsIncomparable()
        {
            var vector1 = new Vector(1, 3, 3);
            var vector2 = new Vector(1, 2, 4);

            vector1.BetterByParetoThan(vector2).Should().BeFalse();
            vector2.BetterByParetoThan(vector1).Should().BeFalse();

            ParetoMethods.CompareByPareto(vector1, vector2).Should().BeFalse();
            ParetoMethods.CompareByPareto(vector2, vector1).Should().BeFalse();
        }

        [Fact]
        public void BestByPareto_OneBestVectorFound()
        {
            var vector1 = new Vector(4, 4, 4);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 3, 2);

            var bestVectors = ParetoMethods.BestByPareto(new[] { vector1, vector2, vector3 });

            bestVectors.Count.Should().Be(1);
            bestVectors.First().Should().BeEquivalentTo(vector1);
        }

        [Fact]
        public void BestByPareto_TwoEqualBestVectorsFound()
        {
            var vector1 = new Vector(4, 4, 4);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 3, 2);
            var vector4 = new Vector(4, 4, 4);

            var bestVectors = ParetoMethods.BestByPareto(new[] { vector1, vector2, vector3, vector4 });

            bestVectors.Count.Should().Be(2);
            bestVectors.Any(x => x == vector1).Should().BeTrue();
            bestVectors.Any(x => x == vector4).Should().BeTrue();
        }

        [Fact]
        public void BestByPareto_TwoUnequalBestVectorsFound()
        {
            var vector1 = new Vector(4, 4, 3);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(4, 3, 4);
            var vector4 = new Vector(1, 3, 2);

            var bestVectors = ParetoMethods.BestByPareto(new[] { vector1, vector2, vector3, vector4 });

            bestVectors.Count.Should().Be(2);
            bestVectors.Any(x => x == vector1).Should().BeTrue();
            bestVectors.Any(x => x == vector3).Should().BeTrue();
        }

        [Fact]
        public void BestByPareto_OneOfThreeBestVectorFound()
        {
            var vector1 = new Vector(1, 1, 2);
            var vector2 = new Vector(1, 0, 1);
            var vector3 = new Vector(5, 1, 2);

            var bestVectors = ParetoMethods.BestByPareto(new[] { vector1, vector2, vector3 });

            bestVectors.Count.Should().Be(1);
            bestVectors.Any(x => x == vector3).Should().BeTrue();
        }

        #region Negative scenarios

        [Fact]
        public void BetterByParetoThan_DifferentDimensionVectorsCompared_ExceptionThrown()
        {
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);

            Assert.Throws<ArgumentException>(() =>
            {
                vector1.BetterByParetoThan(vector2);
            });
        }

        [Fact]
        public void BestByPareto_DifferentDimensionVectorsCompared_ExceptionThrown()
        {
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 2, 3, 4);

            Assert.Throws<ArgumentException>(() => ParetoMethods.BestByPareto(new[] { vector1, vector2, vector3 }));
        }

        #endregion
    }
}
