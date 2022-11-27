using System.Collections;
using System.Text;

namespace AppliedMathLibrary.Objects
{
    /// <summary> Represents the basic implementation of a mathematical polynomial of any power. </summary>
    public sealed class Polynomial : IEnumerable<double>
    {
        private readonly double[] _coefficients;

        /// <summary> Create new Polynomial from provided IEnumerable. Elements are in reverse order (el[0] -> a0). 
        /// Make sure coefficients array has at least 1 element </summary>
        /// <param name="coefficients"> Array of elements of polynomial </param>
        /// <exception cref="ArgumentException"> Expect at least one element to create polynomial </exception>
        public Polynomial(IEnumerable<double> coefficients)
        {
            if (!coefficients.Any())
                throw new ArgumentException("Expect at least one element to create polynomial");

            _coefficients = coefficients.ToArray();
        }

        /// <summary> Create new Polynomial from provided array. Elements are in reverse order (el[0] -> a0). 
        /// Make sure coefficients array has at least 1 element </summary>
        /// <param name="coefficients"> Array of elements of polynomial </param>
        /// <exception cref="ArgumentException"> Expect at least one element to create polynomial </exception>
        public Polynomial(params double[] coefficients)
        {
            if (coefficients.Length < 1)
                throw new ArgumentException("Expect at least one element to create polynomial");

            _coefficients = coefficients.ToArray();
        }

        /// <summary> Copy constryctor </summary>
        /// <param name="other"> Polynomial to make copy from </param>
        public Polynomial(Polynomial other) => _coefficients = other._coefficients.ToArray();

        /// <summary> Get highest power of polynomial. ex: 1 + x^2 -> 2 </summary>
        public int HighestPower
        {
            get
            {
                for (int i = _coefficients.Length - 1; i >= 0; i--)
                {
                    if (_coefficients[i] != 0) return i;
                }
                return 0;
            }
        }

        /// <summary> Simple index implementation </summary>
        /// <param name="i"> Index of element in polynomial </param>
        /// <returns> Polynomial element under index i </returns>
        public double this[int i]
        {
            get => _coefficients[i];
            set => _coefficients[i] = value;
        }

        /// <summary> Multiply two polynomials </summary>
        /// <returns> New polynomial as a product of two provided </returns>
        public static Polynomial Multiply(Polynomial first, Polynomial second)
        {
            var newCoeff = new double[first._coefficients.Length + second._coefficients.Length - 1];

            for (int i = 0; i < first._coefficients.Length; i++)
            {
                for (int j = 0; j < second._coefficients.Length; j++)
                {
                    newCoeff[i + j] += first._coefficients[i] * second._coefficients[j];
                }
            }

            return new(newCoeff);
        }

        /// <summary> Multiply each element of provided polynomial by scalar </summary>
        /// <returns> New polynomial as provided polynomial multiplyed by scalar </returns>
        public static Polynomial Multiply(Polynomial polynomial, double scalar)
        {
            var newPol = new Polynomial(polynomial);

            for (int i = 0; i < newPol._coefficients.Length; i++)
            {
                newPol._coefficients[i] *= scalar;
            }

            return newPol;
        }

        /// <summary> Multiply this polynomial by provided </summary>
        /// <returns> New polynomial as a product of this and provided </returns>
        public Polynomial Multiply(Polynomial other) => Multiply(this, other);

        /// <summary> Multiply each element of this polynomial by scalar </summary>
        /// <returns> New polynomial as a result of multiplying </returns>
        public Polynomial MultiplyBy(double scalar) => Multiply(this, scalar);

        /// <summary> Multiply two polynomials </summary>
        /// <returns> New polynomial as a product of two provided </returns>
        public static Polynomial operator *(Polynomial first, Polynomial second) => Multiply(first, second);

        /// <summary> Multiply each element of provided polynomial by scalar </summary>
        /// <returns> New polynomial as provided polynomial multiplyed by scalar </returns>
        public static Polynomial operator *(Polynomial polynomial, double scalar) => Multiply(polynomial, scalar);

        /// <summary> Multiply each element of provided polynomial by scalar </summary>
        /// <returns> New polynomial as provided polynomial multiplyed by scalar </returns>
        public static Polynomial operator *(double scalar, Polynomial polynomial) => Multiply(polynomial, scalar);

        /// <summary> Sum two polynomials </summary>
        /// <returns> New polynomial as a sum of two provided </returns>
        public static Polynomial Sum(Polynomial first, Polynomial second)
        {
            var higher = first.HighestPower > second.HighestPower ? first : second;
            var smaller = first.HighestPower <= second.HighestPower ? first : second;

            var newPol = new Polynomial(higher);

            for (int i = 0; i < smaller._coefficients.Length; i++)
            {
                newPol._coefficients[i] += smaller._coefficients[i];
            }

            return new(newPol);
        }

        /// <summary> Add provided polynomial to this </summary>
        /// <returns> New polynomial as a sum of two </returns>
        public Polynomial Add(Polynomial other) => Sum(this, other);

        /// <summary> Sum two polynomials </summary>
        /// <returns> New polynomial as a sum of two provided </returns>
        public static Polynomial operator +(Polynomial first, Polynomial second) => Sum(first, second);

        /// <summary> Subtract second polynomial from first </summary>
        /// <returns> New polynomial as a first minus second </returns>
        public static Polynomial Subtract(Polynomial first, Polynomial second)
        {
            var newCoeff = new double[Math.Max(first._coefficients.Length, second._coefficients.Length)];

            for (int i = 0; i < first._coefficients.Length; i++)
            {
                newCoeff[i] += first._coefficients[i];
            }

            for (int i = 0; i < second._coefficients.Length; i++)
            {
                newCoeff[i] -= second._coefficients[i];
            }

            return new(newCoeff);
        }

        /// <summary> Subtract provided polynomial from this </summary>
        /// <returns> New polynomial as a first minus second </returns>
        public Polynomial Subtract(Polynomial other) => Subtract(this, other);

        /// <summary> Subtract second polynomial from first </summary>
        /// <returns> New polynomial as a first minus second </returns>
        public static Polynomial operator -(Polynomial first, Polynomial second) => Subtract(first, second);

        /// <summary> Divide each element of polynomial by scalar. Make sure scalar is not 0 </summary>
        /// <returns> New polynomial as a result of dividing </returns>
        public static Polynomial Divide(Polynomial polynomial, double scalar)
        {
            if (scalar == 0) throw new ArgumentException("Scalar can not be 0");

            var newPoly = new Polynomial(polynomial);

            for (int i = 0; i < newPoly._coefficients.Length; i++)
            {
                newPoly[i] /= scalar;
            }

            return newPoly;
        }

        /// <summary> Divide each element of this polynomial by scalar. Make sure scalar is not 0 </summary>
        /// <returns> New polynomial as a result of dividing </returns>
        public Polynomial DivideBy(double scalar) => Divide(this, scalar);

        /// <summary> Divide each element of polynomial by scalar. Make sure scalar is not 0 </summary>
        /// <returns> New polynomial as a result of dividing </returns>
        public static Polynomial operator /(Polynomial polynomial, double scalar) => Divide(polynomial, scalar);

        /// <summary> Debug ToString representations </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (_coefficients[0] != 0) sb.Append(_coefficients[0]);

            for (int i = 1; i < _coefficients.Length; i++)
            {
                if (_coefficients[i] == 0) continue;
                if (_coefficients[i] > 0 && sb.Length > 0) sb.Append('+');

                if (i == 1) sb.Append($"{_coefficients[i]}x");
                else sb.Append($"{_coefficients[i]}x^{i}");
            }

            return sb.ToString();
        }

        /// <summary> Array out of polynomial. Elements are in reverse order (el[0] -> a0) </summary>
        /// <returns> New array from polynomial coefficients </returns>
        public double[] ToArray() => _coefficients.ToArray();

        #region IEnumerableImplementation

        /// <summary> Default implementation of GetEnumerator </summary>
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_coefficients).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
