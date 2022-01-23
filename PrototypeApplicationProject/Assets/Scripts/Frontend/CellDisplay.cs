using UnityEngine;

/// <summary>
/// Observes a Cell object and changes its own appearance based on the state of
/// the observed cell.
/// </summary>
public class CellDisplay : MonoBehaviour
{
    /// <summary>
    /// Color of the cell display fill when the cell is actively being evaluated 
    /// by the algorithm.
    /// </summary>
    [Header("Colors")]
    [Tooltip("Color of the cell display fill when the cell is actively being " +
        "evaluated by the algorithm.")]
    [SerializeField] private Color activeColor;

    /// <summary>
    /// Color of the cell display fill when the cell has been revisited by an 
    /// algorithm.
    /// </summary>
    [Tooltip("Color of the cell display fill when the cell has been " +
        "revisited by an algorithm.")]
    [SerializeField] private Color revisitedColor;

    /// <summary>
    /// Color of the cell display fill when the cell has been visited by an 
    /// algorithm.
    /// </summary>
    [Tooltip("Color of the cell display fill when the cell has been " +
        " visited by an algorithm.")]
    [SerializeField] private Color visitedColor;

    /// <summary>
    /// Color of the cell wall display when the cell wall is active.
    /// </summary>
    [Tooltip("Color of the cell wall display when the cell wall is active.")]
    [SerializeField] private Color wallColorShown;

    /// <summary>
    /// Sprite renderer representing the area contained by the cell.
    /// </summary>
    [Header("Renderers")]
    [Tooltip("Sprite renderer representing the area contained by the cell.")]
    [SerializeField] private SpriteRenderer fillRenderer;

    /// <summary>
    /// Sprite renderer representing the east wall of the cell.
    /// </summary>
    [Tooltip("Sprite renderer representing the east wall of the cell.")]
    [SerializeField] private SpriteRenderer eastWallRenderer;

    /// <summary>
    /// Sprite renderer representing the north wall of the cell.
    /// </summary>
    [Tooltip("Sprite renderer representing the north wall of the cell.")]
    [SerializeField] private SpriteRenderer northWallRenderer;

    /// <summary>
    /// Sprite renderer representing the south wall of the cell.
    /// </summary>
    [Tooltip("Sprite renderer representing the south wall of the cell.")]
    [SerializeField] private SpriteRenderer southWallRenderer;

    /// <summary>
    /// Sprite renderer representing the west wall of the cell.
    /// </summary>
    [Tooltip("Sprite renderer representing the west wall of the cell.")]
    [SerializeField] private SpriteRenderer westWallRenderer;

    /// <summary>
    /// Underlying cell object driving changes in this display's appearance.
    /// </summary>
    private Cell cell;

    /// <summary>
    /// Underlying cell object driving changes in this display's appearance.
    /// </summary>
    public Cell Cell
    {
        get
        {
            return cell;
        }
        set
        {
            cell = value;

            PositionCell();
            UpdateFillColor();
            UpdateWallColor();

            cell.CellStateUpdated += UpdateFillColor;
            cell.CellWallsUpdated += UpdateWallColor;
        }
    }

    #region MonoBehaviour Methods
    private void OnDisable()
    {
        if (Cell != null)
        {
            cell.CellStateUpdated -= UpdateFillColor;
            Cell.CellWallsUpdated -= UpdateWallColor;
        }
    }
    #endregion

    /// <summary>
    /// Positions the CellDisplay based on the X and Y coordinates of the 
    /// underlying cell object.
    /// </summary>
    public void PositionCell()
    {
        transform.position = new Vector3(cell.X + 0.5f, cell.Y + 0.5f, 0.0f);
    }

    /// <summary>
    /// Responds to changes in cell state by changing the color of the
    /// CellDisplay's area.
    /// </summary>
    private void UpdateFillColor()
    {
        if (!cell.IsPathCell)
        {
            if (cell.DistanceRatio == -1)
            {
                if (cell.Active)
                {
                    fillRenderer.color = activeColor;
                    return;
                }

                if (cell.VisitCount == 1)
                {
                    fillRenderer.color = visitedColor;
                }
                if (cell.VisitCount == 2)
                {
                    fillRenderer.color = revisitedColor;
                }
            }
            else
            {
                fillRenderer.color =
                    Color.Lerp(Color.green, Color.red, cell.DistanceRatio);
            }
        }
        else
        {
            fillRenderer.color = Color.cyan;
        }
    }

    /// <summary>
    /// Responds to changes in the state of the cell's walls by changing the 
    /// color of each wall.
    /// </summary>
    private void UpdateWallColor()
    {
        eastWallRenderer.color = cell.East ? wallColorShown : visitedColor;
        eastWallRenderer.sortingOrder = cell.East ? 3 : 0;

        northWallRenderer.color = cell.North ? wallColorShown : visitedColor;
        northWallRenderer.sortingOrder = cell.North ? 3 : 0;

        southWallRenderer.color = cell.South ? wallColorShown : visitedColor;
        southWallRenderer.sortingOrder = cell.South ? 3 : 0;

        westWallRenderer.color = cell.West ? wallColorShown : visitedColor;
        westWallRenderer.sortingOrder = cell.West ? 3 : 0;
    }
}
