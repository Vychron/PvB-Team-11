using UnityEngine;

/// <summary>
/// Controller is a starting point for block-built code.
/// </summary>
public class Controller : ActionController {

    public override void Execute(Villager villager = null) {
        if (_actions == null)
            return;
        int count = _actions.Count;
        if (count == 0) {
            Debug.LogError("Er zijn geen acties toegevoegd aan de controller.");
            return;
        }
        for (int i = 0; i < count; i++)
            _actions[i].Execute(villager);
    }

    public override string GetText() {
        string output = "";
        int count = _actions.Count;

        if (count > 0)
            for (int i = 0; i < count; i++) {
                if (i != 0)
                    output += "\n";
                output += _actions[i].GetText();
            }
        return output;
    }
}
