using System.Collections.Generic;
using System.Linq;

namespace Tests.LeetCode.Easy;

// https://leetcode.com/problems/average-of-levels-in-binary-tree/
public class AverageOfLevelsInBinaryTree
{
    public IList<double> AverageOfLevels(TreeNode root)
    {
        /*
         * let us remember that:
         * we use a stack to perform DFS
         * we use a quueue to perform BFS
         */

        if (root == null)
        {
            return (IList<double>) Enumerable.Empty<double>();
        }

        var level = new Queue<TreeNode>();
        level.Enqueue(root);

        var averageOfEachTreeLevel = new List<double>();

        while (level.Count > 0)
        {
            averageOfEachTreeLevel.Add(level.Select(x => x.val).Average());

            var count = level.Count;
            for (int i = 0; i < count; i++)
            {
                var item = level.Dequeue();
                if (item.left != null)
                {
                    level.Enqueue(item.left);
                }

                if (item.right != null)
                {
                    level.Enqueue(item.right);
                }
            }
        }

        return averageOfEachTreeLevel;
    }
}


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
