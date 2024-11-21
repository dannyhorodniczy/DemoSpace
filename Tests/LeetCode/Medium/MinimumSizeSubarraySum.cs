using FluentAssertions;
using System;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/minimum-size-subarray-sum/

public class MinimumSizeSubarraySum
{
    [Theory]
    [InlineData(7, new int[] { 2, 3, 1, 2, 4, 3 }, 2)]
    [InlineData(4, new int[] { 1, 4, 4 }, 1)]
    [InlineData(11, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 0)]
    public void Given_WhenMinSubArrayLen_Then(int target, int[] nums, int expected)
    {
        int result = MinSubArrayLen(target, nums);
        result.Should().Be(expected);
    }

    private static int MinSubArrayLen(int target, int[] nums)
    {
        int n = nums.Length;
        int minLength = int.MaxValue;
        int left = 0;
        int sum = 0;

        for (int right = 0; right < n; right++)
        {
            sum += nums[right];

            while (sum >= target)
            {
                minLength = Math.Min(minLength, right - left + 1);
                sum -= nums[left];
                left++;
            }
        }

        return minLength == int.MaxValue ? 0 : minLength;
    }

    /*
     * 1. begin with a window of size 1
     * --> evaluate if it is >= target
     * --> if yes, return 1
     * --> else:
     * - increment the window size from the right
     * --> evaluate if it is >= target
     * --> if yes, save 2 as the smallest window size
     * --> shift the window 1 over to the right
     * --> reevalute, if 
     * --> but what do we do when we've found a window > 1 that satisfies the condition
     * --> how do we
     * 
     * 
     * --> if no, increment the window size again
     * 
     */
}
