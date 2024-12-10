using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/encode-and-decode-tinyurl

public class EncodeAndDecodeTinyUrl
{
    [Theory]
    [InlineData("https://leetcode.com/problems/design-tinyurl")]
    public void Given_When_Then(string longUrl)
    {
        // Given
        var codec = new Codec();

        // When
        string shortUrl = codec.Encode(longUrl);
        var result = codec.Decode(shortUrl);
        string shortUrl2 = codec.Encode(longUrl);
        var result2 = codec.Decode(shortUrl2);

        // Then
        result.Should().Be(longUrl);
        result.Should().Be(result2);
        shortUrl.Should().Be(shortUrl2);
    }
}

// slightly better
public class Codec
{
    private readonly Dictionary<string, string> _shortToLong = new();
    private readonly Dictionary<string, string> _longToShort = new();
    private readonly string _validChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_~";

    // 1. A long URL is passed that we need to redirect to
    // 2. We need to generate a unique short URL based on this
    //- time based seems the easiest solution
    // decode will literally be a dictionary lookup
    // encode is the real problem
    // Encodes a URL to a shortened URL
    public string Encode(string longUrl)
    {
        if (_longToShort.TryGetValue(longUrl, out var code))
        {
            return $"https://service.com/{code}";
        }

        string newCode = string.Empty;

        do
        {
            var s = new char[8];

            for (int i = 0; i < s.Length; i++)
            {
                s[i] = _validChars[Random.Shared.Next(64)];
            }
            newCode = s.ToString();

        } while (_shortToLong.ContainsKey(newCode));

        _shortToLong[newCode] = longUrl;
        _longToShort[longUrl] = newCode;
        return newCode;
    }

    // Decodes a shortened URL to its original URL.
    public string Decode(string shortUrl)
    {
        return _shortToLong.TryGetValue(shortUrl, out var longUrl) ?
            longUrl :
            string.Empty;
    }
}

// trivially simple solution
//public class Codec
//{
//    private readonly Dictionary<string, string> _dic = new();
//    private int i = 0;

//    // 1. A long URL is passed that we need to redirect to
//    // 2. We need to generate a unique short URL based on this
//    //- time based seems the easiest solution
//    // decode will literally be a dictionary lookup
//    // encode is the real problem
//    // Encodes a URL to a shortened URL
//    public string Encode(string longUrl)
//    {
//        string url = $"https://service.com/{i}";
//        i++;

//        _dic[url] = longUrl;
//        return url;
//    }

//    // Decodes a shortened URL to its original URL.
//    public string Decode(string shortUrl)
//    {
//        if (_dic.TryGetValue(shortUrl, out var longUrl))
//        {
//            return longUrl;
//        }

//        return string.Empty;
//    }
//}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.decode(codec.encode(url));