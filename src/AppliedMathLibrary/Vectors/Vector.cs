using AppliedMathLibrary.Points;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMathLibrary.Vectors
{
    public class Vector : IEnumerable<double>
    {
        protected readonly int N;
        protected double[] Elements;

        #region Constructors

        /// <summary> Create a new n-dimensional vector whose elements are zeros </summary>
        /// <param name="n">Vector dimension</param>
        public Vector(int n)
        {
            if (n < 1)
                throw new ArgumentException($"Can not create {n}-dimensional vector. Such a vector has no meaning");

            N = n;
            Elements = new double[N];
        }

        /// <summary> Create a new n-dimensional vector with provided n values. </summary>
        /// <param name="values">n provided values</param>
        public Vector(params double[] values) : this(values.Length)
        {
            Elements = values.Clone() as double[];
        }

        /// <summary> Create a new vector based on provided </summary>
        /// <param name="vector">Old vector</param>
        public Vector(Vector vector)
        {
            N = vector.N;
            Elements = vector.Elements.Clone() as double[];
        }

        /// <summary> Create a new vector based on provided point </summary>
        /// <param name="point">Old point</param>
        public Vector(Point point)
        {
            N = point.Dimension;
            Elements = new double[N];
            for (var i = 0; i < N; i++)
            {
                Elements[i] = point[i];
            }
        }

        #endregion

        #region Properties

        public int Dimension => N;
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

            if (vector1.N != vector2.N)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            for (var i = 0; i < vector1.N; i++)
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

            if (vector1.N != vector2.N)
                return false;

            if (CompareByValue(vector1, vector2))
                return true;

            var firstHaveBiggerElement = false;
            var secondHaveBiggerElement = false;

            for (var i = 0; i < vector1.N; i++)
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
            if (vector1.N != vector2.N)
                throw new ArgumentException("Vectors have different dimensions");

            var newVector = new Vector(vector1.N);

            for (var i = 0; i < vector1.N; i++)
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
            if (vector1.N != vector2.N)
                throw new ArgumentException("Vectors have different dimensions");

            var newVector = new Vector(vector1.N);

            for (var i = 0; i < vector1.N; i++)
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
            if (scalar == 0)
                throw new ArgumentException("Scalar should be different than zero");

            var newVector = new Vector(vector.N);

            for (var i = 0; i < vector.N; i++)
                newVector[i] = vector.Elements[i] / scalar;

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

            for (var i = 0; i < N; i++)
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
            if (vector1.N != vector2.N)
                throw new ArgumentException("Vectors have different dimensions");

            double sum = 0;

            for (var i = 0; i < vector1.N; i++)
                sum += Math.Pow(vector1.Elements[i] - vector2.Elements[i], 2);

            return Math.Sqrt(sum);
        }

        /// <summary> Debug ToString representations </summary>
        public override string ToString() => $"({string.Join(";", Elements)})";

        #endregion

        #region IEnumerableImplementation

        public IEnumerator<double> GetEnumerator()
        {
            return Elements.Cast<double>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
