using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/longest-substring-without-repeating-characters/

public class LongestSubstringWithoutRepeatingCharacters
{
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("", 0)]
    [InlineData("au", 2)]
    [InlineData("dvdf", 3)]
    public void Given_When_Then(string s, int expected)
    {
        // Given
        // When
        int result = LengthOfLongestSubstring(s);

        // Then
        result.Should().Be(expected);
    }

    private static int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int maxLength = 0;
        int startPtr = 0;
        int endPtr = 1;
        var uniqueChars = new HashSet<char> { s[0] };

        while (endPtr < s.Length)
        {
            if (uniqueChars.Contains(s[endPtr]))
            {
                if ((startPtr + 1) < endPtr)
                {
                    if (uniqueChars.Count > maxLength) maxLength = uniqueChars.Count;
                    uniqueChars.Remove(s[startPtr]);
                }
                else
                {
                    endPtr++;
                }

                startPtr++;
            }
            else
            {
                uniqueChars.Add(s[endPtr]);
                endPtr++;
            }
        }

        return Math.Max(uniqueChars.Count, maxLength);
    }
}
