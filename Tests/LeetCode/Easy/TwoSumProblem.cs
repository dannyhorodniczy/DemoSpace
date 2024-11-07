using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;
public class TwoSumProblem
{
    [Theory]
    [InlineData(new int[] { 2, 7, 11, 15 }, 9, new int[] { 0, 1 })]
    [InlineData(new int[] { 3, 2, 4 }, 6, new int[] { 1, 2 })]
    [InlineData(new int[] { 3, 3 }, 6, new int[] { 0, 1 })]
    public void Given_WhenTwoSum_Then(int[] nums, int target, int[] expected)
    {
        var result = TwoSum(nums, target);
        result.Should().BeEquivalentTo(expected, c => c.WithoutStrictOrdering());
    }

    private int[] TwoSum(int[] nums, int target)
    {
        /*
         * new alg:
         * 1. compute the difference between the current index and the target
         * -- save the difference as the key and the index as the value in a dictionary
         * 2. compute the difference between the next index and the target
         * - check to see if that difference exists in the dictionary
         * - if yes, return the dictionary value and the current index
         */

        var dic = new Dictionary<int, int>() { { target - nums[0], 0 } };

        for (int i = 1; i < nums.Length; i++)
        {
            if (dic.ContainsKey(nums[i]))
            {
                return [dic[nums[i]], i];
            }
            dic[target - nums[i]] = i;
        }

        return [0, 1];
        // 1st thought: brute force
        // double for loop, terminate on the correct sum

        /*
                for (int i=0; i < nums.Length - 1; i++)
                {
                    for (int j=i+1; j < nums.Length; j++)
                    {
                        if (nums[i] + nums[j] == target)
                        {
                            return [i, j];
                        }
                    }
                }

                return [0, 1];
        */


        // 2nd thought: order the array
        // find the largest number < target
        // iterate from the bottom of the list
        // if that doesn't work, go 1 index down
        //
        // can't do that... we need to return the indices from the original array
    }
}
