using UnityEngine;

/// <summary>
/// Generates a visual representation of the application's current cell grid.
/// </summary>
public class CellGridDisplay : MonoBehaviour
{
    /// <summary>
    /// Object pooler responsible for pooling frontend CellDisplays.
    /// </summary>
    [Tooltip("Object pooler responsible for pooling frontend CellDisplays.")]
    [SerializeField] private ObjectPooler CellDisplayPooler;

    /// <summary>
    /// Transform to parent CellDisplay instances for organization purposes.
    /// </summary>
    [Tooltip("Transform to parent CellDisplay instances for " + 
        "organization purposes.")]
    [SerializeField] private Transform CellGridParent;

    /// <summary>
    /// CellGrid to be displayed.
    /// </summary>
    private CellGrid cellGrid;

    /// <summary>
    /// CellGrid to be displayed.
    /// </summary>
    public CellGrid CellGrid
    {
        get
        {
            return cellGrid;
        }
        set
        {
            cellGrid = value;
            DrawGrid();
        }
    }

    #region MonoBehaviour Methods
    private void Awake()
    {
        CellDisplayPooler.InitializePool(CellGridParent);
    }
    #endregion

    /// <summary>
    /// Instantiates and activates CellDisplay objects for each cell in the
    /// grid.
    /// </summary>
    public void DrawGrid()
    {
        foreach (Cell cell in cellGrid.Cells)
        {
            GameObject cellDisplayObject = CellDisplayPooler.GetObject();
            CellDisplay cellDisplay = cellDisplayObject.GetComponent<CellDisplay>();
            cellDisplay.Cell = cell;
            cellDisplayObject.SetActive(true);
        }
    }
}
