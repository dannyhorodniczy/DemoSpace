using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/merge-intervals/description/

public class MergeIntervals
{
    [Fact]
    public void Given_When_Then()
    {
        // Given
        int[][] intervals = [[1, 3], [2, 6], [8, 10], [15, 18]];

        // When
        var result = Merge(intervals);

        // Then
        int[][] expected = [[1, 6], [8, 10], [15, 18]];
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Given_When_Then2()
    {
        // Given
        int[][] intervals = [[1, 4], [4, 5]];

        // When
        var result = Merge(intervals);

        // Then
        int[][] expected = [[1, 5]];
        result.Should().BeEquivalentTo(expected);
    }

    private int[][] Merge(int[][] intervals)
    {
        // 1. Sort by 1st index
        var sorted = intervals.OrderBy(x => x[0]).ToArray();
        var mergedIntervals = new List<int[]> { sorted[0] };

        for (int i = 1; i < sorted.Length; i++)
        {
            if (mergedIntervals[^1][1] >= sorted[i][0])
            {
                // merge
                mergedIntervals[^1] = [
                    mergedIntervals[^1][0],
                    Math.Max(mergedIntervals[^1][1], sorted[i][1])
                    ];
            }
            else
            {
                // no merge
                mergedIntervals.Add(sorted[i]);
            }
        }

        return mergedIntervals.ToArray();
    }
}
