using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/search-in-rotated-sorted-array/

public class SearchInRotatedSortedArray
{
    [Theory]
    [InlineData(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0, 4)]
    [InlineData(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 3, -1)]
    [InlineData(new int[] { 1 }, 0, -1)]
    [InlineData(new int[] { 5, 1, 3 }, 5, 0)]
    [InlineData(new int[] { 4, 5, 6, 7, 8, 1, 2, 3 }, 8, 4)]
    [InlineData(new int[] { 5, 1, 2, 3, 4 }, 1, 1)]
    public void Given_WhenSearch_Then(int[] nums, int target, int expected)
    {
        int result = Search(nums, target);
        result.Should().Be(expected);
    }

    private static int Search(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[left] <= nums[mid])
            {
                if (nums[left] <= target && nums[mid] > target)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                // right is rotated sorted
                if (target > nums[mid] && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
        /*
the key in the Serarch rotated sorted:
only act when you need to

2 cases:
is the LHS ascending, or does it have a rotation?
if LHS is ascending, RHS is rotated

within the ascending vs rotated
if the target is in the LHS/RHS, move the pointer as necessary
         */
    }
    private static int SearchOOfN(int[] nums, int target)
    {
        if (target == nums[^1])
        {
            return nums.Length - 1;
        }
        else if (target > nums[^1])
        {
            // iterate from start
            int i = 0;
            while (nums[^1] < nums[i])
            {
                if (nums[i] == target)
                {
                    return i;
                }
                i++;
            }
        }
        else
        {
            // iterate from end
            int i = nums.Length - 2;
            while (i >= 0 && nums[^1] > nums[i])
            {
                if (nums[i] == target)
                {
                    return i;
                }
                i--;
            }
        }

        return -1;
    }
}
