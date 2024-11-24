using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/

public class FindMinimumInRotatedSortedArray
{
    [Theory]
    [InlineData(new int[] { 3, 4, 5, 1, 2 }, 1)]
    [InlineData(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0)]
    [InlineData(new int[] { 11, 13, 15, 17 }, 11)]
    public void Given_WhenFindMin_Then(int[] nums, int expected)
    {
        int result = FindMin(nums);
        result.Should().Be(expected);
    }

    private static int FindMin(int[] nums)
    {

        if (nums.Length == 0)
        {
            return 0;
        }

        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] <= nums[right])
            {
                // array is in sorted ascending state
                // therefore we need to move the right pointer downstream
                // because we are searchig for the minimum
                right = mid;
            }
            else
            {
                // the array is in a rotated state
                // therefore move the left pointer, because we are searching for the minimum
                // which is objectively somewhere upstream
                left = mid + 1;
            }
        }

        return nums[left];
    }

    private static int FindMinOOfN(int[] nums)
    {
        if (nums.Length == 0)
        {
            return 0;
        }


        int i = 0;
        while (nums[^1] < nums[i])
        {
            i++;
        }

        return nums[i];
    }

    /*
     * finding the minimum in an ascending sorted array?
     * well that's easy...
     * but the trick here is that the array is rotated
     * and we don't know by how much
     * 
     * compare the middle with both ends?
     * 
     * nope: create a monotonic relation (just like 0's and 1's flipping question)
     * the last element of the array can be compared with each element
     * if lastElement <= current
     * use the index of the 1st true element on the nums array
     */
}
