using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.LeetCode.Medium;
public class FlipStringToMonotoneIncreasing
{
    [Theory]
    [InlineData("00110", 1)]
    [InlineData("010110", 2)]
    [InlineData("00011000", 2)]
    [InlineData("0101100011", 3)]
    [InlineData("100000001010000", 3)]
    [InlineData("10011111110010111011", 5)]
    public void Given_WhenMinFlipsMonoIncr_Then(string s, int expected)
    {
        Console.WriteLine(s);
        int result = MinFlipsMonoIncr(s);
        result.Should().Be(expected);
    }

    private int MinFlipsMonoIncr(string s)
    {
        int length = s.Length;
        int[] prefixOnes = new int[length + 1];
        int[] suffixZeros = new int[length + 1];

        // To hold the cumulative minimum number of flips.
        int minFlips = int.MaxValue;

        // Calculate the prefix sums of 1s from the beginning of the string to the current position.
        Console.WriteLine("Prefix Ones:");
        // in this case we technically pad 0 with 0, index 0 is never written to
        // the same is true for the SZ, the end of the array is 0 padded, never written to
        // I think it's because when you are at either end, if you assume the 
        for (int i = 1; i <= length; i++)
        {
            prefixOnes[i] = prefixOnes[i - 1] + (s[i - 1] == '1' ? 1 : 0);
            Console.WriteLine($"{i}: [{string.Join(',', prefixOnes)}]");
        }

        Console.WriteLine();
        Console.WriteLine("Suffix zeros:");
        // Calculate the suffix sums of 0s from the end of the string to the current position.
        for (int i = length - 1; i >= 0; i--)
        {
            suffixZeros[i] = suffixZeros[i + 1] + (s[i] == '0' ? 1 : 0);
            Console.WriteLine($"{i}: [{string.Join(',', suffixZeros)}]");
        }

        // Iterate through all possible positions to split the string into two parts
        // and find the minimum number of flips by combining the count of 1s in the prefix
        // and the count of 0s in the suffix.
        Console.WriteLine();
        Console.WriteLine($"PO: [{string.Join(',', prefixOnes)}]");
        Console.WriteLine($"SZ: [{string.Join(',', suffixZeros)}]");
        for (int i = 0; i <= length; i++)
        {
            minFlips = Math.Min(minFlips, prefixOnes[i] + suffixZeros[i]);
            Console.WriteLine(prefixOnes[i] + suffixZeros[i]);
        }

        // Return the cumulative minimum number of flips.
        return minFlips;
    }

    private int MinFlipsMonoIncrTrial(string s)
    {
        /*
         * starting from the beginning
         * iterate until we find a 1, then iterate until we find a 0
         * repeat until EOS
         * 
         * if no changes, return the string
         * 
         * otherwise string must start with 0(s), end with 1(s)
         * 
         * I'm having trouble thinking of the data structure I want to store the changes in
         * let's define a dictionary key=(int, int[])
         * 
         * need to know how many flips there are
         */
        var flips = new List<int>();

        for (int i = 1; i < s.Length; i++)
        {
            if (s[i - 1] != s[i])
            {
                flips.Add(i);
            }
        }

        if (flips.Count == 0)
        {
            return 0;
        }

        bool startsRight = s[0] == '0';

        if (startsRight && flips.Count == 1)
        {
            return 0;
        }

        int zerosToFlip = 0;
        int onesToFlip = 0;
        bool endsRight = s[^1] == '1';

        if (startsRight)
        {
            zerosToFlip = s[flips[0]..].Where(x => x == '0').Count();
            onesToFlip = endsRight ?
                s[flips[0]..flips[^1]].Where(x => x == '1').Count() :
                s[flips[0]..].Where(x => x == '1').Count();
        }
        else
        {
            onesToFlip = endsRight ? s.Where(x => x == '1').Count() : s.Where(x => x == '1').Count();
            zerosToFlip = s.Where(x => x == '0').Count();
        }

        if (zerosToFlip != 0 && onesToFlip != 0)
        {
            return Math.Min(zerosToFlip, onesToFlip);
        }

        return zerosToFlip != 0 ? zerosToFlip : onesToFlip;
    }
}
