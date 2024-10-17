using System;

namespace Tests.LeetCode;

public class RemoveDuplicatesClass
{

    // https://leetcode.com/problems/remove-duplicates-from-sorted-array/

    //int[] arr = [1, 1, 2];

    //var res = RemoveDuplicates(arr);

    //Console.WriteLine();

    //    int[] arr2 = [0, 0, 1, 1, 1, 2, 2, 3, 3, 4];

    //var res2 = RemoveDuplicates(arr2);

    //Console.WriteLine();

    //    int[] arr3 = [1, 1, 2, 2];

    //var res3 = RemoveDuplicates(arr3);

    //Console.WriteLine();

    //    int[] arr4 = [1, 1, 1, 1];

    //var res4 = RemoveDuplicates(arr4);

    //Console.WriteLine();

    //    int[] arr5 = [-1, 0, 0, 0, 0, 3, 3];

    //var res5 = RemoveDuplicates(arr5);
    public int RemoveDuplicates(int[] nums)
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
