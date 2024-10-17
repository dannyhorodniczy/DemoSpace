using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/jump-game/
public class JumpGame
{
    [Theory]
    //[InlineData(new int[] { 2, 3, 1, 1, 4 }, true)]
    //[InlineData(new int[] { 3, 2, 1, 0, 4 }, false)]
    //[InlineData(new int[] { 2, 0 }, true)]
    [InlineData(new int[] { 2, 5, 0, 0 }, true)]
    public void Given_WhenCanJump_Then(int[] nums, bool expected)
    {
        bool result = CanJump(nums);
        result.Should().Be(expected);
    }

    /* 
     * you need a total jump of nums.Length-1
     * 1. take the value of the 1st element of the array
     * 2. determine the values of the possible landing spots
     * 3. make each possible jump (unless you land on 0, or you are >= nums.Length-1)
     * 4. then repeat for each possibility until >= nums.Length-1
     */

    public bool CanJump(int[] nums)
    {
        int distanceToGo = nums.Length - 1;

        return CanJumpInternal(nums, nums[0], distanceToGo);
    }

    private static bool CanJumpInternal(int[] nums, int indexValue, int distanceToGo)
    {
        if (distanceToGo < 1 || indexValue >= distanceToGo)
        {
            return true;
        }

        int[] range = GetJumpRange(nums[indexValue]);
        foreach (var i in range)
        {
            // i represents another range of jumps
            return CanJumpInternal(nums, i, distanceToGo - i);
        }

        return false;
    }

    private static int[] GetJumpRange(int index)
    {
        return index == 0 ? Array.Empty<int>() : Enumerable.Range(1, index).Reverse().ToArray();
    }

    //private static int[] GetNextLandingSpot(int[] nums, int index)
    //{
    //    return index == 0 ? Array.Empty<int>() : Enumerable.Range(1, index).Reverse().ToArray();
    //}
}
