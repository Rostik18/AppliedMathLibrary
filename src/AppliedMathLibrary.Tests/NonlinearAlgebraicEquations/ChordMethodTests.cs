using AppliedMathLibrary.Matrices;
using AppliedMathLibrary.Methods;
using AppliedMathLibrary.NonlinearAlgebraicEquations;
using AppliedMathLibrary.Vectors;
using FluentAssertions;
using System;
using Xunit;

namespace AppliedMathLibrary.Tests.NonlinearAlgebraicEquations
{
    public class ChordMethodTests
    {
        [Fact]
        public void SolveEquationByChordMethod_OneDimensionProblem_EquationSolved()
        {
            var f = (double x) => x * x * x + 3 * x * x + 12 * x + 8;
            var a = -5;
            var b = 5;

            var expectedResult1 = -0.7789808080038313;
            var actualResult1 = ChordMethod.SolveEquation(f, a, b);
            actualResult1.Should().Be(expectedResult1);

            var eps = 0.01;
            var expectedResult2 = -0.8130875934024152;
            var actualResult2 = ChordMethod.SolveEquation(f, a, b, eps);
            actualResult2.Should().Be(expectedResult2);
        }


        //[Fact]
        //public void SolveEquationByChordMethod_OneDimensionProblem_EquationSolved()
        //{
        //    var f = (Vector x) => x[0] * x[0] * x[0] + x[0] - 5;
        //    var a = new Vector(0.5);
        //    var b = new Vector(2.0);

        //    var expectedResult1 = new Vector(1.5159800382238267);
        //    var actualResult1 = ChordMethod.SolveEquation(f, a, b);
        //    actualResult1.Should().BeEquivalentTo(expectedResult1);

        //    var eps = 0.01;
        //    var expectedResult2 = new Vector(1.5148776811060352);
        //    var actualResult2 = ChordMethod.SolveEquation(f, a, b, eps);
        //    actualResult2.Should().BeEquivalentTo(expectedResult2);
        //}

        //[Fact]
        //public void SolveEquationByChordMethod_TwoDimensionProblem_EquationSolved()
        //{
        //    var f = (Vector x) => x[0] * x[0] + x[1] * x[1];
        //    var a = new Vector(-1, -1);
        //    var b = new Vector(2, 2);

        //    var expectedResult1 = new Vector(0, 0);
        //    var actualResult1 = ChordMethod.SolveEquation(f, a, b);
        //    actualResult1.Should().BeEquivalentTo(expectedResult1);
        //}

        //[Fact]
        //public void SolveEquationByChordMethod_ThreeDimensionProblem_EquationSolved()
        //{
        //    var f = (Vector x) => x[0] * x[0] + x[1] * x[1] + x[2] * x[2];
        //    var a = new Vector(-1, -1, -1);
        //    var b = new Vector(1, 1, 1);

        //    var expectedResult1 = new Vector(0);
        //    var actualResult1 = ChordMethod.SolveEquation(f, a, b);
        //    actualResult1.Should().BeEquivalentTo(expectedResult1);
        //}

        #region Negative scenarios

        //[Fact]
        //public void MethodValidatingInput_ExceptionThrown()
        //{
        //    var A = new Matrix(2, 2, new double[] { 1, 8, 1, 4 });
        //    var b = new Vector(21, 30, 17);

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        GaussMethod.SolveMatrixSystem(A, b, out _);
        //    });
        //}

        #endregion
    }
}
