using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/best-time-to-buy-and-sell-stock
public class BestTimeToBuyAndSellStock
{
    [Theory]
    [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, 5)]
    [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
    [InlineData(new int[] { 7, 2, 4, 1 }, 2)]
    [InlineData(new int[] { 11, 2, 7, 1, 4 }, 5)]
    public void Given_WhenMaxProfit_Then(int[] nums, int profit)
    {
        int result = MaxProfit(nums);
        result.Should().Be(profit);
    }

    [Theory]
    [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, 5)]
    [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
    [InlineData(new int[] { 7, 2, 4, 1 }, 2)]
    [InlineData(new int[] { 11, 2, 7, 1, 4 }, 5)]
    public void Given_WhenMaxProfitWayBetter_Then(int[] nums, int profit)
    {
        int result = MaxProfitWayBetter(nums);
        result.Should().Be(profit);
    }

    public static int MaxProfit(int[] prices)
    {
        // 1. find the largest value
        // 2. find the largest (postive) difference between a future day and a past day

        // fairly sure that brute force is the only way
        int maxPrice = prices.Max();
        int minPrice = prices.Min();

        int lowestPriceIndex = Array.IndexOf(prices, minPrice);
        int highestPriceIndex = Array.IndexOf(prices, maxPrice);

        if (lowestPriceIndex < highestPriceIndex)
        {
            return maxPrice - minPrice;
        }

        // slice from lowest index to end
        var sliceLowToEnd = prices[lowestPriceIndex..^0];
        var maxValueOfLowSlice = sliceLowToEnd.Length > 0 ? sliceLowToEnd.Max() : minPrice;
        var possibleMax1 = maxValueOfLowSlice - minPrice;

        // or from 0 to highest index
        var slice0ToHigh = prices[0..highestPriceIndex];
        var minValueOfHighSlice = slice0ToHigh.Length > 0 ? slice0ToHigh.Min() : maxPrice;
        var possibleMax2 = maxPrice - minValueOfHighSlice;

        int possibleMax3 = 0;

        for (int i = highestPriceIndex + 1; i < lowestPriceIndex; i++)
        {
            for (int j = i + 1; j < lowestPriceIndex; j++)
            {
                int difference = prices[j] - prices[i];
                if (difference > possibleMax3)
                {
                    possibleMax3 = difference;
                }
            }
        }

        int[] maxes = [possibleMax1, possibleMax2, possibleMax3];
        return maxes.Max();
    }

    // way better solution
    public static int MaxProfitWayBetter(int[] prices)
    {
        int min = int.MaxValue;
        int maxProfit = 0;

        foreach (int price in prices)
        {
            if (price < min)
            {
                min = price;
            }

            int possibleProfit = price - min;
            if (maxProfit < possibleProfit)
            {
                maxProfit = possibleProfit;
            }
        }

        return maxProfit;
    }
}
