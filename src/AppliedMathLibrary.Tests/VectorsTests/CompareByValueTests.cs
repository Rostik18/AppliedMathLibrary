using System;
using AppliedMathLibrary.Objects;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.VectorsTests
{
    public class CompareByValueTests
    {
        [Fact]
        public void CompareByValue_VectorsComparedCorrectly()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);
            var vector3 = new Vector(1, 2, 4);

            Vector.CompareByValue(vector1, vector2).Should().BeTrue();
            Vector.CompareByValue(vector1, vector3).Should().BeFalse();
        }

        [Fact]
        public void CompareByValue_NullVectorsCompared_ExceptionThrown()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                Vector.CompareByValue(null, null);
            });
        }

        [Fact]
        public void CompareByValue_DifferentDimensionVectorsCompared_ExceptionThrown()
        {
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);

            Assert.Throws<ArgumentException>(() =>
            {
                Vector.CompareByValue(vector1, vector2);
            });
        }
    }
}
