using DemoSpace;
using FluentAssertions;
using Xunit;

namespace Tests.Tools;
public class BstHelpersTests
{
    [Fact]
    public void GivenAnArrayRepresentationOfABst_WhenConstructBst_ThenReturnsRoot()
    {
        // Given
        int?[] array = [4, 2, 7, 1, 3, 6, 9];

        // When
        var root = BstHelper.ConstructBst(array);
        var deconstructed = BstHelper.DeconstructBst(root);

        // Then
        deconstructed.Should().BeEquivalentTo(array, o => o.WithStrictOrdering());
        root.val.Should().Be(4);
    }
}
