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
}
