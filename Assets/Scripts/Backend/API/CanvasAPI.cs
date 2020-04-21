/// <summary>
/// Manages the events related to canvi.
/// </summary>
public static class CanvasAPI {

    /// <summary>
    /// Event to be called when the Blockly canvas is being enabled.
    /// </summary>
    public static SimpleEvent OnCanvasEnabled;

    /// <summary>
    /// Event to be called when the blockly canvas is being disabled.
    /// </summary>
    public static SimpleEvent OnCanvasDisabled;

    /// <summary>
    /// Calls the OnCanvasEnabled event.
    /// </summary>
    public static void CanvasEnabled() {
        OnCanvasEnabled?.Invoke();
    }

    /// <summary>
    /// Calls the OnCanvasDisabled event.
    /// </summary>
    public static void CanvasDisabled() {
        OnCanvasDisabled?.Invoke();
    }
}
