import React, { useState } from "react";
import {
    Box,
    Button,
    Paper,
    TextField,
    Typography,
    Grid
} from "@mui/material";

import PaletteIcon from "@mui/icons-material/Palette";
import InputIcon from "@mui/icons-material/Input";
import TableChartIcon from "@mui/icons-material/TableChart";
import BubbleChartIcon from "@mui/icons-material/BubbleChart";
import ErrorOutlineIcon from "@mui/icons-material/ErrorOutline";

import MatrixTable from "./MatrixTable";
import GraphViewer from "./GraphViewer";
import { isValidMatrix } from "../utils/validateMatrix";
import { fetchGraphColoring } from "../api/graphApi";
import { ColoringResponse } from "../types";

const MatrixInputBuilder = () => {
    const [text, setText] = useState<string>(
        "[[0,1,1,0,0],\n[1,0,1,1,0],\n[1,1,0,1,1],\n[0,1,1,0,1],\n[0,0,1,1,0]]"
    );

    const [matrix, setMatrix] = useState<number[][] | null>(null);
    const [colors, setColors] = useState<number[]>([]);
    const [error, setError] = useState<string | null>(null);

    const handleBuild = async () => {
        try {
            const parsed: number[][] = JSON.parse(text);
            if (!isValidMatrix(parsed)) {
                throw new Error("Invalid matrix structure");
            }

            setMatrix(parsed);
            setColors([]);
            setError(null);

            const result: ColoringResponse = await fetchGraphColoring(parsed);

            if (!result.success) {
                setError(result.errorMessage || "Unknown error");
                return;
            }

            setColors(result.colors);
        } catch (e: any) {
            setError(e.message || "Unexpected error");
        }
    };

    return (
        <Box sx={{ p: 1, backgroundColor: "#f5f5f5", minHeight: "100vh" }}>
            <Box
                sx={{
                    display: "flex", alignItems: "center", justifyContent: "center", gap: 1,
                    py: 4,
                    mb: 4,
                    background: "linear-gradient(135deg, #5B6CFF 0%, #9E35FF 100%)",
                    color: "#fff",
                    textAlign: "center"
                }}
            >
                <PaletteIcon fontSize="large" />

                <Typography variant="h4" fontWeight="bold">
                     Four‑Color Graph Visualizer
                </Typography>
            </Box>

            <Grid container spacing={4} alignItems="flex-start">
                <Grid item xs={12} md={4}>
                    <Paper  elevation={3} sx={{ p: 3, borderRadius: 3 }}>
                        <Box sx={{ display: "flex", alignItems: "center", gap: 1, mb:2 }}>
                            <InputIcon />
                            <Typography variant="h6">Matrix Input</Typography>
                        </Box>

                        <TextField
                            label="Matrix (example: [[0,1],[1,0]])"
                            multiline
                            rows={8}
                            fullWidth
                            value={text}
                            onChange={(e) => setText(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                        />
                        <Button
                            variant="contained"
                            fullWidth
                            sx={{
                                background: "linear-gradient(135deg, #5B6CFF 0%, #9E35FF 100%)",
                                '&:hover': {
                                    background: "linear-gradient(135deg, #4a5aff 0%, #8b2dff 100%)"
                                }
                            }}
                            onClick={handleBuild}
                        >
                            Generate & Color Graph
                        </Button>

                        {error && (
                            <Typography color="error" sx={{ mt: 2 }}>
                                 {error}
                            </Typography>
                        )}
                    </Paper>
                </Grid>

                <Grid item xs={12} md={8}>
                    {matrix && (
                        <Paper elevation={3} sx={{ p: 3, mb: 3, borderRadius: 6 }}>
                            <Box sx={{ display: "flex", alignItems: "center", gap: 1, mb:2 }}>
                                <TableChartIcon />
                                <Typography variant="h6">Adjacency Matrix</Typography>
                            </Box>
                            <MatrixTable matrix={matrix} />
                        </Paper>
                    )}
                    {matrix && colors.length > 0 && (
                        <Paper elevation={3} sx={{ p: 3, borderRadius: 6 }}>
                            <Box sx={{ display: "flex", alignItems: "center", gap: 1, mb:3 }}>
                                <BubbleChartIcon />
                                <Typography variant="h6">Colored Graph</Typography>
                            </Box>
                            <GraphViewer adjacency={matrix} colors={colors} />
                        </Paper>
                    )}
                </Grid>
            </Grid>
        </Box>
    );
};

export default MatrixInputBuilder;
