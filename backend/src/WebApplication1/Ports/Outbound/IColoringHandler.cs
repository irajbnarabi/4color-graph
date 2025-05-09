using WebApplication1.Domain.Models;

namespace WebApplication1.Ports.Outbound;

public interface IColoringHandler
{
    IColoringHandler? Next { get; set; }
    ColoringResult? Handle(int[][] adjacency, int maxColors = 4);
}