using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Hard;

//static void Main(string[] args)
//{
//    Test([10, 13, 12, 14, 15], 2);
//    Test([2, 3, 1, 1, 4], 3);
//    Test([5, 1, 3, 4, 2], 3);
//}

//private static int OddEvenJumps(int[] arr)
//{
//    /*
//     * 
//     * 
//     */
//}

//private static void Test(int[] arr, int expected)
//{
//    // Given
//    // When
//    var result = OddEvenJumps(arr);

//    // Then
//    result.Should().Be(expected);
//}

public class OddEvenJump
{
    [Theory]
    [InlineData(new int[] { 10, 13, 12, 14, 15 }, 2)]
    [InlineData(new int[] { 2, 3, 1, 1, 4 }, 3)]
    [InlineData(new int[] { 5, 1, 3, 4, 2 }, 3)]
    [InlineData(new int[] { 1, 2, 3, 2, 1, 4, 4, 5 }, 6)]
    public void Given_WhenThreeSum_Then1(int[] arr, int expected)
    {
        // Given
        // When
        var result = OddEvenJumpsBruteForce(arr);
        var result2 = OddEvenJumpsMonotonicStack(arr);

        // Then
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }

    private static int OddEvenJumpsMonotonicStack(int[] arr)
    {
        /*
         * now that we know what a monotonic stack is (one with in/decreasing order)
         * we can leverage it for this problem
         * the orignal algorithm tried was simply brute force
         * where we look at all forward elements, compute the minimum, and find the Index
         * this gives a time complexity of O(n^2) (maybe more? would need to analyze)
         * 
         * montonic stacks solve the problem of finding the next greater or or next lesser number
         * in a sequence
         * we need to iterate backwards from the end of given array to do this
         * 
         * we use a monotonic increasing stack to find the next smaller element
         * we use a monotonic decreasing stack to find the next greater element
         * 
         *  
         * we will construct 2 arrays: evenJumps, oddJumps
         * each index will either contain: -1 (invalid), or a positive integer (valid)
         * length = arr.Length - 1 --> because if you are at: arr.Length - 1, you have made it
         * 
         * then we will backwards iterate through the arr
         * 
         */

        int[] oddJumps = new int[arr.Length];
        int[] evenJumps = new int[arr.Length];

        // fill the arrays as invalids
        Array.Fill(oddJumps, -1);
        Array.Fill(evenJumps, -1);

        var stack = new Stack<int>();

        // order the elements increasing and return their indices
        var indexesInIncreasingValueOrder = arr
            .Select((value, index) => (value, index))
            .OrderBy(x => x.value)
            .ThenBy(x => x.index)
            .Select(x => x.index);

        foreach (var i in indexesInIncreasingValueOrder)
        {
            while (stack.Count > 0 && i > stack.Peek())
            {
                oddJumps[stack.Pop()] = i;
            }
            stack.Push(i);
        }

        // odd jumps is set
        // now let's do the evens
        stack.Clear();
        var indexesInDecreasingValueOrder = arr
            .Select((value, index) => (value, index))
            .OrderByDescending(x => x.value)
            .ThenBy(x => x.index)
            .Select(x => x.index);

        foreach (var i in indexesInDecreasingValueOrder)
        {
            while (stack.Count > 0 && i > stack.Peek())
            {
                var p = stack.Peek();
                evenJumps[stack.Pop()] = i;
            }
            stack.Push(i);
        }

        // ok so the 2 stacks should be populated
        // not sure I see an advantage to backwards iteration at this point
        // let's try forward iteration and see what happens
        // in theory... all answers should be computed :think:
        int counter = 1;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            bool isOdd = true;
            int currentJumpPoint = i;
            while (currentJumpPoint < (arr.Length - 1))
            {
                if (isOdd)
                {
                    if (currentJumpPoint == -1) break;

                    currentJumpPoint = oddJumps[currentJumpPoint];
                }
                else
                {
                    if (currentJumpPoint == -1) break;

                    currentJumpPoint = evenJumps[currentJumpPoint];
                }

                isOdd = !isOdd;
            }

            if (currentJumpPoint == (arr.Length - 1)) counter++;
        }

        return counter;
    }

    private static int OddEvenJumpsBruteForce(int[] arr)
    {
        int capacity = arr.Length - 1;
        var dic = new Dictionary<int, (int? OddLower, int? EvenUpper)>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            dic[i] = (null, null);

            // preprocessing is how we solve this one
            // don't solve it in your iteration loop

            // OddJump: Find the smallest number greater than or equal to current number
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

            // EvenJump: Find the greatest number less than or equal to the current number
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
