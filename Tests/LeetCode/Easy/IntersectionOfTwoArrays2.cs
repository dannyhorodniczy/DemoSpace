using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/intersection-of-two-arrays-ii/

public class IntersectionOfTwoArrays2
{
    [Theory]
    [InlineData(new int[] { 1, 2, 2, 1 }, new int[] { 2, 2 }, new int[] { 2, 2 })]
    [InlineData(new int[] { 4, 9, 5 }, new int[] { 9, 4, 9, 8, 4 }, new int[] { 4, 9 })]
    [InlineData(new int[] { 1 }, new int[] { 1 }, new int[] { 1 })]
    [InlineData(new int[] { 2, 1 }, new int[] { 1, 2 }, new int[] { 1, 2 })]
    public void Given_WhenIntersect_Then(int[] nums1, int[] nums2, int[] expected)
    {
        // Given
        // When
        var result = Intersect(nums1, nums2);

        // Then
        result.Should().BeEquivalentTo(expected);
    }

    private static int[] Intersect(int[] nums1, int[] nums2)
    {
        int[] smaller;
        List<int> larger;
        List<int> result = new List<int>();

        if (nums1.Length > nums2.Length)
        {
            larger = nums1.ToList();
            smaller = nums2;
        }
        else
        {
            larger = nums2.ToList();
            smaller = nums1;
        }

        foreach (int num in smaller)
        {
            if (larger.Contains(num))
            {
                result.Add(num);
                larger.Remove(num);
            }
        }

        return result.ToArray();
    }
}
