using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/kth-smallest-element-in-a-bst/

public class KthSmallestElementInABst
{
    [Theory]
    [InlineData(1, 1)]
    public void Given_When_Then(int k, int expected)
    {
        int?[] nums = [3, 1, 4, null, 2];
        var root = BstBuilder.ConstructBst(nums);
        int? result = new PracticeSolution().KthSmallest(root, k);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, 3)]
    public void Given_When_Then2(int k, int expected)
    {
        int?[] nums = [5, 3, 6, 2, 4, null, null, 1];
        var root = BstBuilder.ConstructBst(nums);
        int? result = new PracticeSolution().KthSmallest(root, k);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, 3)]
    public void Given_When_Then3(int k, int expected)
    {
        int?[] nums = [2, 1, 3];
        var root = BstBuilder.ConstructBst(nums);
        int? result = new PracticeSolution().KthSmallest(root, k);
        result.Should().Be(expected);
    }
}

public class PracticeSolution
{
    // ok so there is the default in order traversal way of doing this
    // I will memorize it at some points
    // but right now, I don't care
    //
    // let's think about a binary tree structure
    // to find the smallest (1st) element, go left until you can't
    // then go right, and left again until you can't (that's the next)
    // if you cannot go right, you go up (that's the next)

    public int KthSmallest(TreeNode root, int k)
    {
        // setup
        int count = 0;
        int kthSmallest = 0;
        var current = root;
        Stack<TreeNode> stack = new Stack<TreeNode>();

    GoLeft:
        while (current.left != null)
        {
            stack.Push(current);
            current = current.left;
        }

        count++;
        if (count == k)
        {
            return current.val;
        }

        if (current.right != null)
        {
            current = current.right;
            goto GoLeft;
        }

        // we need to backtrack
        do
        {
            current = stack.Pop();

            count++;
            if (count == k)
            {
                return current.val;
            }

        } while (current.right == null);

        current = current.right;
        goto GoLeft;
    }
}

public class MyActualSolution
{
    public int KthSmallest(TreeNode root, int k)
    {
        int count = 0;
        // go as far left as possible
        // find the smallest value basically and then backtrack from there

        // 1. Go left until you can't (smallest element found)
        Stack<TreeNode> stack = new();
        TreeNode kthSmallest = root;

        while (count < k)
        {
        // go as far left as possible, record each node along the way
        FarLeft:
            while (kthSmallest.left != null)
            {
                stack.Push(kthSmallest);
                kthSmallest = kthSmallest.left;
            }

            // we are as far left as possible
            // increment the count, check if done
            count++;
            if (count == k)
            {
                return kthSmallest.val;
            }

            // now check right
            // if it exists, we need to go as far left as possible again
            if (kthSmallest.right != null)
            {
                kthSmallest = kthSmallest.right;
                goto FarLeft;
            }

            // we are as far left as possible and a right node does not exist:
            // we must backtrack until a right node is available
            // go back one (pop one off the stack)
            do
            {
                kthSmallest = stack.Pop();

                // check if we're there
                count++;
                if (count == k)
                {
                    return kthSmallest.val;
                }


            } while (kthSmallest.right == null);

            kthSmallest = kthSmallest.right;
        }

        return root.val;
    }
}

public class BasicSolution
{
    private List<int> InAscendingOrder = new();

    public int KthSmallestRecursive(TreeNode root, int k)
    {
        this.InAscendingOrderFullTraversal(root);
        return InAscendingOrder[k - 1];
    }

    private TreeNode InAscendingOrderFullTraversal(TreeNode node)
    {
        if (node != null)
        {
            InAscendingOrderFullTraversal(node.left);
            InAscendingOrder.Add(node.val);
            InAscendingOrderFullTraversal(node.right);
        }

        return null;
    }

}