using NUnit.Framework;

/// <summary>
/// A collection of unit tests for the CellGrid class.
/// </summary>
public class CellGridTest
{
    [Test]
    public void PopulateGrid_XandYPassedToConstructor_CellCountIsXtimesY()
    {
        // Populate grid is called by the constructor of CellGrid.
        CellGrid cellGrid = new CellGrid(2, 2);

        Assert.AreEqual(4, cellGrid.Cells.Count);
    }
}
