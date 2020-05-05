/// <summary>
/// Manages the events of the grid.
/// </summary>
public static class GridAPI {

    /// <summary>
    /// Simple event to be called when the grid finished generating.
    /// </summary>
    public static SimpleEvent OnGridCreated;

    /// <summary>
    /// Simple event to be called when the grid has been updated;
    /// </summary>
    public static SimpleEvent OnGridUpdated;

    /// <summary>
    /// Initialize everything that needs to be placed on the grid.
    /// </summary>
    public static void InitializeGrid() {
        OnGridCreated?.Invoke();
    }

    /// <summary>
    /// Update everything that requires updates from the grid.
    /// </summary>
    public static void UpdateGrid() {
        OnGridUpdated?.Invoke();
    }

}
