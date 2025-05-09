import React, { useEffect, useState } from 'react';
import {
    Box,
    Typography,
} from "@mui/material";
import GraphViewer from './components/GraphViewer';
import MatrixTable from './components/MatrixTable';
import MatrixInputBuilder from "./components/MatrixInputBuilder";

const dummyMatrix = [
  [0,1,1,0,0],
  [1,0,1,1,0],
  [1,1,0,1,1],
  [0,1,1,0,1],
  [0,0,1,1,0]
];

const App = () => {
  const [colors, setColors] = useState<number[]>([]);

  useEffect(() => {
    fetch("/api/color", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ matrix: dummyMatrix })
    })
        .then((res) => res.json())
        .then((data) => setColors(data));
  }, []);

  return (
      <div style={{padding: 20}}>
          <div>
              <MatrixInputBuilder/>
          </div>
      </div>
  );
};

export default App;