using FluentAssertions;
using System;
using Xunit;

namespace Tests.LeetCode.Hard;

// https://leetcode.com/problems/trapping-rain-water/

public class TrappingRainWater
{

    [Theory]
    [InlineData(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }, 6)]
    [InlineData(new int[] { 4, 2, 0, 3, 2, 5 }, 9)]
    public void Given_WhenTrap_Then(int[] nums, int profit)
    {
        // Given
        // When
        int result = Trap(nums);

        // Then
        result.Should().Be(profit);
    }

    private static int Trap(int[] height)
    {
        int ans = 0;
        int[] rightMax = new int[height.Length];
        rightMax[^1] = height[^1];

        for (int i = (rightMax.Length - 2); i > 0; i--)
        {
            rightMax[i] = Math.Max(height[i], rightMax[i + 1]);
        }

        int[] leftMax = new int[height.Length];
        leftMax[0] = height[0];

        for (int i = 1; i < (leftMax.Length - 1); i++)
        {
            leftMax[i] = Math.Max(leftMax[i - 1], height[i]);
            ans += Math.Min(leftMax[i], rightMax[i]) - height[i];
        }

        return ans;
    }
}
