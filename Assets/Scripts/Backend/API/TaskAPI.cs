/// <summary>
/// Manages the execution of tasks.
/// </summary>
public static class TaskAPI {

    /// <summary>
    /// Event for completing a task.
    /// </summary>
    public static VillagerEvent OnTaskCompleted;

    /// <summary>
    /// Event for arriving at a task location.
    /// </summary>
    public static VillagerEvent OnArriveAtTaskLocation;

    /// <summary>
    /// Calls the OnTaskCompleted event.
    /// </summary>
    /// <param name="villager">Villager assigned to the event's task.</param>
    public static void TaskCompleted(Villager villager) {
        OnTaskCompleted?.Invoke(villager);
    }

    /// <summary>
    /// Calls the OnArriveAtTaskLocation event.
    /// </summary>
    /// <param name="villager">Villager that arrived at it's destination.</param>
    public static void ArriveAtTaskLocation(Villager villager) {
        OnArriveAtTaskLocation?.Invoke(villager);
    }
}
