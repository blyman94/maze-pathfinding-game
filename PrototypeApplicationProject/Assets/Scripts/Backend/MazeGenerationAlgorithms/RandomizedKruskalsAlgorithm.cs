using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A method of maze generation that randomly selects a cell in a cell grid and
/// adds its neighbors to a set. Once all cells in the grid are members of the 
/// same set, the algorithm is finished.
/// </summary>
[CreateAssetMenu(menuName =
    "Maze Generation Algorithm.../Randomized Kruskal's Algorithm",
    fileName = "RandomizedKruskalsAlgorithm")]
public class RandomizedKruskalsAlgorithm : MazeGenerationAlgorithm,
    IMazeGenerationAlgorithm
{
    /// <summary>
    /// Disjoint Set data structure used to determine if cells are members of
    /// the same set.
    /// </summary>
    private DisjointSet disjointSet;

    /// <summary>
    /// Randomized order of cells through which the algorithm will traverse.
    /// </summary>
    private List<Cell> randomizedCellList;

    /// <summary>
    /// Random number generator for Cell list randomization efforts.
    /// </summary>
    private System.Random rnd;

    public void Execute(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            current = cellGrid.Cells[0];
            current.VisitCount++;
            current.Active = true;

            rnd = new System.Random();

            disjointSet = new DisjointSet(cellGrid.Cells);
            randomizedCellList = 
                cellGrid.Cells.OrderBy(item => rnd.Next()).ToList();
        }

        foreach (Cell cell in randomizedCellList)
        {
            cell.VisitCount++;
            List<Cell> neighbors = cell.GetNeighbors();

            foreach (Cell neighbor in neighbors)
            {
                if (disjointSet.Find(cell) != disjointSet.Find(neighbor))
                {
                    cell.RemoveWall(neighbor);
                    disjointSet.Union(cell, neighbor);
                    neighbor.VisitCount++;
                }
            }
            iterations++;
        }
    }

    public bool ExecuteIterative(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            current = cellGrid.Cells[0];
            current.VisitCount++;
            current.Active = true;

            rnd = new System.Random();

            disjointSet = new DisjointSet(cellGrid.Cells);
            randomizedCellList =
                cellGrid.Cells.OrderBy(item => rnd.Next()).ToList();
        }

        Cell cell = randomizedCellList[iterations];
        cell.VisitCount++;
        List<Cell> neighbors = cell.GetNeighbors();

        foreach (Cell neighbor in neighbors)
        {
            if (disjointSet.Find(cell) != disjointSet.Find(neighbor))
            {
                cell.RemoveWall(neighbor);
                disjointSet.Union(cell, neighbor);
                neighbor.VisitCount++;
            }
        }

        iterations++;

        if (iterations > randomizedCellList.Count - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
