using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/letter-combinations-of-a-phone-number/

public class LetterCombinationsOfAPhoneNumber
{
    [Theory]
    [InlineData("23", new string[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" })]
    [InlineData("", new string[] { })]
    [InlineData("2", new string[] { "a", "b", "c" })]
    public void Given_WhenLetterCombinations_Then(string digits, string[] expected)
    {
        var result = LetterCombinations(digits);
        result.Should().BeEquivalentTo(expected);
    }

    private static IList<string> LetterCombinations(string digits)
    {
        if (digits.Length == 0)
        {
            return [];
        }

        var dic = new Dictionary<char, char[]> {
            { '2', new char[] { 'a', 'b', 'c' } },
            { '3', new char[] { 'd', 'e', 'f' } },
            { '4', new char[] { 'g', 'h', 'i' } },
            { '5', new char[] { 'j', 'k', 'l' } },
            { '6', new char[] { 'm', 'n', 'o' } },
            { '7', new char[] { 'p', 'q', 'r', 's' } },
            { '8', new char[] { 't', 'u', 'v' } },
            { '9', new char[] { 'w', 'x', 'y', 'z' } },
        };

        var output = new List<string>();
        foreach (char c in dic[digits[0]])
        {
            output.Add($"{c}");
        }

        for (int i = 1; i < digits.Length; i++)
        {
            var chars = dic[digits[i]];
            var tempOutput = new List<string>();

            for (int j = 0; j < output.Count; j++)
            {
                foreach (char c in chars)
                {
                    tempOutput.Add($"{output[j]}{c}");
                }
            }

            output = tempOutput;
        }

        return output;
    }
}
