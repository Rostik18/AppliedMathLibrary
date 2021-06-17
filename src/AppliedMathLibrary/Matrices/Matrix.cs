using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMathLibrary.Matrices
{
    /// <summary> Matrix nxm with double elements. </summary>
    public class Matrix : IEnumerable<double>
    {
        private readonly int _n;
        private readonly int _m;
        private double[,] _elements;

        #region Constructors

        /// <summary> Create nxm matrix whose elements are zeros. </summary>
        /// <param name="n">Matrix rows number</param>
        /// <param name="m">Matrix columns number</param>
        public Matrix(int n, int m)
        {
            if (n < 1 || m < 1)
                throw new ArgumentException($"Can not create {n}x{m} matrix. Such a matrix has no meaning");

            _n = n;
            _m = m;
            _elements = new double[_n, _m];
        }

        /// <summary> Create nxm matrix with provided values. Expect n * m values. </summary>
        /// <param name="n">Matrix rows number</param>
        /// <param name="m">Matrix columns number</param>
        /// <param name="values">Matrix values</param>
        public Matrix(int n, int m, params double[] values) : this(n, m)
        {
            if (_n * _m != values.Length)
                throw new ArgumentException($"Expected {_n * _m} values but received {values.Length}");

            for (var i = 0; i < _n; i++)
            {
                for (var j = 0; j < _m; j++)
                {
                    _elements[i, j] = values[i * _m + j];
                }
            }
        }

        /// <summary> Create nxn matrix whose elements are zeros. </summary>
        /// <param name="n">Matrix rows and columns number</param>
        public Matrix(int n) : this(n, n) { }

        /// <summary> Create nxn matrix with provided values. Expect n * n values. </summary>
        /// <param name="n">Matrix rows and columns number</param>
        /// <param name="values">Matrix values</param>
        public Matrix(int n, params double[] values) : this(n, n, values) { }

        /// <summary> Create new matrix based on provided. </summary>
        /// <param name="matrix">Old matrix</param>
        public Matrix(Matrix matrix)
        {
            _n = matrix._n;
            _m = matrix._m;
            _elements = matrix._elements.Clone() as double[,];
        }

        #endregion

        #region Properties

        public int Rows => _n;
        public int Columns => _m;
        public double this[int i, int j]
        {
            get => _elements[i, j];
            set => _elements[i, j] = value;
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
