using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;
public class FSTMI_ForPractice
{
    [Theory]
    [InlineData("00110", 1)]
    [InlineData("010110", 2)]
    [InlineData("00011000", 2)]
    [InlineData("11011", 1)]
    [InlineData("0101100011", 3)]
    [InlineData("100000001010000", 3)]
    [InlineData("10011111110010111011", 5)]
    public void Given_WhenMinFlipsMonoIncr_Then(string s, int minFlips)
    {
        int result = MinFlipsMonoIncr(s);
        result.Should().Be(minFlips);
    }

    private static int MinFlipsMonoIncr(string s)
    {
        return 0;
    }
}