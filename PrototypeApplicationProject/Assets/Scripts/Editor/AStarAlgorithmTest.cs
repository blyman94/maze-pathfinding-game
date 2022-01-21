using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// A collection of unit tests for the AStarAlgorithm class. Note that the 
/// Execute and ExecuteIterative methods share identical logic, and thus only 
/// one method needs to be unit tested.
/// </summary>
public class AStarAlgorithmTest
{
    #region Execute Tests
    [Test]
    public void Execute_SmallMaze_ShortestPathReturned()
    {
        // Generate the short maze
        CellGrid cellGrid = new CellGrid(5, 5);

        GenerateSmallMaze(cellGrid);

        // Construct expected result
        Stack<Cell> expected = DefineExpectedShortestPath(cellGrid);

        // Conduct the pathfinding algorithm
        AStarAlgorithm algorithm =
            ScriptableObject.CreateInstance<AStarAlgorithm>();
        algorithm.FindPath(cellGrid);
        Stack<Cell> actual = algorithm.GetPathResult();

        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// Helper method that explicitly places the cells constituting the shortest
    /// path of the test maze into a stack.
    /// </summary>
    /// <param name="cellGrid">The grid upon which the maze is 
    /// generated.</param>
    /// <returns>A stack of cells representing the shortest path to complete the
    /// maze.</returns>
    private static Stack<Cell> DefineExpectedShortestPath(CellGrid cellGrid)
    {
        Stack<Cell> expected = new Stack<Cell>();
        expected.Push(cellGrid.Cells[cellGrid.Cells.Count - 1]);
        expected.Push(cellGrid.Cells[19]);
        expected.Push(cellGrid.Cells[18]);
        expected.Push(cellGrid.Cells[23]);
        expected.Push(cellGrid.Cells[22]);
        expected.Push(cellGrid.Cells[17]);
        expected.Push(cellGrid.Cells[12]);
        expected.Push(cellGrid.Cells[11]);
        expected.Push(cellGrid.Cells[10]);
        expected.Push(cellGrid.Cells[5]);
        expected.Push(cellGrid.Cells[6]);
        expected.Push(cellGrid.Cells[7]);
        expected.Push(cellGrid.Cells[2]);
        expected.Push(cellGrid.Cells[1]);
        expected.Push(cellGrid.Cells[0]);
        return expected;
    }

    /// <summary>
    /// Helper method that generates a maze with a known shortest path.
    /// </summary>
    /// <param name="cellGrid">The cell grid upon which the maze is 
    /// generated.</param>
    private static void GenerateSmallMaze(CellGrid cellGrid)
    {
        // Generate the maze by removing specific walls.
        cellGrid.Cells[0].RemoveWall(cellGrid.Cells[1]);

        cellGrid.Cells[1].RemoveWall(cellGrid.Cells[2]);

        cellGrid.Cells[2].RemoveWall(cellGrid.Cells[7]);

        cellGrid.Cells[3].RemoveWall(cellGrid.Cells[4]);
        cellGrid.Cells[3].RemoveWall(cellGrid.Cells[8]);

        cellGrid.Cells[4].RemoveWall(cellGrid.Cells[9]);

        cellGrid.Cells[5].RemoveWall(cellGrid.Cells[6]);
        cellGrid.Cells[5].RemoveWall(cellGrid.Cells[10]);

        cellGrid.Cells[6].RemoveWall(cellGrid.Cells[7]);

        cellGrid.Cells[8].RemoveWall(cellGrid.Cells[13]);

        cellGrid.Cells[10].RemoveWall(cellGrid.Cells[11]);

        cellGrid.Cells[11].RemoveWall(cellGrid.Cells[12]);

        cellGrid.Cells[12].RemoveWall(cellGrid.Cells[17]);

        cellGrid.Cells[13].RemoveWall(cellGrid.Cells[14]);

        cellGrid.Cells[14].RemoveWall(cellGrid.Cells[19]);

        cellGrid.Cells[15].RemoveWall(cellGrid.Cells[16]);
        cellGrid.Cells[15].RemoveWall(cellGrid.Cells[20]);

        cellGrid.Cells[16].RemoveWall(cellGrid.Cells[21]);

        cellGrid.Cells[17].RemoveWall(cellGrid.Cells[22]);

        cellGrid.Cells[18].RemoveWall(cellGrid.Cells[23]);
        cellGrid.Cells[18].RemoveWall(cellGrid.Cells[19]);

        cellGrid.Cells[19].RemoveWall(cellGrid.Cells[24]);

        cellGrid.Cells[21].RemoveWall(cellGrid.Cells[22]);

        cellGrid.Cells[22].RemoveWall(cellGrid.Cells[23]);
    }
    #endregion
}