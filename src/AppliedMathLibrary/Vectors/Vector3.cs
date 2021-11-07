using System;

namespace AppliedMathLibrary.Vectors
{
    public class Vector3 : Vector
    {
        private const int Vector3Dimension = 3;

        #region Constructors

        /// <summary> Create a new 3-dimensional vector whose elements are zeros </summary>
        public Vector3() : base(Vector3Dimension)
        {
            Elements = new double[N];
        }

        /// <summary> Create a new 3-dimensional vector with 3 provided values. </summary>
        /// <param name="values">n provided values</param>
        public Vector3(params double[] values) : base(Vector3Dimension)
        {
            if(values.Length != Vector3Dimension)
                throw new ArgumentException("Vector3 expert 3 values");

            Elements = values.Clone() as double[];
        }

        /// <summary> Create a new Vector3 based on provided </summary>
        /// <param name="vector">Old vector</param>
        public Vector3(Vector3 vector) : base(Vector3Dimension)
        {
            Elements = vector.Elements.Clone() as double[];
        }

        #endregion

        #region Properties

        public double X => Elements[0];
        public double Y => Elements[1];
        public double Z => Elements[2];

        #endregion

        #region Methods

        #endregion

        #region IEnumerableImplementation

        #endregion
    }
}
