namespace WebApplication1.Domain.Models;

/// <summary>
/// Final graph coloring plus metadata.
/// </summary>
public record ColoringResult(
    int[] Colors,
    string AlgorithmUsed,
    bool Success,
    string? ErrorMessage
);