using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/group-anagrams/
// slow but it works

public class GroupAnagramsClass
{
    [Fact]
    public void Given_When_Then()
    {
        string[] strs = ["eat", "tea", "tan", "ate", "nat", "bat"];
        IList<IList<string>> expected = [["bat"], ["nat", "tan"], ["ate", "eat", "tea"]];

        var result = GroupAnagrams(strs);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Given_When_Then2()
    {
        string[] strs = [""];
        IList<IList<string>> expected = [[""]];

        var result = GroupAnagrams(strs);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Given_When_Then3()
    {
        string[] strs = ["", ""];
        IList<IList<string>> expected = [["", ""]];

        var result = GroupAnagrams(strs);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Given_When_Then4()
    {
        string[] strs = ["a"];
        IList<IList<string>> expected = [["a"]];

        var result = GroupAnagrams(strs);
        result.Should().BeEquivalentTo(expected);
    }

    private static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var strsHashset = new HashSet<string>(strs);
        var alreadyFound = new HashSet<int>();
        var ret = new List<IList<string>>();

        for (int i = 0; i < strs.Length; i++)
        {
            if (alreadyFound.Contains(i))
            {
                continue;
            }

            var tempRet = new List<string>
            {
                strs[i]
            };

            for (int j = i + 1; j < strs.Length; j++)
            {
                if (alreadyFound.Contains(j))
                {
                    continue;
                }

                if (AreAnagrams(strs[i], strs[j]))
                {
                    alreadyFound.Add(i);
                    alreadyFound.Add(j);
                    tempRet.Add(strs[j]);
                }
            }

            ret.Add(tempRet);
        }

        return ret;
    }

    private static bool AreAnagrams(string word1, string word2)
    {
        if (word1.Length != word2.Length)
        {
            return false;
        }

        var charArray1 = word1.ToCharArray();
        var charArray2 = word2.ToCharArray();

        Array.Sort(charArray1);
        Array.Sort(charArray2);

        return new string(charArray1) == new string(charArray2);
    }
}
