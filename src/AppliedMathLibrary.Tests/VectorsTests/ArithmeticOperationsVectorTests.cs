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
        public void SubtractVectors_ProperDimensionAndValues_VectorsSubtracted()
        {
            var vectorA = new Vector(1, 2, 3);
            var vectorB = new Vector(3, 3, 3);
            var vectorC = new Vector(-2, -1, 0);

            var rez = vectorA.Subtract(vectorB);

            rez.Should().BeEquivalentTo(vectorC);
        }

        #endregion

        #region Negative scenarios

        [Fact]
        public void SubtractDifferentVectors_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var vectorA = new Vector(1, 2);
                var vectorB = new Vector(3, 3, 3);

                vectorA.Subtract(vectorB);
            });
        }

        #endregion
    }
}
