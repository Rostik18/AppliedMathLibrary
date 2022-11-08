using AppliedMathLibrary.NumericalMethods;
using FluentAssertions;
using System;
using Xunit;

namespace AppliedMathLibrary.Tests.NumericalMethods
{
    public class IntegralsTests
    {
        [Theory]
        [InlineData(0, Math.PI, 0.000001, 1.9999999999897489, 1.9999999999893363)]
        [InlineData(0, Math.PI, 0.00001, 1.999999999996136, 1.999999999971118)]
        [InlineData(0, Math.PI, 0.0001, 2.0000000008052763, 1.9999999940399076)]
        [InlineData(0, Math.PI, 0.001, 2.0000000003679577, 1.9999996577143837)]
        [InlineData(0, Math.PI, 0.01, 2.0000070650799056, 1.9999820650436764)]
        [InlineData(0, Math.PI, 0.1, 1.9999683662670709, 1.997468926590932)]
        [InlineData(0, Math.PI, 1, 2.075392669312214, 1.8213284156635117)]
        public void CalculateIntegralsForSin_DifferentDeviation_IntegralsCalculated(double a, double b, double h, double expectedRect, double expectedTrap)
        {
            var f = (double x) => Math.Sin(x);

            var actualRectanglesResult = Integrals.RectanglesMethod(f, a, b, IntegrationOptions.Full, h);
            var actualTrapeziumResult = Integrals.TrapeziumMethod(f, a, b, IntegrationOptions.Full, h);

            actualRectanglesResult.IsSuccess.Should().BeTrue();
            actualTrapeziumResult.IsSuccess.Should().BeTrue();
            actualRectanglesResult.Value.Should().Be(expectedRect);
            actualTrapeziumResult.Value.Should().Be(expectedTrap);
        }

        [Theory]
        [InlineData(0, 2 * Math.PI, IntegrationOptions.Full, 3.999999999710259, 3.9999999997097495)]
        [InlineData(0, 2 * Math.PI, IntegrationOptions.UpperOX, 1.9999999999897489, 1.9999999999894897)]
        [InlineData(0, 2 * Math.PI, IntegrationOptions.LowerOX, 1.999999999720412, 1.9999999997201579)]
        [InlineData(0, 2 * Math.PI, IntegrationOptions.UpperMinusLower, 0.00000000026929226829878286, 0.0000000002692877832557762)]
        public void CalculateIntegralsForSin_DifferentOptions_IntegralsCalculated(double a, double b, IntegrationOptions ops, double expectedRect, double expectedTrap)
        {
            var f = (double x) => Math.Sin(x);

            var actualRectanglesResult = Integrals.RectanglesMethod(f, a, b, ops);
            var actualTrapeziumResult = Integrals.TrapeziumMethod(f, a, b, ops);

            actualRectanglesResult.IsSuccess.Should().BeTrue();
            actualTrapeziumResult.IsSuccess.Should().BeTrue();
            actualRectanglesResult.Value.Should().Be(expectedRect);
            actualTrapeziumResult.Value.Should().Be(expectedTrap);
        }

        #region Negative scenarios

        [Fact]
        public void CalculateIntegrals_WrongInterval_IntegralNotCalculated()
        {
            var f = (double x) => x;

            var rectanglesResult = Integrals.RectanglesMethod(f, 1, 0);
            var trapeziumResult = Integrals.TrapeziumMethod(f, 0, 0);

            rectanglesResult.IsFailure.Should().BeTrue();
            trapeziumResult.IsFailure.Should().BeTrue();
        }

        #endregion
    }
}
