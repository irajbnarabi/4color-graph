using WebApplication1.Adapters.Out;
using WebApplication1.Domain.Models;
using WebApplication1.Ports.Inbound;
using WebApplication1.Ports.Outbound;

namespace WebApplication1.Application.GraphColoring;

public class ColoringService : IColoringService
{
    private readonly IColoringHandler _rootHandler;

    public ColoringService()
    {
        var dsatur = new DsatHandler();
        var bt = new BacktrackingHandler();
        dsatur.Next = bt;
        _rootHandler = dsatur;
    }

    public ColoringResult ColorGraph(int[][] adjacency, int maxColors = 4)
    {
        var result = _rootHandler.Handle(adjacency, maxColors);
        return result ??  new ColoringResult(Array.Empty<int>(), "Unknown", false, $"Graph could not be colored with {maxColors} colors.");
    }
}