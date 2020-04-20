using UnityEngine;

/// <summary>
/// Boolean is used to state "true" or "false".
/// </summary>
public class Boolean : Argument {

    [SerializeField]
    private bool _value = false;

    public override float GetValue() {
        if (_value)
            return 1;
        return 0;
    }
}
