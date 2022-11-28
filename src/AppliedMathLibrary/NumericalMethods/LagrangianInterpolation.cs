using AppliedMathLibrary.Objects;

namespace AppliedMathLibrary.NumericalMethods
{
    /// <summary> Provides methods for function numerical interpolation by the Lagrangian method </summary>
    public static class LagrangianInterpolation
    {
        /// <summary> Creates the delegate which represents the interpolation function built from provided arrays of x and y axis values. 
        /// Make sure x and y arrays have the same length </summary>
        /// <param name="xAxisValues"> Array of x axis values </param>
        /// <param name="yAxisValues"> Array of y axis values </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Result of interpolation as polynomial function delegate </returns>
        public static Result<Func<double, double>> CreatePolinimial(IEnumerable<double> xAxisValues, IEnumerable<double> yAxisValues, CancellationToken ct = default)
        {
            var coefficients = CalculatePolynomialCoefficients(xAxisValues, yAxisValues, ct);

            if (coefficients.IsFailure) return Result.Failure<Func<double, double>>(coefficients.Error);

            var f = (double x) =>
            {
                var sum = 0.0;

                for (int i = 0; i < coefficients.Value.Length; i++)
                {
                    sum += coefficients.Value[i] * Math.Pow(x, i);
                }

                return sum;
            };

            return f;
        }

        /// <summary> Calculating coefficients of polynomial which interpolating the function defined by arrays of x and y axis values. 
        /// Make sure x and y arrays have the same length </summary>
        /// <param name="xAxisValues"> Array of x axis values </param>
        /// <param name="yAxisValues"> Array of y axis values </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Result of interpolation as array of polynomial coefficients in reverse order (el[0] -> a0) </returns>
        public static Result<double[]> CalculatePolynomialCoefficients(IEnumerable<double> xAxisValues, IEnumerable<double> yAxisValues, CancellationToken ct = default)
        {
            if (xAxisValues.Count() < 2) Result.Failure<Func<double, double>>($"Expect at least 2 interpolation points, but resive {xAxisValues.Count()}");
            if (xAxisValues.Count() != yAxisValues.Count()) Result.Failure<Func<double, double>>($"Expect both {nameof(xAxisValues)} and {nameof(yAxisValues)} has the same amounght of elements, {xAxisValues.Count()} != {xAxisValues.Count()}");

            var x = xAxisValues.ToArray();
            var y = yAxisValues.ToArray();

            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            var lagrangianSum = new Polynomial(0);

            for (int i = 0; i < x.Length; i++)
            {
                if (ct.IsCancellationRequested) return Result.Failure<double[]>("Method execution canceled");

                var pol = new Polynomial(1);

                var a = y[i];

                for (int j = 0; j < x.Length; j++)
                {
                    if (j == i) continue;

                    pol *= new Polynomial(-x[j], 1);

                    a /= x[i] - x[j];
                }

                lagrangianSum += a * pol;
            }

            return lagrangianSum.ToArray();
        }
    }
}
