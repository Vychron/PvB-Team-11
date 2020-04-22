using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A simple action to print a line to the console.
/// </summary>
public class PrintLineAction : Action {

    [SerializeField]
    private InputField _input = null;

    [SerializeField]
    private string _text => _input.text;

    public override void Execute() {
        print(_text);
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "PrintLine(\"" + _text + "\");";
    }
}
