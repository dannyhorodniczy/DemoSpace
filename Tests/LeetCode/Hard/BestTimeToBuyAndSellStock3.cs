using FluentAssertions;
using System;
using Xunit;

namespace Tests.LeetCode.Hard;

// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/description/

public class BestTimeToBuyAndSellStock3
{
    /*
 * ok, here we are again
 * same problem as before, but in this case we are able to make 2 transactions
 * 
 * uhhhh I think:
 * 1. find the largest transaction working backwards
 * 
 * edge case: numbers always decreasing numbers
 * 
 * so I believe you want to find the highest possible profit
 * remove that set, search the other 2 sets to see if a larger profit is possoble?
 * I have relative faith that this would work
 * let's see
 * 
 * 1. save the last number
 * 2. compare it to the 2nd last number
 * 3. if 2nd last is lower, compute the difference, save the 2 indices
 * 4. if it's higher, save the index
 * 5. repeat until largest difference between indices is found 
 * 6. the perform the same algorithm on the 2 smaller sets
 */

    [Theory]
    [InlineData(new int[] { 3, 3, 5, 0, 0, 3, 1, 4 }, 6)]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
    [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
    [InlineData(new int[] { 2, 1, 4 }, 3)]
    public void Given_WhenMaxProfit_Then(int[] nums, int profit)
    {
        int result = MaxProfit(nums);
        result.Should().Be(profit);
    }

    public int MaxProfit(int[] prices)
    {
        (int lowIndex, int highIndex, int largestPrice) = GetPriceAndBounds(prices);
        (_, _, int possiblelargestPrice1) = GetPriceAndBounds(prices[0..(lowIndex + 1)]);
        (_, _, int possiblelargestPrice2) = GetPriceAndBounds(prices[highIndex..]);

        return Math.Max(largestPrice + possiblelargestPrice1, largestPrice + possiblelargestPrice2);

    }

    private static (int lowIndex, int highIndex, int largestPrice) GetPriceAndBounds(int[] prices)
    {
        int largestPrice = 0;
        int lowIndex = 0;
        int highIndex = prices.Length - 1;
        for (int i = prices.Length - 2; i > -1; i--)
        {
            int difference = prices[highIndex] - prices[i];
            if (difference > largestPrice)
            {
                lowIndex = i;
                largestPrice = difference;
            }
            else
            {
                highIndex = i;
            }
        }

        return (lowIndex, highIndex, largestPrice);
    }
}
