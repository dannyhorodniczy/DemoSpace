using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;
public class FinalPricesWithASpecialDiscountInAShop
{
    [Theory]
    [InlineData(new int[] { 8, 4, 6, 2, 3 }, new int[] { 4, 2, 4, 2, 3 })]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 10, 1, 1, 6 }, new int[] { 9, 0, 1, 6 })]
    public void Given_WhenFinalPrices_Then(int[] prices, int[] expected)
    {
        // Given
        // When
        var result = FinalPrices(prices);

        // Then
        result.Should().BeEquivalentTo(expected, o => o.WithStrictOrdering());
    }

    private static int[] FinalPrices(int[] prices)
    {
        /*
         * we know that we want to use a monotonic stack here
         * because we are searching for the next smallest element
         * 
         * because it's next smallest, we know that we want a monotonically
         * increasing stack
         * 
         * so we will precompute the given discount per index
         * 
         * then loop again to subtract the discount from the original price
         * to get the final answer
         */

        int[] discounts = new int[prices.Length];
        Array.Fill(discounts, -1);

        var stack = new Stack<int>();

        for (int i = 0; i < prices.Length; i++)
        {
            while (stack.Count > 0 && prices[stack.Peek()] >= prices[i])
            {
                discounts[stack.Pop()] = i;
            }

            stack.Push(i);
        }

        int[] ans = new int[prices.Length];

        for (int i = 0; i < prices.Length; i++)
        {
            ans[i] = discounts[i] == -1 ?
                prices[i] :
                prices[i] - prices[discounts[i]];
        }

        return ans;
    }
}
