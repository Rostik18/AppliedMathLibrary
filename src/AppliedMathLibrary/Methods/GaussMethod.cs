using AppliedMathLibrary.Objects;

namespace AppliedMathLibrary.Methods
{
    /// <summary> Gauss method for solving systems of linear algebraic equations </summary>
    public static class GaussMethod
    {
        /// <summary> 
        /// Method reduces matrix to the upper triangle by elementary matrix transformations and solving SLAE.
        /// Calculate matrix determinant as a side effect.
        /// Make sure matrix is square and rows count and b vector dimension are equal.
        /// </summary>
        /// <param name="A"> Square matrix of system coefficients </param>
        /// <param name="b"> Free members Vector </param>
        /// <param name="detA"> Determinant of matrix </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Result of vetror of system solution </returns>
        public static Result<Vector> SolveMatrixSystem(Matrix A, Vector b, out double detA, CancellationToken ct = default)
        {
            detA = 1.0;

            if (!A.IsSquare || A.Rows != b.Dimension)
                return Result.Failure<Vector>("Vector dimension should be equal to matrix rows count");

            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            var copyA = new Matrix(A);
            var copyb = new Vector(b);
            var x = new Vector(copyA.Rows);
            var swapCount = 0;

            for (int k = 0; k < copyA.Rows - 1; k++)
            {
                if (ct.IsCancellationRequested) return Result.Failure<Vector>("Method execution canceled");

                if (copyA[k, k] == 0)
                {
                    SwapLines(k, copyA, copyb);
                    swapCount++;
                }

                for (int i = k + 1; i < copyA.Rows; i++)
                {
                    double m = -copyA[i, k] / copyA[k, k];
                    copyb[i] += m * copyb[k];

                    for (int j = k + 1; j < copyA.Rows; j++)
                    {
                        copyA[i, j] += m * copyA[k, j];
                    }
                    copyA[i, k] = 0;
                }
            }

            x[copyA.Rows - 1] = copyb[copyA.Rows - 1] / copyA[copyA.Rows - 1, copyA.Rows - 1];
            for (int k = copyA.Rows - 2; k >= 0; k--)
            {
                x[k] = (copyb[k] - RowSum(k, copyA, x)) / copyA[k, k];
            }

            for (int i = 0; i < copyA.Rows; i++)
            {
                detA *= copyA[i, i];
            }
            if (swapCount % 2 == 1)
                detA *= -1;

            return x;
        }

        private static double RowSum(int k, Matrix A, Vector x)
        {
            double sum = 0;
            for (int j = k + 1; j < A.Rows; j++)
            {
                sum += A[k, j] * x[j];
            }
            return sum;
        }

        private static void SwapLines(int k, Matrix A, Vector b)
        {
            int swapRow = k;
            double maxElementInDiagonal = A[k, k];
            for (int i = k; i < A.Rows; i++)
            {
                if (A[i, i] != 0 && i != k && Math.Abs(A[i, i]) > Math.Abs(maxElementInDiagonal))
                {
                    maxElementInDiagonal = A[i, i];
                    swapRow = i;
                }
            }
            for (int j = 0; j < A.Rows; j++)
            {
                (A[swapRow, j], A[k, j]) = (A[k, j], A[swapRow, j]);
            }

            (b[swapRow], b[k]) = (b[k], b[swapRow]);
        }
    }
}
