using FluentAssertions;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;
public class ReverseWordsInAString
{
    [Theory]
    [InlineData("the sky is blue", "blue is sky the")]
    [InlineData("  hello world  ", "world hello")]
    [InlineData("a good   example", "example good a")]
    public void Given_WhenRemoveDuplicates_Then(string input, string expected)
    {
        string result = ReverseWords(input);
        result.Should().Be(expected);
    }

    private static string ReverseWords(string s)
    {
        string ret = string.Empty;

        var words = s.Split(' ');
        foreach (var word in words)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                continue;
            }
            ret = $"{word} {ret}";
        }
        return ret.Trim();
    }

    private static string ReverseWordsSlowAndMoreMechanical(string s)
    {
        string sanitized = s.Trim();
        int start = 0;
        string word = string.Empty;
        string ret = string.Empty;

        for (int end = 0; end < sanitized.Length; end++)
        {
            if (sanitized[end] == ' ')
            {
                ret = $"{word} {ret}".Trim();
                word = string.Empty;
                continue;
            }

            word += sanitized[end];
        }

        return $"{word} {ret}".Trim();
    }

    private static string ReverseWordsSimpleSolution(string s)
    {
        string[] words = s.Split(' ').Where(x => x != string.Empty).ToArray();

        string ret = string.Empty;
        for (int i = 0; i < words.Length; i++)
        {
            ret = $"{words[i]} {ret}";
        }

        return ret.Trim();
    }

    /*
     * this is a 2 pointers problem
     * 
     * 1. sanitize the string (Trim == TrimStart && TrimEnd)
     * 2. for...
     * --> extend the end pointer until you reach a space
     * --> put the word in a new string with a leading space
     * --> repeat, reset the 2 pointers, always prepend the new word at the start of the string
     * --> TrimStart and return
     */
}
