using System.Collections.Generic;

/// <summary>
/// Describes the behaviour of a pathfinding algorithm that can find the unique 
/// path between a start cell and a destination cell on a grid of cells.
/// </summary>
public interface IPathfindingAlgorithm
{
    /// <summary>
    /// Executes the pathfinding algorithm to find the path. Should only be 
    /// called once when used.
    /// </summary>
    void FindPath(CellGrid cellGrid);

    /// <summary>
    /// Executes the pathfinding algorithm to find the path. Uses a
    /// boolean return value to suspend iteration until the next call. Should 
    /// be called many times (during an Update loop, for example) when used.
    /// </summary>
    bool FindPathIterative(CellGrid cellGrid);

    /// <summary>
    /// Returns a stack representing the shortest path, with the start cell
    /// at the top of the stack and the destination at the bottom of the stack.
    /// </summary>
    /// <returns>A stack of cells representing the shortest path from the start
    /// cell to the destination cell.</returns>
    Stack<Cell> GetPathResult();

    /// <summary>
    /// Resets the algorithm to its initial state.
    /// </summary>
    void Reset();

    /// <summary>
    /// Traverses the path backward from the destination node, marking each
    /// cell it visits as part of the final path.
    /// </summary>
    bool TraversePathIterative();
}
