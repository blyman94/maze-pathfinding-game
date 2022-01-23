using UnityEngine;

/// <summary>
/// Responsible for ensuring the camera is always centered on the cell grid
/// based on the grid's size.
/// </summary>
public class AlignCameraToGrid : MonoBehaviour
{
    /// <summary>
    /// Camera through which the grid will be viewed.
    /// </summary>
    [Tooltip("Camera through which the grid will be viewed.")]
    [SerializeField] private Camera mainCamera;

    /// <summary>
    /// Integer variable dictating the column count of the grid.
    /// </summary>
    [Tooltip("Integer variable dictating the column count of the grid.")]
    [SerializeField] private IntVariable gridColumnCount;

    /// <summary>
    /// Integer variable dictating the row count of the grid.
    /// </summary>
    [Tooltip("Integer variable dictating the row count of the grid.")]
    [SerializeField] private IntVariable gridRowCount;

    /// <summary>
    /// Number of units around the grid's top and bottom edges.
    /// </summary>
    [Tooltip("Number of units around the grid's top and bottom edges.")]
    [SerializeField] private int borderUnits;

    /// <summary>
    /// Units to shift the camera on the horizontal axis.
    /// </summary>
    [Tooltip("Units to shift the camera on the horizontal axis.")]
    [SerializeField] private float xOffset;

    /// <summary>
    /// Units to shift the camera on the horizontal axis.
    /// </summary>
    [Tooltip("Units to shift the camera on the vertical axis.")]
    [SerializeField] private float yOffset;

    #region MonoBehaviour Methods
    private void Start()
    {
        AlignCamera();
    }
    #endregion

    /// <summary>
    /// Aligns the camera's view to the grid, leaving a border around the top 
    /// and bottom edges of the grid.
    /// </summary>
    private void AlignCamera()
    {
        float cameraSize = gridRowCount.Value * 0.5f;
        mainCamera.orthographicSize = cameraSize + borderUnits;
        mainCamera.transform.position =
            new Vector3(gridColumnCount.Value * 0.5f, cameraSize,
            mainCamera.transform.position.z);

        mainCamera.transform.position =
            new Vector3(mainCamera.transform.position.x + xOffset,
                mainCamera.transform.position.y + yOffset, 
                mainCamera.transform.position.z);
    }
}
