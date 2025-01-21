using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/merge-sorted-array/

public class MergeSortedArray
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 0, 0, 0 }, 3, new int[] { 2, 5, 6 }, 3, new int[] { 1, 2, 2, 3, 5, 6 })]
    [InlineData(new int[] { 2, 0 }, 1, new int[] { 1 }, 1, new int[] { 1, 2 })]
    [InlineData(new int[] { 1 }, 1, new int[] { }, 0, new int[] { 1 })]
    [InlineData(new int[] { 0 }, 0, new int[] { 1 }, 1, new int[] { 1 })]
    public void Given_WhenMerge1_Then(int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        Merge1(nums1, m, nums2, n);
        nums1.Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static void Merge1(int[] nums1, int m, int[] nums2, int n)
    {
        if (m == 0)
        {
            Array.Copy(nums2, nums1, n);
            return;
        }

        if (n == 0)
        {
            return;
        }

        int i = 0, j = 0, count = 0;
        while (i < nums1.Length)
        {
            if (nums1[i] > nums2[j])
            {
                // shift the array
                Array.Copy(nums1, i, nums1, i + 1, nums1.Length - i - 1);
                //Console.WriteLine($"shifted array: {string.Join(", ", nums1)}");
                // set the sorted value
                nums1[i] = nums2[j];
                //Console.WriteLine($"new value set: {string.Join(", ", nums1)}");
                if (j == n - 1)
                {
                    break;
                }
                j++;
                i++;
            }
            else
            {
                if (count < m)
                {
                    count++;
                    i++;
                }
                else
                {
                    Array.Copy(nums2, j, nums1, i, n - j);
                    //Console.WriteLine($"shifted array: {string.Join(", ", nums1)}");
                    break;
                }
            }
        }
    }


    [Theory]
    [InlineData(new int[] { 1, 2, 3, 0, 0, 0 }, 3, new int[] { 2, 5, 6 }, 3, new int[] { 1, 2, 2, 3, 5, 6 })]
    [InlineData(new int[] { 2, 0 }, 1, new int[] { 1 }, 1, new int[] { 1, 2 })]
    [InlineData(new int[] { 1 }, 1, new int[] { }, 0, new int[] { 1 })]
    [InlineData(new int[] { 0 }, 0, new int[] { 1 }, 1, new int[] { 1 })]
    public void Given_WhenMerge2_Then(int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        Merge2(nums1, m, nums2, n);
        nums1.Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static void Merge2(int[] nums1, int m, int[] nums2, int n)
    {
        if (n == 0)
        {
            return;
        }

        if (m == 0)
        {
            Array.Copy(nums2, nums1, m + n);
            return;
        }

        int i = 0; // nums1 iterator
        int j = 0; // nums2 iterator
        int k = 0; // keeps count of when nums1 is in the correct order
                   // used to short circuit the algo when we can just tack on nums2 to the end
        while (i < (m + n) && j < n)
        {
            int arr1 = nums1[i];
            int arr2 = nums2[j];
            if (arr1 <= arr2)
            {
                if (k >= m)
                {
                    // can just add the rest of nums2 to nums1
                    var temp = nums1[0..i]
                    .Concat(nums2[j..])
                    .ToArray();

                    Array.Copy(temp, nums1, m + n);

                    return;
                }

                // nums1 is in correct order
                k++;
                i++;
            }
            else
            {
                // nums1 is not in correct order
                // pick up+shift nums1 from i..L-1
                var temp = nums1[0..i]
                    .Concat([nums2[j]])
                    .Concat(nums1[i..^1])
                    .ToArray();

                Array.Copy(temp, nums1, m + n);

                i++;
                j++;
            }
        }
    }
}
