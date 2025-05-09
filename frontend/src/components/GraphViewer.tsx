import React, { useEffect, useState } from "react";
// @ts-ignore
import CytoscapeComponent from "react-cytoscapejs";
import { useColorAnimation } from "../hooks/useColorAnimation";

const colorPalette = ["#00BCD4", "#E91E63", "#FFEB3B", "#000000"];
const defaultColor = "#ccc";

type GraphViewerProps = {
    adjacency: number[][];
    colors: number[];
};

const GraphViewer = ({ adjacency, colors }: GraphViewerProps) => {
    const [elements, setElements] = useState<any[]>([]);
    const animatedColors = useColorAnimation(colors);

    useEffect(() => {
        if (!adjacency?.length || colors.length !== adjacency.length) return;

        const nodes = adjacency.map((_, i) => ({
            data: { id: `n${i}`, label: String.fromCharCode(65 + i) }
        }));

        const edges: any[] = [];
        adjacency.forEach((row, i) =>
            row.forEach((val, j) => {
                if (val === 1 && i < j) {
                    edges.push({ data: { source: `n${i}`, target: `n${j}` } });
                }
            })
        );

        setElements([...nodes, ...edges]);
    }, [adjacency, colors]);

    const cyKey = JSON.stringify(animatedColors);

    return (
        <div style={{ width: "100%", height: 640, border: "1px solid #ccc", background: "#f4f4f4" }}>
            <CytoscapeComponent
                key={cyKey}
                elements={elements}
                style={{ width: "100%", height: "100%" }}
                layout={{ name: "circle", fit: true, padding: 10 }}
                stylesheet={[
                    {
                        selector: "node",
                        style: {
                            backgroundColor: (ele: any) => {
                                const idx = parseInt(ele.id().replace("n", ""));
                                const colorIndex = animatedColors[idx];
                                return colorIndex === -1 ? defaultColor : colorPalette[colorIndex];
                            },
                            label: "data(label)",
                            color: "#fff",
                            textValign: "center",
                            textHalign: "center",
                            fontSize: "16px",
                            fontWeight: "bold",
                            width: 34,
                            height: 34
                        }
                    },
                    {
                        selector: "edge",
                        style: {
                            width: 2,
                            lineColor: "#999",
                            curveStyle: "bezier"
                        }
                    }
                ]}
            />
        </div>
    );
};

export default GraphViewer;
