# 🎨 Coloring Algorithms – Four-Color Graph Visualizer

This document outlines the two main algorithms used to solve the Four-Color problem in this application.

---

## 🎯 Problem Statement

> Color each vertex of a graph so that no two adjacent vertices share the same color, using as few colors as possible.  
> For planar graphs, **4 colors are always sufficient** (Four Color Theorem).

---

## 1️⃣ DSatur (Degree of Saturation) – Greedy Heuristic

### How it works:

1. Pick the uncolored node with the **highest saturation** (number of different colors among its neighbors)
2. Assign it the **lowest available color**
3. Update saturation for its neighbors
4. Repeat until all nodes are colored

### Complexity:
- Time: **O(V²)** in naive implementation

### Pros:
- ✅ Very fast
- ✅ Works well on sparse graphs

### Cons:
- ❌ Might fail even if a valid 4-coloring exists
- ❌ Doesn't guarantee optimal solution

---

## 2️⃣ Backtracking – Exact Search

### How it works:

Recursive Depth-First Search:

```text
TryColor(v):
    for color in [0..3]:
        if safe:
            assign color
            recurse on next vertex
            backtrack if needed
```

### Complexity:
- Time: **Exponential** in worst-case

### Pros:
- ✅ Always finds a solution if one exists
- ✅ Guaranteed to work under Four Color Theorem

### Cons:
- ❌ Much slower than DSatur
- ❌ Poor scalability for large dense graphs

---

## 🤝 Hybrid with Chain of Responsibility

We combine both algorithms using the **Chain of Responsibility** pattern:

```text
Request → DSatur → (if failed) → Backtracking → (if failed) → null
```

This gives:
- ✅ Fast result when DSatur works
- ✅ Guaranteed fallback if DSatur fails

---

## 🧪 Example: K₄ Graph

Adjacency Matrix:

```
0 1 1 1
1 0 1 1
1 1 0 1
1 1 1 0
```

- This graph **requires 4 colors**
- DSatur will succeed here
- Backtracking won't be called

---

## 🛠️ Future Extensions

- Genetic algorithms
- SAT solver integration
- Constraint propagation strategies

The architecture supports easy plugging of new handlers into the chain.