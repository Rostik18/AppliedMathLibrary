using System.Collections;
using System.Text;

namespace AppliedMathLibrary.Objects
{
    /// <summary> Matrix nxm with double elements </summary>
    public class Matrix : IEnumerable<double>
    {
        private readonly int _n;
        private readonly int _m;
        private readonly double[,] _elements;

        #region Constructors

        /// <summary> Create nxm matrix whose elements are zeros </summary>
        /// <param name="n">Matrix rows number</param>
        /// <param name="m">Matrix columns number</param>
        public Matrix(int n, int m)
        {
            if (n < 1 || m < 1)
                throw new ArgumentException($"Can not create {n}x{m} matrix. Such a matrix has no meaning");

            _n = n;
            _m = m;
            _elements = new double[_n, _m];
        }

        /// <summary> Create nxm matrix with provided values. Expect n * m values </summary>
        /// <param name="n">Matrix rows number</param>
        /// <param name="m">Matrix columns number</param>
        /// <param name="values">Matrix values</param>
        public Matrix(int n, int m, params double[] values) : this(n, m)
        {
            if (_n * _m != values.Length)
                throw new ArgumentException($"Expected {_n * _m} values but received {values.Length}");

            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    _elements[i, j] = values[i * _m + j];
                }
            }
        }

        /// <summary> Create nxm matrix with provided values. Expect n * m values </summary>
        /// <param name="n">Matrix rows number</param>
        /// <param name="m">Matrix columns number</param>
        /// <param name="values">Matrix values</param>
        public Matrix(int n, int m, IEnumerable<double> values) : this(n, m)
        {
            var valuesList = values.ToList();

            if (_n * _m != valuesList.Count)
                throw new ArgumentException($"Expected {_n * _m} values but received {valuesList.Count}");

            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    _elements[i, j] = valuesList[i * _m + j];
                }
            }
        }

        /// <summary> Create nxn matrix whose elements are zeros </summary>
        /// <param name="n"> Matrix rows and columns number </param>
        public Matrix(int n) : this(n, n) { }

        /// <summary> Create nxn matrix with provided values. Expect n * n values </summary>
        /// <param name="n">Matrix rows and columns number</param>
        /// <param name="values">Matrix values</param>
        public Matrix(int n, params double[] values) : this(n, n, values) { }

        /// <summary> Create nxn matrix with provided values. Expect n * n values </summary>
        /// <param name="n">Matrix rows and columns number</param>
        /// <param name="values">Matrix values</param>
        public Matrix(int n, IEnumerable<double> values) : this(n, n, values) { }

        /// <summary> Create new matrix based on provided </summary>
        /// <param name="matrix">Old matrix</param>
        public Matrix(Matrix matrix)
        {
            _n = matrix._n;
            _m = matrix._m;
            _elements = matrix._elements.Clone() as double[,] ?? new double[_n, _m];
        }

        /// <summary> Create new nxm matrix based on n provided vectors with m dimension. All vectors should have the same dimension </summary>
        /// <param name="vectors">Array of same dimension vectors</param>
        public Matrix(params Vector[] vectors)
        {
            if (!vectors.Any())
                throw new ArgumentException("Expect at least 1 vector");

            _m = vectors.First().Dimension;

            if (vectors.Any(x => x.Dimension != _m))
                throw new ArgumentException("Not all provided vectors have the same dimension");

            _n = vectors.Length;
            _elements = new double[_n, _m];

            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    _elements[i, j] = vectors[i][j];
                }
            }
        }

        #endregion

        #region Properties

        /// <summary> Is matrix number of rows and columns equal </summary>
        public bool IsSquare => _n == _m;

        /// <summary> Number of rows this matrix has </summary>
        public int Rows => _n;

        /// <summary> Number of columns this matrix has </summary>
        public int Columns => _m;

        /// <summary> Default two dimension indexer </summary>
        /// <param name="i"> Index of row </param>
        /// <param name="j"> Index of column </param>
        /// <returns> Value of matrix under index i and j </returns>
        public double this[int i, int j]
        {
            get => _elements[i, j];
            set => _elements[i, j] = value;
        }

        #endregion

        #region Methods

        /// <summary> Returns new transposed matrix </summary>
        /// <param name="matrix"> Matrix that needs to be transposed </param>
        /// <returns> New transposed matrix </returns>
        public static Matrix Transpose(Matrix matrix)
        {
            var newMatrix = new Matrix(matrix._m, matrix._n);

            for (var i = 0; i < newMatrix._n; i++)
            {
                for (var j = 0; j < newMatrix._m; j++)
                {
                    newMatrix._elements[i, j] = matrix._elements[j, i];
                }
            }

            return newMatrix;
        }

        /// <summary> Creates a new transposed matrix based on this </summary>
        /// <returns> New transposed matrix </returns>
        public Matrix Transpose() => Transpose(this);

        /// <summary> Returns new list of vectors where each vector is matrix row </summary>
        /// <returns> New list of vectors </returns>
        public List<Vector> ToVectors()
        {
            var vectors = new List<Vector>();
            var row = new double[_m];

            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    row[j] = _elements[i, j];
                }
                vectors.Add(new Vector(row));
            }

            return vectors;
        }

        /// <summary> Subtract provided matrix from this matrix. Expect matrices with similar dimension </summary>
        /// <param name="matrix"> Subtraction matrix </param>
        /// <returns> New subtracted matrix </returns>
        public Matrix Subtract(Matrix matrix) => Subtract(this, matrix);

        /// <summary> Subtract matrix B from matrix A. Expect matrices with similar dimension </summary>
        /// <param name="A"> Left matrix </param>
        /// <param name="B"> Right matrix </param>
        /// <returns> New subtracted matrix </returns>
        public static Matrix operator -(Matrix A, Matrix B) => Subtract(A, B);

        /// <summary> Subtract matrix B from matrix A. Expect matrices with similar dimension </summary>
        /// <param name="A"> Left matrix </param>
        /// <param name="B"> Right matrix </param>
        /// <returns> New subtracted matrix </returns>
        public static Matrix Subtract(Matrix A, Matrix B)
        {
            if (A._n != B._n || A._m != B._m)
                throw new ArgumentException("Matrices have different dimensions");

            var newMatrix = new Matrix(A._n, A._m);

            for (var i = 0; i < A._n; i++)
            {
                for (var j = 0; j < A._m; j++)
                {
                    newMatrix[i, j] = A._elements[i, j] - B._elements[i, j];
                }
            }

            return newMatrix;
        }

        /// <summary> Multiply provided vector to this matrix. Expect vector dimension similar with matrix columns </summary>
        /// <param name="vector"> Multiplication vector </param>
        /// <returns> New matrix-vector product </returns>
        public Vector MultiplyBy(Vector vector) => Multiply(this, vector);

        /// <summary> Multiply matrix on left to provided vector from right. Expect vector dimension similar with matrix columns </summary>
        /// <param name="matrix"> Matrix on left </param>
        /// <param name="vector"> Vector on right </param>
        /// <returns> New vector as product of matrix and vector multiplication </returns>
        public static Vector operator *(Matrix matrix, Vector vector) => Multiply(matrix, vector);

        /// <summary> Multiply matrix on left to provided vector from right. Expect vector dimension similar with matrix columns </summary>
        /// <param name="matrix"> Matrix on left </param>
        /// <param name="vector"> Vector on right </param>
        /// <returns> New vector as product of matrix and vector multiplication </returns>
        /// <exception cref="ArgumentException"> When Matrix and Vector has incompatible dimensions </exception>
        public static Vector Multiply(Matrix matrix, Vector vector)
        {
            if (matrix._m != vector.Dimension)
                throw new ArgumentException("Matrices columns count different then vector dimension");

            var newVector = new Vector(matrix._n);

            for (var i = 0; i < matrix._n; i++)
            {
                var rowSum = 0.0;
                for (var j = 0; j < matrix._m; j++)
                {
                    rowSum += matrix._elements[i, j] * vector[j];
                }

                newVector[i] = rowSum;
            }

            return newVector;
        }

        /// <summary> Divide each element of provided matrix by scalar </summary>
        /// <returns> New matrix as a result of dividing </returns>
        public static Matrix Divide(Matrix matrix, double scalar)
        {
            if (scalar == 0) throw new DivideByZeroException("Scalar can not be 0");

            var newMatrix = new Matrix(matrix);

            for (var i = 0; i < matrix._n; i++)
            {
                for (var j = 0; j < matrix._m; j++)
                {
                    newMatrix[i, j] /= scalar;
                }
            }

            return newMatrix;
        }

        /// <summary> Divide each element of this matrix by scalar </summary>
        /// <returns> New matrix as a result of dividing </returns>
        public Matrix DivideBy(double scalar) => Divide(this, scalar);

        /// <summary> Divide each element of provided matrix by scalar </summary>
        /// <returns> New matrix as a result of dividing </returns>
        public static Matrix operator /(Matrix matrix, double scalar) => Divide(matrix, scalar);

        /// <summary> Created identity (I) nxn matrix with 1 on main diagonal </summary>
        /// <param name="n"> Matrix rows and columns number </param>
        /// <returns> New identity (I) nxn matrix with 1 on main diagonal </returns>
        public static Matrix CreateIdentityMatrix(int n)
        {
            var matrix = new Matrix(n);

            for (int i = 0; i < n; i++) matrix[i, i] = 1;

            return matrix;
        }

        /// <summary> Recursively calculating determinant of this matrix. Make sure its square </summary>
        /// <returns> Result of determinant calculation </returns>
        public Result<double> CalculateDeterminant() => CalculateDeterminant(this);

        /// <summary> Recursively calculating determinant of a square matrix </summary>
        /// <param name="matrix"> The square matrix </param>
        /// <returns> Result of determinant calculation </returns>
        public static Result<double> CalculateDeterminant(Matrix matrix)
        {
            if (!matrix.IsSquare) return Result.Failure<double>("Cannot calculate determinant of matrix which is not square");

            if (matrix._n == 1) return matrix[0, 0];
            if (matrix._n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            var det = 0.0;

            for (int i = 0; i < matrix._n; i++)
            {
                if (matrix[0, i] == 0) continue;

                det += Math.Pow(-1, i) * matrix[0, i] * CalculateDeterminant(CutRowAndColumn(matrix, 0, i)).Value;
            }

            return det;
        }

        /// <summary> Calculate inverse matrix by the minors method. Make sure the matrix is square and has nonzero determinant </summary>
        /// <returns> Result of matrix inversion </returns>
        public Result<Matrix> CalculateInverse() => CalculateInverse(this);

        /// <summary> Calculate inverse matrix by the minors method. Make sure the matrix is square and has nonzero determinant </summary>
        /// <returns> Result of matrix inversion </returns>
        public static Result<Matrix> CalculateInverse(Matrix matrix)
        {
            if (!matrix.IsSquare) return Result.Failure<Matrix>("Cannot calculate determinant of matrix which is not square");

            var detResult = CalculateDeterminant(matrix);

            if (detResult.IsFailure) return Result.Failure<Matrix>(detResult.Error);
            if (detResult.Value == 0) return Result.Failure<Matrix>("Determinant of initial matrix is 0. Inverse matrix does not exist");
            if (matrix._n < 2) return new Matrix(1, 1.0 / matrix[0, 0]);

            var minorsMatrix = CalculateMinorsMatrix(matrix);

            // Cofactors
            for (int i = 0; i < minorsMatrix._n; i++)
            {
                for (int j = 0; j < minorsMatrix._m; j++)
                {
                    if ((i + j) % 2 == 1)
                        minorsMatrix[i, j] *= -1;
                }
            }

            // Adjugate
            return minorsMatrix.Transpose() / detResult.Value;
        }

        private static Matrix CalculateMinorsMatrix(Matrix matrix)
        {
            var minorsMatrix = new Matrix(matrix._n, matrix._m);

            for (int i = 0; i < matrix._n; i++)
            {
                for (int j = 0; j < matrix._m; j++)
                {
                    minorsMatrix[i, j] = CalculateDeterminant(CutRowAndColumn(matrix, i, j)).Value;
                }
            }

            return minorsMatrix;
        }

        private static Matrix CutRowAndColumn(Matrix matrix, int rowIndex, int columnIndex)
        {
            var newMatrix = new Matrix(matrix._n - 1, matrix._m - 1);

            for (int i = 0; i < newMatrix._n; i++)
            {
                for (int j = 0; j < newMatrix._m; j++)
                {
                    if (i < rowIndex && j < columnIndex) newMatrix[i, j] = matrix[i, j];
                    else if (i >= rowIndex && j < columnIndex) newMatrix[i, j] = matrix[i + 1, j];
                    else if (i < rowIndex && j >= columnIndex) newMatrix[i, j] = matrix[i, j + 1];
                    else newMatrix[i, j] = matrix[i + 1, j + 1];
                }
            }

            return newMatrix;
        }

        /// <summary> Debug string representations </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _m; j++)
                {
                    sb.Append($"{_elements[i, j]}; ");
                }
                sb.Append("  ");
            }

            return sb.ToString();
        }

        #endregion

        #region IEnumerableImplementation

        /// <summary> Default implementation of GetEnumerator </summary>
        public IEnumerator<double> GetEnumerator()
        {
            return _elements.Cast<double>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
