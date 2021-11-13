using AppliedMathLibrary.Vectors;
using FluentAssertions;
using System;
using Xunit;

namespace AppliedMathLibrary.Tests.VectorsTests
{
    public class ArithmeticOperationsVectorTests
    {
        #region Positive scenarios

        [Fact]
        public void CalcilateDistanceBetweenVectors()
        {
            var vectorA = new Vector(1, 2);
            var vectorB = new Vector(2, 1);

            vectorA.DistanceTo(vectorA).Should().Be(0);
            Vector.DistanceBetween(vectorA, vectorA).Should().Be(0);

            vectorA.DistanceTo(vectorB).Should().Be(Math.Sqrt(2));
            Vector.DistanceBetween(vectorA, vectorB).Should().Be(Math.Sqrt(2));

            vectorA.DistanceTo(vectorB).Should().Be(vectorB.DistanceTo(vectorA));
            Vector.DistanceBetween(vectorA, vectorB).Should().Be(Vector.DistanceBetween(vectorB, vectorA));
        }

        [Fact]
        public void CalcilateVectorNorm()
        {
            var rand = new Random();

            var randVector = new Vector(rand.Next() - 10, rand.Next() - 10);
            var vector = new Vector(1, 2, 3);

            randVector.Norm().Should().BeGreaterOrEqualTo(0);
            Vector.Norm(randVector).Should().BeGreaterOrEqualTo(0);

            vector.Norm().Should().Be(Math.Sqrt(14));
            Vector.Norm(vector).Should().Be(Math.Sqrt(14));
        }

        [Fact]
        public void SubtractVectors_ProperDimensionAndValues_VectorsSubtracted()
        {
            var vectorA = new Vector(1, 2, 3);
            var vectorB = new Vector(3, 3, 3);
            var vectorC = new Vector(-2, -1, 0);

            var rez1 = vectorA.Subtract(vectorB);
            var rez2 = vectorA - vectorB;
            var rez3 = Vector.Subtract(vectorA, vectorB);

            rez1.Should().BeEquivalentTo(vectorC);
            rez2.Should().BeEquivalentTo(vectorC);
            rez3.Should().BeEquivalentTo(vectorC);
        }

        #endregion

        #region Negative scenarios

        [Fact]
        public void SubtractDifferentVectors_ExceptionThrown()
        {
            var vectorA = new Vector(1, 2);
            var vectorB = new Vector(3, 3, 3);

            Assert.Throws<ArgumentException>(() => vectorA.Subtract(vectorB));
            Assert.Throws<ArgumentException>(() => vectorA - vectorB);
            Assert.Throws<ArgumentException>(() => Vector.Subtract(vectorA, vectorB));
        }

        [Fact]
        public void CalcilateDistanceBetweenDifferentVectors_ExceptionThrown()
        {
            var vectorA = new Vector(1, 2);
            var vectorB = new Vector(3, 3, 3);

            Assert.Throws<ArgumentException>(() => vectorA.DistanceTo(vectorB));
            Assert.Throws<ArgumentException>(() => Vector.DistanceBetween(vectorA, vectorB));
        }

        #endregion
    }
}
