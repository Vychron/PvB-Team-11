using UnityEngine;
/// <summary>
/// Manages villager events.
/// </summary>
public static class VillagerAPI {

    /// <summary>
    /// Event to be called when a villager completes it's movement path.
    /// </summary>
    public static VillagerEvent OnMovementCompleted;

    /// <summary>
    /// Event to be called when a villager arrives in town.
    /// </summary>
    public static VillagerEvent OnVillagerArrive;

    /// <summary>
    /// Event to be called when a villager leaves town.
    /// </summary>
    public static VillagerEvent OnLeaveVillage;

    /// <summary>
    /// Event to be called when new movement is assigned to a villager.
    /// </summary>
    public static MovementEvent OnMovementAssigned;

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
    /// Calls the OnVillagerLeave event.
    /// </summary>
    /// <param name="villager">The villager the event is directed at.</param>
    public static void LeaveVillage(Villager villager) {
        ResourceContainer.RemoveVillager(villager);
        OnLeaveVillage?.Invoke(villager);
    }

    /// <summary>
    /// Calls the OnMovementAssigned event.
    /// </summary>
    /// <param name="villager">Villager that will move to the location.</param>
    /// <param name="location">Location the villager will move to.</param>
    public static void MovementAssigned(Villager villager, Vector2 location) {
        OnMovementAssigned?.Invoke(villager, location);
    }
}
