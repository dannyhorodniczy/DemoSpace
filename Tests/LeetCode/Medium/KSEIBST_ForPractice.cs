using DemoSpace;
using FluentAssertions;
using Xunit;

namespace Tests.LeetCode.Medium;
public class KSEIBST_ForPractice
{
    [Theory]
    [InlineData(1, 1)]
    public void Given_When_Then(int k, int expected)
    {
        int?[] nums = [3, 1, 4, null, 2];
        var root = BstBuilder.ConstructBst(nums);
        int? result = KthSmallest(root, k);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, 3)]
    public void Given_When_Then2(int k, int expected)
    {
        int?[] nums = [5, 3, 6, 2, 4, null, null, 1];
        var root = BstBuilder.ConstructBst(nums);
        int? result = KthSmallest(root, k);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, 3)]
    public void Given_When_Then3(int k, int expected)
    {
        int?[] nums = [2, 1, 3];
        var root = BstBuilder.ConstructBst(nums);
        int? result = KthSmallest(root, k);
        result.Should().Be(expected);
    }

    private static int KthSmallest(TreeNode root, int k)
    {
        return 0;
    }
}
