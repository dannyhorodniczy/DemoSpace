using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/next-greater-element-i/

public class NextGreaterElement1
{
    [Theory]
    [InlineData(new int[] { 4, 1, 2 }, new int[] { 1, 3, 4, 2 }, new int[] { -1, 3, -1 })]
    [InlineData(new int[] { 2, 4 }, new int[] { 1, 2, 3, 4 }, new int[] { 3, -1 })]
    [InlineData(new int[] { 3, 1, 5, 7, 9, 2, 6 }, new int[] { 1, 2, 3, 5, 6, 7, 9, 11 }, new int[] { 5, 2, 6, 9, 11, 3, 7 })]
    public void Given_WhenNextGreaterElement_Then(int[] nums1, int[] nums2, int[] expected)
    {
        // Given
        // When
        var result = NextGreaterElement(nums1, nums2);

        // Then
        result.Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static int[] NextGreaterElement(int[] nums1, int[] nums2)
    {
        /*
         * we will use a monotonically decreasing stack in this case (because of next greater)
         * 
         * find the index j such that nums1[i] == nums2[j] 
         * and determine the next greater element of nums2[j] in nums2
         * If there is no next greater element,
         * then the answer for this query is -1.
         * 
         */

        int[] indexOfNextGreater = new int[nums2.Length];
        Array.Fill(indexOfNextGreater, -1);

        var stack = new Stack<int>();

        for (int j = 0; j < nums2.Length; j++)
        {
            // next greater number
            // while the stack is populated
            // and the element value is greater than the stack value
            while (stack.Count > 0 && nums2[j] > nums2[stack.Peek()])
            {
                // pop the index off the stack
                // use it as the index of the nextGreater array
                // save the other index at that space
                indexOfNextGreater[stack.Pop()] = j;
            }

            stack.Push(j);
        }

        var ans = new int[nums1.Length];

        // try counting down
        for (int i = 0; i < ans.Length; i++)
        {
            var ind = Array.IndexOf(nums2, nums1[i]);

            if (indexOfNextGreater[ind] == -1) ans[i] = -1;
            else ans[i] = nums2[indexOfNextGreater[ind]];
        }

        return ans;
    }
}
