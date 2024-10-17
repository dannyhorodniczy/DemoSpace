using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode;

// https://leetcode.com/problems/majority-element
public class MajorityElementClass
{
    [Theory]
    [InlineData(new int[] { 3, 2, 3 }, 3)]
    [InlineData(new int[] { 2, 2, 1, 1, 1, 2, 2 }, 2)]
    public void Given_WhenMajorityElement_Then(int[] nums, int profit)
    {
        int result = MajorityElement(nums);
        result.Should().Be(profit);
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 3 }, 3)]
    [InlineData(new int[] { 2, 2, 1, 1, 1, 2, 2 }, 2)]
    public void Given_WhenMajorityElement_TimeComplexity_Oof1_Then(int[] nums, int profit)
    {
        int result = MajorityElement(nums);
        result.Should().Be(profit);
    }

    public int MajorityElement(int[] nums)
    {
        var dic = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            if (dic.ContainsKey(num))
            {
                dic[num]++;
            }
            else
            {
                dic[num] = 1;
            }
        }

        int majority = 0;
        int count = 0;
        foreach (var item in dic)
        {
            if (item.Value > count)
            {
                count = item.Value;
                majority = item.Key;
            }
        }
        return majority;
    }

    public static int MajorityElement_TimeComplexity_Oof1(int[] nums)
    {
        int result = 0;
        int majority = 0;

        foreach (int i in nums)
        {
            if (majority == 0)
            {
                result = i;
            }

            if (i == result)
            {
                majority++;
            }
            else
            {
                majority--;
            }
        }

        return result;
    }
}
