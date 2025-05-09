using WebApplication1.Domain.Models;
using WebApplication1.Ports.Outbound;

namespace WebApplication1.Adapters.Out;

public class DsatHandler : IColoringHandler
{
    public IColoringHandler? Next { get; set; }

    public ColoringResult? Handle(int[][] adj, int maxColors = 4)
    {
        try
        {
            int n = adj.Length;
            var colors = Enumerable.Repeat(-1, n).ToArray();
            var saturation = new int[n];
            var degrees = adj.Select(r => r.Count(x => x == 1)).ToArray();

            var neighbors = adj.Select(row => row.Select((v, i) => v == 1 ? i : -1)
                .Where(i => i != -1).ToList()).ToList();

            for (int step = 0; step < n; step++)
            {
                int v = SelectNextVertex(colors, saturation, degrees);
                int color = FindAvailableColor(v, neighbors, colors, maxColors);
                if (color == -1)
                    throw new Exception("Unable to color graph with 4 colors. Needs backtracking.");

                colors[v] = color;
                foreach (int u in neighbors[v])
                {
                    if (colors[u] == -1)
                    {
                        var distinctColors = neighbors[u].Select(n => colors[n])
                            .Where(c => c != -1)
                            .Distinct()
                            .Count();
                        saturation[u] = distinctColors;
                    }
                }
            }

            return new ColoringResult(colors, "DSatur", true, null);

        }
        catch
        {
            return Next?.Handle(adj, maxColors)
                   ?? new ColoringResult(Array.Empty<int>(), "DSatur", false, "DSatur failed and no fallback available");
        }
    }
    private static int SelectNextVertex(int[] colors, int[] saturation, int[] degrees)
    {
        int best = -1;
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i] != -1) continue;
            if (best == -1 ||
                saturation[i] > saturation[best] ||
                (saturation[i] == saturation[best] && degrees[i] > degrees[best]))
            {
                best = i;
            }
        }
        return best;
    }

    private static int FindAvailableColor(int v, List<List<int>> neighbors,
        int[] colors, int maxColors)
    {
        var used = new bool[maxColors];
        foreach (int u in neighbors[v])
            if (colors[u] != -1)
                used[colors[u]] = true;

        for (int c = 0; c < maxColors; c++)
            if (!used[c]) return c;
        return -1;
    }
}