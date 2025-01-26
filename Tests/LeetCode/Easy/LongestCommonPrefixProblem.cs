using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/longest-common-prefix/

public class LongestCommonPrefixProblem
{
    [Theory]
    [InlineData(new string[] { "flower", "flow", "flight" }, "fl")]
    [InlineData(new string[] { "dog", "racecar", "car" }, "")]
    public void Given_WhenLongestCommonPrefix_Then(string[] strs, string expected)
    {
        // Given
        // When
        var result = LongestCommonPrefix(strs);

        // Then
        result.Should().Be(expected);
    }

    private static string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0) return string.Empty;

        // the strategy here is to take the 1st string and compare it to all subsubsequent strings
        // start a for loop at the 2nd index
        // compare prefix with strs[i]
        // if strs[i] does not start with prefix, then truncate the last char of prefix
        // if prefix == "", then return ""

        string prefix = strs[0];

        for (int i = 1; i < strs.Length; i++)
        {
            while (!strs[i].StartsWith(prefix))
            {
                prefix = prefix.Substring(0, prefix.Length - 1);

                if (prefix.Length == 0) return string.Empty;
            }
        }

        return prefix;
    }
}
