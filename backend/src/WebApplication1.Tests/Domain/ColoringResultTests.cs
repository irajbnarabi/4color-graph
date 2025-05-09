using FluentAssertions;
using WebApplication1.Domain.Models;

namespace WebApplication1.Tests.Domain;

public class ColoringResultTests
{
    [Fact]
    public void Should_Create_Successful_Result_With_Colors()
    {
        var result = new ColoringResult(new[] { 0, 1, 2 }, "DSatur", true, null);

        result.Success.Should().BeTrue();
        result.Colors.Should().BeEquivalentTo(new[] { 0, 1, 2 });
        result.AlgorithmUsed.Should().Be("DSatur");
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void Should_Create_Error_Result()
    {
        var result = new ColoringResult(Array.Empty<int>(), "Backtracking", false, "Failed");

        result.Success.Should().BeFalse();
        result.Colors.Should().BeEmpty();
        result.ErrorMessage.Should().Be("Failed");
    }
}