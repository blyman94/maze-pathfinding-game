using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pathfinding algorithm that seeks to minimize the distance from the start
/// cell and the estimated distance to the destination cell when pathfinding.
/// </summary>
[CreateAssetMenu(menuName =
    "Pathfinding Algorithm.../AStar Algorithm",
    fileName = "AStarAlgorithm")]
public class AStarAlgorithm : PathfindingAlgorithm, IPathfindingAlgorithm
{
    public void FindPath(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            unvisitedCells = new List<Cell>();
            visitedCells = new List<Cell>();

            start = cellGrid.Cells[0];
            current = start;
            current.DistanceRatio = 1.0f;

            destination = cellGrid.Cells[cellGrid.Cells.Count - 1];
            destination.DistanceRatio = 0.0f;

            maxDistance = current.GetManhattanDistance(destination);
        }

        while (current != destination)
        {
            List<Cell> neighbors = current.GetTraversableNeighbors();
            foreach (Cell neighbor in neighbors)
            {
                if (!(visitedCells.Contains(neighbor)))
                {
                    unvisitedCells.Add(neighbor);
                    neighbor.PreviousCell = current;
                }
            }

            foreach (Cell unvisitedCell in unvisitedCells)
            {
                unvisitedCell.GCost = current.GCost + 1;
                unvisitedCell.HCost =
                    unvisitedCell.GetManhattanDistance(destination);
            }

            if (unvisitedCells.Contains(current))
            {
                unvisitedCells.Remove(current);
            }
            if (!(visitedCells.Contains(current)))
            {
                visitedCells.Add(current);
            }

            current = MinFCost(unvisitedCells);
            current.DistanceRatio =
                    current.GetManhattanDistance(destination) / maxDistance;
            iterations++;
        }
    }

    public bool FindPathIterative(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            unvisitedCells = new List<Cell>();
            visitedCells = new List<Cell>();

            current = cellGrid.Cells[0];
            current.DistanceRatio = 1.0f;

            destination = cellGrid.Cells[cellGrid.Cells.Count - 1];
            destination.DistanceRatio = 0.0f;

            maxDistance = current.GetManhattanDistance(destination);
        }

        List<Cell> neighbors = current.GetTraversableNeighbors();
        foreach (Cell neighbor in neighbors)
        {
            if (!(visitedCells.Contains(neighbor)))
            {
                unvisitedCells.Add(neighbor);
                neighbor.PreviousCell = current;
            }
        }

        foreach (Cell unvisitedCell in unvisitedCells)
        {
            unvisitedCell.GCost = current.GCost + 1;
            unvisitedCell.HCost =
                unvisitedCell.GetManhattanDistance(destination);
        }

        if (unvisitedCells.Contains(current))
        {
            unvisitedCells.Remove(current);
        }
        if (!(visitedCells.Contains(current)))
        {
            visitedCells.Add(current);
        }

        current = MinFCost(unvisitedCells);
        current.DistanceRatio =
                current.GetManhattanDistance(destination) / maxDistance;
        if (current == destination)
        {
            return true;
        }
        else
        {
            iterations++;
            return false;
        }
    }

    /// <summary>
    /// Returns the cell with the minimum GCost from a list of cells.
    /// </summary>
    /// <param name="cells">List of cells from which to determine the minimum
    /// GCost.</param>
    /// <returns>The cell with the minimum GCost from the passed list.</returns>
    private Cell MinFCost(List<Cell> cells)
    {
        Cell shortestCell = null;
        foreach (Cell cell in unvisitedCells)
        {
            if (shortestCell != null)
            {
                if (cell.FCost < shortestCell.FCost)
                {
                    shortestCell = cell;
                }
            }
            else
            {
                shortestCell = cell;
            }
        }
        return shortestCell;
    }
}
