export function isValidMatrix(matrix: number[][]): boolean {
    return Array.isArray(matrix) &&
        matrix.every(
            (row) =>
                Array.isArray(row) &&
                row.length === matrix.length &&
                row.every((cell) => typeof cell === "number")
        );
}
