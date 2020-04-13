/// <summary>
/// Manages the events of the grid.
/// </summary>
public static class GridAPI {

    /// <summary>
    /// Simple event to be called when the grid finished generating.
    /// </summary>
    public static SimpleEvent OnGridCreated;

    /// <summary>
    /// Initialize everything that needs to be placed on the grid.
    /// </summary>
    public static void InitializeGrid() {
        OnGridCreated?.Invoke();
    }
}
