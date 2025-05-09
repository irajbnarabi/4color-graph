import React from "react";
import {
    Box,
} from "@mui/material";
type MatrixTableProps = {
    matrix: number[][];
};

const MatrixTable = ({ matrix }: MatrixTableProps) => {
    const labels = matrix.map((_, i) => String.fromCharCode(65 + i)); // A, B, C...

    return (
        <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
        <table style={{
            borderCollapse: "collapse",
            marginBottom: "1rem",
            textAlign: "center"
        }}>
            <thead>
            <tr>
                <th></th>
                {labels.map((label, idx) => (
                    <th key={idx} style={{ padding: "6px", border: "1px solid #ccc" }}>
                        {label}
                    </th>
                ))}
            </tr>
            </thead>
            <tbody>
            {matrix.map((row, i) => (
                <tr key={i}>
                    <th style={{ padding: "6px", border: "1px solid #ccc" }}>
                        {labels[i]}
                    </th>
                    {row.map((cell, j) => (
                        <td key={j} style={{
                            border: "1px solid #ccc",
                            padding: "6px",
                            background: cell === 1 ? "#e0f7fa" : "#fff"
                        }}>
                            {cell}
                        </td>
                    ))}
                </tr>
            ))}
            </tbody>
        </table>
        </Box>
    );
};

export default MatrixTable;
