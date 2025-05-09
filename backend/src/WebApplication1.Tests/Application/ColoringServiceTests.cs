using FluentAssertions;
using WebApplication1.Ports.Inbound;
using WebApplication1.Ports.Outbound;
using WebApplication1.Domain.Models;

namespace WebApplication1.Tests.Application;

public class ColoringServiceTests
{
    [Fact]
    public void Should_Return_Backtracking_Result_When_DSatur_Fails()
    {
        // Arrange
        var fakeBacktracking = new StubHandler("Backtracking", true);
        var failingDsat = new StubHandler("DSatur", false) { Next = fakeBacktracking };

        var svc = new ColoringServiceForTest(failingDsat);

        // Act
        var result = svc.ColorGraph(new[] {
            new[] { 0, 1 },
            new[] { 1, 0 }
        });

        // Assert
        result.AlgorithmUsed.Should().Be("Backtracking");
        result.Success.Should().BeTrue();
    }

    private class StubHandler : IColoringHandler
    {
        private readonly string _name;
        private readonly bool _succeed;
        public IColoringHandler? Next { get; set; }

        public StubHandler(string name, bool succeed)
        {
            _name = name;
            _succeed = succeed;
        }

        public ColoringResult? Handle(int[][] _, int maxColors)
        {
            return _succeed
                ? new ColoringResult(new[] { 0, 1 }, _name, true, null)
                : Next?.Handle(_, maxColors);
        }
    }

    private class ColoringServiceForTest : IColoringService
    {
        private readonly IColoringHandler _handler;
        public ColoringServiceForTest(IColoringHandler h) => _handler = h;
        public ColoringResult ColorGraph(int[][] adj, int maxColors = 4) =>
            _handler.Handle(adj, maxColors)!;
    }
}