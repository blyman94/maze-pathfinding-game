using NUnit.Framework;

/// <summary>
/// A collection of unit tests for the DisjointSet class.
/// </summary>
public class DisjointSetTest
{
    #region Find Tests
    [Test]
    public void Find_ParentOfXisX_ReturnsX()
    {
        CellGrid grid = new CellGrid(2, 2);
        Cell subject = grid.Cells[0];
        DisjointSet dSet = new DisjointSet(grid.Cells);
        // When a DisjointSet is initialized, each element of the passed list is
        // its own parent.
        Assert.AreEqual(subject, dSet.Find(subject));
    }

    [Test]
    public void Find_ParentOfXisY_ReturnsY()
    {
        CellGrid grid = new CellGrid(2, 2);
        Cell subjectX = grid.Cells[0];
        Cell subjectY = grid.Cells[1];
        DisjointSet dSet = new DisjointSet(grid.Cells);
        dSet.Union(subjectY, subjectX);

        Assert.AreEqual(subjectY, dSet.Find(subjectX));
    }
    #endregion

    #region Union Tests
    [Test]
    public void Union_ParentOfXisXBeforeAndYAfter_ReturnsXBeforeAndYAfter()
    {
        CellGrid grid = new CellGrid(2, 2);
        Cell subjectX = grid.Cells[0];
        Cell subjectY = grid.Cells[1];
        DisjointSet dSet = new DisjointSet(grid.Cells);
        // When a DisjointSet is initialized, each element of the passed list is
        // its own parent.
        Assert.AreEqual(subjectX, dSet.Find(subjectX));
        dSet.Union(subjectY, subjectX);
        Assert.AreEqual(subjectY, dSet.Find(subjectX));
    }
    #endregion
}
