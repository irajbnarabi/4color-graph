import { ColoringResponse } from "../types";

export async function fetchGraphColoring(matrix: number[][]): Promise<ColoringResponse> {
    const res = await fetch("/api/color", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ matrix })
    });

    if (!res.ok) {
        throw new Error("Failed to fetch coloring result");
    }

    return res.json();
}
