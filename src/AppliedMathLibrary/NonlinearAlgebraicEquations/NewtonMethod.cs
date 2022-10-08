using System.ComponentModel.DataAnnotations;

namespace AppliedMathLibrary.NonlinearAlgebraicEquations
{
    /// <summary> 
    /// Newton method (or method of tangents, or Newton-Raphson method) is an iterative 
    /// numerical method for finding the approximate roots of a nonlinear algebraic equation F(x) = 0. 
    /// </summary>
    public static class NewtonMethod
    {
        /// <summary>
        /// Newton method for solving nonlinear algebraic equation of first dimension (R^1 -> R^1).
        /// </summary>
        /// <param name="f"> Nonlinear function R^1 -> R^1 </param>
        /// <param name="fDerivative"> Derivative of function f. Make sure you calculated it right </param>
        /// <param name="x"> Initial approximation </param>
        /// <param name="eps"> Optional calculation accuracy parameter </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Calculation result </returns>
        public static Result<double> SolveEquation(Func<double, double> f, Func<double, double> fDerivative, double x = 1, [Range(0.1, 1)] double eps = Constants.Epsilon, CancellationToken ct = default)
        {
            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            var xp = x;

            double iterationIncrease;

            do
            {
                if (ct.IsCancellationRequested) return Result.Failure<double>("Method execution canceled");

                var xn = xp - f(xp) / fDerivative(xp);

                iterationIncrease = Math.Abs(xn - xp);

                xp = xn;
            }
            while (iterationIncrease > eps);

            return xp;
        }
    }
}
