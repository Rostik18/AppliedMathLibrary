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
        public void MatricesSubtraction_ProperDimensionAndValues_NewProperMatricesCreated()
        {
            var A = new Matrix(2, 3, 1, 2, 3, 4, 5, 6);
            var B = new Matrix(2, 3, 2, 3, 4, 5, 6, 7);
            var C = new Matrix(2, 3, -1, -1, -1, -1, -1, -1);

            var rez1 = A.Subtract(B);
            var rez2 = A - B;
            var rez3 = Matrix.Subtract(A, B);

            rez1.Should().BeEquivalentTo(C);
            rez2.Should().BeEquivalentTo(C);
            rez3.Should().BeEquivalentTo(C);
        }

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

        [Fact]
        public void SubtractDifferentMatrices_ExceptionThrown()
        {
            var A = new Matrix(2, 3, 1, 2, 3, 4, 5, 6);
            var B = new Matrix(3, 2, 2, 3, 4, 5, 6, 7);

            Assert.Throws<ArgumentException>(() => A.Subtract(B));
            Assert.Throws<ArgumentException>(() => A - B);
            Assert.Throws<ArgumentException>(() => Matrix.Subtract(A, B));
        }

        #endregion
    }
}
