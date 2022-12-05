using AppliedMathLibrary.NumericalMethods;
using BenchmarkDotNet.Attributes;

namespace AppliedMathLibrary.Benchmarks.Benchmarks
{
    [MemoryDiagnoser(false)]
    public class IntegralsBenchmarks
    {
        private readonly Func<double, double> fSin;
        private readonly Func<double, double> fLog10;
        private const double a1 = 0;
        private const double b1 = Math.PI;

        private const double a2 = -10000;
        private const double b2 = 10000;

        public IntegralsBenchmarks()
        {
            fSin = (x) => Math.Sin(x);
            fLog10 = (x) => Math.Log10(x * x - x);
        }

        [Benchmark]
        public Result<double> RectanglesMethod_fSin_From0_ToPi_Full() => Integrals.RectanglesMethod(fSin, a1, b1, IntegrationOptions.Full);
        [Benchmark]
        public Result<double> TrapeziumMethod_fSin_From0_ToPi_Full() => Integrals.TrapeziumMethod(fSin, a1, b1, IntegrationOptions.Full);
        [Benchmark]
        public Result<double> RectanglesMethod_fSin_From0_ToPi_UpperOX() => Integrals.RectanglesMethod(fSin, a1, b1, IntegrationOptions.UpperOX);
        [Benchmark]
        public Result<double> TrapeziumMethod_fSin_From0_ToPi_UpperOX() => Integrals.TrapeziumMethod(fSin, a1, b1, IntegrationOptions.UpperOX);
        [Benchmark]
        public Result<double> RectanglesMethod_fSin_From0_ToPi_LowerOX() => Integrals.RectanglesMethod(fSin, a1, b1, IntegrationOptions.LowerOX);
        [Benchmark]
        public Result<double> TrapeziumMethod_fSin_From0_ToPi_LowerOX() => Integrals.TrapeziumMethod(fSin, a1, b1, IntegrationOptions.LowerOX);
        [Benchmark]
        public Result<double> RectanglesMethod_fSin_From0_ToPi_UpperMinusLower() => Integrals.RectanglesMethod(fSin, a1, b1, IntegrationOptions.UpperMinusLower);
        [Benchmark]
        public Result<double> TrapeziumMethod_fSin_From0_ToPi_UpperMinusLower() => Integrals.TrapeziumMethod(fSin, a1, b1, IntegrationOptions.UpperMinusLower);


        [Benchmark]
        public Result<double> RectanglesMethod_fLog10_FromMinus10000_To10000_Full() => Integrals.RectanglesMethod(fLog10, a2, b2, IntegrationOptions.Full);
        [Benchmark]
        public Result<double> TrapeziumMethod_fLog10_FromMinus10000_To10000_Full() => Integrals.TrapeziumMethod(fLog10, a2, b2, IntegrationOptions.Full);
        [Benchmark]
        public Result<double> RectanglesMethod_fLog10_FromMinus10000_To10000_UpperOX() => Integrals.RectanglesMethod(fLog10, a2, b2, IntegrationOptions.UpperOX);
        [Benchmark]
        public Result<double> TrapeziumMethod_fLog10_FromMinus10000_To10000_UpperOX() => Integrals.TrapeziumMethod(fLog10, a2, b2, IntegrationOptions.UpperOX);
        [Benchmark]
        public Result<double> RectanglesMethod_fLog10_FromMinus10000_To10000_LowerOX() => Integrals.RectanglesMethod(fLog10, a2, b2, IntegrationOptions.LowerOX);
        [Benchmark]
        public Result<double> TrapeziumMethod_fLog10_FromMinus10000_To10000_LowerOX() => Integrals.TrapeziumMethod(fLog10, a2, b2, IntegrationOptions.LowerOX);
        [Benchmark]
        public Result<double> RectanglesMethod_fLog10_FromMinus10000_To10000_UpperMinusLower() => Integrals.RectanglesMethod(fLog10, a2, b2, IntegrationOptions.UpperMinusLower);
        [Benchmark]
        public Result<double> TrapeziumMethod_fLog10_FromMinus10000_To10000_UpperMinusLower() => Integrals.TrapeziumMethod(fLog10, a2, b2, IntegrationOptions.UpperMinusLower);

        //|                                                         Method |        Mean |    Error |   StdDev |      Median | Allocated |
        //|--------------------------------------------------------------- |------------:|---------:|---------:|------------:|----------:|
        //|                          RectanglesMethod_fSin_From0_ToPi_Full |    27.73 ms | 0.542 ms | 0.646 ms |    27.43 ms |     199 B |
        //|                           TrapeziumMethod_fSin_From0_ToPi_Full |    28.56 ms | 0.257 ms | 0.228 ms |    28.62 ms |     210 B |
        //|                       RectanglesMethod_fSin_From0_ToPi_UpperOX |    29.27 ms | 0.570 ms | 0.560 ms |    29.07 ms |     199 B |
        //|                        TrapeziumMethod_fSin_From0_ToPi_UpperOX |    28.08 ms | 0.232 ms | 0.181 ms |    28.08 ms |     204 B |
        //|                       RectanglesMethod_fSin_From0_ToPi_LowerOX |    29.60 ms | 0.538 ms | 0.971 ms |    29.17 ms |     198 B |
        //|                        TrapeziumMethod_fSin_From0_ToPi_LowerOX |    28.41 ms | 0.198 ms | 0.185 ms |    28.33 ms |     197 B |
        //|               RectanglesMethod_fSin_From0_ToPi_UpperMinusLower |    29.03 ms | 0.379 ms | 0.316 ms |    28.97 ms |     196 B |
        //|                TrapeziumMethod_fSin_From0_ToPi_UpperMinusLower |    27.96 ms | 0.502 ms | 0.470 ms |    27.92 ms |     200 B |
        //|            RectanglesMethod_fLog10_FromMinus10000_To10000_Full | 5,008.75 ms | 4.834 ms | 4.522 ms | 5,009.56 ms |     848 B |
        //|             TrapeziumMethod_fLog10_FromMinus10000_To10000_Full | 5,006.36 ms | 4.772 ms | 4.463 ms | 5,004.73 ms |     848 B |
        //|         RectanglesMethod_fLog10_FromMinus10000_To10000_UpperOX | 5,008.50 ms | 5.938 ms | 5.554 ms | 5,010.09 ms |     848 B |
        //|          TrapeziumMethod_fLog10_FromMinus10000_To10000_UpperOX | 5,008.85 ms | 4.555 ms | 4.261 ms | 5,008.00 ms |   4,088 B |
        //|         RectanglesMethod_fLog10_FromMinus10000_To10000_LowerOX | 5,009.18 ms | 5.892 ms | 5.511 ms | 5,011.09 ms |     848 B |
        //|          TrapeziumMethod_fLog10_FromMinus10000_To10000_LowerOX | 5,007.49 ms | 4.425 ms | 4.139 ms | 5,006.96 ms |     848 B |
        //| RectanglesMethod_fLog10_FromMinus10000_To10000_UpperMinusLower | 5,008.06 ms | 4.543 ms | 4.250 ms | 5,007.25 ms |   4,088 B |
        //|  TrapeziumMethod_fLog10_FromMinus10000_To10000_UpperMinusLower | 5,006.94 ms | 4.322 ms | 4.043 ms | 5,007.02 ms |   4,088 B |
    }
}
