using UnityEngine;

/// <summary>
/// Generates a maze and executes a pathfinding algorithm on that maze. Uses the 
/// iterative execution of the algorithm to show each step of the calculation.
/// </summary>
public class PathfindingVisualizer : AlgorithmVisualizer
{
    /// <summary>
    /// Pathfinding algorithm to visualize iteratively.
    /// </summary>
    [Tooltip("Pathfinding algorithm to visualize iteratively.")]
    [SerializeField] private PathfindingAlgorithm algorithmToVisualize;

    /// <summary>
    /// Maze generation algorithm to generate a solveable maze.
    /// </summary>
    [Tooltip("Maze generation algorithm to generate a solveable maze.")]
    [SerializeField] private MazeGenerationAlgorithm mazeAlgorithm;

    /// <summary>
    /// IMazeGenerationAlgorithm representation of the algorithm used to
    /// generate the maze.
    /// </summary>
    private IMazeGenerationAlgorithm mazeGenerationAlgorithm;

    /// <summary>
    /// IPathfindingAlgorithm representation of the subject algorithm.
    /// </summary>
    private IPathfindingAlgorithm pathfindingAlgorithm;

    /// <summary>
    /// Determines if the final path has been calculated by the algorithm.
    /// </summary>
    private bool pathFinished = false;

    #region MonoBehaviour Methods
    private void Start()
    {
        pathfindingAlgorithm = (IPathfindingAlgorithm)algorithmToVisualize;
        mazeGenerationAlgorithm = (IMazeGenerationAlgorithm)mazeAlgorithm;

        cellGrid = new CellGrid(gridColumnCount.Value, gridRowCount.Value);
        cellGridDisplay.CellGrid = cellGrid;
        cellGrid.Cells[0].DistanceRatio = 1.0f;
        cellGrid.Cells[cellGrid.Cells.Count - 1].DistanceRatio = 0.0f;

        mazeGenerationAlgorithm.Reset();
        mazeGenerationAlgorithm.Execute(cellGrid);
    }
    private void Update()
    {
        if (IsPlaying)
        {
            if (!algorithmFinished)
            {
                if (framesPassed >= 60 / framesPerSecond.Value)
                {
                    framesPassed = 0;
                    algorithmFinished =
                        pathfindingAlgorithm.FindPathIterative(cellGrid);
                }
                framesPassed++;
            }
            else
            {
                if (framesPassed >= 60 / framesPerSecond.Value)
                {
                    framesPassed = 0;
                    pathFinished = pathfindingAlgorithm.TraversePathIterative();
                }
                framesPassed++;
            }
        }

    }
    #endregion

    #region AlgorithmVisualizer Methods
    public override void StartVisualizer()
    {
        pathfindingAlgorithm.Reset();
        IsPlaying = true;
        algorithmFinished = false;
        pathFinished = false;

        framesPerSecond.Value = 2;
        framesPassed = 0;
    }
    #endregion
}
