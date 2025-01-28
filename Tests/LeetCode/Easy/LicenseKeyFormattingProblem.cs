using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/license-key-formatting/

/*
 * how would you scale this algorithm/computation?
 * 
 * the real answer is: (surprise surprise) it depends
 * 
 * What does the data look like? (problem says: 1 <= s.length <= 105)
 * Are we dealing with 10 1 billion char keys?
 * Or are we dealing with 1 billions 10 char keys?
 * Or 500 million 100 char keys?
 * 
 * What is the data rate?
 * Are we dealing with 1 license key per hour?
 * Surges of license key conversion requests?
 * 
 * How does the data come in?
 * Are we reading from a database?
 * Do the keys come via API call?
 * 
 * Does the data need storing?
 * Do we need to store the formatted keys?
 * For how long?
 * 
 * for larger strings:
 * - vertical scaling makes sense, as well as possibly 
 * - processing the string in chunks
 * --> process the chunks in parallel (use tasks!)
 * --> I guess we would stream the data in, then the problem becomes:
 * --> what is faster: the algorithm or the stream speed?
 * 
 * for smaller, more numerous strings;
 * - horizontal scaling makes sense
 * - also in C# we could take advantage of Tasks or TPL
 * --> in our case the computation is local, so it might not make sense, buuut:
 * 
 * 
 */

public class LicenseKeyFormattingProblem
{
    [Theory]
    [InlineData("5F3Z-2e-9-w", 4, "5F3Z-2E9W")]
    [InlineData("2-5g-3-J", 2, "2-5G-3J")]
    public void Given_WhenLongestCommonPrefix_Then(string s, int k, string expected)
    {
        // Given
        // When
        var result = LicenseKeyFormatting(s, k);

        // Then
        result.Should().Be(expected);
    }

    private static string LicenseKeyFormatting(string s, int k)
    {
        var preString = new Stack<char>();
        int counter = 0;

        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (counter == k)
            {
                preString.Push('-');
                counter = 0;
            }

            if (s[i] == '-')
            {
                continue;
            }

            preString.Push(char.ToUpper(s[i]));
            counter++;
        }

        var str = new string(preString.ToArray());
        return str.TrimStart('-');
    }
}
