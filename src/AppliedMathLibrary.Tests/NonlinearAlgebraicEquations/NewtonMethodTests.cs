using AppliedMathLibrary.NonlinearAlgebraicEquations;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.NonlinearAlgebraicEquations
{
    public class NewtonMethodTests
    {
        [Fact]
        public void SolveEquationByNewtonMethod_EquationSolved()
        {
            var f = (double x) => x * x - 3;
            var fDerivative = (double x) => 2 * x;
            var x = 2;

            var expectedResult1 = 1.7320508075688772;
            var actualResult1 = NewtonMethod.SolveEquation(f, fDerivative, x);
            actualResult1.IsSuccess.Should().BeTrue();
            actualResult1.Value.Should().Be(expectedResult1);

            var actualResult2 = NewtonMethod.SolveEquation(f, fDerivative);
            actualResult2.IsSuccess.Should().BeTrue();
            actualResult2.Value.Should().Be(expectedResult1);

            var eps = 0.01;
            var expectedResult3 = 1.7320508100147276;
            var actualResult3 = NewtonMethod.SolveEquation(f, fDerivative, x, eps);
            actualResult3.IsSuccess.Should().BeTrue();
            actualResult3.Value.Should().Be(expectedResult3);
        }

        #region Negative scenarios

        [Fact]
        public void SolveEquationByNewtonMethod_WrongDerivative_MethodFinishedWithTimeout()
        {
            var f = (double x) => x * x - 3;
            var fDerivative = (double x) => x;
            var x = 2;

            var expectedResult1 = 1.7320508075688772;
            var actualResult1 = NewtonMethod.SolveEquation(f, fDerivative, x);
            actualResult1.IsSuccess.Should().BeFalse();
            actualResult1.Value.Should().NotBe(expectedResult1);
        }

        #endregion
    }
}
