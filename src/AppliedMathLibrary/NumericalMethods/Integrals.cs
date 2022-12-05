using System.ComponentModel.DataAnnotations;

namespace AppliedMathLibrary.NumericalMethods
{
    /// <summary>
    /// Provides methods for numerical calculation of integrals
    /// </summary>
    public static class Integrals
    {
        /// <summary>
        /// Calculate function f on defined [a; b] interval by splitting it in n (n = (b-a)/h) rectangles and summing its square.
        /// Make sure your function is continuous.
        /// </summary>
        /// <param name="f"> Сontinuous function for which the integral should be calculated </param>
        /// <param name="a"> Left side of integration interval </param>
        /// <param name="b"> Right side of integration interval </param>
        /// <param name="options"> Integral calculation options </param>
        /// <param name="h"> Width of each rectangles. should be in range <see cref="Constants.Epsilon"/> - 1 </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Area of the integrated figure </returns>
        public static Result<double> RectanglesMethod(Func<double, double> f, double a, double b, IntegrationOptions options = IntegrationOptions.Full, [Range(Constants.Epsilon, 1)] double h = Constants.Epsilon, CancellationToken ct = default)
        {
            if (a >= b) return Result.Failure<double>("Parameter a should be less than parameter b");

            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            double result = 0;
            double x = a + h / 2;

            while (!ct.IsCancellationRequested && x < b)
            {
                result += CalcIncrement(h * f(x), options);
                x += h;
            }

            if (ct.IsCancellationRequested) return Result.Failure<double>("Method execution canceled");

            return result;
        }

        /// <summary>
        /// Calculate function f on defined [a; b] interval by splitting it in n (n = (b-a)/h) trapezium and summing its square.
        /// Make sure your function is continuous.
        /// </summary>
        /// <param name="f"> Сontinuous function for which the integral should be calculated </param>
        /// <param name="a"> Left side of integration interval </param>
        /// <param name="b"> Right side of integration interval </param>
        /// <param name="options"> Integral calculation options </param>
        /// <param name="h"> Width of each rectangles. should be in range <see cref="Constants.Epsilon"/> - 1 </param>
        /// <param name="ct"> Cancellation token. 5 seconds by default </param>
        /// <returns> Area of the integrated figure </returns>
        public static Result<double> TrapeziumMethod(Func<double, double> f, double a, double b, IntegrationOptions options = IntegrationOptions.Full, [Range(Constants.Epsilon, 1)] double h = Constants.Epsilon, CancellationToken ct = default)
        {
            if (a >= b) return Result.Failure<double>("Parameter a should be less than parameter b");

            if (ct == default) ct = new CancellationTokenSource(Constants.Timeout5s).Token;

            double result = 0;

            double left = f(a);
            double x = a + h;
            double right = f(x);

            double halfH = h / 2;

            while (!ct.IsCancellationRequested && x < b)
            {
                result += CalcIncrement(halfH * (left + right), options);
                left = right;
                x += h;
                right = f(x);
            }

            if (ct.IsCancellationRequested) return Result.Failure<double>("Method execution canceled");

            return result;
        }

        private static double CalcIncrement(double x, IntegrationOptions options)
        {
            if (options == IntegrationOptions.Full) return Abs(x);
            if (options == IntegrationOptions.UpperOX) return x > 0 ? x : 0;
            if (options == IntegrationOptions.LowerOX) return x < 0 ? Abs(x) : 0;
            return x;
        }

        private static double Abs(double x) => x < 0 ? -x : x;
    }

    /// <summary> Integral calculation options </summary>
    public enum IntegrationOptions
    {
        /// <summary> Calculate the total area of the figure relative to the OX axis </summary>
        Full,

        /// <summary> Calculate the total area of the figure relative to the OX axis </summary>
        UpperOX,

        /// <summary> Calculate the area of the figure below the OX axis </summary>
        LowerOX,

        /// <summary> Calculate the area of the figure above the OX axis excluding the area below the OX axis </summary>
        UpperMinusLower
    }
}
