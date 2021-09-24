using System.Collections.Generic;
using AppliedMathLibrary.Matrices;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.MatricesTests
{
    public class ToVectorsTests
    {
        [Fact]
        public void ToVectors_ListOfVectorsReturned()
        {
            var n = 3;
            var m = 2;
            var values = new List<double> { 1, 2, 3, 4, 5, 6 };

            var matrix = new Matrix(n, m, values);

            var vectors = matrix.ToVectors();

            vectors[0][0].Should().Be(1);
            vectors[0][1].Should().Be(2);
            vectors[1][0].Should().Be(3);
            vectors[1][1].Should().Be(4);
            vectors[2][0].Should().Be(5);
            vectors[2][1].Should().Be(6);
        }
    }
}
