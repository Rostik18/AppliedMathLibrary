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

        /// <summary> Create a new n-dimensional vector with provided values. Expect n values </summary>
        /// <param name="n">Vector dimension</param>
        /// <param name="values">n provided values</param>
        public Vector(int n, params double[] values) : this(n)
        {
            if (n != values.Length)
                throw new ArgumentException($"Expected {_n} values but received {values.Length}");

            _elements = values.Clone() as double[];
        }

        /// <summary> Create a new vector based on provided </summary>
        /// <param name="vector">Old vector</param>
        public Vector(Vector vector)
        {
            _n = vector._n;
            _elements = vector._elements.Clone() as double[];
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
