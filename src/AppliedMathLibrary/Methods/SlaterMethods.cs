using AppliedMathLibrary.Vectors;

namespace AppliedMathLibrary.Methods
{
    /// <summary> Static methods for Slater compiration </summary>
    public static class SlaterMethods
    {
        /// <summary> Compares vectors with equal dimension by Slater principle.  True - if this vector is better. False - in all other cases </summary>
        /// <param name="current"> Base, current vector for comparison </param>
        /// <param name="vector"> Vector for comparison </param>
        /// <returns> True - if this vector is better by Slater than provided. False - in all other cases </returns>
        public static bool BetterBySlaterThan(this Vector current, Vector vector)
        {
            if (current.Dimension != vector.Dimension)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            var currentHasBiggerElements = false;

            for (var i = 0; i < current.Dimension; i++)
            {
                if (current[i] > vector[i])
                    currentHasBiggerElements = true;
                else return false;
            }

            return currentHasBiggerElements;
        }

        /// <summary> Compares two vectors with equal dimension by Slater principle.  True - if first vector is better. False - in all other cases </summary>
        /// <returns> True - if first vector is better by Slater than second. False - in all other cases </returns>
        public static bool CompareBySlater(Vector vector1, Vector vector2) => vector1.BetterBySlaterThan(vector2);

        /// <summary> Returns best vectors by Slater if any. All vectors should have equal dimension </summary>
        /// <returns> Best vectors by Slater if any </returns>
        public static List<Vector> BestBySlater(IEnumerable<Vector> vectors)
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

                        if (bestVectors[i].BetterBySlaterThan(bestVectors[j]))
                            bestVectors.RemoveAt(j);
                    }
                }
            } while (itemsCount > bestVectors.Count);

            return bestVectors;
        }
    }
}
