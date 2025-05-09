# 🏛️ Backend Architecture – Four-Color Graph Visualizer

This document explains the structure, design patterns, and logic behind the backend of the application.

---

## 🧱 Layered Hexagonal Architecture

The backend follows a Hexagonal (Ports and Adapters) architecture:

```
Domain/
  Models/ColoringResult.cs

Ports/
  Inbound/IColoringService.cs
  Outbound/IColoringHandler.cs

Application/
  GraphColoring/ColoringService.cs   ← orchestrates the handler chain

Adapters/
  Out/ DsatHandler.cs, BacktrackingHandler.cs
  In/  Api/ColoringEndpoints.cs      ← ASP.NET Minimal API
```

- **Domain:** Plain models, pure logic.
- **Ports:** Interfaces for input/output (e.g., IColoringService, IColoringHandler).
- **Application:** Core business logic; service coordinates algorithm handlers.
- **Adapters:** Implementation of input (API) and output (coloring algorithms).

---

## 🔁 Chain of Responsibility Pattern

Implemented in the `GraphColoring` layer via `IColoringHandler`:

```csharp
ColoringResult? Handle(int[][] adj, int maxColors);
```

### Structure:

```
DsatHandler ──► BacktrackingHandler ──► null
```

- Each handler tries to solve the problem.
- If it fails, it calls `Next`.
- Stops on first successful handler.

---

## 🧠 Why Chain of Responsibility?

- Cleanly supports **multiple strategies** (greedy, backtracking, etc.)
- Enables easy extension (e.g., plug in GeneticAlgorithmHandler)
- Keeps services loosely coupled

---

## 🚦 API Design

Implemented using ASP.NET Core Minimal APIs:

| Route        | Method | Body                      | Output                |
|--------------|--------|---------------------------|------------------------|
| `/api/color` | POST   | `{ matrix, maxColors }`   | `ColoringResult` object |

### ColoringResult shape:

```json
{
  "colors": [0, 1, 2, 0],
  "algorithmUsed": "DSatur",
  "success": true,
  "errorMessage": null
}
```

---

## 📦 Key Classes

- `ColoringService.cs` — entry point, wires the handler chain
- `IColoringHandler` — port interface for algorithms
- `DsatHandler.cs` — greedy DSatur coloring
- `BacktrackingHandler.cs` — recursive exact coloring

---

## ✅ Benefits

- Highly testable (via interface-based design)
- Extendable (add/remove strategies easily)
- Aligned with clean/hexagonal architecture principles