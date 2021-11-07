using System;
using AppliedMathLibrary.Matrices;
using AppliedMathLibrary.Vectors;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.MatricesTests
{
    public class ArithmeticOperationsMatrixTests
    {
        #region Positive scenarios

        [Fact]
        public void MatrixMultiplyByVectors_ProperDimensionAndValues_NewVectorCreated()
        {
            var matrix = new Matrix(2, 3, 1, -1, 2, 0, -3, 1);
            var vector = new Vector(2, 1, 0);

            var rez = matrix.Multiply(vector);

            rez.Should().NotBeNull();

            rez[0].Should().Be(1);
            rez[1].Should().Be(-3);
        }

        #endregion

        #region Negative scenarios

        [Fact]
        public void MatrixMultiplyByVectors_DifferentDimentions_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix(2, 3, 1, -1, 2, 0, -3, 1);
                var vector = new Vector(2, 1);

                matrix.Multiply(vector);
            });
        }

        #endregion
    }
}
