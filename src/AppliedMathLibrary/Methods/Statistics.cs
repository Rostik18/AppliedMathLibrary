using System;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMathLibrary.Methods
{
    /// <summary> Simple statistics static methods </summary>
    public static class Statistics
    {
        /// <summary> Calculate arithmetic mean of provided array of numbers Example: (1, 2, 6) -> 3.0 </summary>
        public static double Mean(IEnumerable<double> items) => items.Average();

        /// <summary> Calculate median of provided array of numbers, returns 0 if array is empty. Example: (2, 3, 5) -> 3; (2, 4) -> 3 </summary>
        public static double Median(IEnumerable<double> items)
        {
            var list = items.ToList();
            if (list.Count == 0) return 0;

            return list.Count % 2 == 0
                ? (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2
                : list[list.Count / 2];
        }

        /// <summary> Calculate mode of provided array of numbers. The most frequent number. Example: (1, 1, 2, 5, 6, 6, 9) -> (1, 6) </summary>
        public static double[] Mode(IEnumerable<double> items)
        {
            var frequencies = new Dictionary<double, int>();

            foreach (var item in items)
            {
                if (!frequencies.ContainsKey(item)) frequencies[item] = 1;
                else frequencies[item]++;
            }

            var maxFrequency = frequencies.Max(x => x.Value);

            return frequencies.Where(x => x.Value == maxFrequency).Select(x => x.Key).ToArray();
        }

        /// <summary> Generate array of provided length with random numbers in defined range. By default in 0.0 - 1.0 range </summary>
        public static List<double> GenerateRandomArray(int length, double min = 0, double max = 1)
        {
            var random = new Random();
            var array = new List<double>(length);
            var diff = max - min;

            for (int i = 0; i < length; i++)
                array.Add(random.NextDouble() * diff + min);

            return array;
        }

        #region simple random sampling without replacement

        /// <summary> Randomly takes n dimension sample from provided general sample </summary>
        public static List<T> RandomSampleByRule1<T>(IEnumerable<T> generalSample, int n)
        {
            var generalSampleArr = generalSample.ToArray();
            var randomSample = new List<T>(n);
            var randomArray = GenerateRandomArray(generalSampleArr.Length);

            for (int k = 0; k < generalSampleArr.Length; k++)
            {
                if (randomArray[k] < (double)(n - randomSample.Count) / (generalSampleArr.Length - k /*+ 1*/))
                    randomSample.Add(generalSampleArr[k]);

                if (n == randomSample.Count) break;
            }

            return randomSample;
        }

        /// <summary> Randomly takes n dimension sample from provided general sample </summary>
        public static List<T> RandomSampleByRule2<T>(IEnumerable<T> generalSample, int n)
        {
            var random = new Random();
            var generalSampleArr = generalSample.ToArray();
            var randomSample = new List<T>(n + 1);
            randomSample.AddRange(generalSample.Take(n));

            for (int k = n; k < generalSampleArr.Length; k++)
            {
                if (random.NextDouble() < n / (k + 1.0))
                {
                    randomSample.Add(generalSampleArr[k]);
                    randomSample.RemoveAt(random.Next(n));
                }
            }

            return randomSample;
        }

        #endregion
    }
}
