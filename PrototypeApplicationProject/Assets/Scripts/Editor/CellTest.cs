using System.Collections.Generic;
using NUnit.Framework;

/// <summary>
/// A collection of unit tests for the Cell class.
/// </summary>
public class CellTest
{
    #region GetNeighbors Tests
    [Test]
    public void GetNeighbors_CornerCell_ReturnsEastAndNorthNeighbors()
    {
        CellGrid cellGrid = new CellGrid(2, 2);
        Cell subject = cellGrid.Cells[0];

        List<Cell> expected = new List<Cell>()
        {
            cellGrid.Cells[1], // east
            cellGrid.Cells[2]  // north
        };

        Assert.AreEqual(expected, subject.GetNeighbors());
    }
    [Test]
    public void GetNeighbors_EdgeCell_ReturnsEastNorthWestNeighbors()
    {
        CellGrid cellGrid = new CellGrid(3, 2);
        Cell subject = cellGrid.Cells[1];

        List<Cell> expected = new List<Cell>()
        {
            cellGrid.Cells[2], // east
            cellGrid.Cells[4], // north
            cellGrid.Cells[0]  // west
        };

        Assert.AreEqual(expected, subject.GetNeighbors());
    }
    [Test]
    public void GetNeighbors_MiddleCell_ReturnsEastNorthSouthWestNeighbors()
    {
        CellGrid cellGrid = new CellGrid(3, 3);
        Cell subject = cellGrid.Cells[4];

        List<Cell> expected = new List<Cell>()
        {
            cellGrid.Cells[5], // east
            cellGrid.Cells[7], // north
            cellGrid.Cells[1], // south
            cellGrid.Cells[3]  // west
        };

        Assert.AreEqual(expected, subject.GetNeighbors());
    }

    [Test]
    public void GetNeighbors_VisitedFalse_ReturnsUnvisitedNeighbors()
    {
        CellGrid cellGrid = new CellGrid(2, 2);
        Cell subject = cellGrid.Cells[0];
        cellGrid.Cells[1].VisitCount++;

        List<Cell> expected = new List<Cell>()
        {
            cellGrid.Cells[2] // north, east neighbor should not be returned.
        };

        Assert.AreEqual(expected, subject.GetNeighbors(visitedState: false));
    }

    [Test]
    public void GetNeighbors_VisitedTrue_ReturnsVisitedNeighbors()
    {
        CellGrid cellGrid = new CellGrid(2, 2);
        Cell subject = cellGrid.Cells[0];
        cellGrid.Cells[1].VisitCount++;

        List<Cell> expected = new List<Cell>()
        {
            cellGrid.Cells[1] // east, north neighbor should not be returned.
        };

        Assert.AreEqual(expected, subject.GetNeighbors(visitedState: true));
    }
    #endregion

    #region GetTraversableNeighbors Tests
    [Test]
    public void GetTraversableNeighbors_EastNeighborTraversable_ReturnsEastNeighbor()
    {
        CellGrid cellGrid = new CellGrid(2, 2);
        Cell subject = cellGrid.Cells[0];

        subject.RemoveWall(cellGrid.Cells[1]);

        List<Cell> expected = new List<Cell>()
        {
            cellGrid.Cells[1] // east, north neighbor should not be returned.
        };

        Assert.AreEqual(expected, subject.GetTraversableNeighbors());
    }
    #endregion

    #region RemoveWall Tests
    [Test]
    public void RemoveWall_EastNeighborPassed_EastWallofSubjectAndWestWallOfTargetRemoved()
    {
        CellGrid cellGrid = new CellGrid(2, 2);
        Cell subject = cellGrid.Cells[0];

        subject.RemoveWall(cellGrid.Cells[1]);

        Assert.False(subject.East);
        Assert.False(cellGrid.Cells[1].West);
    }
    #endregion
}
