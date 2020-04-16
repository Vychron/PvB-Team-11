/// <summary>
/// Manages villager events.
/// </summary>
public static class VillagerAPI {

    /// <summary>
    /// Event to be called when a villager completes a task.
    /// </summary>
    public static VillagerEvent OnTaskCompleted;

    /// <summary>
    /// Event to be called when a villager Gets a task assigned.
    /// </summary>
    public static VillagerEvent OnTaskAssigned;

    /// <summary>
    /// Event to be called when a villager completes it's movement path.
    /// </summary>
    public static VillagerEvent OnMovementCompleted;

    /// <summary>
    /// Event to be called when a villager Arrives in town.
    /// </summary>
    public static VillagerEvent OnVillagerArrive;

    /// <summary>
    /// Event to be called when a villager Leaves town.
    /// </summary>
    public static VillagerEvent OnVillagerLeave;

    /// <summary>
    /// Calls the OnTaskAssigned event.
    /// </summary>
    /// <param name="villager">The villager the event is directed at.</param>
    public static void AssignTask(Villager villager) {
        OnTaskAssigned?.Invoke(villager);
    }

    /// <summary>
    /// Calls the OnTaskCompleted event.
    /// </summary>
    /// <param name="villager">The villager the event is directed at.</param>
    public static void FinishTask(Villager villager) {
        OnTaskCompleted?.Invoke(villager);
    }

    /// <summary>
    /// Calls the OnMovementCompleted event.
    /// </summary>
    /// <param name="villager">The villager the event is directed at.</param>
    public static void FinishMoving(Villager villager) {
        OnMovementCompleted?.Invoke(villager);
    }

    /// <summary>
    /// Calls the OnVillagerArrive event.
    /// </summary>
    /// <param name="villager">The villager the event is directed at.</param>
    public static void JoinVillage(Villager villager) {
        ResourceContainer.AddVillager(villager);
        OnVillagerArrive?.Invoke(villager);
    }

    /// <summary>
    /// Calls the OnVIllagerLeave event.
    /// </summary>
    /// <param name="villager">The villager the event is directed at.</param>
    public static void LeaveVillage(Villager villager) {
        ResourceContainer.RemoveVillager(villager);
        OnVillagerLeave?.Invoke(villager);
    }
}
