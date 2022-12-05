using AppliedMathLibrary.NumericalMethods;
using FluentAssertions;
using System;
using Xunit;

namespace AppliedMathLibrary.Tests.NumericalMethods
{
    public class LagrangianInterpolationTests
    {
        [Fact]
        public void InterpolateSimpleFync_PolinimialCreated()
        {
            var xAxis = new double[] { 1, 2, 3, 5 };
            var yAxis = new double[] { 1, 5, 17, 89 };

            var polinimialResult = LagrangianInterpolation.CreatePolinimial(xAxis, yAxis);

            polinimialResult.IsSuccess.Should().BeTrue();

            var f = polinimialResult.Value;

            for (int i = 0; i < xAxis.Length; i++)
            {
                Math.Round(f(xAxis[i]), 5).Should().Be(yAxis[i]);
            }
        }

        [Fact]
        public void InterpolateSimpleFync_CoefficientsCalculated()
        {
            var xAxis = new double[] { 1, 2, 3, 5 };
            var yAxis = new double[] { 1, 5, 17, 89 };
            var expectedCoefficients = new double[] { -1, 3.000000000000007, -2, 1.0000000000000004 };

            var coefficientsResult = LagrangianInterpolation.CalculatePolynomialCoefficients(xAxis, yAxis);

            coefficientsResult.IsSuccess.Should().BeTrue();

            coefficientsResult.Value.Should().BeEquivalentTo(expectedCoefficients);
        }
    }
}
