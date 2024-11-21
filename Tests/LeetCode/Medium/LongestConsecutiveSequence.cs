using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/longest-consecutive-sequence/

public class LongestConsecutiveSequence
{
    [Theory]
    [InlineData(new int[] { 100, 4, 200, 1, 3, 2 }, 4)]
    [InlineData(new int[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 }, 9)]
    public void Given_When_Then(int[] nums, int expected)
    {
        int result = LongestConsecutive(nums);
        result.Should().Be(expected);
    }

    public int LongestConsecutive(int[] nums)
    {
        // edge cases
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        // a hashset will remove duplicates and create O(1) lookup time
        // technically, to create the structure it's O(n)...
        var hashset = new HashSet<int>(nums);
        int longestConsecutive = 1;

        foreach (var i in hashset)
        {
            // we can ignore the element if its predecessor exists
            if (hashset.Contains(i - 1))
            {
                continue;
            }

            // count each element in sequence
            int counter = 1;
            while (hashset.Contains(i + counter))
            {
                counter++;
            }
            // if tempMax > max ==> max = TempMax
            longestConsecutive = Math.Max(longestConsecutive, counter);
        }

        return longestConsecutive;
    }
}
