using FluentAssertions;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/jump-game/
public class JumpGame
{

    /* 
     * you need a total jump of nums.Length-1
     * 1. take the value of the 1st element of the array
     * 2. determine the values of the possible landing spots
     * 3. make each possible jump (unless you land on 0, or you are >= nums.Length-1)
     * 4. then repeat for each possibility until >= nums.Length-1
     * 
     * so this algorithm will work, but it is slow
     * I think it's very important to have a proper algorithm thought out before implementation
     * so in this case, the logic was "let's brute force through a potentially *VERY* large tree
     * for perhaps 1 branch that will work
     * 
     * I think better logic is, let's try to get to the start from the end
     * because if a branch exists, we should find it sooner
     */

    [Theory]
    [InlineData(new int[] { 2, 3, 1, 1, 4 }, true)]
    [InlineData(new int[] { 3, 2, 1, 0, 4 }, false)]
    [InlineData(new int[] { 2, 0 }, true)]
    [InlineData(new int[] { 2, 5, 0, 0 }, true)]
    [InlineData(new int[] { 2, 0, 0 }, true)]
    [InlineData(new int[] { 1, 2, 3 }, true)]
    public void Given_WhenCanJump_Then(int[] nums, bool expected)
    {
        bool result = CanJump(nums);
        result.Should().Be(expected);
    }

    public bool CanJump(int[] nums)
    {
        if (nums.Length < 2 || (nums.Length == 2 && nums[0] > 0))
        {
            return true;
        }

        if (nums[0] == 0)
        {
            return false;
        }

        bool canJump = false;
        int jumpLength = 1;

        for (int i = nums.Length - 2; i > -1; i--)
        {
            if (nums[i] >= jumpLength)
            {
                jumpLength = 1;
                canJump = true;
            }
            else
            {
                canJump = false;
                jumpLength++;
            }
        }

        return canJump;
    }

    [Theory]
    [InlineData(new int[] { 2, 3, 1, 1, 4 }, true)]
    [InlineData(new int[] { 3, 2, 1, 0, 4 }, false)]
    [InlineData(new int[] { 2, 0 }, true)]
    [InlineData(new int[] { 2, 5, 0, 0 }, true)]
    public void Given_WhenCanJumpBad_Then(int[] nums, bool expected)
    {
        bool result = CanJumpBad(nums);
        result.Should().Be(expected);
    }

    public bool CanJumpBad(int[] nums)
    {
        int distanceToGo = nums.Length - 1;
        if (distanceToGo < 1)
        {
            return true;
        }

        return CanJumpBadInternal(nums, 0, distanceToGo);
    }

    private static bool CanJumpBadInternal(int[] nums, int index, int distanceToGo)
    {
        if (nums[index] >= distanceToGo)
        {
            return true;
        }

        if (nums[index] < 1)
        {
            return false;
        }

        foreach (var i in Enumerable.Range(1, nums[index]))
        {
            // i represents another range of jumps
            if (CanJumpBadInternal(nums, i + index, distanceToGo - i))
            {
                return true;
            }
        }

        return false;
    }
}
