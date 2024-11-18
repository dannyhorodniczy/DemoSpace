using System.Collections.Generic;
using System.Linq;

namespace Tests.LeetCode.Medium;

// https://leetcode.com/problems/binary-tree-level-order-traversal/

public class BinaryTreeLevelOrderTraversal
{
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var levelOrder = new List<IList<int>>();
        if (root == null)
        {
            return levelOrder;
        }

        var level = new Queue<TreeNode>();
        level.Enqueue(root);

        while (level.Count > 0)
        {
            levelOrder.Add(level.Select(x => x.val).ToList());

            int count = level.Count;

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

        return levelOrder;
    }
}
