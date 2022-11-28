using System.Collections;

namespace AppliedMathLibrary.Objects
{
    /// <summary> Represents the basic implementation of a mathematical point of any dimension. Similar to <see cref="Vector"/> </summary>
    public class Point : IEnumerable<double>
    {
        private readonly double[] _elements;

        #region Constructors

        /// <summary> Create a new n-dimensional point whose elements are zeros </summary>
        /// <param name="n">Vector dimension</param>
        public Point(int n)
        {
            if (n < 1)
                throw new ArgumentException($"Can not create {n}-dimensional point. Such a point has no meaning");

            _elements = new double[n];
        }

        /// <summary> Create a new n-dimensional point with provided n values. </summary>
        /// <param name="values"> n provided values </param>
        public Point(params double[] values) : this(values.Length) => _elements = values.ToArray();

        /// <summary> Create a new point based on provided </summary>
        /// <param name="point">Old point</param>
        public Point(Point point) => _elements = point.ToArray();

        /// <summary> Create a new point based on provided vector </summary>
        /// <param name="vector"> Old vector </param>
        public Point(Vector vector)
        {
            _elements = new double[vector.Dimension];
            for (var i = 0; i < vector.Dimension; i++)
            {
                _elements[i] = vector[i];
            }
        }

        #endregion

        #region Properties

        /// <summary> Point dimension </summary>
        public int Dimension => _elements.Length;

        /// <summary> Simple index implementation </summary>
        /// <param name="i"> Index of element in point </param>
        /// <returns> Point element under index i </returns>
        public double this[int i]
        {
            get => _elements[i];
            set => _elements[i] = value;
        }

        #endregion

        #region Methods

        #endregion

        #region IEnumerableImplementation

        /// <summary> Default implementation of GetEnumerator </summary>
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
