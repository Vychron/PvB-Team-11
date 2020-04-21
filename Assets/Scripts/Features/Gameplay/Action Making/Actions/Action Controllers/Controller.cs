using UnityEngine;

/// <summary>
/// Controller is a starting point for block-built code.
/// </summary>
public class Controller : ActionController {

    public override void Execute() {
        if (_actions == null)
            return;
        int count = _actions.Count;
        if (count == 0) {
            Debug.LogError("Er zijn geen acties toegevoegd aan de controller.");
            return;
        }
        for (int i = 0; i < count; i++)
            _actions[i].Execute();
    }
}
