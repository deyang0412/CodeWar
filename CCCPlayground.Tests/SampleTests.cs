using System; using Xunit; namespace CCCPlayground.Tests {     public class SampleTests     {         public SampleTests()         {         }                  [Fact(DisplayName = nameof(SampleTest))]         public void SampleTest()         {             Assert.Equal(3, Kata.CountCombinations(4, new int[] { 1, 2 }));         }          [Fact(DisplayName = nameof(AnotherSimpleCase))]         public void AnotherSimpleCase()         {             Assert.Equal(4, Kata.CountCombinations(10, new int[] { 5, 2, 3 }));         }          [Fact(DisplayName = nameof(NoChance))]         public void NoChance()         {             Assert.Equal(0, Kata.CountCombinations(11, new int[] { 5, 7 }));         }          [Fact]         public void Simple()         {             Assert.Equal(158, Kata.CountCombinations(100, new int[] { 1, 5, 10, 50 }));         }     } }  