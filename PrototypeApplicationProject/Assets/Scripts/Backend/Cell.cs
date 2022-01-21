using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing a location in 2D space, with an X and Y coordinate and 
/// four walls. Stores information about itself when parsed by a maze generation
/// or pathfinding algorithm.
/// </summary>
public class Cell
{
    /// <summary>
    /// Delegate to signal when the state of this cell's walls has changed.
    /// </summary>
    public CellWallsUpdated CellWallsUpdated;

    /// <summary>
    /// Delegate to signal when the state of this cell has changed.
    /// </summary>
    public CellStateUpdated CellStateUpdated;

    /// <summary>
    /// The distance of the cell from a designated start node.
    /// </summary>
    public int GCost { get; set; }

    /// <summary>
    /// The Manhattan distance of the cell from a designated desination node.
    /// </summary>
    public int HCost { get; set; }

    /// <summary>
    /// The previous cell to this cell in a pathfinding algorithm.
    /// </summary>
    public Cell PreviousCell { get; set; }

    /// <summary>
    /// CellGrid this cell belongs to.
    /// </summary>
    public CellGrid Grid;

    /// <summary>
    /// X coordinate of the cell on the CellGrid.
    /// </summary>
    public int X;

    /// <summary>
    /// Y coordinate of the cell on the CellGrid.
    /// </summary>
    public int Y;

    /// <summary>
    /// Bool to determine if the cell is actively being considered by an
    /// algorithm.
    /// </summary>
    private bool active;

    /// <summary>
    /// Ratio of the cell's manhattan distance from a designated destination 
    /// cell and the manhattan distance of a designated start cell from a 
    /// destination cell. Used to update the cell visually during pathfinding.
    /// </summary>
    private float distanceRatio;

    /// <summary>
    /// Bool representing the state of the east wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    private bool east;

    /// <summary>
    /// Bool representing the state of the north wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    private bool north;

    /// <summary>
    /// Bool representing if the cell is a part of the final path calculated by
    /// a pathfinding algorithm.
    /// </summary>
    private bool isPathCell;

    /// <summary>
    /// Bool representing the state of the south wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    private bool south;

    /// <summary>
    /// Number of times the cell has been visited by a maze generation 
    /// algorithm.
    /// </summary>
    private int visitCount;

    /// <summary>
    /// Bool representing the state of the west wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    private bool west;

    #region Properties
    /// <summary>
    /// Bool to determine if the cell is actively being considered by the 
    /// algorithm.
    /// </summary>
    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
            CellStateUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Ratio of the cell's manhattan distance from a designated destination 
    /// cell and the manhattan distance of a designated start cell from a 
    /// destination cell. Used to update the cell visually during pathfinding.
    /// </summary>
    public float DistanceRatio
    {
        get
        {
            return distanceRatio;
        }
        set
        {
            distanceRatio = value;
            CellStateUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Bool representing the state of the east wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    public bool East
    {
        get
        {
            return east;
        }
        set
        {
            east = value;
            CellWallsUpdated?.Invoke();
        }
    }

    /// <summary>
    /// The sum of the cell's distance from a designated start node and the 
    /// heuristic distance from a designated destination node.
    /// </summary>
    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }

    /// <summary>
    /// Bool representing the state of the north wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    public bool North
    {
        get
        {
            return north;
        }
        set
        {
            north = value;
            CellWallsUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Bool representing if the cell is a part of the final path calculated by
    /// a pathfinding algorithm.
    /// </summary>
    public bool IsPathCell
    {
        get
        {
            return isPathCell;
        }
        set
        {
            isPathCell = value;
            CellStateUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Bool representing the state of the south wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    public bool South
    {
        get
        {
            return south;
        }
        set
        {
            south = value;
            CellWallsUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Number of times the cell has been visited by a maze generation 
    /// algorithm.
    /// </summary>
    public int VisitCount
    {
        get
        {
            return visitCount;
        }
        set
        {
            visitCount = value;
            CellStateUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Bool representing the state of the west wall. True represents an active 
    /// wall, false represents an inactive wall.
    /// </summary>
    public bool West
    {
        get
        {
            return west;
        }
        set
        {
            west = value;
            CellWallsUpdated?.Invoke();
        }
    }
    #endregion

    /// <summary>
    /// Constructor for the Cell object. Each cell must be initialized with an 
    /// X and Y coordinate, as well as a reference to the grid to which the 
    /// cell belongs.
    /// </summary>
    /// <param name="x">The X coordinate of the cell in the grid.</param>
    /// <param name="y">The Y coordinate of the cell in teh grid.</param>
    /// <param name="grid">The grid to which the cell belongs.</param>
    public Cell(int x, int y, CellGrid grid)
    {
        // Gridspace Data
        X = x;
        Y = y;
        this.Grid = grid;

        // Walls
        east = true;
        north = true;
        south = true;
        west = true;

        // Maze Generation
        active = false;
        visitCount = 0;

        // Pathfinding
        DistanceRatio = -1;
        GCost = 0;
        HCost = 0;
        IsPathCell = false;
        PreviousCell = null;
    }

    /// <summary>
    /// Gets all neighbors of the cell by calculating their position in each of
    /// the four directions. Edge cells have fewer than four neighbors.
    /// </summary>
    /// <returns>A list of cells representing all of this cell's 
    /// neighbors.</returns>
    public List<Cell> GetNeighbors()
    {
        List<Cell> neighbors = new List<Cell>();

        Cell east = Index(X + 1, Y) == -1 ? null : Grid.Cells[Index(X + 1, Y)];
        Cell north = Index(X, Y + 1) == -1 ? null : Grid.Cells[Index(X, Y + 1)];
        Cell south = Index(X, Y - 1) == -1 ? null : Grid.Cells[Index(X, Y - 1)];
        Cell west = Index(X - 1, Y) == -1 ? null : Grid.Cells[Index(X - 1, Y)];

        if (east != null)
        {
            neighbors.Add(east);
        }
        if (north != null)
        {
            neighbors.Add(north);
        }
        if (south != null)
        {
            neighbors.Add(south);
        }
        if (west != null)
        {
            neighbors.Add(west);
        }

        return neighbors;
    }

    /// <summary>
    /// Overload for the GetNeighbors function that allows for retrieval of
    /// neighbors that have or have not been visited.
    /// </summary>
    /// <param name="visitedState">The state of the neighbors to be returned.
    /// True if only visited neighbors should be retrieved, false if only
    /// unvisited neighbors should be retrieved.</param>
    /// <returns>List of cells that are neighbors of this cell that also meet 
    /// the passed visitedState criteria.</returns>
    public List<Cell> GetNeighbors(bool visitedState)
    {
        List<Cell> neighborsOfState = new List<Cell>();
        List<Cell> neighbors = GetNeighbors();

        if (visitedState)
        {
            foreach (Cell neighbor in neighbors)
            {
                if (neighbor.VisitCount > 0)
                {
                    neighborsOfState.Add(neighbor);
                }
            }
        }
        else
        {
            foreach (Cell neighbor in neighbors)
            {
                if (neighbor.VisitCount <= 0)
                {
                    neighborsOfState.Add(neighbor);
                }
            }
        }

        return neighborsOfState;
    }

    /// <summary>
    /// Gets a list of traversable neighbors of this cell. A neighbor is
    /// considered traversable if and only if the wall between it and this cell
    /// is inactive.
    /// </summary>
    /// <returns>A list of cells representing traversable neighbors.</returns>
    public List<Cell> GetTraversableNeighbors()
    {
        List<Cell> traversableNeighbors = new List<Cell>();

        Cell east = Index(X + 1, Y) == -1 ? null : Grid.Cells[Index(X + 1, Y)];
        Cell north = Index(X, Y + 1) == -1 ? null : Grid.Cells[Index(X, Y + 1)];
        Cell south = Index(X, Y - 1) == -1 ? null : Grid.Cells[Index(X, Y - 1)];
        Cell west = Index(X - 1, Y) == -1 ? null : Grid.Cells[Index(X - 1, Y)];

        if (east != null && !East)
        {
            traversableNeighbors.Add(east);
        }
        if (north != null && !North)
        {
            traversableNeighbors.Add(north);
        }
        if (south != null && !South)
        {
            traversableNeighbors.Add(south);
        }
        if (west != null && !West)
        {
            traversableNeighbors.Add(west);
        }

        return traversableNeighbors;
    }

    /// <summary>
    /// Returns the Manhattan distance between this cell and a passed
    /// destination cell.
    /// </summary>
    /// <param name="destinationCell">The cell to which the manhattan distance
    /// will be calculated.</param>
    /// <returns>The Manhattan distance between this cell and a passed
    /// destination cell.</returns>
    public int GetManhattanDistance(Cell destinationCell)
    {
        return Mathf.Abs(X - destinationCell.X) + 
            Mathf.Abs(Y - destinationCell.Y);
    }

    /// <summary>
    /// Removes the wall between this cell and an adjacent cell.
    /// </summary>
    /// <param name="adjacentCell">A cell neighboring this cell. The wall
    /// between this cell and the passed cell will be removed.</param>
    public void RemoveWall(Cell adjacentCell)
    {
        int dX = X - adjacentCell.X;
        if (dX == 1)
        {
            // next is west of this cell.
            West = false;
            adjacentCell.East = false;
        }
        else if (dX == -1)
        {
            // next is east of this cell.
            East = false;
            adjacentCell.West = false;
        }

        int dY = Y - adjacentCell.Y;
        if (dY == 1)
        {
            // next is south of this cell.
            South = false;
            adjacentCell.North = false;
        }
        else if (dY == -1)
        {
            // next is north of this cell.
            North = false;
            adjacentCell.South = false;
        }
    }

    /// <summary>
    /// Converts x and y coordinates to a one-dimensional index to access a 
    /// cell in a grid where the grid is stored as an array.
    /// </summary>
    /// <param name="x">The X coordinate of the cell in 2D space.</param>
    /// <param name="y">The Y coordinate of the cell in 2D space.</param>
    /// <returns>Int representing the index of the passed coordinates in 1D
    /// space.</returns>
    private int Index(int x, int y)
    {
        if (x < 0 || y < 0 || x > Grid.Columns - 1 || y > Grid.Rows - 1)
        {
            return -1;
        }

        return x + (y * Grid.Columns);
    }
}
