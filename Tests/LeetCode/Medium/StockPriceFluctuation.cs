using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/stock-price-fluctuation/

public class StockPriceFluctuation
{
    [Fact]
    public void Given_When_Then()
    {
        StockPrice stockPrice = new StockPrice();
        stockPrice.Update(1, 10); // Timestamps are [1] with corresponding prices [10].
        stockPrice.Update(2, 5);  // Timestamps are [1,2] with corresponding prices [10,5].
        stockPrice.Current().Should().Be(5);     // return 5, the latest timestamp is 2 with the price being 5.
        stockPrice.Maximum().Should().Be(10);     // return 10, the maximum price is 10 at timestamp 1.
        stockPrice.Update(1, 3);  // The previous timestamp 1 had the wrong price, so it is updated to 3.
                                  // Timestamps are [1,2] with corresponding prices [3,5].
        stockPrice.Maximum().Should().Be(5);     // return 5, the maximum price is 5 after the correction.
        stockPrice.Update(4, 2);  // Timestamps are [1,2,4] with corresponding prices [3,5,2].
        stockPrice.Minimum().Should().Be(2);     // return 2, the minimum price is 2 at timestamp 4.
    }
}

public class StockPrice
{
    private readonly Dictionary<int, int> _timestampToStockPrices = new();

    private readonly PriorityQueue<int, int> _minHeap = new();

    // PriorityQueue default implementation is minHeap
    // the the equality comparer to make it a maxHeap
    private readonly PriorityQueue<int, int> _maxHeap = new(Comparer<int>.Create((x, y) => y - x));

    private int _currentTime = 0;

    public void Update(int timestamp, int price)
    {
        _currentTime = Math.Max(timestamp, _currentTime);
        _timestampToStockPrices[timestamp] = price;
        _minHeap.Enqueue(timestamp, price);
        _maxHeap.Enqueue(timestamp, price);
    }

    public int Current()
    {
        return _timestampToStockPrices[_currentTime];
    }

    public int Maximum()
    {
        // remove irrelevant items from the queue until we find a valid item
        // re enqueue the valid item
        int time = 0;
        int price = 0;
        while (_maxHeap.TryDequeue(out time, out price) &&
            _timestampToStockPrices[time] != price) ;

        _maxHeap.Enqueue(time, price);
        return price;
    }

    public int Minimum()
    {
        // remove irrelevant items from the queue until we find a valid item
        // re enqueue the valid item
        int time = 0;
        int price = 0;
        while (_minHeap.TryDequeue(out time, out price) &&
            _timestampToStockPrices[time] != price) ;

        _minHeap.Enqueue(time, price);
        return price;
    }
}