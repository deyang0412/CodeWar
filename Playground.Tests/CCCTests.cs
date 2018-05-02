using System;
using Playgound.Katas;
using Xunit;
namespace Playground.Tests
{
    public class CCCTests
    {
        public CCCTests()
        {
        }

        [Fact(DisplayName = nameof(SampleTest))]
        public void SampleTest()
        {
            Assert.Equal(3, CCC.CountCombinations(4, new int[] { 1, 2 }));
        }

        [Fact(DisplayName = nameof(AnotherSimpleCase))]
        public void AnotherSimpleCase()
        {
            Assert.Equal(4, CCC.CountCombinations(10, new int[] { 5, 2, 3 }));
        }

        [Fact(DisplayName = nameof(NoChance))]
        public void NoChance()
        {
            Assert.Equal(0, CCC.CountCombinations(11, new int[] { 5, 7 }));
        }

        [Fact]
        public void Simple()
        {
            Assert.Equal(158, CCC.CountCombinations(100, new int[] { 1, 5, 10, 50 }));
        }
    }
}
