using AppliedMathLibrary.Objects;
using BenchmarkDotNet.Attributes;

namespace AppliedMathLibrary.Benchmarks.Benchmarks
{
    [MemoryDiagnoser(false)]
    public class MatrixBenchmarks
    {
        private readonly Matrix matrix3x3;
        private readonly Matrix matrix4x4;
        private readonly Matrix matrix5x5;
        private readonly Matrix matrix10x10;

        public MatrixBenchmarks()
        {
            matrix3x3 = new(3, new[] {
                1.0, 1, -1,
                1, -1, 1,
                -1, 1, 1
            });

            matrix4x4 = new(4, new[] {
                1.0, 1, 1, -1,
                1, 1, -1, 1,
                1, -1, 1, 1,
                -1, 1, 1, 1
            });

            matrix5x5 = new(5, new[] {
                1.0, 1, 1, 1, -1,
                1, 1, 1, -1, 1,
                1, 1, -1, 1, 1,
                1, -1, 1, 1, 1,
                -1, 1, 1, 1, 1
            });

            matrix10x10 = new(10, new[] {
                1.0, 1, 1, 1, 1, 1, 1, 1, 1, -1,
                1, 1, 1, 1, 1, 1, 1, 1, -1, 1,
                1, 1, 1, 1, 1, 1, 1, -1, 1, 1,
                1, 1, 1, 1, 1, 1, -1, 1, 1, 1,
                1, 1, 1, 1, 1, -1, 1, 1, 1, 1,
                1, 1, 1, 1, -1, 1, 1, 1, 1, 1,
                1, 1, 1, -1, 1, 1, 1, 1, 1, 1,
                1, 1, -1, 1, 1, 1, 1, 1, 1, 1,
                1, -1, 1, 1, 1, 1, 1, 1, 1, 1,
                -1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            });
        }

        [Benchmark] public Result<Matrix> CalculateInverse_matrix3x3() => Matrix.CalculateInverse(matrix3x3);
        [Benchmark] public Result<double> CalculateDeterminant_matrix3x3() => Matrix.CalculateDeterminant(matrix3x3);
        [Benchmark] public Result<Matrix> CalculateInverse_matrix4x4() => Matrix.CalculateInverse(matrix4x4);
        [Benchmark] public Result<double> CalculateDeterminant_matrix4x4() => Matrix.CalculateDeterminant(matrix4x4);
        [Benchmark] public Result<Matrix> CalculateInverse_matrix5x5() => Matrix.CalculateInverse(matrix5x5);
        [Benchmark] public Result<double> CalculateDeterminant_matrix5x5() => Matrix.CalculateDeterminant(matrix5x5);
        [Benchmark] public Result<Matrix> CalculateInverse_matrix10x10() => Matrix.CalculateInverse(matrix10x10);
        [Benchmark] public Result<double> CalculateDeterminant_matrix10x10() => Matrix.CalculateDeterminant(matrix10x10);

        //|                           Method |               Mean |             Error |            StdDev |       Allocated |
        //|--------------------------------- |-------------------:|------------------:|------------------:|----------------:|
        //|       CalculateInverse_matrix3x3 |         2,085.0 ns |          87.35 ns |         257.54 ns |         1,680 B |
        //|   CalculateDeterminant_matrix3x3 |           359.5 ns |          11.11 ns |          31.71 ns |           312 B |
        //|       CalculateInverse_matrix4x4 |        11,376.9 ns |         634.73 ns |       1,871.50 ns |         9,720 B |
        //|   CalculateDeterminant_matrix4x4 |         2,305.8 ns |          71.84 ns |         210.69 ns |         1,824 B |
        //|       CalculateInverse_matrix5x5 |        77,476.9 ns |       2,741.24 ns |       8,082.61 ns |        61,536 B |
        //|   CalculateDeterminant_matrix5x5 |        12,664.5 ns |         565.19 ns |       1,648.69 ns |        10,120 B |
        //|     CalculateInverse_matrix10x10 | 4,422,296,719.0 ns | 142,455,953.25 ns | 420,034,592.30 ns | 3,481,088,904 B |
        //| CalculateDeterminant_matrix10x10 |   264,502,964.9 ns |   5,256,612.65 ns |  10,252,619.27 ns |   316,462,512 B |

        // 1 us      : 1 Microsecond (0.000001 sec)
    }
}
