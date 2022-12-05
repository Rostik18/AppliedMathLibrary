using AppliedMathLibrary.Objects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AppliedMathLibrary.Tests.ObjectsTests
{
    public class PolynomialTests
    {
        [Theory]
        [InlineData(new[] { 1.0, 2 }, new[] { -2.0, 1 }, new[] { -2.0, -3, 2 })]
        [InlineData(new[] { 0.0, 5, -3 }, new[] { 1.0, 0, 1 }, new[] { 0.0, 5, -3, 5, -3 })]
        public void MultiplyPolynoms_CorrectProduct(double[] coeff1, double[] coeff2, double[] expected)
        {
            var pol1 = new Polynomial(coeff1);
            var pol2 = new Polynomial(coeff2);
            var expectedPol = new Polynomial(expected);

            var result1 = pol1.Multiply(pol2);
            var result2 = Polynomial.Multiply(pol1, pol2);
            var result3 = pol1 * pol2;

            result1.Should().BeEquivalentTo(expectedPol);
            result2.Should().BeEquivalentTo(expectedPol);
            result3.Should().BeEquivalentTo(expectedPol);
        }

        [Theory]
        [InlineData(new[] { 1.0, 2 }, 1, new[] { 1.0, 2 })]
        [InlineData(new[] { 1.0, 2 }, 0, new[] { 0.0, 0 })]
        [InlineData(new[] { 1.0, 2 }, -1, new[] { -1.0, -2 })]
        [InlineData(new[] { 1.0, 2 }, 2.5, new[] { 2.5, 5 })]
        public void MultiplyPolynomByScalar_CorrectProduct(double[] coeff, double scalar, double[] expected)
        {
            var pol = new Polynomial(coeff);
            var expectedPol = new Polynomial(expected);

            var result1 = pol.MultiplyBy(scalar);
            var result2 = Polynomial.Multiply(pol, scalar);
            var result3 = pol * scalar;
            var result4 = scalar * pol;

            result1.Should().BeEquivalentTo(expectedPol);
            result2.Should().BeEquivalentTo(expectedPol);
            result3.Should().BeEquivalentTo(expectedPol);
            result4.Should().BeEquivalentTo(expectedPol);
        }

        [Theory]
        [InlineData(new[] { 1.0, 2 }, new[] { -2.0, 1 }, new[] { -1.0, 3 })]
        [InlineData(new[] { 0.0, 5, -3 }, new[] { 1.0, 0, 1 }, new[] { 1.0, 5, -2 })]
        [InlineData(new[] { 0.0, 5, -3, 2 }, new[] { 1.0, 0, 1 }, new[] { 1.0, 5, -2, 2 })]
        [InlineData(new[] { 0.0, 5, 1 }, new[] { 1.0, 0, 1, 2 }, new[] { 1.0, 5, 2, 2 })]
        public void AddPolynoms_CorrectSum(double[] coeff1, double[] coeff2, double[] expected)
        {
            var pol1 = new Polynomial(coeff1);
            var pol2 = new Polynomial(coeff2);
            var expectedPol = new Polynomial(expected);

            var result1 = pol1.Add(pol2);
            var result2 = Polynomial.Sum(pol1, pol2);
            var result3 = pol1 + pol2;

            result1.Should().BeEquivalentTo(expectedPol);
            result2.Should().BeEquivalentTo(expectedPol);
            result3.Should().BeEquivalentTo(expectedPol);
        }

        [Theory]
        [InlineData(new[] { 1.0, 2 }, new[] { -2.0, 1 }, new[] { 3.0, 1 })]
        [InlineData(new[] { 0.0, 5, -3 }, new[] { 1.0, 0, 1 }, new[] { -1.0, 5, -4 })]
        [InlineData(new[] { 0.0, 5, -3, 2 }, new[] { 1.0, 0, 1 }, new[] { -1.0, 5, -4, 2 })]
        [InlineData(new[] { 0.0, -5, 1 }, new[] { 1.0, -1, 1, 2 }, new[] { -1.0, -4, 0, -2 })]
        public void SubtractPolynoms_CorrectResult(double[] coeff1, double[] coeff2, double[] expected)
        {
            var pol1 = new Polynomial(coeff1);
            var pol2 = new Polynomial(coeff2);
            var expectedPol = new Polynomial(expected);

            var result1 = pol1.Subtract(pol2);
            var result2 = Polynomial.Subtract(pol1, pol2);
            var result3 = pol1 - pol2;

            result1.Should().BeEquivalentTo(expectedPol);
            result2.Should().BeEquivalentTo(expectedPol);
            result3.Should().BeEquivalentTo(expectedPol);
        }

        [Theory]
        [InlineData(new[] { 1.0, 2 }, 2, new[] { 0.5, 1 })]
        [InlineData(new[] { 0.0, 4.5, -3 }, -1.5, new[] { 0.0, -3, 2 })]
        public void DividePolynom_CorrectResult(double[] coeff, double scalar, double[] expected)
        {
            var pol = new Polynomial(coeff);
            var expectedPol = new Polynomial(expected);

            var result1 = pol.DivideBy(scalar);
            var result2 = Polynomial.Divide(pol, scalar);
            var result3 = pol / scalar;

            result1.Should().BeEquivalentTo(expectedPol);
            result2.Should().BeEquivalentTo(expectedPol);
            result3.Should().BeEquivalentTo(expectedPol);
        }

        [Theory]
        [InlineData(new[] { 0.0 }, 0)]
        [InlineData(new[] { 1.0 }, 0)]
        [InlineData(new[] { 1.0, 2 }, 1)]
        [InlineData(new[] { 1.0, 2, 0 }, 1)]
        public void HighestPower_CorrectResult(double[] coeff, int expected)
        {
            var pol = new Polynomial(coeff);

            pol.HighestPower.Should().Be(expected);
        }

        [Fact]
        public void ToArray_NewArrayReturned()
        {
            var initialArray = new double[] { 1, 2 };

            var newArray = new Polynomial(initialArray).ToArray();

            newArray.Should().BeEquivalentTo(initialArray);
            newArray.Equals(initialArray).Should().BeFalse();
        }

        #region Negative scenarios

        [Fact]
        public void CreateEmptyPolynomial_ExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => new Polynomial());
            Assert.Throws<ArgumentException>(() => new Polynomial(Array.Empty<double>()));
            Assert.Throws<ArgumentException>(() => new Polynomial(new List<double>()));
        }

        [Fact]
        public void DividePolynomByZero_ExceptionThrown()
        {
            Assert.Throws<DivideByZeroException>(() => new Polynomial(1, 2).DivideBy(0));
            Assert.Throws<DivideByZeroException>(() => Polynomial.Divide(new(1, 2), 0));
            Assert.Throws<DivideByZeroException>(() => new Polynomial(1, 2) / 0);
        }

        #endregion
    }
}
