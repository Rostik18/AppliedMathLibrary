using System;
using AppliedMathLibrary.Objects;
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
            var expected = new Vector(1, -3);

            var rez1 = matrix.MultiplyBy(vector);
            var rez2 = matrix * vector;
            var rez3 = Matrix.Multiply(matrix, vector);

            rez1.Should().BeEquivalentTo(expected);
            rez2.Should().BeEquivalentTo(expected);
            rez3.Should().BeEquivalentTo(expected);
        }

        #endregion

        #region Negative scenarios

        [Fact]
        public void MatrixMultiplyByVectors_DifferentDimentions_ExceptionThrown()
        {
            var matrix = new Matrix(2, 3, 1, -1, 2, 0, -3, 1);
            var vector = new Vector(2, 1);

            Assert.Throws<ArgumentException>(() => matrix.MultiplyBy(vector));
            Assert.Throws<ArgumentException>(() => matrix * vector);
            Assert.Throws<ArgumentException>(() => Matrix.Multiply(matrix, vector));
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
