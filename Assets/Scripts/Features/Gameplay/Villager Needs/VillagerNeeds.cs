using UnityEngine;

/// <summary>
/// Manages the needs of a villager.
/// </summary>
public class VillagerNeeds : MonoBehaviour {

    /// <summary>
    /// Returns the needs of the villager as a Vector3Int.
    /// </summary>
    public Vector3Int GetNeeds {
        get { return new Vector3Int(_hunger, _boredom, _satisfaction); }
    }

    private int
        _hunger = 0,
        _boredom = 0,
        _satisfaction = 0;
}
