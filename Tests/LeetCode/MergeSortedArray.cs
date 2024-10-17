using System;

namespace Tests.LeetCode;

// https://leetcode.com/problems/merge-sorted-array/
public class MergeSortedArray
{


    //int[] nums1 = [1, 2, 3, 0, 0, 0];
    //int m = 3;
    //int[] nums2 = [2, 5, 6];
    //int n  = 3;
    //Merge(nums1, m, nums2, n);

    //Console.WriteLine(string.Join(", ", nums1));


    //int[] nums1 = [2, 0];
    //int m = 1;
    //int[] nums2 = [1];
    //int n  = 1;
    //Merge(nums1, m, nums2, n);

    //Console.WriteLine(string.Join(", ", nums1));

    public static void Merge(int[] nums1, int m, int[] nums2, int n)
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
}
