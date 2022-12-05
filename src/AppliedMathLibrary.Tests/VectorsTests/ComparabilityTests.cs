using System;
using AppliedMathLibrary.Objects;
using FluentAssertions;
using Xunit;

namespace AppliedMathLibrary.Tests.VectorsTests
{
    public class ComparabilityTests
    {
        [Fact]
        public void Compare_UnequalVectors_ReturnsFalse()
        {
            var vector1 = new Vector(1, 3, 2);
            var vector2 = new Vector(1, 2, 3);

            Vector.Comparable(vector1, vector2).Should().BeFalse();
            Vector.Comparable(vector2, vector1).Should().BeFalse();
            vector1.ComparableWith(vector2).Should().BeFalse();
        }

        [Fact]
        public void Compare_OneVectorIsBigger_ReturnsTrue()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(2, 2, 3);

            Vector.Comparable(vector1, vector2).Should().BeTrue();
            Vector.Comparable(vector2, vector1).Should().BeTrue();
            vector1.ComparableWith(vector2).Should().BeTrue();
        }

        [Fact]
        public void Compare_EqualVectors_ReturnsTrue()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            Vector.Comparable(vector1, vector2).Should().BeTrue();
            vector1.ComparableWith(vector2).Should().BeTrue();
        }

        [Fact]
        public void Compare_DifferentDimensionVectorsCompared_ReturnsFalse()
        {
            var vector1 = new Vector(1, 2);
            var vector2 = new Vector(1, 2, 3);

            Vector.Comparable(vector1, vector2).Should().BeFalse();
            vector1.ComparableWith(vector2).Should().BeFalse();
        }

        [Fact]
        public void Compare_NullVectorsProvided_ExceptionThrown()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                Vector.Comparable(null, null);
                new Vector().ComparableWith(null);
            });
        }
    }
}
