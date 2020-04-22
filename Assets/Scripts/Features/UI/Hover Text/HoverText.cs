using UnityEngine;

/// <summary>
/// HoverText is a singleton text object that is used to show text when hovering over certain objects.
/// </summary>
public class HoverText : MonoBehaviour
{
    /// <summary>
    /// A static reference to the component.
    /// </summary>
    public static HoverText Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
