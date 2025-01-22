using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/kth-largest-element-in-an-array/

// max heap implementation done by GitHub Copilot
public class KthLargestElementInAnArray
{
    [Theory]
    [InlineData(new int[] { 3, 2, 1, 5, 6, 4 }, 2, 5)]
    [InlineData(new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4, 4)]
    public void Given_WhenFindKthLargest_Then(int[] nums, int k, int expected)
    {
        int result = FindKthLargest(nums, k);
        result.Should().Be(expected);
    }

    private static int FindKthLargest(int[] nums, int k)
    {
        PriorityQueue<int, int> _minHeap = new(k);

        for (int i = 0; i < k; i++)
        {
            _minHeap.Enqueue(nums[i], nums[i]);
        }

        for (int i = k; i < nums.Length; i++)
        {
            if (nums[i] > _minHeap.Peek())
            {
                _minHeap.DequeueEnqueue(nums[i], nums[i]);
            }
        }

        return _minHeap.Peek();
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 5, 6, 4 }, 2, 5)]
    [InlineData(new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4, 4)]
    public void Given_WhenFindKthLargestOld_Then(int[] nums, int k, int expected)
    {
        int result = FindKthLargestOld(nums, k);
        result.Should().Be(expected);
    }

    private static int FindKthLargestOld(int[] nums, int k)
    {
        if (nums.Length == 0)
        {
            return 0;
        }

        // insert the 1st k elements into the min heap
        var minHeap = new MinHeap();
        for (int i = 0; i < k; i++)
        {
            minHeap.Add(nums[i]);
        }

        // now iterate through the rest of the array
        // if the number is bigger than the minimum, pop it off and add it
        for (int i = k; i < nums.Length; i++)
        {
            if (nums[i] > minHeap.Peek())
            {
                minHeap.Pop();
                minHeap.Add(nums[i]);
            }
        }

        // top of the minheap is the Kth largest element ;)
        return minHeap.Peek();
    }

    public class MinHeap
    {
        private List<int> _elements = new List<int>();

        public int GetSize()
        {
            return _elements.Count;
        }

        public int Peek()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }
            return _elements[0];
        }

        public void Add(int element)
        {
            _elements.Add(element);
            HeapifyUp(_elements.Count - 1);
        }

        public int Pop()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            int result = _elements[0];
            _elements[0] = _elements[_elements.Count - 1];
            _elements.RemoveAt(_elements.Count - 1);

            HeapifyDown(0);
            return result;
        }

        private void HeapifyUp(int index)
        {
            while (HasParent(index) && _elements[index] < GetParent(index))
            {
                int parentIndex = GetParentIndex(index);
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void HeapifyDown(int index)
        {
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index) < GetLeftChild(index))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (_elements[index] < _elements[smallerChildIndex])
                {
                    break;
                }

                Swap(index, smallerChildIndex);
                index = smallerChildIndex;
            }
        }

        private bool HasParent(int index) => index > 0;
        private int GetParentIndex(int index) => (index - 1) / 2;
        private int GetParent(int index) => _elements[GetParentIndex(index)];

        private bool HasLeftChild(int index) => GetLeftChildIndex(index) < _elements.Count;
        private int GetLeftChildIndex(int index) => 2 * index + 1;
        private int GetLeftChild(int index) => _elements[GetLeftChildIndex(index)];

        private bool HasRightChild(int index) => GetRightChildIndex(index) < _elements.Count;
        private int GetRightChildIndex(int index) => 2 * index + 2;
        private int GetRightChild(int index) => _elements[GetRightChildIndex(index)];

        private void Swap(int indexOne, int indexTwo)
        {
            int temp = _elements[indexOne];
            _elements[indexOne] = _elements[indexTwo];
            _elements[indexTwo] = temp;
        }
    }
}
