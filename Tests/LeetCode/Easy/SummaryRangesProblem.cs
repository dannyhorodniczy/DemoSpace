using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/summary-ranges/

public class SummaryRangesProblem
{
    [Theory]
    [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, new string[] { "0->2", "4->5", "7" })]
    [InlineData(new int[] { 7, 6, 4, 3, 1 }, new string[] { "0", "2->4", "6", "8->9" })]
    [InlineData(new int[] { 7, 2, 4, 1 }, new string[] { "-1" })]
    public void Given_WhenSummaryRanges_Then(int[] nums, string[] expected)
    {
        // Given
        // When
        var result = SummaryRanges(nums);

        // Then
        result.Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static IList<string> SummaryRanges(int[] nums)
    {
        /*
         * let's iterate foreach on the array
         * keep a counter and when the counter doesn't match the value we act
         */

        if (nums.Length == 1) return [nums[0].ToString()];

        var list = new List<string>();
        int? start = null;
        int? end = null;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i - 1] + 1 == nums[i])
            {
                if (!start.HasValue)
                {
                    start = nums[i - 1];
                }

                end = nums[i];

                if (i == nums.Length - 1)
                {
                    list.Add($"{start}->{end}");
                }
            }
            else
            {
                // 3 cases:
                // 1. number is on it's own
                if (!start.HasValue && !end.HasValue)
                {
                    list.Add($"{nums[i - 1]}");
                }

                // 2. Start an end need to be added
                if (start.HasValue && end.HasValue)
                {
                    list.Add($"{start}->{end}");
                }

                // 3. we're at the end and need to add the end
                if (i == nums.Length - 1)
                {
                    list.Add($"{nums[i]}");
                }

                start = null;
                end = null;
            }
        }

        return list;
    }
}
