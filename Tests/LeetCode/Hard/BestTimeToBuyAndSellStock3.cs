using FluentAssertions;
using System;
using Xunit;

namespace Tests.LeetCode.Hard;

// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/

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
 * 
 * This algorithm is fundamentally flawed
 * It assumes that the largest possible difference is *always* part of the solution
 * which it is not... so... time to figure it out.
 */

    /*
     * Strategy #2
     * 1. Brute force: Compute all possible differences (keep their indices, starting with the largest diff)
     * 
     * let's think about it:
     * there are some possible solutions
     * 1. 1 transaction (largest)
     * 2. 2 transactions (largest + not largest)
     * 3. 2 transactions (both < largest)
     * 
     * we are guaranteed to have 1 period, or 2 entirely separate periods
     * so I guess
     * 1. Compute the largest difference & period
     * 2. Compute the next largest difference & period
     * if no overlap, return
     * if overlap:
     * 3. Compute the next largest difference & period
     * 
     * The answer is dynamic programming, and it's related to optimization
     */

    [Theory]
    //[InlineData(new int[] { 3, 3, 5, 0, 0, 3, 1, 4 }, 6)]
    //[InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
    //[InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
    //[InlineData(new int[] { 2, 1, 4 }, 3)]
    [InlineData(new int[] { 1, 2, 4, 2, 5, 7, 2, 4, 9, 0 }, 13)]
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
        int possibleHighIndex = prices.Length - 1;
        for (int i = prices.Length - 2; i > -1; i--)
        {
            int difference = prices[possibleHighIndex] - prices[i];
            if (difference > largestPrice)
            {
                lowIndex = i;
                highIndex = possibleHighIndex;
                largestPrice = difference;
            }
            else
            {
                possibleHighIndex = i;
            }
        }

        return (lowIndex, highIndex, largestPrice);
    }
}
