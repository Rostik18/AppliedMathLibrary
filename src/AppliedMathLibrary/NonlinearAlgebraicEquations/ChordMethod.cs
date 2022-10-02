using AppliedMathLibrary.Vectors;
using System.ComponentModel.DataAnnotations;

namespace AppliedMathLibrary.NonlinearAlgebraicEquations
{
    /// <summary> 
    /// Chords method (or method of linear interpolation, or the method of proportional parts) is an 
    /// iterative numerical method for finding the approximate roots of a nonlinear algebraic equation. 
    /// </summary>
    public static class ChordMethod
    {
        /// <summary>
        /// Chords method for solving nonlinear algebraic equation of first dimension (R^1 -> R^1).
        /// </summary>
        /// <param name="f"> Nonlinear function R^1 -> R^1 </param>
        /// <param name="a"> Left side of range where function is defined </param>
        /// <param name="b"> Right side of range where function is defined </param>
        /// <param name="eps"> Optional calculation accuracy parameter</param>
        /// <returns></returns>
        public static double SolveEquation(Func<double, double> f, double a, double b, [Range(0.1, 1)] double eps = Constants.Epsilon)
        {
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
                var fc = f(c);
                var fxp = f(xp);

                var xn = xp - fxp * ((c - xp) / (fc - fxp));

                iterationIncrease = Math.Abs(xn - xp);

                xp = xn;
            }
            while (iterationIncrease > eps);

            return xp;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="f"></param>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <param name="eps"></param>
        ///// <returns></returns>
        //public static Vector SolveEquation(Func<Vector, double> f, Vector a, Vector b, [Range(0.1, 1)] double eps = Constants.Epsilon)
        //{
        //    if (a.Dimension != b.Dimension)
        //        throw new ArgumentException("Expec vectors of similar dimension");

        //    var xp = new Vector(a);
        //    var c = new Vector(b);

        //    if (f(a) > f(b))
        //    {
        //        xp = new Vector(b);
        //        c = new Vector(a);
        //    }

        //    double iterationIncrease;

        //    do
        //    {
        //        var fc = f(c);
        //        var fxp = f(xp);

        //        var xn = xp - fxp * ((c - xp) / (fc - fxp));

        //        iterationIncrease = xn.DistanceTo(xp);

        //        xp = xn;
        //    }
        //    while (iterationIncrease > eps);

        //    return xp;
        //}
    }
}
