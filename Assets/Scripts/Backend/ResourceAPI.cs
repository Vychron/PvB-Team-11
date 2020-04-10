/// <summary>
/// Manages the backend of the resources.
/// </summary>
public static class ResourceAPI {

    /// <summary>
    /// Event to be called when changes are made to the amount of resources.
    /// </summary>
    public static SimpleEvent OnUpdateResources;

    /// <summary>
    /// Update the resources.
    /// </summary>
    public static void UpdateResources() {
        OnUpdateResources.Invoke();
    }
}
