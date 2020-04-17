using UnityEngine;

/// <summary>
/// Editor component to alter resources in the editor during runtime.
/// This script is for testing purposes.
/// </summary>
public class ResourceCheat : MonoBehaviour {

    /// <summary>
    /// Edits the appreciation by X.
    /// </summary>
    /// <param name="amount">the amount you want to add to the appariciation (negative to subtract).</param>
    public void EditAppreciation(int amount) {
        ResourceContainer.Appreciation += amount;
        ResourceAPI.UpdateResources();
    }

    /// <summary>
    /// Edits the wood count by X.
    /// </summary>
    /// <param name="amount">the amount you want to add to the wood count (negative to subtract).</param>
    public void EditWood(int amount) {
        ResourceContainer.Wood += amount;
        ResourceAPI.UpdateResources();
    }

    /// <summary>
    /// Edits the stone count by X.
    /// </summary>
    /// <param name="amount">the amount you want to add to the stone count (negative to subtract).</param>
    public void EditStone(int amount) {
        ResourceContainer.Stone += amount;
        ResourceAPI.UpdateResources();
    }

    /// <summary>
    /// Edits the food count by X.
    /// </summary>
    /// <param name="amount">the amount you want to add to the food count (negative to subtract).</param>
    public void EditFood(int amount) {
        ResourceContainer.Food += amount;
        ResourceAPI.UpdateResources();
    }
}