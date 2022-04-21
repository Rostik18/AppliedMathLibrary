using AppliedMathLibrary.Matrices;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.MatricesTests
{
    public class TransposeMatrixTests
    {
        [Theory]
        [InlineData(2, 2, new double[] { 1, 2, 3, 4 })]
        [InlineData(2, 3, new double[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(3, 2, new double[] { 1, 2, 3, 4, 5, 6 })]
        public void TransposeMatrix_MatrixTransposed(int n, int m, double[] values)
        {
            var matrix = new Matrix(n, m, values);

            var transposedMatrix = matrix.Transpose();

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    transposedMatrix[j, i].Should().Be(values[i * m + j]);
                }
            }
        }
    }
}
