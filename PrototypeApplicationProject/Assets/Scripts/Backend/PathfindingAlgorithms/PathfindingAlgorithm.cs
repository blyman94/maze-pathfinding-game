using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class housing methods and data members common to both the
/// Dijkstra's and AStar algorithms for pathfinding.
/// </summary>
public abstract class PathfindingAlgorithm : ScriptableObject
{
    /// <summary>
    /// Cell currently being evaluated by the algorithm.
    /// </summary>
    protected Cell current;

    /// <summary>
    /// The destination cell of the algorithm. The algorithm will end when it
    /// reaches this cell.
    /// </summary>
    protected Cell destination;

    /// <summary>
    /// Number of iterations the algorithm has completed.
    /// </summary>
    protected int iterations;

    /// <summary>
    /// The Manhattan distance between the start cell and the destination cell
    /// used to calculate distance ratios.
    /// </summary>
    protected int maxDistance;

    /// <summary>
    /// The cell the algorithm begins evaluation on.
    /// </summary>
    protected Cell start;

    /// <summary>
    /// List of all cells in the cell grid that have not been visited by the 
    /// algorithm yet.
    /// </summary>
    protected List<Cell> unvisitedCells;

    /// <summary>
    /// List of all cells in the cell grid that have been visited by the
    /// algorithm.
    /// </summary>
    protected List<Cell> visitedCells;

    /// <summary>
    /// Returns a stack representing the shortest path, with the start cell
    /// at the top of the stack and the destination at the bottom of the stack.
    /// </summary>
    /// <returns>A stack of cells representing the shortest path from the start
    /// cell to the destination cell.</returns>
    public Stack<Cell> GetPathResult()
    {
        Stack<Cell> pathStack = new Stack<Cell>();
        pathStack.Push(destination);

        current = destination.PreviousCell;

        while (current != start)
        {
            pathStack.Push(current);
            current = current.PreviousCell;
        }

        pathStack.Push(start);

        return pathStack;
    }

    /// <summary>
    /// Resets the algorithm to its initial state.
    /// </summary>
    public void Reset()
    {
        iterations = 0;
    }

    /// <summary>
    /// Traverses the path backward from the destination node, marking each
    /// cell it visits as part of the final path.
    /// </summary>
    public bool TraversePathIterative()
    {
        if (current != start)
        {
            if (current != destination && current.PreviousCell != start)
            {
                current.IsPathCell = true;
            }
            current = current.PreviousCell;
            return false;
        }
        else
        {
            return true;
        }
    }
}
