using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/next-greater-element-ii/

public class NextGreaterElement2
{
    [Theory]
    [InlineData(new int[] { 1, 2, 1 }, new int[] { 2, -1, 2 })]
    [InlineData(new int[] { 1, 2, 3, 4, 3 }, new int[] { 2, 3, 4, -1, 4 })]
    public void Given_WhenNextGreaterElements_Then(int[] nums, int[] expected)
    {
        // Given
        // When
        var result = NextGreaterElements(nums);

        // Then
        result.Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static int[] NextGreaterElements(int[] nums)
    {
        int length = (nums.Length * 2) - 1;
        int[] rotated = new int[length];

        for (int i = 0; i < rotated.Length; i++)
        {
            rotated[i] = nums[i % nums.Length];
        }

        int[] nextGreater = new int[length];
        Array.Fill(nextGreater, -1);

        var stack = new Stack<int>();
        for (int i = 0; i < nextGreater.Length; i++)
        {
            while (stack.Count > 0 && rotated[i] > rotated[stack.Peek()])
            {
                nextGreater[stack.Pop()] = i;
            }
            stack.Push(i);
        }

        int[] ans = new int[nums.Length];

        for (int i = 0; i < ans.Length; i++)
        {
            int ngi = nextGreater[i];
            if (ngi == -1) ans[i] = ngi;
            else ans[i] = rotated[ngi];
        }

        return ans;
    }
}
