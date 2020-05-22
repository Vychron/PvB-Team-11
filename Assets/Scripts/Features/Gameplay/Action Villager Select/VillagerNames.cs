using UnityEngine;

/// <summary>
/// Contains the list of names the villagers can have.
/// </summary>
public class VillagerNames : MonoBehaviour {

    /// <summary>
    /// Static reference to the component.
    /// </summary>
    public static VillagerNames Instance = null;

    /// <summary>
    /// List of possible villager names.
    /// </summary>
    public string[]
        MaleNames,
        FemaleNames;

    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
    }
}