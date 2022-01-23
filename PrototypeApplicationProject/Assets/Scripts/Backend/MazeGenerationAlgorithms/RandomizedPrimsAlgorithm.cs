using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A method of maze generation that selects random cells from a list of cells
/// adjacent to the existing maze, and randomly adds them to the maze.
/// </summary>
[CreateAssetMenu(menuName =
    "Maze Generation Algorithm.../Randomized Prim's Algorithm",
    fileName = "RandomizedPrimsAlgorithm")]
public class RandomizedPrimsAlgorithm : MazeGenerationAlgorithm,
    IMazeGenerationAlgorithm
{
    /// <summary>
    /// The list of potential next cells for the algorithm to evaluate.
    /// </summary>
    private List<Cell> availableCells;

    /// <summary>
    /// The previous cell this algorithm has evaluated.
    /// </summary>
    private Cell previous;

    public void Execute(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            current = cellGrid.Cells[0];
            previous = null;
            current.VisitCount++;

            availableCells = new List<Cell>();
            availableCells.AddRange(current.GetNeighbors(visitedState: false));
        }

        while (availableCells.Count > 0)
        {
            if (previous != null)
            {
                previous.Active = false;
            }

            current = availableCells[Random.Range(0, availableCells.Count)];
            current.Active = true;

            List<Cell> visitedNeighbors =
                current.GetNeighbors(visitedState: true);

            int wallRemoveIndex = Random.Range(0, visitedNeighbors.Count);
            current.RemoveWall(visitedNeighbors[wallRemoveIndex]);
            current.VisitCount++;

            foreach (Cell unvisitedNeighbor in
                current.GetNeighbors(visitedState: false))
            {
                if (!availableCells.Contains(unvisitedNeighbor))
                {
                    availableCells.Add(unvisitedNeighbor);
                }
            }

            availableCells.Remove(current);
            previous = current;
            iterations++;
        }
    }

    public bool ExecuteIterative(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            current = cellGrid.Cells[0];
            previous = null;
            current.VisitCount++;

            availableCells = new List<Cell>();
            availableCells.AddRange(current.GetNeighbors(visitedState: false));
        }

        if (availableCells.Count > 0)
        {
            if (previous != null)
            {
                previous.Active = false;
            }

            current = availableCells[Random.Range(0, availableCells.Count)];
            current.Active = true;

            List<Cell> visitedNeighbors =
                current.GetNeighbors(visitedState: true);

            int wallRemoveIndex = Random.Range(0, visitedNeighbors.Count);
            current.RemoveWall(visitedNeighbors[wallRemoveIndex]);
            current.VisitCount++;

            foreach (Cell unvisitedNeighbor in
                current.GetNeighbors(visitedState: false))
            {
                if (!availableCells.Contains(unvisitedNeighbor))
                {
                    availableCells.Add(unvisitedNeighbor);
                }
            }

            availableCells.Remove(current);
            iterations++;
            previous = current;
            return false;
        }
        else
        {
            current.Active = false;
            return true;
        }
    }
}
