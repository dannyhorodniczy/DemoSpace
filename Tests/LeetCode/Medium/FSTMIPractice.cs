using FluentAssertions;
using System;
using Xunit;

namespace Tests.LeetCode.Medium;
public class FSTMIPractice
{
    [Theory]
    [InlineData("00110", 1)]
    [InlineData("010110", 2)]
    [InlineData("00011000", 2)]
    [InlineData("11011", 1)]
    public void Given_WhenMinFlipsMonoIncr_Then(string s, int minFlips)
    {
        int result = MinFlipsMonoIncr(s);
        result.Should().Be(minFlips);
    }

    public int MinFlipsMonoIncr(string s)
    {
        /*
         * ok this is a dynamic programming problem because
         * it has a number of solutions that we need to caluculate to figure out the optimal solution
         * 
         * we know that a monotone increasing binary string is one the begins with 0's and ends with 1's
         * or is either all 0's or 1's
         * 
         * it's important to remember that you don't need to flip just 0's or just 1's
         * you can flip both :)
         * 
         * we need to count the number of required flips for both
         * so we'll pass an array from start to finish to count the number of 1's to flip to 0's
         * pass another array from finish to start to count the number of 0's to flip to 1's
         * the arrays will be string length + 1
         * the 1st index of the 1s to 0's array will always be 0
         * the last index of the 0's to 1's array will always be 0
         * because at the extreme ends of the string there are no flips to be done
         * 
         * then you iterate on the 2 arrays at the same time, sum their values, return the minimum value
         */

        int[] onesToFlip = new int[s.Length + 1];
        int[] zerosToFlip = new int[s.Length + 1];

        for (int i = 1; i < s.Length + 1; i++)
        {
            int isOne = s[i - 1] == '1' ? 1 : 0;
            onesToFlip[i] = onesToFlip[i - 1] + isOne;
        }

        for (int i = s.Length - 1; i > -1; i--)
        {
            int isZero = s[i] == '0' ? 1 : 0;
            zerosToFlip[i] = zerosToFlip[i + 1] + isZero;
        }

        int minFlips = int.MaxValue;
        for (int i = 0; i < s.Length + 1; i++)
        {
            minFlips = Math.Min(minFlips, onesToFlip[i] + zerosToFlip[i]);
        }

        return minFlips;
    }
}
