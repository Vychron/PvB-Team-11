using UnityEngine;

/// <summary>
/// Represents a for-loop.
/// Executes actions x times.
/// </summary>
public class ForLoop : ActionController {

    [SerializeField]
    private int _count = 0;

    public override void Execute() {
        /*
            To avoid confusion about private int _count and local int count:
            The private int _count is the amount of iterations the for loop will make.
            The local int count is the sum of the actions to be executed.
        */
        if (_count == 0) {
            Debug.LogError("De herhaling wordt 0 keer uitgevoerd.");
            return;
        }
        int count = _actions.Count;
        if (count == 0) {
            Debug.Log("Er zijn geen acties om uit te voeren.");
            return;
        }
        for (int i = 0; i < _count; i++)
            for (int j = 0; j < count; j++)
                _actions[j].Execute();
    }

    public override string GetText() {
        string output = "";

        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";

        output += indent + "for ( int i = 0; i < " + _count + "; i++)\n" + indent + "{";
        int count = _actions.Count;
        if (count > 0)
            for (int i = 0; i < count; i++)
                output += "\n" + _actions[i].GetText();

        output += "\n" + indent + "}";
        return output;
    }
}
