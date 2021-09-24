using AppliedMathLibrary.Points;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMathLibrary.Vectors
{
    public class Vector : IEnumerable<double>
    {
        private readonly int _n;
        private double[] _elements;

        #region Constructors

        /// <summary> Create a new n-dimensional vector whose elements are zeros </summary>
        /// <param name="n">Vector dimension</param>
        public Vector(int n)
        {
            if (n < 1)
                throw new ArgumentException($"Can not create {n}-dimensional vector. Such a vector has no meaning");

            _n = n;
            _elements = new double[_n];
        }

        /// <summary> Create a new n-dimensional vector with provided n values. </summary>
        /// <param name="values">n provided values</param>
        public Vector(params double[] values) : this(values.Length)
        {
            _elements = values.Clone() as double[];
        }

        /// <summary> Create a new vector based on provided </summary>
        /// <param name="vector">Old vector</param>
        public Vector(Vector vector)
        {
            _n = vector._n;
            _elements = vector._elements.Clone() as double[];
        }

        /// <summary> Create a new vector based on provided point </summary>
        /// <param name="point">Old point</param>
        public Vector(Point point)
        {
            _n = point.Dimension;
            _elements = new double[_n];
            for (var i = 0; i < _n; i++)
            {
                _elements[i] = point[i];
            }
        }

        #endregion

        #region Properties

        public int Dimension => _n;
        public double this[int i]
        {
            get => _elements[i];
            set => _elements[i] = value;
        }

        #endregion

        #region Methods

        /// <summary> Compares vectors with equal dimension by Pareto principle.  True - if this vector is better. False - in all other cases </summary>
        /// <param name="vector"> Vector for comparison </param>
        /// <returns> True - if this vector is better by Pareto than provided. False - in all other cases </returns>
        public bool BetterByParetoThan(Vector vector)
        {
            if (_n != vector._n)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            var firstHaveBiggerElement = false;
            var secondHaveBiggerElement = false;

            for (var i = 0; i < _n; i++)
            {
                if (_elements[i] > vector._elements[i])
                    firstHaveBiggerElement = true;
                else if (_elements[i] < vector._elements[i])
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

        /// <summary> Compares two vectors with equal dimension by Pareto principle.  True - if first vector is better. False - in all other cases </summary>
        /// <returns> True - if first vector is better by Pareto than second. False - in all other cases </returns>
        public static bool CompareByValue(Vector vector1, Vector vector2)
        {
            if (vector1 is null || vector2 is null)
                throw new NullReferenceException("Comparing vectors cannot be null");

            if (vector1._n != vector2._n)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            for (var i = 0; i < vector1._n; i++)
            {
                if (Math.Abs(vector1._elements[i] - vector2._elements[i]) > 0.0000000000000001)
                    return false;
            }

            return true;
        }

        /// <summary> Returns best vectors by Pareto if any. All vectors should have equal dimension </summary>
        /// <returns> Best vectors by Pareto if any </returns>
        public static List<Vector> BestByPareto(IEnumerable<Vector> vectors)
        {
            var vectorsList = vectors.ToList();
            var bestVectors = new List<Vector>();

            for (var i = 0; i < vectorsList.Count; i++)
            {
                for (var j = 0; j < vectorsList.Count; j++)
                {
                    if (i == j) continue;

                    if (vectorsList[i].BetterByParetoThan(vectorsList[j]))
                    {
                        bestVectors.Add(vectorsList[i]);
                        break;
                    }
                }
            }

            return bestVectors;
        }

        #endregion

        #region IEnumerableImplementation

        public IEnumerator<double> GetEnumerator()
        {
            return _elements.Cast<double>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
