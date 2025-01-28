using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/fruit-into-baskets/

public class FruitIntoBaskets
{
    [Theory]
    [InlineData(new int[] { 1, 2, 1 }, 3)]
    [InlineData(new int[] { 0, 1, 2, 2 }, 3)]
    [InlineData(new int[] { 1, 2, 3, 2, 2 }, 4)]
    [InlineData(new int[] { 3, 3, 3, 1, 2, 1, 1, 2, 3, 3, 4 }, 5)]
    public void Given_WhenTotalFruit_Then(int[] fruits, int expected)
    {
        // Given
        // When
        int result = TotalFruit(fruits);

        // Then
        result.Should().Be(expected);
    }

    private static int TotalFruit(int[] fruits)
    {
        if (fruits.Length < 3) return fruits.Length;

        var dic = InitializeFruitDictionary(fruits);

        int start = 0;
        int currentSeqence = 2;
        int longestSeqence = 2;

        for (int end = 2; end < fruits.Length; end++)
        {
            var current = fruits[end];

            // the fruit exists
            if (dic.ContainsKey(fruits[end]))
            {
                dic[fruits[end]]++;
                currentSeqence++;
                longestSeqence = currentSeqence > longestSeqence ? currentSeqence : longestSeqence;
            }
            // the fruit does not exist and there is space
            else if (dic.Count < 2)
            {
                dic[fruits[end]] = 1;
                currentSeqence++;
                longestSeqence = currentSeqence > longestSeqence ? currentSeqence : longestSeqence;
            }
            // the fruit does not exist and there is no space
            else
            {
                // move the start point until there is only 1 dictionary element
                do
                {
                    dic[fruits[start]]--;

                    if (dic[fruits[start]] == 0)
                    {
                        dic.Remove(fruits[start]);
                    }

                    currentSeqence--;
                    start++;

                } while (dic.Count == 2);

                dic[fruits[end]] = 1;
                currentSeqence++;
                longestSeqence = currentSeqence > longestSeqence ? currentSeqence : longestSeqence;
            }

        }

        return longestSeqence;
    }

    private static Dictionary<int, int> InitializeFruitDictionary(int[] fruits)
    {
        var dic = new Dictionary<int, int>();

        foreach (int fruit in fruits[0..2])
        {
            if (dic.TryGetValue(fruit, out int value))
                dic[fruit]++;
            else
                dic[fruit] = 1;
        }

        return dic;
    }
}
