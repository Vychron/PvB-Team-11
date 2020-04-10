/// <summary>
/// Manages the backend of the resources.
/// </summary>
public static class ResourceAPI {

    /// <summary>
    /// Event to be called when changes are made to the amount of resources.
    /// </summary>
    public static SimpleEvent OnUpdateResources;

    /// <summary>
    /// Event to be called when changes are made to the population capacity.
    /// </summary>
    public static SimpleEvent OnUpdatePopulationCap;

    /// <summary>
    /// Update the resources.
    /// </summary>
    public static void UpdateResources() {
        OnUpdateResources.Invoke();
    }

    /// <summary>
    /// Update the population and capacity.
    /// </summary>
    public static void UpdatePopulationCap() {
        // Re-checks the conditions for changing population count.
        ResourceContainer.PopulationCount = ResourceContainer.PopulationCount;

        OnUpdatePopulationCap?.Invoke();
        OnUpdateResources?.Invoke();
    }
}
