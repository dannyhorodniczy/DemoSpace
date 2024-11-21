using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/minimum-size-subarray-sum/

// This does work, but it's too slow on leetcode

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
        int minWindowSize = int.MaxValue;
        int start = 0;
        int end = 0;
        bool targetHit = false;

        while (end < nums.Length)
        {
            //int[] window = nums[start..(end + 1)];
            var window = nums.Skip(start).Take((end - start) + 1);
            int sum = window.Sum();

            if (sum >= target)
            {
                //if (window.Count() == 1)
                //{
                //    return 1;
                //}

                // shift the window
                // only increment the end if the targetHit == false
                if (targetHit == false && end != (nums.Length - 1))
                {
                    end++;
                }
                start++;


                targetHit = true;
                minWindowSize = window.Count() < minWindowSize ? window.Count() : minWindowSize;
                continue;
            }

            targetHit = false;
            end++;
        }

        return minWindowSize == int.MaxValue ? 0 : minWindowSize;
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
