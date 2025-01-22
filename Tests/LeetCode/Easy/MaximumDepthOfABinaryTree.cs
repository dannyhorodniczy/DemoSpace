using DemoSpace;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/maximum-depth-of-binary-tree/

public class MaximumDepthOfABinaryTree
{
    [Theory]
    [InlineData(3)]
    public void Given_When_Then(int expected)
    {
        // Given
        int?[] nums = [3, 9, 20, null, null, 15, 7];
        var root = BstBuilder.ConstructBst(nums);

        // When
        int result = MaxDepthDFS(root);
        int result2 = MaxDepthBFS(root);

        // Then
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }

    [Theory]
    [InlineData(2)]
    public void Given_When_Then2(int expected)
    {
        // Given
        int?[] nums = [1, null, 2];
        var root = BstBuilder.ConstructBst(nums);

        // When
        int result = MaxDepthDFS(root);
        int result2 = MaxDepthBFS(root);

        // Then
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }

    [Theory]
    [InlineData(0)]
    public void Given_When_Then3(int expected)
    {
        // Given
        int?[] nums = [];
        var root = BstBuilder.ConstructBst(nums);

        // When
        int result = MaxDepthDFS(root);
        int result2 = MaxDepthBFS(root);

        // Then
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }

    [Theory]
    [InlineData(4)]
    public void Given_When_Then4(int expected)
    {
        // Given
        int?[] nums = [0, 2, 4, 1, null, 3, -1, 5, 1, null, 6, null, 8];
        var root = BstBuilder.ConstructBst(nums);

        // When
        int result = MaxDepthDFS(root);
        int result2 = MaxDepthBFS(root);

        // Then
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }

    // seems to be the preferred solution
    // need to work on intuitive understanding of DFS (recursive vs Stack usage)
    private static int MaxDepthDFS(TreeNode root)
    {
        if (root == null) return 0;

        var maxLeft = MaxDepthDFS(root.left);
        var maxRight = MaxDepthDFS(root.right);

        return 1 + Math.Max(maxLeft, maxRight);
    }

    private static int MaxDepthBFS(TreeNode root)
    {
        if (root == null) return 0;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        int maxDepth = 0;

        while (q.Count > 0)
        {
            maxDepth++;
            int qCount = q.Count;
            for (int i = 0; i < qCount; i++)
            {
                var item = q.Dequeue();
                if (item.left != null) q.Enqueue(item.left);
                if (item.right != null) q.Enqueue(item.right);
            }
        }

        return maxDepth;
    }
}
