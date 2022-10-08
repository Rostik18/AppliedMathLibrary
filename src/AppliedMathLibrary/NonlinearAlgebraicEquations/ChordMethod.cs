using System.ComponentModel.DataAnnotations;

namespace AppliedMathLibrary.NonlinearAlgebraicEquations
{
    /// <summary> 
    /// Chords method (or method of linear interpolation, or the method of proportional parts) is an 
    /// iterative numerical method for finding the approximate roots of a nonlinear algebraic equation F(x) = 0. 
    /// </summary>
    public static class ChordMethod
    {
        /// <summary>
        /// Chords method for solving nonlinear algebraic equation of first dimension (R^1 -> R^1).
        /// </summary>
        /// <param name="f"> Nonlinear function R^1 -> R^1 </param>
        /// <param name="x"> Initial approximation </param>
        /// <param name="eps"> Optional calculation accuracy parameter </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Calculation result </returns>
        public static Result<double> SolveEquation(Func<double, double> f, double x = 1, [Range(0.1, 1)] double eps = Constants.Epsilon, CancellationToken ct = default)
        {
            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            var xp1 = x;
            var xp2 = x - eps;

            double iterationIncrease;

            do
            {
                if (ct.IsCancellationRequested) return Result.Failure<double>("Method execution canceled");

                var fxp1 = f(xp1);
                var fxp2 = f(xp2);

                var xn = xp1 - fxp1 * ((xp1 - xp2) / (fxp1 - fxp2));

                iterationIncrease = Math.Abs(xn - xp1);

                xp2 = xp1;
                xp1 = xn;
            }
            while (iterationIncrease > eps);

            return xp1;
        }
    }
}
