using AppliedMathLibrary.Methods;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace AppliedMathLibrary.Benchmarks
{
    [MemoryDiagnoser(false)]
    public class StatisticsBenchmarks
    {
        private const int N = 1000;
        private readonly List<double> generalSample1000;

        public StatisticsBenchmarks()
        {
            var random = new Random();
            generalSample1000 = new List<double>(N);
            for (int i = 0; i < N; i++)
                generalSample1000.Add(random.Next(N));
        }

        [Benchmark]
        public List<double> RandomSampleByRule1_n100_N1000() => Statistics.RandomSampleByRule1(generalSample1000, 100);

        [Benchmark]
        public List<double> RandomSampleByRule2_n100_N1000() => Statistics.RandomSampleByRule2(generalSample1000, 100);

        [Benchmark]
        public List<double> RandomSampleByRule1_n500_N1000() => Statistics.RandomSampleByRule1(generalSample1000, 500);

        [Benchmark]
        public List<double> RandomSampleByRule2_n500_N1000() => Statistics.RandomSampleByRule2(generalSample1000, 500);

        [Benchmark]
        public List<double> RandomSampleByRule1_n900_N1000() => Statistics.RandomSampleByRule1(generalSample1000, 900);

        [Benchmark]
        public List<double> RandomSampleByRule2_n900_N1000() => Statistics.RandomSampleByRule2(generalSample1000, 900);
    }
}
