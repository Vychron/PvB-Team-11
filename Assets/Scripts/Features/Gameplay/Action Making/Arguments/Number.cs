using UnityEngine;

/// <summary>
/// Number is a numeric value to be used as an argument.
/// </summary>
public class Number : Argument {

    [SerializeField]
    private float _value = 0;

    public override float GetValue() {
        return _value;
    }
}
