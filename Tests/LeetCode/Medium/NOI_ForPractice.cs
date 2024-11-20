using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/number-of-islands/

public class NOI_ForPractice
{
    [Fact]
    public void Given_WhenNumIslands_Then()
    {
        char[][] grid = [
  ['1','1','1','1','0'],
  ['1','1','0','1','0'],
  ['1','1','0','0','0'],
  ['0','0','0','0','0']
];
        int result = NumIslands(grid);
        result.Should().Be(1);
    }

    [Fact]
    public void Given_WhenNumIslands_Then2()
    {
        char[][] grid = [
  ['1','1','0','0','0'],
  ['1','1','0','0','0'],
  ['0','0','1','0','0'],
  ['0','0','0','1','1']
];
        int result = NumIslands(grid);
        result.Should().Be(3);
    }

    [Fact]
    public void Given_WhenNumIslands_Then3()
    {
        char[][] grid = [
  ['1','1','1'],
  ['0','1','0'],
  ['0','1','0']
];
        int result = NumIslands(grid);
        result.Should().Be(1);
    }

    private int NumIslands(char[][] grid)
    {
        return 0;
    }
}
