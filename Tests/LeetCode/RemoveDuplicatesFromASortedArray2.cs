using FluentAssertions;
using Xunit;

namespace Tests.LeetCode;

// https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii
public class RemoveDuplicatesFromASortedArray2
{
    [Theory]
    [InlineData(new int[] { 1, 1, 1, 2, 2, 3 }, 5, new int[] { 1, 1, 2, 2, 3 })]
    [InlineData(new int[] { 0, 0, 1, 1, 1, 1, 2, 3, 3 }, 7, new int[] { 0, 0, 1, 1, 2, 3, 3 })]
    [InlineData(new int[] { 1, 1, 1 }, 2, new int[] { 1, 1 })]
    public void Given_WhenRemoveDuplicates_Then(int[] nums, int usefulDigitCount, int[] expected)
    {
        int result = RemoveDuplicates(nums);
        result.Should().Be(usefulDigitCount);
        nums[..usefulDigitCount].Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    // this solution can be thought of as a filter passing over the sorted array
    // 
    public int RemoveDuplicates(int[] nums)
    {
        // we are allowed 2 duplicates in a row
        // so if the length is less than 3
        // just return the length
        if (nums.Length < 3)
        {
            return nums.Length;
        }

        // j represents the number of useful digits
        int j = 2;
        // because we are allowed to have 2 duplicates, we begin the loop at index == 2
        for (int i = 2; i < nums.Length; i++)
        {
            // we compare 2 indexes apart (because 2 duplicates are allowed)
            // if they are unequal, then we have a useful digit
            if (nums[i] != nums[j - 2])
            {
                nums[j] = nums[i]; // set the jth digit equal to the ith digit
                                   // so, set the useful digit (which will in theory be trailing at some point)
                                   // to the current value
                                   // think of it as shifting the array to the left for each correct value found
                                   // to start, no shifting is done
                                   // when 1 error (duplicate) is found, we begin to shift all correct values 1 space down
                                   // 2 errors found? begin shift 2 spaces down
                                   // n errors found? begin shift n spaces down

                j++; // increment j because the j-2 digit is useful
            }
        }
        return j;
    }
}
