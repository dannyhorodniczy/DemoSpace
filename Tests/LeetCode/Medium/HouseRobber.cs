using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/house-robber/

/*
 * ok, so we need to take any array
 * and find the largest possible sum of non adjacent indicies
 * 
 * 2 base cases: odds or evens
 * you simply need to return the max possible profit, no need to explain the indexes
 * 
 * we need to iterate while building the solution
 * 
 * 
 */

public class HouseRobber
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, 4)]
    [InlineData(new int[] { 2, 7, 9, 3, 1 }, 12)]
    [InlineData(new int[] { 10, 0, 0, 10, 1 }, 20)]
    [InlineData(new int[] { 20, 15, 0, 10, 1 }, 30)]
    [InlineData(new int[] { 1, 3, 1 }, 3)]
    [InlineData(new int[] { 1, 3, 1, 3, 100 }, 103)]
    [InlineData(new int[] { 2, 4, 8, 9, 9, 3 }, 19)]
    public void Given_WhenRob_Then(int[] nums, int profit)
    {
        int result = Rob(nums);
        result.Should().Be(profit);
    }

    private int Rob(int[] nums)
    {
        // Recursion
        // return RobRecurBase(nums, 0);

        // Recusion with memoization
        //var dic = new Dictionary<int, int>();
        //return RobRecurWithMemoization(nums, 0, dic);

        // iteration with memoization:
        //return RobIteratively(nums);

        // iteration with only n variables
        return RobIterativelyWithFewestVars(nums);
    }

    private static int RobIterativelyWithFewestVars(int[] nums)
    {
        // why length + 1?
        // I think it's specific to this problem (and the looping that we're doing)

        // why save the 1st value?
        // the memo starts with zero, then is initialized with the 1st value from nums
        int prev2 = 0;
        int prev1 = nums[0];
        int current = 0;
        for (int i = 2; i < (nums.Length + 1); i++)
        {
            // next memo = max(current_memo, previous_memo + current_val
            // so: the next value is the max of:
            // 1. the current memo
            // OR
            // 2. the previous memo + current value
            //memo[i + 1] = Math.Max(memo[i], memo[i - 1] + nums[i]);
            current = Math.Max(prev1, prev2 + nums[i - 1]);
            prev2 = prev1;
            prev1 = current;
        }

        return current;
    }

    private static int RobIteratively(int[] nums)
    {
        // why length + 1?
        // I think it's specific to this problem (and the looping that we're doing)
        int[] memo = new int[nums.Length + 1];
        // why save the 1st value?
        // the memo starts with zero, then is initialized with the 1st value from nums
        memo[1] = nums[0];

        //for (int i = 1; i < nums.Length; i++)
        for (int i = 2; i < memo.Length; i++)
        {
            // next memo = max(current_memo, previous_memo + current_val
            // so: the next value is the max of:
            // 1. the current memo
            // OR
            // 2. the previous memo + current value
            //memo[i + 1] = Math.Max(memo[i], memo[i - 1] + nums[i]);
            memo[i] = Math.Max(memo[i - 1], memo[i - 2] + nums[i - 1]);
        }

        return memo[^1];
    }

    private static int RobRecurWithMemoization(int[] nums, int index, Dictionary<int, int> dic)
    {
        if (dic.TryGetValue(index, out var result))
        {
            return result;
        }

        if (index < nums.Length)
        {
            // select the maximum of:
            // 1. robbing the next house
            // OR
            // 2. robbing the current house and the 1 that's 2 doors down

            int profitFromNextHouse = RobRecurWithMemoization(nums, index + 1, dic);
            // save result
            int profitFromThisHouseAnd2DoorsDown = nums[index] + RobRecurWithMemoization(nums, index + 2, dic);
            // save result
            var max = Math.Max(profitFromNextHouse, profitFromThisHouseAnd2DoorsDown);
            dic[index] = max;
            return max;
        }

        return 0;
    }

    private static int RobRecurBase(int[] nums, int index)
    {
        if (index < nums.Length)
        {
            // select the maximum of robbing the next house
            // OR
            // robbing the current house and the 1 that's 2 doors down
            int profitFromNextHouse = RobRecurBase(nums, index + 1);
            int profitFromThisHouseAnd2DoorsDown = nums[index] + RobRecurBase(nums, index + 2);
            return Math.Max(profitFromNextHouse, profitFromThisHouseAnd2DoorsDown);
        }

        return 0;
    }

    /*
     * ok, how can we memoize this solution
     * how do we save the results of previously computed solutions?
     * let's look at Fibonacci..
     */



    private static int Recur(int sum, int current, int[] nums)
    {
        if (current == -1)
        {
            return sum;
        }

        var indexOfMax = Array.IndexOf(nums, current);

        // add lower adjacent
        int lowerAdjacentIndex = indexOfMax - 1;
        if (lowerAdjacentIndex > -1)
        {
            nums[lowerAdjacentIndex] = -1;
        }

        // add upper bound
        int upperAdjacentIndex = indexOfMax + 1;
        if (upperAdjacentIndex < nums.Length)
        {
            nums[upperAdjacentIndex] = -1;
        }
        sum += current;
        nums[indexOfMax] = -1;

        return Recur(sum, nums.Max(), nums);
    }

    private static int Recur2(int sum, HashSet<int> visited, int[] nums)
    {
        int[] filtered = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            if (visited.Contains(i))
            {
                continue;
            }
            filtered[i] = nums[i];
        }

        var max = filtered.Max();
        //var indicesOfMax = filtered.FindAllIndexOf(max);
        return 0;

    }

    private int Rob2(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        if (nums.Length == 2)
        {
            return nums.Max();
        }

        if (nums.Length == 3)
        {
            return Math.Max(nums[0] + nums[2], nums[1]);
        }

        if (nums.Length == 4)
        {
            int temp = Math.Max(nums[0] + nums[2], nums[1] + nums[3]);
            return Math.Max(temp, nums[0] + nums[3]);
        }

        int maxLoot = 0;
        var set = new HashSet<int>();
        var ordered = nums.OrderByDescending(x => x).ToArray();
        //var orderedDistinct = ordered.Distinct();

        // for each value in ordered
        // computer all possible sums
        // return the max

        foreach (var _ in ordered)
        {
            int sum = 0;
            foreach (var j in ordered)
            {
                // sucessively find next smallest values

                // 1. Determine number of times the number occurs
                var firstOccurrenceIndex = Array.IndexOf(ordered, j);
                var lastOccurrenceIndex = Array.LastIndexOf(ordered, j);
                int startIndex = 0;
                for (var k = firstOccurrenceIndex; k < lastOccurrenceIndex + 1; k++)
                {
                    int actualIndex = Array.IndexOf(nums, j, startIndex);

                    if (set.Contains(actualIndex))
                    {
                        continue;
                    }
                    startIndex++;

                    // add lower adjacent
                    int lowerAdjacentIndex = actualIndex - 1;
                    if (lowerAdjacentIndex > -1)
                    {
                        set.Add(lowerAdjacentIndex);
                    }

                    // add upper bound
                    int upperAdjacentIndex = actualIndex + 1;
                    if (upperAdjacentIndex < nums.Length)
                    {
                        set.Add(upperAdjacentIndex);
                    }
                    set.Add(actualIndex);
                    sum += j;
                }
            }

            maxLoot = Math.Max(sum, maxLoot);
        }

        return maxLoot;
    }
}


public static class Helper
{
    public static int[] FindAllIndexOf<T>(this IEnumerable<T> values, T val)
    {
        return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(x => x != -1).ToArray();
    }
}
