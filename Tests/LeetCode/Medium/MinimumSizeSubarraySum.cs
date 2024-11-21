using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/minimum-size-subarray-sum/

public class MinimumSizeSubarraySum
{
    [Theory]
    [InlineData(7, new int[] { 2, 3, 1, 2, 4, 3 }, 2)]
    [InlineData(4, new int[] { 1, 4, 4 }, 1)]
    [InlineData(11, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 0)]
    [InlineData(15, new int[] { 5, 1, 3, 5, 10, 7, 4, 9, 2, 8 }, 2)]
    public void Given_WhenMinSubArrayLen_Then(int target, int[] nums, int expected)
    {
        int result = MinSubArrayLen(target, nums);
        result.Should().Be(expected);

        int result2 = MinSubArrayLenSlow(target, nums);
        result2.Should().Be(expected);
    }

    private static int MinSubArrayLen(int target, int[] nums)
    {
        int sum = 0;
        int start = 0;
        int end = 0;
        int minLength = int.MaxValue;

        for (; end < nums.Length; end++)
        {
            sum += nums[end];

            while (sum >= target)
            {
                minLength = Math.Min(minLength, end - start + 1);

                sum -= nums[start];
                start++;
            }
        }

        return start > 0 ?
            minLength :
            0;
    }

    /*
     * Referring to above:
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


    private static int MinSubArrayLenSlow(int target, int[] nums)
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
}
