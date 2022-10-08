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
        /// <param name="a"> Left side of range where function is defined </param>
        /// <param name="b"> Right side of range where function is defined </param>
        /// <param name="eps"> Optional calculation accuracy parameter </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Calculation result </returns>
        public static Result<double> SolveEquation(Func<double, double> f, double a, double b, [Range(0.1, 1)] double eps = Constants.Epsilon, CancellationToken ct = default)
        {
            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            var xp = a;
            var c = b;

            if (f(a) > f(b))
            {
                xp = b;
                c = a;
            }

            double iterationIncrease;

            do
            {
                if (ct.IsCancellationRequested) return Result.Failure<double>("Method execution canceled");

                var fc = f(c);
                var fxp = f(xp);

                var xn = xp - fxp * ((c - xp) / (fc - fxp));

                iterationIncrease = Math.Abs(xn - xp);

                xp = xn;
            }
            while (iterationIncrease > eps);

            return xp;
        }
    }
}
