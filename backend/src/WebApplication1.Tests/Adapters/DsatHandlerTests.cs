using FluentAssertions;
using WebApplication1.Adapters.Out;
using WebApplication1.Ports.Outbound;
namespace WebApplication1.Tests.Adapters;

public class DsatHandlerTests
{
    [Fact]
    public void Should_Color_Simple_Graph_With_DSatur()
    {
        var handler = new DsatHandler();
        var result = handler.Handle(new[] {
            new[] { 0, 1 },
            new[] { 1, 0 }
        });

        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
        result.AlgorithmUsed.Should().Be("DSatur");
        result.Colors.Should().HaveCount(2);
    }
}