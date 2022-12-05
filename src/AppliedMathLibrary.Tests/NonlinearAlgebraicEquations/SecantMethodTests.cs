using AppliedMathLibrary.NonlinearAlgebraicEquations;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.NonlinearAlgebraicEquations
{
    public class SecantMethodTests
    {
        [Fact]
        public void SolveEquationBySecantMethod_OneDimensionProblem_EquationSolved()
        {
            var f = (double x) => x * x * x + 3 * x * x + 12 * x + 8;
            var x = 2;

            var expectedResult1 = -0.7789774626079577;
            var actualResult1 = SecantMethod.SolveEquation(f, x);
            actualResult1.IsSuccess.Should().BeTrue();
            actualResult1.Value.Should().Be(expectedResult1);

            var expectedResult2 = -0.778977462595375;
            var actualResult2 = SecantMethod.SolveEquation(f);
            actualResult2.IsSuccess.Should().BeTrue();
            actualResult2.Value.Should().Be(expectedResult2);

            var eps = 0.01;
            var expectedResult3 = -0.7789769802086401;
            var actualResult3 = SecantMethod.SolveEquation(f, x, eps);
            actualResult3.IsSuccess.Should().BeTrue();
            actualResult3.Value.Should().Be(expectedResult3);

            var f2 = (double x) => x * x - 3;

            var expectedResult4 = 1.7320508075688819;
            var actualResult4 = SecantMethod.SolveEquation(f2, x);
            actualResult4.IsSuccess.Should().BeTrue();
            actualResult4.Value.Should().Be(expectedResult4);

            var f3 = (double x) => x * x - 1;

            var expectedResult5 = 1.0000000000000002;
            var actualResult5 = SecantMethod.SolveEquation(f3, x);
            actualResult5.IsSuccess.Should().BeTrue();
            actualResult5.Value.Should().Be(expectedResult5);
        }

        #region Negative scenarios

        #endregion
    }
}
