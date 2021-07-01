﻿using AppliedMathLibrary.Vectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMathLibrary.Points
{
    public class Point : IEnumerable<double>
    {
        private readonly int _n;
        private double[] _elements;

        #region Constructors

        /// <summary> Create a new n-dimensional point whose elements are zeros </summary>
        /// <param name="n">Vector dimension</param>
        public Point(int n)
        {
            if (n < 1)
                throw new ArgumentException($"Can not create {n}-dimensional point. Such a point has no meaning");

            _n = n;
            _elements = new double[_n];
        }

        /// <summary> Create a new n-dimensional point with provided values. Expect n values </summary>
        /// <param name="n">Point dimension</param>
        /// <param name="values">n provided values</param>
        public Point(int n, params double[] values) : this(n)
        {
            if (n != values.Length)
                throw new ArgumentException($"Expected {_n} values but received {values.Length}");

            _elements = values.Clone() as double[];
        }

        /// <summary> Create a new point based on provided </summary>
        /// <param name="point">Old point</param>
        public Point(Point point)
        {
            _n = point._n;
            _elements = point._elements.Clone() as double[];
        }

        /// <summary> Create a new point based on provided vector </summary>
        /// <param name="point">Old vector</param>
        public Point(Vector vector)
        {
            _n = vector.Dimension;
            _elements = new double[_n];
            for (var i = 0; i < _n; i++)
            {
                _elements[i] = vector[i];
            }
        }

        #endregion

        #region Properties

        public int Dimension => _n;
        public double this[int i] {
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
