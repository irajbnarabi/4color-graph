using FluentAssertions;
using WebApplication1.Adapters.Out;

namespace WebApplication1.Tests.Adapters;

public class BacktrackingHandlerTests
{
    [Fact]
    public void Should_Color_K3_With_3_Colors()
    {
        var handler = new BacktrackingHandler();
        var result = handler.Handle(new[] {
            new[] { 0, 1, 1 },
            new[] { 1, 0, 1 },
            new[] { 1, 1, 0 }
        }, 3);

        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
        result.Colors.Should().OnlyHaveUniqueItems();
    }
    
    [Fact]
    public void Should_Color_K4_With_4_Colors()
    {
        var handler = new BacktrackingHandler();

        var k4 = new[]
        {
            new[] { 0, 1, 1, 1 },
            new[] { 1, 0, 1, 1 },
            new[] { 1, 1, 0, 1 },
            new[] { 1, 1, 1, 0 }
        };

        // Act
        var result = handler.Handle(k4, 4);

        // Assert
        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
        result.Colors.Should().OnlyHaveUniqueItems();
        result.Colors.Should().HaveCount(4);
    }


    [Fact]
    public void Should_Fail_K4_With_3_Colors()
    {
        var handler = new BacktrackingHandler();
        var result = handler.Handle(new[] {
            new[] { 0, 1, 1, 1 },
            new[] { 1, 0, 1, 1 },
            new[] { 1, 1, 0, 1 },
            new[] { 1, 1, 1, 0 }
        }, 3);
    
        result.Should().NotBeNull();
        result!.Success.Should().BeFalse();
    }
}