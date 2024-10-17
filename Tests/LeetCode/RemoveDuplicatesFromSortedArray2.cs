namespace Tests.LeetCode;

internal class RemoveDuplicatesFromSortedArray2
{
    //static void Main(string[] args)
    //{
    //    int[] nums1 = [1, 1, 1, 2, 2, 3];
    //    int a1 = 5;
    //    int[] a1nums = [1, 1, 2, 2, 3];
    //    var r1 = RemoveDuplicates(nums1);
    //    r1.Should().Be(a1);

    //    int[] nums2 = [0, 0, 1, 1, 1, 1, 2, 3, 3];
    //    int a2 = 7;
    //    int[] a2nums = [0, 0, 1, 1, 2, 3, 3];
    //    var r2 = RemoveDuplicates(nums2);
    //    r2.Should().Be(a2);

    //    int[] nums3 = [1, 1, 1];
    //    int a3 = 2;
    //    int[] a3nums = [1, 1];
    //    var r3 = RemoveDuplicates(nums3);
    //    r3.Should().Be(a3);
    //}

    // this is the correct solution... not super intuitive
    // will need to memorize it I think
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
                nums[j] = nums[i]; // set the jth didgit equal to the ith digit
                                   // this part still does not make sense to me

                j++; // increment j because the j-2 digit is useful
            }
        }
        return j;
    }

    //public static int RemoveDuplicates(int[] nums)
    //{
    //    int totalDuplicateCount = 0;
    //    int totalIgnoredCount = 0;
    //    for (int i = 0; i < nums.Length - totalIgnoredCount - 1;)
    //    {
    //        int duplicateCount = 0;

    //        for (int j = i + 1; j < nums.Length - totalIgnoredCount; j++)
    //        {
    //            if (j == i + 1 && nums[i] != nums[j])
    //            {
    //                i++;
    //                break;
    //            }
    //            else if (j == nums.Length - totalIgnoredCount - 1)
    //            {
    //                if (nums.Length > 2 && duplicateCount > 0 && totalIgnoredCount == 0)
    //                {
    //                    totalIgnoredCount++;
    //                }
    //                i++;
    //                break;
    //            }
    //            else if (nums[i] == nums[j])
    //            {
    //                duplicateCount++;
    //                continue;
    //            }
    //            else if(duplicateCount > 1)
    //            {
    //                Console.WriteLine(string.Join(", ", nums));
    //                Array.Copy(nums, i + duplicateCount, nums, i + 1, nums.Length - duplicateCount - i);
    //                Console.WriteLine(string.Join(", ", nums));
    //                totalIgnoredCount += duplicateCount - 1;
    //                totalDuplicateCount += duplicateCount;
    //                i += duplicateCount;
    //                break;
    //            }

    //            // duplicate count == 1 here
    //            totalDuplicateCount += duplicateCount;
    //            i += 2;
    //            break;
    //        }
    //    }

    //    int v = nums.Length - totalIgnoredCount;
    //    Console.WriteLine(v);
    //    return v;
    //}
}
