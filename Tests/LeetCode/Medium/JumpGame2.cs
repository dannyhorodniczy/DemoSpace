using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/jump-game-ii
public class JumpGame2
{
    /*
     * in the previous problem, we simply wanted to determine whether or not the jumps were possible
     * in this case we want the minimum number of jumps
     * my intuition is that the previous algorithm is NOT suitable
     * in fact I believe it would find the most jumps possible
     * 
     * in this case, I think we need to use the forward tree algorithm again
     * this algorithm tests the largest jumps 1st...
     * 
     * hmm. maybe we should compare the results of all jumps, and then always pick the largest jump
     * 
     * int jumpCount = 0;
     * int remainingJumpLength = nums.Length - 1;
     * so, we start at the 1st index, pick the jump length
     * int jumpLength = nums[0];
     * if the jumpLength is >= remainingJumpLength: return jumpCount
     * remainingJumpLength -= jumpLength;
     * pick the highest jump length from the jump repeat un
     * jumpLength = Max(nums[n..m])
     * 
     */

    [Theory]
    [InlineData(new int[] { 2, 3, 1, 1, 4 }, 2)]
    [InlineData(new int[] { 2, 3, 0, 1, 4 }, 2)]
    [InlineData(new int[] { 2, 20, 1, 1, 1 }, 2)]
    [InlineData(new int[] { 1, 2, 1, 1, 1 }, 3)]
    [InlineData(new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 0 }, 2)]
    [InlineData(new int[] { 10, 9, 8, 7, 6, 20, 4, 3, 2, 1, 1, 1, 0 }, 2)]
    public void Given_WhenJumpGameAgainAndBetter_Then(int[] nums, int expected)
    {
        var result = Jump(nums);
        result.Should().Be(expected);
    }

    // ok let's go back to the algorithm from the 1st problem
    // we will work our way backward
    // start from the 2nd last index, find the largest jump that gets you to the last index
    // repeat until we have reached the 1st index
    public int Jump(int[] nums)
    {
        if (nums.Length < 2)
        {
            return 0;
        }

        if (nums[0] >= nums.Length - 1)
        {
            return 1;
        }

        int jumpCount = 1;
        int indexOfBiggestJump = nums.Length - 2;
        do
        {

            indexOfBiggestJump = GetIndexOfBiggestJump(nums, indexOfBiggestJump);
            if (indexOfBiggestJump != 0)
            {
                jumpCount++;
            }
            indexOfBiggestJump--;
        } while (indexOfBiggestJump > 0);

        return jumpCount;
    }

    private static int GetIndexOfBiggestJump(int[] nums, int indexOfBiggestJump)
    {
        int j = 1;
        for (int i = indexOfBiggestJump; i > -1; i--)
        {
            if (nums[i] >= j)
            {
                indexOfBiggestJump = i;
            }
            j++;
        }

        return indexOfBiggestJump;
    }
}
