/// <summary>
/// Describes the behaviour of a maze generation algorithm that can generate a
/// traversable maze from a grid of cells.
/// </summary>
public interface IMazeGenerationAlgorithm
{
    /// <summary>
    /// Executes the maze generation algorithm to generate a maze. Should only
    /// be called once when used.
    /// </summary>
    void Execute(CellGrid cellGrid);

    /// <summary>
    /// Executes the maze generation algorithm to generate a maze. Uses a
    /// boolean return value to suspend iteration until the next call. Should 
    /// be called many times (during an Update loop, for example) when used.
    /// </summary>
    bool ExecuteIterative(CellGrid cellGrid);

    /// <summary>
    /// Resets the algorithm to its initial state.
    /// </summary>
    void Reset();
}
