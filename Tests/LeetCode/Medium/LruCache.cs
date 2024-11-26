using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/lru-cache/

public class LruCache
{
    [Fact]
    public void Given_When_Then()
    {
        LRUCache lRUCache = new LRUCache(2);
        lRUCache.Put(1, 1); // cache is {1=1}
        lRUCache.Put(2, 2); // cache is {1=1, 2=2}
        lRUCache.Get(1).Should().Be(1);    // return 1
        lRUCache.Put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
        lRUCache.Get(2).Should().Be(-1);    // returns -1 (not found)
        lRUCache.Put(4, 4); // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
        lRUCache.Get(1).Should().Be(-1);    // return -1 (not found)
        lRUCache.Get(3).Should().Be(3);    // return 3
        lRUCache.Get(4).Should().Be(4);    // return 4
    }

    [Fact]
    public void Given_When_Then2()
    {
        LRUCache lRUCache = new LRUCache(1);
        lRUCache.Put(2, 1); // cache is {2=1}
        lRUCache.Get(2).Should().Be(1);    // return 1
        lRUCache.Put(3, 2); // evicts key 2, cache is {3=2}
        lRUCache.Get(2).Should().Be(-1);    // return -1
        lRUCache.Get(3).Should().Be(2);    // return 2
    }


    [Fact]
    public void Given_When_Then3()
    {
        LRUCache lRUCache = new LRUCache(2);
        lRUCache.Get(2).Should().Be(-1);    // return -1
        lRUCache.Put(2, 6); // cache is {2=6}
        lRUCache.Get(1).Should().Be(-1);    // returns -1 (not found)
        lRUCache.Put(1, 5); // cache is {1=5, 2=6}
        lRUCache.Put(1, 2); // cache is {1=2, 2=6}
        lRUCache.Get(1).Should().Be(2);    // return -1 (not found)
        lRUCache.Get(2).Should().Be(6);    // return 3
    }
}

public class LRUCache
{
    private readonly int _capacity;
    private readonly Dictionary<int, LinkedListNode<KeyValuePair<int, int>>> _cache;
    private readonly LinkedList<KeyValuePair<int, int>> _cacheList;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cache = new(_capacity);
        _cacheList = new();
    }

    public int Get(int key)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            _cacheList.Remove(node);
            var newNode = new KeyValuePair<int, int>(node.Value.Key, node.Value.Value);
            _cache[key] = _cacheList.AddFirst(newNode);
            return newNode.Value;
        }

        return -1;
    }

    public void Put(int key, int value)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            _cacheList.Remove(node);
            var tempNode = new KeyValuePair<int, int>(key, value);
            _cache[key] = _cacheList.AddFirst(tempNode);
            return;
        }

        if (_capacity == _cache.Count)
        {
            _cache.Remove(_cacheList.Last.Value.Key);
            _cacheList.RemoveLast();
        }

        var newNode = new KeyValuePair<int, int>(key, value);
        _cache[key] = _cacheList.AddFirst(newNode);
    }
}

//public class LRUCache
//{
//    private readonly Dictionary<int, (int value, int rank, int key)> _dictionary;
//    private readonly int _capacity;
//    private int _rank = -1;

//    public LRUCache(int capacity)
//    {
//        _capacity = capacity;
//        _dictionary = new Dictionary<int, (int, int, int)>(_capacity);
//        _dictionary.EnsureCapacity(_capacity);
//    }

//    public int Get(int key)
//    {
//        if (_dictionary.TryGetValue(key, out var vrk))
//        {
//            // mark the key as recently used
//            _rank++;
//            _dictionary[key] = (vrk.value, _rank, key);

//            return vrk.value;
//        }

//        return -1;
//    }

//    public void Put(int key, int value)
//    {
//        if (_dictionary.ContainsKey(key))
//        {
//            _dictionary.Remove(key);
//        }
//        else if (_dictionary.Count == _capacity)
//        {
//            // is the cache at capacity?
//            // yes: remove the lowest rank
//            (int value, int rank, int key) lowestVrk = (0, _rank, 0);
//            foreach (var vrk in _dictionary.Values)
//            {
//                if (vrk.rank <= lowestVrk.Item2)
//                {
//                    lowestVrk = vrk;
//                }
//            }

//            _dictionary.Remove(lowestVrk.key);
//        }

//        // in all cases: add the new kvp and rank as most recent
//        _rank++;
//        _dictionary[key] = (value, _rank, key);
//    }
//}
