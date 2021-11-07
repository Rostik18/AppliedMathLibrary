using AppliedMathLibrary.Matrices;
using AppliedMathLibrary.Vectors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AppliedMathLibrary.Tests.MatricesTests
{
    public class CreateMatrixTests
    {
        #region Positive scenarios

        [Theory]
        [InlineData(1, 1, new double[] { 1 })]
        [InlineData(1, 2, new double[] { 1, 2 })]
        [InlineData(1, 3, new double[] { 1, 2, 3 })]
        [InlineData(2, 1, new double[] { 1, 2 })]
        [InlineData(3, 1, new double[] { 1, 2, 3 })]
        [InlineData(2, 2, new double[] { 1, 2, 3, 4 })]
        [InlineData(2, 3, new double[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(3, 2, new double[] { 1, 2, 3, 4, 5, 6 })]
        public void CreateMatrix_ProperDimensionAndValues_MatrixCreated(int n, int m, double[] values)
        {
            var matrixnxm = new Matrix(n, m, values);

            matrixnxm.Should().NotBeNull();
            matrixnxm.Rows.Should().Be(n);
            matrixnxm.Columns.Should().Be(m);

            for (var i = 0; i < matrixnxm.Rows; i++)
            {
                for (var j = 0; j < matrixnxm.Columns; j++)
                {
                    matrixnxm[i, j].Should().Be(values[i * m + j]);
                }
            }
        }

        // todo: move to separate class
        /*[Fact]
        public void InverseMatrix_ProperDimensionAndValues_MatrixCreated()
        {
            var matrix = new Matrix(3, new double[]
            {
                1, 2, 3,
                0, 1, 4,
                5, 6, 0
            });

            matrix.Should().NotBeNull();

            matrix = matrix.CalculateInverse();

            (matrix[0, 0] == -24 && matrix[0, 1] == 18 && matrix[0, 2] == 5 &&
             matrix[1, 0] == 20 && matrix[1, 1] == -15 && matrix[1, 2] == -4 &&
             matrix[2, 0] == -5 && matrix[2, 1] == 4 && matrix[2, 2] == 1).Should().BeTrue();
        }*/

        [Theory]
        [InlineData(2, 3, new double[] { 1, 2, 3, 4, 5, 6 })]
        public void CreateMatrix_ProperDimensionAndValuesList_MatrixCreated(int n, int m, IEnumerable<double> values)
        {
            var matrixnxm = new Matrix(n, m, values);

            var valuesList = values.ToList();

            matrixnxm.Should().NotBeNull();
            matrixnxm.Rows.Should().Be(n);
            matrixnxm.Columns.Should().Be(m);

            for (var i = 0; i < matrixnxm.Rows; i++)
            {
                for (var j = 0; j < matrixnxm.Columns; j++)
                {
                    matrixnxm[i, j].Should().Be(valuesList[i * m + j]);
                }
            }
        }

        [Theory]
        [InlineData(1, new double[] { 1 })]
        [InlineData(2, new double[] { 1, 2, 3, 4 })]
        [InlineData(3, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public void CreateSquareMatrix_ProperDimensionAndValues_MatrixCreated(int n, double[] values)
        {
            var matrixnxn = new Matrix(n, values);

            matrixnxn.Should().NotBeNull();
            matrixnxn.Rows.Should().Be(n);
            matrixnxn.Columns.Should().Be(n);

            for (var i = 0; i < matrixnxn.Rows; i++)
            {
                for (var j = 0; j < matrixnxn.Columns; j++)
                {
                    matrixnxn[i, j].Should().Be(values[i * n + j]);
                }
            }
        }

        [Theory]
        [InlineData(2, new double[] { 1, 2, 3, 4 })]
        public void CreateSquareMatrix_ProperDimensionAndValuesList_MatrixCreated(int n, IEnumerable<double> values)
        {
            var matrixnxn = new Matrix(n, values);

            var valuesList = values.ToList();

            matrixnxn.Should().NotBeNull();
            matrixnxn.Rows.Should().Be(n);
            matrixnxn.Columns.Should().Be(n);

            for (var i = 0; i < matrixnxn.Rows; i++)
            {
                for (var j = 0; j < matrixnxn.Columns; j++)
                {
                    matrixnxn[i, j].Should().Be(valuesList[i * n + j]);
                }
            }
        }

        [Fact]
        public void CreateMatrixWithCopyConstructor_MatrixCreated()
        {
            var matrix2x3 = new Matrix(2, 3, 1, 2, 3, 4, 5, 6);

            var copiedMatrix = new Matrix(matrix2x3);

            copiedMatrix.Should().NotBeNull();
            copiedMatrix.Rows.Should().Be(2);
            copiedMatrix.Columns.Should().Be(3);
            copiedMatrix[0, 0].Should().Be(1);
            copiedMatrix[0, 1].Should().Be(2);
            copiedMatrix[0, 2].Should().Be(3);
            copiedMatrix[1, 0].Should().Be(4);
            copiedMatrix[1, 1].Should().Be(5);
            copiedMatrix[1, 2].Should().Be(6);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CreateSquareEmptyMatrix_MatrixCreated(int n)
        {
            var matrixnxn = new Matrix(n);

            matrixnxn.Should().NotBeNull();
            matrixnxn.Rows.Should().Be(n);
            matrixnxn.Columns.Should().Be(n);
            matrixnxn.All(x => x == 0).Should().BeTrue();
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(2, 2)]
        public void CreateEmptyMatrix_MatrixCreated(int n, int m)
        {
            var matrixnxn = new Matrix(n, m);

            matrixnxn.Should().NotBeNull();
            matrixnxn.Rows.Should().Be(n);
            matrixnxn.Columns.Should().Be(m);
            matrixnxn.All(x => x == 0).Should().BeTrue();
        }

        [Fact]
        public void CreateMatrixBasedOnVector_MatrixCreated()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(4, 5, 6);

            var matrix = new Matrix(v1, v2);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(2);
            matrix.Columns.Should().Be(3);
            matrix[0, 0].Should().Be(1);
            matrix[0, 2].Should().Be(3);
            matrix[1, 0].Should().Be(4);
            matrix[1, 2].Should().Be(6);
        }

        #endregion

        #region Negative scenarios

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateSquareEmptyMatrix_ExceptionThrown(int n)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix(n);
            });
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public void CreateEmptyMatrix_ExceptionThrown(int n, int m)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix(n, m);
            });
        }

        [Theory]
        [InlineData(1, new double[] { 1, 2 })]
        [InlineData(2, new double[] { 1, 2, 3 })]
        [InlineData(3, new double[] { })]
        [InlineData(-1, new double[] { 1 })]
        public void CreateSquareMatrixWithWrongParameters_ExceptionThrown(int n, double[] values)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix(n, values);
            });
        }

        [Theory]
        [InlineData(1, 2, new double[] { 1 })]
        [InlineData(2, 1, new double[] { 1, 2, 3 })]
        [InlineData(3, 3, new double[] { })]
        [InlineData(-1, 2, new double[] { 1, 2 })]
        public void CreateMatrixWithWrongParameters_ExceptionThrown(int n, int m, double[] values)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix(n, m, values);
            });
        }

        [Fact]
        public void CreateMatrixBasedOnVector_WrongAmountOfVectorsProvided_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix();
            });
        }

        [Fact]
        public void CreateMatrixBasedOnVector_ProvidedVectorsWithDifferentDimension_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var v1 = new Vector(2, 1, 2);
                var v2 = new Vector(3, 4, 5, 6);

                var matrix = new Matrix(v1, v2);
            });
        }

        #endregion
    }
}
