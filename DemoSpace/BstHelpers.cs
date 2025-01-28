using System.Collections.Generic;

namespace DemoSpace;
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public static class BstHelper
{
    public static TreeNode ConstructBst(int?[] nums)
    {
        if (nums.Length == 0)
        {
            return null;
        }

        Queue<int?> queue = new();
        foreach (var num in nums)
        {
            queue.Enqueue(num);
        }

        List<TreeNode> nodes = new();

        var val = queue.Dequeue();
        var root = new TreeNode(val.Value);
        nodes.Add(root);

        while (queue.Count > 0)
        {
            var current = nodes[0];
            nodes.RemoveAt(0);
            var leftValue = queue.Dequeue();

            if (leftValue != null)
            {
                current.left = new TreeNode(leftValue.Value);
                nodes.Add(current.left);
            }

            if (queue.Count > 0)
            {
                var rightValue = queue.Dequeue();
                if (rightValue != null)
                {
                    current.right = new TreeNode(rightValue.Value);
                    nodes.Add(current.right);
                }
            }
        }

        return root;
    }

    public static int?[] DeconstructBst(TreeNode root)
    {
        if (root == null)
        {
            return new int?[] { };
        }

        List<int?> result = new List<int?>();
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current != null)
            {
                result.Add(current.val);
                queue.Enqueue(current.left);
                queue.Enqueue(current.right);
            }
            else
            {
                result.Add(null);
            }
        }

        // Remove trailing nulls
        for (int i = result.Count - 1; i >= 0; i--)
        {
            if (result[i] == null)
            {
                result.RemoveAt(i);
            }
            else
            {
                break;
            }
        }

        return result.ToArray();
    }
}
