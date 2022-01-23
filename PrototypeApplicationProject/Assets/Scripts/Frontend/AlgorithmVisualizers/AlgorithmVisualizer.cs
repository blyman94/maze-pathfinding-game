using UnityEngine;

/// <summary>
/// Generalization of the Maze and Pathfinder visualizers.
/// </summary>
public abstract class AlgorithmVisualizer : MonoBehaviour
{
    /// <summary>
    /// Determines if the visualizer should be iterating.
    /// </summary>
    public bool IsPlaying { get; set; } = false;

    /// <summary>
    /// CellGridDisplay upon which to visualize the algorithm.
    /// </summary>
    [Tooltip("CellGridDisplay upon which to visualize the algorithm.")]
    [SerializeField] protected CellGridDisplay cellGridDisplay;

    /// <summary>
    /// Integer variable dictating how fast the visualization runs.
    /// </summary>
    [Tooltip("Integer variable dictating the row count of the grid.")]
    [SerializeField] protected IntVariable framesPerSecond;

    /// <summary>
    /// Integer variable dictating the column count of the grid.
    /// </summary>
    [Tooltip("Integer variable dictating the column count of the grid.")]
    [SerializeField] protected IntVariable gridColumnCount;

    /// <summary>
    /// Integer variable dictating the row count of the grid.
    /// </summary>
    [Tooltip("Integer variable dictating the row count of the grid.")]
    [SerializeField] protected IntVariable gridRowCount;

    /// <summary>
    /// Bool signaling that the algorithm has finished.
    /// </summary>
    protected bool algorithmFinished = false;

    /// <summary>
    /// Cell grid upon which the algorithm will be visualized.
    /// </summary>
    protected CellGrid cellGrid;

    /// <summary>
    /// Integer to track how many frames have occured over the execution.
    /// </summary>
    protected int framesPassed;

    /// <summary>
    /// Begins the visualization.
    /// </summary>
    public abstract void StartVisualizer();
}
