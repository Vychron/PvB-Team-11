using UnityEngine;

/// <summary>
/// Highlighter is a singleton text object that is used to highlight the area that the action requires.
/// </summary>
public class Highlighter : MonoBehaviour {
    /// <summary>
    /// A static reference to the component.
    /// </summary>
    public static Highlighter Instance;

    private void Start() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
