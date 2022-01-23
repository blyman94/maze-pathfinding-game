using UnityEngine;

/// <summary>
/// Abstract class housing methods and data members common among all three maze
/// generation algorithms in this prototype.
/// </summary>
public class MazeGenerationAlgorithm : ScriptableObject
{
    /// <summary>
    /// Cell currently being evaluated by the algorithm.
    /// </summary>
    protected Cell current;

    /// <summary>
    /// Number of iterations the algorithm has completed.
    /// </summary>
    protected int iterations;

    #region ScriptableObject Methods
    private void OnEnable()
    {
        Reset();
    }
    #endregion

    /// <summary>
    /// Resets the algorithm to its initial state.
    /// </summary>
    public void Reset()
    {
        iterations = 0;
    }
}
