using System.Collections.Generic;

/// <summary>
/// A 2D collection of cells represented in 1D space.
/// </summary>
public class CellGrid
{
    /// <summary>
    /// 1D representation of the cell grid.
    /// </summary>
    public List<Cell> Cells;

    /// <summary>
    /// Cell row count in 2D space.
    /// </summary>
    public int Rows;

    /// <summary>
    /// Cell column count in 2D space.
    /// </summary>
    public int Columns;

    /// <summary>
    /// Constructor for the cell grid object. Each cell grid must have a number
    /// of rows and columns.
    /// </summary>
    /// <param name="columns">Columns in the cell grid.</param>
    /// <param name="rows">Rows in the cell grid.</param>
    public CellGrid(int columns, int rows)
    {
        Rows = rows;
        Columns = columns;
        Cells = new List<Cell>();
        PopulateGrid();
    }

    /// <summary>
    /// Constructs a new cell for each coordinate pair in the grid, then stores
    /// the cell in the 1D representation list.
    /// </summary>
    private void PopulateGrid()
    {
        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Cell cell = new Cell(x, y, this);
                Cells.Add(cell);
            }
        }
    }
}
