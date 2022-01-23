using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A method of maze generation that executes a random walk around the cell
/// grid, backtracking when it reaches a cell with no unvisited neighbors,
/// until ever cell on the grid has been visited.
/// </summary>
[CreateAssetMenu(menuName =
    "Maze Generation Algorithm.../Randomized Depth-first Search",
    fileName = "RandomizedDepthFirstSearch")]
public class RandomizedDepthFirstSearch : MazeGenerationAlgorithm,
    IMazeGenerationAlgorithm
{
    /// <summary>
    /// The next cell to be evaluated by the algorithm.
    /// </summary>
    private Cell next;

    /// <summary>
    /// The stack of cells that have been traversed thus far by the algorithm.
    /// </summary>
    private Stack<Cell> traversalStack;

    public void Execute(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            current = cellGrid.Cells[0];
            current.VisitCount++;
            current.Active = true;

            traversalStack = new Stack<Cell>();
            traversalStack.Push(this.current);
        }

        while (traversalStack.Count > 0)
        {
            List<Cell> unvisitedNeighbors =
                current.GetNeighbors(visitedState: false);
            if (unvisitedNeighbors.Count > 0)
            {
                next = unvisitedNeighbors[Random.Range(0,
                    unvisitedNeighbors.Count)];
            }
            else
            {
                next = null;
            }

            if (next != null)
            {
                current.RemoveWall(next);
                current.Active = false;

                current = next;

                current.VisitCount++;
                current.Active = true;
                traversalStack.Push(current);
            }
            else
            {
                current.Active = false;

                current = traversalStack.Pop();
                current.Active = true;
                current.VisitCount++;
            }
            iterations++;
        }
        current.Active = false;
    }

    public bool ExecuteIterative(CellGrid cellGrid)
    {
        if (iterations == 0)
        {
            current = cellGrid.Cells[0];
            current.VisitCount++;
            current.Active = true;

            traversalStack = new Stack<Cell>();
            traversalStack.Push(this.current);
        }

        List<Cell> unvisitedNeighbors =
                current.GetNeighbors(visitedState: false);
        if (unvisitedNeighbors.Count > 0)
        {
            next = unvisitedNeighbors[Random.Range(0,
                unvisitedNeighbors.Count)];
        }
        else
        {
            next = null;
        }

        if (next != null)
        {
            current.RemoveWall(next);
            current.Active = false;

            current = next;

            current.VisitCount++;
            current.Active = true;
            traversalStack.Push(current);
            iterations++;
            return false;
        }
        else if (traversalStack.Count > 0)
        {
            current.Active = false;

            current = traversalStack.Pop();
            current.Active = true;
            current.VisitCount++;
            iterations++;
            return false;
        }
        else
        {
            current.Active = false;
            return true;
        }
    }
}
