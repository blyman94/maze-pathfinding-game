using System.Collections.Generic;

/// <summary>
/// A data structure that stores a collection of non-overlapping sets as node 
/// trees and provides an efficient manner of determining if any two elements 
/// belong to the same set.
/// </summary>
public class DisjointSet
{
    /// <summary>
    /// Stores the parent-child relationship between two cells in the set. A
    /// cell is considered a "root" if its parent is itself.
    /// </summary>
    private Dictionary<Cell, Cell> parent;

    /// <summary>
    /// Constructor for a disjoint set data structure. Requires a list of cells.
    /// Upon initialization, each cell in the passed list is considered its own
    /// parent.
    /// </summary>
    /// <param name="initialSets">List of cells to be stored in the disjoint set
    /// data structure.</param>
    public DisjointSet(List<Cell> initialSets)
    {
        parent = new Dictionary<Cell, Cell>();
        foreach (Cell cell in initialSets)
        {
            parent[cell] = cell;
        }
    }

    /// <summary>
    /// Returns the "root" of a passed cell x. A cell is considered a "root" if 
    /// its parent is itself. Two cells with the same root are considered
    /// members of the same set. Therefore, this method helps determine if two 
    /// cells are members of the same set.
    /// </summary>
    /// <param name="x">The cell for which the root will be calculated.</param>
    /// <returns>A cell representing the root of the passed cell x.</returns>
    public Cell Find(Cell x)
    {
        if (parent[x] != x)
        {
            return Find(parent[x]);
        }
        else
        {
            return x;
        }
    }

    /// <summary>
    /// Joins the root of two disjoint sets in the data structure, merging them.
    /// </summary>
    /// <param name="x">One cell to be merged.</param>
    /// <param name="y">The other cell to be merged.</param>
    public void Union(Cell x, Cell y)
    {
        parent[Find(y)] = Find(x);
    }
}
