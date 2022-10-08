using AppliedMathLibrary.NonlinearAlgebraicEquations;
using FluentAssertions;
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
            actualResult1.IsSuccess.Should().BeTrue();
            actualResult1.Value.Should().Be(expectedResult1);

            var eps = 0.01;
            var expectedResult2 = -0.8130875934024152;
            var actualResult2 = ChordMethod.SolveEquation(f, a, b, eps);
            actualResult2.IsSuccess.Should().BeTrue();
            actualResult2.Value.Should().Be(expectedResult2);

            var f2 = (double x) => x * x - 3;
            var a2 = 0;
            var b2 = 3;

            var expectedResult3 = 1.7320506804317222;
            var actualResult3 = ChordMethod.SolveEquation(f2, a2, b2);
            actualResult3.IsSuccess.Should().BeTrue();
            actualResult3.Value.Should().Be(expectedResult3);
        }

        #region Negative scenarios

        #endregion
    }
}
