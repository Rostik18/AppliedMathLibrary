using AppliedMathLibrary.Methods;
using AppliedMathLibrary.Objects;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.Methods
{
    public class GaussMethodTests
    {
        [Fact]
        public void SolveMatrixSystemByGaussMethod_MatrixSolved_CorrectDeterminant()
        {
            var A = new Matrix(3, new double[] { 4, 2, -1, 5, 3, -2, 3, 2, -3 });
            var b = new Vector(1, 2, 0);

            var expectedResult = new Vector(-1, 3, 1);
            var expectedDet = -3;

            var actualResult = GaussMethod.SolveMatrixSystem(A, b, out var actualDet);

            actualResult.IsSuccess.Should().BeTrue();
            actualResult.Value.Should().BeEquivalentTo(expectedResult);
            actualDet.Should().Be(expectedDet);
        }

        #region Negative scenarios

        [Fact]
        public void MethodValidatingInput_ExceptionThrown()
        {
            var A = new Matrix(2, 2, new double[] { 1, 8, 1, 4 });
            var b = new Vector(21, 30, 17);

            var result = GaussMethod.SolveMatrixSystem(A, b, out _);

            result.IsSuccess.Should().BeFalse();
        }

        #endregion
    }
}
