using WebApplication1.Ports.Inbound;

namespace WebApplication1.Adapters.In;

public static class ColoringEndpoints
{
    public static void MapColoring(this WebApplication app)
    {
        app.MapPost("/api/color", (AdjMatrix req, IColoringService svc) =>
        {
            var result = svc.ColorGraph(req.Matrix);

            if (!result.Success)
            {
                return Results.UnprocessableEntity(new
                {
                    error = result.ErrorMessage,
                    algorithm = result.AlgorithmUsed
                });
            }

            return Results.Ok(result);
        });
    }
}

public record AdjMatrix(int[][] Matrix);