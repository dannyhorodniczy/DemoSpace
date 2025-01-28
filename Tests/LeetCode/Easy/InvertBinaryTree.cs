using DemoSpace;
using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/invert-binary-tree/

public class InvertBinaryTree
{
    private static TreeNode InvertTree(TreeNode root)
    {
        /*
         * the best thing to do, as always
         * is to understand *exactly* what the problem is asking for
         * 
         * so in this case, we basically want to BFS flip each child
         * for the recursive algo will take a node and flip the children
         * if one or more is not null
         * if both children are null, then we don't give a shit
         */

        InvertTreeInternal(root);

        return root;
    }

    private static void InvertTreeInternal(TreeNode root)
    {
        if (root == null || (root.left == null && root.right == null)) return;

        var left = root.left;
        root.left = root.right;
        root.right = left;

        InvertTreeInternal(root.left);
        InvertTreeInternal(root.right);
    }

    [Fact]
    public void Given_When_Then1()
    {
        // Given
        int?[] originalBst = [4, 2, 7, 1, 3, 6, 9];
        var root = BstHelper.ConstructBst(originalBst);

        int?[] invertedBst = [4, 7, 2, 9, 6, 3, 1];

        // When
        var result = InvertTree(root);

        // Then
        var deconstructedResult = BstHelper.DeconstructBst(result);
        deconstructedResult.Should().BeEquivalentTo(invertedBst, o => o.WithStrictOrdering());
    }

    [Fact]
    public void Given_When_Then2()
    {
        // Given
        int?[] originalBst = [2, 1, 3];
        var root = BstHelper.ConstructBst(originalBst);

        int?[] invertedBst = [2, 3, 1];

        // When
        var result = InvertTree(root);

        // Then
        var deconstructedResult = BstHelper.DeconstructBst(result);
        deconstructedResult.Should().BeEquivalentTo(invertedBst, o => o.WithStrictOrdering());
    }

    [Fact]
    public void Given_When_Then3()
    {
        // Given
        int?[] originalBst = [];
        var root = BstHelper.ConstructBst(originalBst);

        int?[] invertedBst = [];

        // When
        var result = InvertTree(root);

        // Then
        var deconstructedResult = BstHelper.DeconstructBst(result);
        deconstructedResult.Should().BeEquivalentTo(invertedBst, o => o.WithStrictOrdering());
    }

    [Fact]
    public void Given_When_Then4()
    {
        // Given
        int?[] originalBst = [1, 2];
        var root = BstHelper.ConstructBst(originalBst);

        int?[] invertedBst = [1, null, 2];

        // When
        var result = InvertTree(root);

        // Then
        var deconstructedResult = BstHelper.DeconstructBst(result);
        deconstructedResult.Should().BeEquivalentTo(invertedBst, o => o.WithStrictOrdering());
    }
}
