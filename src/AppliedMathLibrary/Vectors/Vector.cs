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

        /// <summary> Compares vectors with equal dimension by Pareto principle.  True - if this vector is better. False - in all other cases </summary>
        /// <param name="vector"> Vector for comparison </param>
        /// <returns> True - if this vector is better by Pareto than provided. False - in all other cases </returns>
        public bool BetterByParetoThan(Vector vector)
        {
            if (N != vector.N)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            var firstHaveBiggerElement = false;
            var secondHaveBiggerElement = false;

            for (var i = 0; i < N; i++)
            {
                if (Elements[i] > vector.Elements[i])
                    firstHaveBiggerElement = true;
                else if (Elements[i] < vector.Elements[i])
                    secondHaveBiggerElement = true;
            }

            return firstHaveBiggerElement && !secondHaveBiggerElement;
        }

        /// <summary> Compares two vectors with equal dimension by Pareto principle.  True - if first vector is better. False - in all other cases </summary>
        /// <returns> True - if first vector is better by Pareto than second. False - in all other cases </returns>
        public static bool CompareByPareto(Vector vector1, Vector vector2)
        {
            return vector1.BetterByParetoThan(vector2);
        }

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

        /// <summary> Returns best vectors by Pareto if any. All vectors should have equal dimension </summary>
        /// <returns> Best vectors by Pareto if any </returns>
        public static List<Vector> BestByPareto(IEnumerable<Vector> vectors)
        {
            var bestVectors = vectors.ToList();

            if (bestVectors.Count < 2)
                return bestVectors;

            int itemsCount;
            do
            {
                itemsCount = bestVectors.Count;
                for (var i = 0; i < bestVectors.Count; i++)
                {
                    for (var j = 0; j < bestVectors.Count; j++)
                    {
                        if (i == j || i >= bestVectors.Count || j >= bestVectors.Count) continue;

                        if (bestVectors[i].BetterByParetoThan(bestVectors[j]))
                            bestVectors.RemoveAt(j);
                    }
                }
            } while (itemsCount > bestVectors.Count);

            return bestVectors;
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
