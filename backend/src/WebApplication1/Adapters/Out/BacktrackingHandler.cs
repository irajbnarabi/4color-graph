using WebApplication1.Domain.Models;
using WebApplication1.Ports.Outbound;

namespace WebApplication1.Adapters.Out;

public class BacktrackingHandler : IColoringHandler
{
    public IColoringHandler? Next { get; set; }

    public ColoringResult? Handle(int[][] adj, int maxColors = 4)
    {
        int n = adj.Length;
        var colors = Enumerable.Repeat(-1, n).ToArray();

        bool success = TryColor(0);
        return success
            ? new ColoringResult(colors, "Backtracking", true, null)
            : Next?.Handle(adj, maxColors)
              ?? new ColoringResult(Array.Empty<int>(), "Backtracking", false, "Backtracking failed");

        bool TryColor(int v)
        {
            if (v == n) return true;
            for (int c = 0; c < maxColors; c++)
            {
                if (IsSafe(v, c))
                {
                    colors[v] = c;
                    if (TryColor(v + 1)) return true;
                    colors[v] = -1;
                }
            }
            return false;
        }

        bool IsSafe(int v, int col)
        {
            for (int u = 0; u < n; u++)
                if (adj[v][u] == 1 && colors[u] == col)
                    return false;
            return true;
        }
    }
}