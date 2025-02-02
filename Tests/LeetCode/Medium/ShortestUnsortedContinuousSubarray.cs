using FluentAssertions;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/shortest-unsorted-continuous-subarray/

public class ShortestUnsortedContinuousSubarray
{
    [Theory]
    [InlineData(new int[] { 2, 6, 4, 8, 10, 9, 15 }, 5)]
    [InlineData(new int[] { 1, 2, 3, 4 }, 0)]
    [InlineData(new int[] { 1 }, 0)]
    [InlineData(new int[] { 2, 1 }, 2)]
    [InlineData(new int[] { 1, 3, 2, 4, 5 }, 2)]
    [InlineData(new int[] { 1, 2, 3, 3, 3 }, 0)]
    [InlineData(new int[] { 5, 4, 3, 2, 1 }, 5)]
    public void Given_WhenFindUnsortedSubarray_Then(int[] nums, int expected)
    {
        // Given
        // When
        var result = FindUnsortedSubarray(nums);

        // Then
        result.Should().Be(expected);
    }

    private static int FindUnsortedSubarray(int[] nums)
    {
        var sorted = nums.Order().ToArray();
        var diffs = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            diffs[i] = sorted[i] - nums[i];
        }

        if (diffs.All(x => x == 0)) return 0;
        if (!diffs.Any(x => x == 0)) return nums.Length;

        int frontPtr = 0;
        bool frontStop = false;
        int backPtr = nums.Length - 1;
        bool backStop = false;

        while (!frontStop || !backStop)
        {
            if (!frontStop && diffs[frontPtr] == 0)
            {
                frontPtr++;
            }
            else
            {
                frontStop = true;
            }


            if (!backStop && diffs[backPtr] == 0)
            {
                backPtr--;
            }
            else
            {
                backStop = true;
            }
        }

        return backPtr - frontPtr + 1;
    }
}
