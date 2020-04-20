using UnityEngine;

/// <summary>
/// A simple action to print a line to the console.
/// </summary>
public class PrintLineAction : Action {

    [SerializeField]
    private string _text = "";

    public override void Execute() {
        print(_text);
    }
}
