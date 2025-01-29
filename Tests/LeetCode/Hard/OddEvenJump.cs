using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Hard;
public class OddEvenJump
{
    [Theory]
    [InlineData(new int[] { 10, 13, 12, 14, 15 }, 2)]
    [InlineData(new int[] { 2, 3, 1, 1, 4 }, 3)]
    [InlineData(new int[] { 5, 1, 3, 4, 2 }, 3)]
    public void Given_WhenThreeSum_Then1(int[] arr, int expected)
    {
        // Given
        // When
        var result = OddEvenJumps(arr);

        // Then
        result.Should().Be(expected);
    }

    private static int OddEvenJumps(int[] arr)
    {
        int capacity = arr.Length - 1;
        var dic = new Dictionary<int, (int? OddLower, int? EvenUpper)>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            dic[i] = (null, null);

            // preprocessing is how we solve this one
            // don't solve it in your iteration loop

            // Find the smallest number greater than or equal to current number
            var range = arr[(i + 1)..];
            var validJumpsOdd = range.Where(x => x >= arr[i]);
            if (!validJumpsOdd.Any())
            {
                dic[i] = (null, dic[i].EvenUpper);
            }
            else
            {
                int v = Array.IndexOf(range, validJumpsOdd.Min());
                var indexOfSmallest = v + (arr.Length - range.Length);
                dic[i] = (indexOfSmallest, dic[i].EvenUpper);
            }

            // Find the greatest number less than or equal to the current number
            var validJumpsEven = range.Where(x => x <= arr[i]);
            if (!validJumpsEven.Any())
            {
                dic[i] = (dic[i].OddLower, null);
            }
            else
            {
                int v = Array.IndexOf(range, validJumpsEven.Max());
                var indexOfLargest = v + (arr.Length - range.Length);
                dic[i] = (dic[i].OddLower, indexOfLargest);
            }
        }

        int count = 1;
        for (int i = arr.Length - 2; i >= 0; i--)
        {
            int jumpStartingPoint = i;
            bool isOddJump = true;

            while (jumpStartingPoint < capacity)
            {
                if (isOddJump)
                {
                    if (!dic[jumpStartingPoint].OddLower.HasValue) break;

                    jumpStartingPoint = dic[jumpStartingPoint].OddLower.Value;
                }
                else
                {
                    if (!dic[jumpStartingPoint].EvenUpper.HasValue) break;

                    jumpStartingPoint = dic[jumpStartingPoint].EvenUpper.Value;
                }

                isOddJump = !isOddJump;
            }

            if (jumpStartingPoint == capacity) count++;
        }

        return count;
    }
}
