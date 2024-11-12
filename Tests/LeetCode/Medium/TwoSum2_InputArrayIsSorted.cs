using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/

public class TwoSum2_InputArrayIsSorted
{
    [Theory]
    [InlineData(new int[] { 2, 7, 11, 15 }, 9, new int[] { 1, 2 })]
    [InlineData(new int[] { 2, 3, 4 }, 6, new int[] { 1, 3 })]
    [InlineData(new int[] { -1, 0 }, -1, new int[] { 1, 2 })]
    [InlineData(new int[] { 0, 0 }, 0, new int[] { 1, 2 })]
    [InlineData(new int[] { -5, -3, 0, 2, 4, 6, 8 }, 5, new int[] { 2, 7 })]
    public void Given_WhenTwoSum_Then(int[] nums, int target, int[] expected)
    {
        var result = TwoSum(nums, target);
        result.Should().BeEquivalentTo(expected);
    }

    public int[] TwoSum(int[] numbers, int target)
    {
        /*
         * we know the array is sorted, this is a huge advantage
         * we will iterate from the end until the value is <= the target
         * j = ^^ this index
         * i = 0;
         * then check to see if nums[i] + nums[i] == target
         * if yes: return [i+1, j+1]
         * else if (nums[0] + nums[i] > target)
         * j--; continue;
         * else
         * i++; continue;
         * 
         */
        int upperIndex = numbers.Length - 1;
        //for (int i = numbers.Length - 1; i > -1; i--)
        //{
        //    if ((target >= 0 && numbers[i] <= target) || (target <= 0 && numbers[i] >= target))
        //    {
        //        upperIndex = i;
        //        break;
        //    }
        //}

        int lowerIndex = 0;
        while (lowerIndex < upperIndex)
        {
            int result = numbers[lowerIndex] + numbers[upperIndex];
            if (result == target)
            {
                return [lowerIndex + 1, upperIndex + 1];
            }
            else if (result > target)
            {
                upperIndex--;
            }
            else
            {
                lowerIndex++;
            }
        }

        throw new System.Exception("Nope");
    }
}
