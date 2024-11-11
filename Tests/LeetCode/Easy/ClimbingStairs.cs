using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/climbing-stairs/

public class ClimbingStairs
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 5)]
    [InlineData(5, 8)]
    [InlineData(6, 13)]
    [InlineData(7, 21)]
    public void Given_WhenClimbStairs_Then(int n, int expected)
    {
        int result = ClimbStairs(n);
        result.Should().Be(expected);
    }

    public int ClimbStairs(int n)
    {
        /*
         * this reminds of the Fibonacci calculation
         * because the calculation always needs to be done from the base case
         * 
         * so we have 2 possible step sizes 1 & 2
         * -- there is always the case of all single steps
         * -- then the case of 1 set of 2 steps (at all possible indices)
         * -- then the case of 2 sets of 2 steps (at all possible indices)
         * -- then the case of t sets of 2 steps (at all possible indices)
         * -- t <= n/2
         * 
         * -- there is also the spacing between the 2 steps
         * --  
         */

        // after writing out the 1st few solutions it looked a lot like Fibonacci
        // which is turns out is the case for n > 3

        if (n < 4)
        {
            return n;
        }

        int prev2 = 2;
        int prev1 = 3;
        int current = 0;

        for (int i = 4; i <= n; i++)
        {
            current = prev1 + prev2;
            prev2 = prev1;
            prev1 = current;
        }

        return current;
    }
}
