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
        [Benchmark] public Result<Matrix> CalculateInverse_matrix4x4() => Matrix.CalculateInverse(matrix4x4);
        [Benchmark] public Result<Matrix> CalculateInverse_matrix5x5() => Matrix.CalculateInverse(matrix5x5);
        [Benchmark] public Result<Matrix> CalculateInverse_matrix10x10() => Matrix.CalculateInverse(matrix10x10);

        //|                       Method |             Mean |          Error |         StdDev |    Allocated |
        //|----------------------------- |-----------------:|---------------:|---------------:|-------------:|
        //|   CalculateInverse_matrix3x3 |         1.328 us |      0.0094 us |      0.0083 us |         2 KB |
        //|   CalculateInverse_matrix4x4 |         7.822 us |      0.0689 us |      0.0538 us |         9 KB |
        //|   CalculateInverse_matrix5x5 |        49.613 us |      0.9385 us |      1.0431 us |        60 KB |
        //| CalculateInverse_matrix10x10 | 2,849,996.260 us | 56,265.7571 us | 64,795.7295 us | 3,399,511 KB |

        // 1 us      : 1 Microsecond (0.000001 sec)
    }
}
