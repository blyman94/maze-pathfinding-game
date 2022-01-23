using UnityEngine;

/// <summary>
/// Executes a maze generation algorithm on a new cell grid. Uses the iterative
/// execution of the algorithm to show each step of the calculation.
/// </summary>
public class MazeGenerationVisualizer : AlgorithmVisualizer
{
    /// <summary>
    /// Maze generation algorithm to visualize iteratively.
    /// </summary>
    [Tooltip("Maze generation algorithm to visualize iteratively.")]
    [SerializeField] private MazeGenerationAlgorithm algorithmToVisualize;

    /// <summary>
    /// IMazeGenerationAlgorithm representation of the subject algorithm.
    /// </summary>
    private IMazeGenerationAlgorithm mazeGenerationAlgorithm;
    
    #region MonoBehaviour Methods
    private void Start()
    {
        mazeGenerationAlgorithm =
            (IMazeGenerationAlgorithm)algorithmToVisualize;

        cellGrid = new CellGrid(gridColumnCount.Value, gridRowCount.Value);
        cellGridDisplay.CellGrid = cellGrid;
    }
    private void Update()
    {
        if (!algorithmFinished && IsPlaying)
        {
            if (framesPassed >= 60 / framesPerSecond.Value)
            {
                framesPassed = 0;
                algorithmFinished =
                    mazeGenerationAlgorithm.ExecuteIterative(cellGrid);
            }
            framesPassed++;
        }
    }
    #endregion

    #region AlgorithmVisualizer Methods
    public override void StartVisualizer()
    {
        mazeGenerationAlgorithm.Reset();
        IsPlaying = true;
        algorithmFinished = false;

        framesPerSecond.Value = 2;
        framesPassed = 0;
    }
    #endregion
}
