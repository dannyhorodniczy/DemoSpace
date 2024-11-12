using FluentAssertions;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/valid-palindrome/

public class ValidPalindrome
{
    [Theory]
    [InlineData("A man, a plan, a canal: Panama", true)]
    [InlineData("race a car", false)]
    [InlineData(" ", true)]
    [InlineData("0P", false)]
    public void Given_WhenMaxProfit_Then(string s, bool expected)
    {
        bool result = IsPalindrome(s);
        result.Should().Be(expected);
    }

    public bool IsPalindrome(string s)
    {
        /*
         * let's start with our approach
         * 1. Sanitize the string (all lower alphanumeric chars)
         * 2. Find the midpoint
         * 3. slice the last array correctly, compare it with the 1st array
         */

        string sanitized = new string(s.Where(x => char.IsLetter(x) || char.IsNumber(x)).ToArray()).ToLower();

        int subArrayLength = (int) (sanitized.Length / 2);
        int startIndexOfArray2 = sanitized.Length % 2 == 0 ? subArrayLength : subArrayLength + 1;

        var array2 = sanitized[startIndexOfArray2..].Reverse().ToArray();

        for (int i = 0; i < array2.Length; i++)
        {
            if (array2[i] != sanitized[i])
            {
                return false;
            }
        }

        return true;
    }
}
