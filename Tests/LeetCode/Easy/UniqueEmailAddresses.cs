using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;
public class UniqueEmailAddresses
{
    [Theory]
    [InlineData(new string[] { "test.email+alex@leetcode.com", "test.e.mail+bob.cathy@leetcode.com", "testemail+david@lee.tcode.com" }, 2)]
    [InlineData(new string[] { "a@leetcode.com", "b@leetcode.com", "c@leetcode.com" }, 3)]
    public void GivenEmails_WhenNumUniqueEmails_Then(string[] emails, int expected)
    {
        // Given
        // When
        var result = NumUniqueEmails(emails);

        // Then
        result.Should().Be(expected);
    }

    private static int NumUniqueEmails(string[] emails)
    {
        int uniqueCount = 0;

        // key is the domain name
        // hashset contains a sanitized local name
        var dic = new Dictionary<string, HashSet<string>>();

        foreach (string email in emails)
        {
            // get the domain name
            var split = email.Split('@');

            var domainName = split[1];
            string sanitizedLocalName = split[0];

            var plusIndex = split[0].IndexOf('+');
            if (plusIndex > 0)
            {
                sanitizedLocalName = sanitizedLocalName.Substring(0, plusIndex);
            }
            sanitizedLocalName = sanitizedLocalName.Replace(".", string.Empty);

            if (!dic.ContainsKey(domainName))
            {
                dic[domainName] = new HashSet<string>();
            }

            if (dic[domainName].Add(sanitizedLocalName))
            {
                uniqueCount++;
            }
        }

        return uniqueCount;
    }
}
