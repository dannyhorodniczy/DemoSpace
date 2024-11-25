using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;
public class SAL_ForPractice
{
    [Fact]
    public void Given_WhenSnakesAndLadders_Then()
    {
        int[][] board = [
            [-1, -1, -1, -1, -1, -1],
            [-1, -1, -1, -1, -1, -1],
            [-1, -1, -1, -1, -1, -1],
            [-1, 35, -1, -1, 13, -1],
            [-1, -1, -1, -1, -1, -1],
            [-1, 15, -1, -1, -1, -1]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(4);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then2()
    {
        int[][] board = [
            [-1, -1],
            [-1, 3]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(1);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then3()
    {
        int[][] board = [
            [-1,-1,-1],
            [-1,9,8],
            [-1,8,9]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(1);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then4()
    {
        int[][] board = [
            [-1,4,-1],
            [6,2,6],
            [-1,3,-1]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(2);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then5()
    {
        int[][] board = [
            [1,1,-1],
            [1,1,1],
            [-1,1,1]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(-1);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then6()
    {
        int[][] board = [
            [-1,1,2,-1],
            [2,13,15,-1],
            [-1,10,-1,-1],
            [-1,6,2,8]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(2);
    }

    [Fact]
    public void Given_WhenSnakesAndLadders_Then7()
    {
        int[][] board = [
            [-1,-1,19,10,-1],
            [2,-1,-1,6,-1],
            [-1,17,-1,19,-1],
            [25,-1,20,-1,-1],
            [-1,-1,-1,-1,15]
            ];

        var result = SnakesAndLadders(board);
        result.Should().Be(2);
    }

    private static int SnakesAndLadders(int[][] board)
    {
        return 0;
    }
}
