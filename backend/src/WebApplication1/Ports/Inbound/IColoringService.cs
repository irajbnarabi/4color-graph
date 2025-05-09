using WebApplication1.Domain.Models;

namespace WebApplication1.Ports.Inbound;

public interface IColoringService
{
    ColoringResult ColorGraph(int[][] adjacency, int maxColors = 4);
}

    