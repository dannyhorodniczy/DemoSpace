using FluentAssertions;
using System;
using Xunit;

namespace Tests.LeetCode;

// https://leetcode.com/problems/remove-duplicates-from-sorted-array/
public class RemoveDuplicatesFromASortedArray
{
    [Theory]
    [InlineData(new int[] { 0, 0, 1, 1, 1, 1, 2, 3, 3 }, 4, new int[] { 0, 1, 2, 3 })]
    [InlineData(new int[] { 1, 1, 1, 2, 2, 3 }, 3, new int[] { 1, 2, 3 })]
    [InlineData(new int[] { 1, 1, 2 }, 2, new int[] { 1, 2 })]
    [InlineData(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5, new int[] { 0, 1, 2, 3, 4 })]
    [InlineData(new int[] { 1, 1, 2, 2 }, 2, new int[] { 1, 2 })]
    [InlineData(new int[] { 1, 1, 1, 1 }, 1, new int[] { 1 })]
    [InlineData(new int[] { -1, 0, 0, 0, 0, 3, 3 }, 3, new int[] { -1, 0, 3 })]
    public void Given_WhenRemoveDuplicates_Then(int[] nums, int usefulDigitCount, int[] expected)
    {
        int result = RemoveDuplicates(nums);
        result.Should().Be(usefulDigitCount);
        nums[..usefulDigitCount].Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    [Theory]
    [InlineData(new int[] { 0, 0, 1, 1, 1, 1, 2, 3, 3 }, 4, new int[] { 0, 1, 2, 3 })]
    [InlineData(new int[] { 1, 1, 1, 2, 2, 3 }, 3, new int[] { 1, 2, 3 })]
    [InlineData(new int[] { 1, 1, 2 }, 2, new int[] { 1, 2 })]
    [InlineData(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5, new int[] { 0, 1, 2, 3, 4 })]
    [InlineData(new int[] { 1, 1, 2, 2 }, 2, new int[] { 1, 2 })]
    [InlineData(new int[] { 1, 1, 1, 1 }, 1, new int[] { 1 })]
    [InlineData(new int[] { -1, 0, 0, 0, 0, 3, 3 }, 3, new int[] { -1, 0, 3 })]
    public void Given_WhenRemoveDuplicatesLessGood_Then(int[] nums, int usefulDigitCount, int[] expected)
    {
        int result = RemoveDuplicatesLessGood(nums);
        result.Should().Be(usefulDigitCount);
        nums[..usefulDigitCount].Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static int RemoveDuplicates(int[] nums)
    {
        if (nums.Length < 2)
        {
            return 1;
        }

        int usefulDigitCount = 1;
        for (int j = 1; j < nums.Length; j++)
        {
            if (nums[j] != nums[usefulDigitCount - 1])
            {
                nums[usefulDigitCount] = nums[j];
                usefulDigitCount++;
            }
        }
        return usefulDigitCount;
    }

    public int RemoveDuplicatesLessGood(int[] nums)
    {
        int terminationCount = nums.Length;

        for (int i = 0; i < terminationCount - 1; i++)
        {
            int duplicateCount = 0;
            for (int j = i + 1; j < terminationCount; j++)
            {
                if (nums[i] == nums[j])
                {
                    duplicateCount++;
                    continue;
                }
                else
                {
                    if (duplicateCount == 0)
                    {
                        break;
                    }
                    else
                    {
                        var temp = nums[(j - 1)..];
                        Array.Copy(temp, 0, nums, i, temp.Length);
                        break;
                    }
                }
            }
            terminationCount -= duplicateCount;
        }

        return terminationCount;
    }
}
