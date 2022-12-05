using System.Collections;

namespace AppliedMathLibrary.Objects
{
    /// <summary> Represents the basic implementation of a mathematical vector of any dimension. </summary>
    public class Vector : IEnumerable<double>
    {
        /// <summary> Array of elements representing the vector direction </summary>
        protected double[] Elements;

        #region Constructors

        /// <summary> Create a new n-dimensional vector whose elements are zeros </summary>
        /// <param name="n">Vector dimension</param>
        public Vector(int n)
        {
            if (n < 1)
                throw new ArgumentException($"Can not create {n}-dimensional vector. Such a vector has no meaning");

            Elements = new double[n];
        }

        /// <summary> Create a new n-dimensional vector with provided n values. </summary>
        /// <param name="values">n provided values</param>
        public Vector(params double[] values) : this(values.Length)
        {
            Elements = values.ToArray();
        }

        /// <summary> Create a new vector based on provided </summary>
        /// <param name="vector">Old vector</param>
        public Vector(Vector vector)
        {
            Elements = vector.Elements.ToArray();
        }

        /// <summary> Create a new vector based on provided point </summary>
        /// <param name="point">Old point</param>
        public Vector(Point point)
        {
            Elements = point.ToArray();
        }

        #endregion

        #region Properties

        /// <summary> Vector dimension </summary>
        public int Dimension => Elements.Length;

        /// <summary> Simple index implementation </summary>
        /// <param name="i"> Index of element in point </param>
        /// <returns> Point element under index i </returns>
        public double this[int i]
        {
            get => Elements[i];
            set => Elements[i] = value;
        }

        #endregion

        #region Methods

        /// <summary> Compares two vectors with equal dimension by values. True - if all values of first vector equal to seconds. False - in other case </summary>
        /// <returns> True - if all values of first vector equal to seconds. False - in other case </returns>
        public static bool CompareByValue(Vector vector1, Vector vector2)
        {
            if (vector1 is null || vector2 is null)
                throw new NullReferenceException("Comparing vectors cannot be null");

            if (vector1.Dimension != vector2.Dimension)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            for (var i = 0; i < vector1.Dimension; i++)
            {
                if (Math.Abs(vector1.Elements[i] - vector2.Elements[i]) > 0.0000000000000001)
                    return false;
            }

            return true;
        }

        /// <summary> Compares this vector with equal dimension vector by values. True - if all values of this vector equal to provided. False - in other case </summary>
        /// <returns> True - if all values of this vector equal to provided. False - in other case </returns>
        public bool CompareByValueWith(Vector vector) => CompareByValue(this, vector);

        /// <summary> Can two vectors be compared. True - if two vectors can be compared. False - if cannot </summary>
        /// <returns> True - if two vectors can be compared. False - if cannot </returns>
        public static bool Comparable(Vector vector1, Vector vector2)
        {
            if (vector1 is null || vector2 is null)
                throw new NullReferenceException("Comparing vectors cannot be null");

            if (vector1.Dimension != vector2.Dimension)
                return false;

            if (CompareByValue(vector1, vector2))
                return true;

            var firstHaveBiggerElement = false;
            var secondHaveBiggerElement = false;

            for (var i = 0; i < vector1.Dimension; i++)
            {
                if (vector1.Elements[i] > vector2.Elements[i])
                    firstHaveBiggerElement = true;
                else if (vector1.Elements[i] < vector2.Elements[i])
                    secondHaveBiggerElement = true;
            }

            return firstHaveBiggerElement && !secondHaveBiggerElement || !firstHaveBiggerElement && secondHaveBiggerElement;
        }

        /// <summary> Can this vector be compared with provided. True - if two vectors can be compared. False - if cannot </summary>
        /// <returns> True - if two vectors can be compared. False - if cannot </returns>
        public bool ComparableWith(Vector vector) => Comparable(this, vector);

        /// <summary> Subtract provided vector from this vector. Expect vectors with similar dimension </summary>
        /// <param name="vector"> Subtraction vector </param>
        /// <returns> New subtracted vector </returns>
        public Vector Subtract(Vector vector) => Subtract(this, vector);

        /// <summary> Subtract vector2 from vector1. Expect vectors with similar dimension </summary>
        /// <param name="vector1"> Vector from which is subtracted </param>
        /// <param name="vector2"> Subtraction vector </param>
        /// <returns> New subtracted vector </returns>
        public static Vector operator -(Vector vector1, Vector vector2) => Subtract(vector1, vector2);

        /// <summary> Subtract vector2 from vector1. Expect vectors with similar dimension </summary>
        /// <param name="vector1"> Vector from which is subtracted </param>
        /// <param name="vector2"> Subtraction vector </param>
        /// <returns> New subtracted vector </returns>
        public static Vector Subtract(Vector vector1, Vector vector2)
        {
            if (vector1.Dimension != vector2.Dimension)
                throw new ArgumentException("Vectors have different dimensions");

            var newVector = new Vector(vector1.Dimension);

            for (var i = 0; i < vector1.Dimension; i++)
                newVector[i] = vector1.Elements[i] - vector2.Elements[i];

            return newVector;
        }

        /// <summary> Add provided vector to this vector. Expect vectors with similar dimension </summary>
        /// <returns> New vector as sum of this and provided </returns>
        public Vector Add(Vector vector) => Sum(this, vector);

        /// <summary> Add vector2 to vector1. Expect vectors with similar dimension </summary>
        /// <returns> New vector as sum of two provided </returns>
        public static Vector operator +(Vector vector1, Vector vector2) => Sum(vector1, vector2);

        /// <summary> Add vector2 to vector1. Expect vectors with similar dimension </summary>
        /// <returns> New vector as sum of two provided </returns>
        public static Vector Sum(Vector vector1, Vector vector2)
        {
            if (vector1.Dimension != vector2.Dimension)
                throw new ArgumentException("Vectors have different dimensions");

            var newVector = new Vector(vector1.Dimension);

            for (var i = 0; i < vector1.Dimension; i++)
                newVector[i] = vector1.Elements[i] + vector2.Elements[i];

            return newVector;
        }

        /// <summary> Divide current vector by scalar. Expect scalar not zero </summary>
        /// <returns> A new vector with elements as elements of provided vector divided by scalar </returns>
        public Vector DivideBy(double scalar) => Divide(this, scalar);

        /// <summary> Divide vector by scalar. Expect scalar not zero </summary>
        /// <returns> A new vector with elements as elements of provided vector divided by scalar </returns>
        public static Vector operator /(Vector vector, double scalar) => Divide(vector, scalar);

        /// <summary> Divide vector by scalar. Expect scalar not zero </summary>
        /// <returns> A new vector with elements as elements of provided vector divided by scalar </returns>
        public static Vector Divide(Vector vector, double scalar)
        {
            if (scalar == 0) throw new DivideByZeroException("Scalar can not be 0");

            var newVector = new Vector(vector.Dimension);

            for (var i = 0; i < vector.Dimension; i++)
                newVector[i] = vector.Elements[i] / scalar;

            return newVector;
        }

        /// <summary> Multiply this vector by scalar  </summary>
        /// <returns> A new vector with elements as elements of provided vector divided by scalar </returns>
        public Vector MultiplyBy(double scalar) => Multiply(this, scalar);

        /// <summary> Multiply vector by scalar </summary>
        /// <returns> A new vector with elements as elements of provided vector multiplied by scalar </returns>
        public static Vector operator *(Vector vector, double scalar) => Multiply(vector, scalar);

        /// <summary> Multiply vector by scalar </summary>
        /// <returns> A new vector with elements as elements of provided vector multiplied by scalar </returns>
        public static Vector operator *(double scalar, Vector vector) => Multiply(vector, scalar);

        /// <summary> Multiply vector by scalar </summary>
        /// <returns> A new vector with elements as elements of provided vector multiplied by scalar </returns>
        public static Vector Multiply(Vector vector, double scalar)
        {
            var newVector = new Vector(vector.Dimension);

            for (var i = 0; i < vector.Dimension; i++)
                newVector[i] = vector.Elements[i] * scalar;

            return newVector;
        }

        /// <summary> Calculate Euclidean Norm of provided vector </summary>
        /// <returns> Euclidean Norm of provided vector </returns>
        public static double Norm(Vector vector) => vector.Norm();

        /// <summary> Calculate Euclidean Norm of current vector </summary>
        /// <returns> Euclidean Norm of this vector </returns>
        public double Norm()
        {
            double sum = 0;

            for (var i = 0; i < Dimension; i++)
                sum += Elements[i] * Elements[i];

            return Math.Sqrt(sum);
        }

        /// <summary> Calculate distance between this vector and provided. Expect vectors with similar dimension </summary>
        /// <returns> Distance between two vectors </returns>
        public double DistanceTo(Vector vector) => DistanceBetween(this, vector);

        /// <summary> Calculate distance between two vectors. Expect vectors with similar dimension </summary>
        /// <returns> Distance between two vectors </returns>
        public static double DistanceBetween(Vector vector1, Vector vector2)
        {
            if (vector1.Dimension != vector2.Dimension)
                throw new ArgumentException("Vectors have different dimensions");

            double sum = 0;

            for (var i = 0; i < vector1.Dimension; i++)
                sum += Math.Pow(vector1.Elements[i] - vector2.Elements[i], 2);

            return Math.Sqrt(sum);
        }

        /// <summary> Debug ToString representations </summary>
        public override string ToString() => $"({string.Join(";", Elements)})";

        #endregion

        #region IEnumerableImplementation

        /// <summary> Default implementation of GetEnumerator </summary>
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)Elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
