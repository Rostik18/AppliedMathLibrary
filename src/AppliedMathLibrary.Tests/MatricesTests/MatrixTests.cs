using AppliedMathLibrary.Objects;
using FluentAssertions;
using System;
using Xunit;

namespace AppliedMathLibrary.Tests.MatricesTests
{
    public class MatrixTests
    {
        [Theory]
        [InlineData(1, new[] { 1.0 }, 1)]
        [InlineData(2, new[] { 4.0, 6, 3, 8 }, 14)]
        [InlineData(3, new[] { 6.0, 1, 1, 4, -2, 5, 2, 8, 7 }, -306)]
        [InlineData(3, new[] { 0.0, 1, 1, 0, -2, 5, 0, 8, 7 }, 0)]
        [InlineData(4, new[] { 4.0, 3, 2, 2, 0, 1, -3, 3, 0, -1, 3, 3, 0, 3, 1, 1 }, -240)]
        public void CalculateMatrixDeterminant_ResultCorrect(int n, double[] values, double expectedDet)
        {
            var matrix = new Matrix(n, values);

            var actualDet1 = matrix.CalculateDeterminant();
            var actualDet2 = Matrix.CalculateDeterminant(matrix);

            actualDet1.IsSuccess.Should().BeTrue();
            actualDet2.IsSuccess.Should().BeTrue();

            actualDet1.Value.Should().Be(expectedDet);
            actualDet2.Value.Should().Be(expectedDet);
        }

        [Theory]
        [InlineData(1, new[] { 1.0 }, 1, new[] { 1.0 })]
        [InlineData(2, new[] { 4.0, 6, 3, 8 }, 2, new[] { 2.0, 3, 1.5, 4 })]
        [InlineData(2, new[] { 6.0, 3, -3, 9 }, -1.5, new[] { 2.0, -2.0, -4.0, -6.0 })]
        public void DivideMatrixByScalar_ResultCorrect(int n, double[] values, double scalar, double[] expectedValues)
        {
            var matrix = new Matrix(n, values);
            var expectedMatrix = new Matrix(n, expectedValues);

            var actualResult1 = matrix.DivideBy(scalar);
            var actualResult2 = Matrix.Divide(matrix, scalar);
            var actualResult3 = matrix / scalar;

            actualResult1.Should().BeEquivalentTo(expectedMatrix);
            actualResult2.Should().BeEquivalentTo(expectedMatrix);
            actualResult3.Should().BeEquivalentTo(expectedMatrix);
        }

        #region Negative scenarios

        [Fact]
        public void CalculateDeterminantFromNotSquareMatrix_ResultIsFailure()
        {
            var matrix = new Matrix(2, 3, new[] { 1.0, 2, 3, 4, 5, 6 });

            var result1 = matrix.CalculateDeterminant();
            var result2 = Matrix.CalculateDeterminant(matrix);

            result1.IsSuccess.Should().BeFalse();
            result2.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void DivideMatrixByZero_ExceptionThrown()
        {
            Assert.Throws<DivideByZeroException>(() => new Matrix(2, 3, new[] { 1.0, 2, 3, 4, 5, 6 }).DivideBy(0));
            Assert.Throws<DivideByZeroException>(() => Matrix.Divide(new(2, 3, new[] { 1.0, 2, 3, 4, 5, 6 }), 0));
            Assert.Throws<DivideByZeroException>(() => new Matrix(2, 3, new[] { 1.0, 2, 3, 4, 5, 6 }) / 0);
        }

        #endregion
    }
}
