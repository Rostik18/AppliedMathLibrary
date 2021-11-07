using System;

namespace AppliedMathLibrary.Vectors
{
    public class Vector2 : Vector
    {
        private const int Vector2Dimension = 2;

        #region Constructors

        /// <summary> Create a new 2-dimensional vector whose elements are zeros </summary>
        public Vector2() : base(Vector2Dimension)
        {
            Elements = new double[N];
        }

        /// <summary> Create a new 2-dimensional vector with 2 provided values. </summary>
        /// <param name="values">n provided values</param>
        public Vector2(params double[] values) : base(Vector2Dimension)
        {
            if(values.Length != Vector2Dimension)
                throw new ArgumentException("Vector3 expert 3 values");

            Elements = values.Clone() as double[];
        }

        /// <summary> Create a new Vector2 based on provided </summary>
        /// <param name="vector">Old vector</param>
        public Vector2(Vector2 vector) : base(Vector2Dimension)
        {
            Elements = vector.Elements.Clone() as double[];
        }

        #endregion

        #region Properties

        public double X => Elements[0];
        public double Y => Elements[1];

        #endregion

        #region Methods

        #endregion

        #region IEnumerableImplementation

        #endregion
    }
}
