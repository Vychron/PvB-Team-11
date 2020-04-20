using UnityEngine;

/// <summary>
/// Event for special movement actions.
/// </summary>
/// <param name="villager">The villager to assign the event to.</param>
/// <param name="location">The world destination of the movement event.</param>
public delegate void MovementEvent(Villager villager, Vector2 location);