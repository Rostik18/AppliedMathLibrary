using AppliedMathLibrary.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppliedMathLibrary.Methods
{
    /// <summary> Static methods for Pareto compiration </summary>
    public static class ParetoMethods
    {
        /// <summary> Compares vectors with equal dimension by Pareto principle.  True - if this vector is better. False - in all other cases </summary>
        /// <param name="vector"> Vector for comparison </param>
        /// <returns> True - if this vector is better by Pareto than provided. False - in all other cases </returns>
        public static bool BetterByParetoThan(this Vector current, Vector vector)
        {
            if (current.Dimension != vector.Dimension)
                throw new ArgumentException("Vectors with different dimension cannot be compared");

            var firstHaveBiggerElement = false;
            var secondHaveBiggerElement = false;

            for (var i = 0; i < current.Dimension; i++)
            {
                if (current[i] > vector[i])
                    firstHaveBiggerElement = true;
                else if (current[i] < vector[i])
                    secondHaveBiggerElement = true;
            }

            return firstHaveBiggerElement && !secondHaveBiggerElement;
        }

        /// <summary> Compares two vectors with equal dimension by Pareto principle.  True - if first vector is better. False - in all other cases </summary>
        /// <returns> True - if first vector is better by Pareto than second. False - in all other cases </returns>
        public static bool CompareByPareto(Vector vector1, Vector vector2) => vector1.BetterByParetoThan(vector2);

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
    }
}
