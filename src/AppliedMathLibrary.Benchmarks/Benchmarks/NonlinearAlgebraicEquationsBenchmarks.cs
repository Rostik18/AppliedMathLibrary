using AppliedMathLibrary.NonlinearAlgebraicEquations;
using BenchmarkDotNet.Attributes;

namespace AppliedMathLibrary.Benchmarks.Benchmarks
{
    [MemoryDiagnoser(false)]
    public class NonlinearAlgebraicEquationsBenchmarks
    {
        private readonly Func<double, double> fSin;
        private readonly Func<double, double> fSinDer;
        private readonly Func<double, double> fxCube;
        private readonly Func<double, double> fxCubeDer;

        public NonlinearAlgebraicEquationsBenchmarks()
        {
            fSin = (x) => Math.Sin(x);
            fSinDer = (x) => Math.Cos(x);
            fxCube = (x) => x * x * x;
            fxCubeDer = (x) => 3 * x * x;
        }

        [Benchmark] public Result<double> NewtonMethod_fSin_StartFrom3() => NewtonMethod.SolveEquation(fSin, fSinDer, 3);
        [Benchmark] public Result<double> NewtonMethod_fSin_StartFrom4() => NewtonMethod.SolveEquation(fSin, fSinDer, 4);
        [Benchmark] public Result<double> SecantMethod_fSin_StartFrom3() => SecantMethod.SolveEquation(fSin, 3);
        [Benchmark] public Result<double> SecantMethod_fSin_StartFrom4() => SecantMethod.SolveEquation(fSin, 4);

        [Benchmark] public Result<double> NewtonMethod_fxCube_StartFromMin1() => NewtonMethod.SolveEquation(fxCube, fxCubeDer, -1);
        [Benchmark] public Result<double> NewtonMethod_fxCube_StartFrom1() => NewtonMethod.SolveEquation(fxCube, fxCubeDer, 1);
        [Benchmark] public Result<double> SecantMethod_fxCube_StartFromMin1() => SecantMethod.SolveEquation(fxCube, -1);
        [Benchmark] public Result<double> SecantMethod_fxCube_StartFrom1() => SecantMethod.SolveEquation(fxCube, 1);

        //|                            Method |       Mean |    Error |    StdDev | Allocated |
        //|---------------------------------- |-----------:|---------:|----------:|----------:|
        //|      NewtonMethod_fSin_StartFrom3 |   626.5 ns | 32.18 ns |  94.88 ns |     152 B |
        //|      NewtonMethod_fSin_StartFrom4 |   663.4 ns | 28.08 ns |  82.36 ns |     144 B |
        //|      SecantMethod_fSin_StartFrom3 |   700.0 ns | 21.04 ns |  61.37 ns |     144 B |
        //|      SecantMethod_fSin_StartFrom4 |   874.8 ns | 56.46 ns | 165.58 ns |     144 B |
        //| NewtonMethod_fxCube_StartFromMin1 |   791.3 ns | 32.13 ns |  93.72 ns |     144 B |
        //|    NewtonMethod_fxCube_StartFrom1 |   802.4 ns | 47.45 ns | 136.90 ns |     144 B |
        //| SecantMethod_fxCube_StartFromMin1 | 1,204.9 ns | 49.40 ns | 145.66 ns |     144 B |
        //|    SecantMethod_fxCube_StartFrom1 | 1,358.3 ns | 27.07 ns |  75.47 ns |     144 B |
    }
}
