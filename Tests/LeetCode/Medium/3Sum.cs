using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/3sum/description/

public class _3Sum
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { 2, 3, 1 })]
    public void TestListComparisons(int[] nums1, int[] nums2)
    {
        nums1.Should().BeEquivalentTo(nums2);
        nums1.Except(nums2).Should().BeEmpty();
        nums2.Except(nums1).Should().BeEmpty();
    }

    [Theory]
    [InlineData(new int[] { -1, 0, 1, 2, -1, -4 })]
    public void Given_WhenThreeSum_Then1(int[] nums)
    {
        var result = ThreeSum(nums);

        IList<IList<int>> expected = [[-1, -1, 2], [-1, 0, 1]];
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(new int[] { 0, 1, 1 })]
    public void Given_WhenThreeSum_Then2(int[] nums)
    {
        var result = ThreeSum(nums);

        IList<IList<int>> expected = [];
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(new int[] { 0, 0, 0 })]
    public void Given_WhenThreeSum_Then3(int[] nums)
    {
        var result = ThreeSum(nums);

        IList<IList<int>> expected = [[0, 0, 0]];
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(new int[] { 3, 0, -2, -1, 1, 2 })]
    public void Given_WhenThreeSum_Then4(int[] nums)
    {
        var result = ThreeSum(nums);

        IList<IList<int>> expected = [[-2, -1, 3], [-2, 0, 2], [-1, 0, 1]];
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(new int[] { -4, -2, 1, -5, -4, -4, 4, -2, 0, 4, 0, -2, 3, 1, -5, 0 })]
    public void Given_WhenThreeSum_Then5(int[] nums)
    {
        var result = ThreeSum(nums);

        IList<IList<int>> expected = [[-5, 1, 4], [-4, 0, 4], [-4, 1, 3], [-2, -2, 4], [-2, 1, 1], [0, 0, 0]];
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(new int[] { 2, 0, -2, -5, -5, -3, 2, -4 })]
    public void Given_WhenThreeSum_Then6(int[] nums)
    {
        var result = ThreeSum(nums);

        IList<IList<int>> expected = [[-4, 2, 2], [-2, 0, 2]];
        result.Should().BeEquivalentTo(expected);
    }

    // pretty sure it works
    // but is slow
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var ordered = nums.Order().ToArray();

        var list = new List<IList<int>>();

        for (int i = 0; i < ordered.Length - 2; i++)
        {
            // Skip duplicate elements for i
            if (i > 0 && ordered[i] == ordered[i - 1])
            {
                continue;
            }

            int lowerIndex = i + 1;
            int upperIndex = ordered.Length - 1;

            while (lowerIndex < upperIndex)
            {
                var oi = ordered[i];
                var ol = ordered[lowerIndex];
                var ou = ordered[upperIndex];
                int result = oi + ol + ou;

                if (result == 0)
                {

                    int[] item = [oi, ol, ou];

                    bool isNewItem = true;
                    foreach (var item2 in list)
                    {
                        isNewItem = item.Except(item2).Any() || item2.Except(item).Any();
                        if (!isNewItem)
                        {
                            break;
                        }
                    }

                    if (isNewItem)
                    {
                        list.Add(item);
                    }

                    upperIndex--;
                    lowerIndex++;
                }
                else if (result > 0)
                {
                    upperIndex--;
                }
                else
                {
                    lowerIndex++;
                }
            }

        }

        return list;
    }
}
