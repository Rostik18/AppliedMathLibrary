using AppliedMathLibrary.Methods;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System;
using System.Collections.Generic;

namespace AppliedMathLibrary.Benchmarks
{
    [MemoryDiagnoser]
    public class StatisticsBenchmarks
    {
        private readonly int n;
        private readonly List<double> generalSample;

        public StatisticsBenchmarks()
        {
            var random = new Random();
            n = random.Next(1, 100);
            var N = random.Next(100, 1000);
            generalSample = new List<double>(N);
            for (int i = 0; i < N; i++)
                generalSample.Add(random.Next(1000));
        }

        [Benchmark]
        public List<double> RandomSampleByRule1() => Statistics.RandomSampleByRule1(generalSample, n);

        [Benchmark]
        public List<double> RandomSampleByRule2() => Statistics.RandomSampleByRule2(generalSample, n);
    }
}
