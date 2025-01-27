using FluentAssertions;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/palindrome-number/

public class PalindromeNumber
{
    [Theory]
    [InlineData(121, true)]
    [InlineData(-121, false)]
    [InlineData(10, false)]
    [InlineData(1000000001, true)]
    public void Given_WhenMerge1_Then(int x, bool expected)
    {
        // Given
        // When
        var result = IsPalindrome(x);

        // Then
        result.Should().Be(expected);
    }

    private static bool IsPalindrome(int x)
    {
        string stringForm = x.ToString();

        int i = 0;
        int j = stringForm.Length - 1;

        for (; i < j; i++, j--)
        {
            if (stringForm[i] != stringForm[j]) return false;
        }

        return true;
    }

    private static bool IsPalindromeBad(int x)
    {
        string stringForm = x.ToString();

        if (stringForm.Length == 1) return true;

        string first = string.Empty, last = string.Empty;

        if (stringForm.Length == 2)
        {
            first = stringForm[0].ToString();
            last = stringForm[1].ToString();
        }
        else
        {
            int halfLength = stringForm.Length / 2;
            int nub = (stringForm.Length % 2) == 0 ? 0 : 1;
            first = stringForm[0..halfLength];
            last = string.Concat(stringForm[(halfLength + nub)..].Reverse());
        }

        for (int i = 0; i < first.Length; i++)
        {
            if (first[i] != last[i]) return false;
        }

        return true;
    }
}
