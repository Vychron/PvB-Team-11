using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calculation is for simple number operations and returns a numeric value based on the calculation.
/// </summary>
public class Calculation : Operation {

    [SerializeField]
    private Calculations _calculation => (Calculations)_dropdown.value;

    public override float GetValue() {
        int count = _arguments.Count;
        if (count < 2) {
            Debug.LogError("Er zijn niet genoeg getallen gegeven om met elkaar te verrekenen.");
            return 0;
        }
        for (int i = 0; i < count; i++)
            if (
                _arguments[i].GetType() != typeof(Number) &&
                _arguments[i].GetType() != typeof(Calculation)
               ) {
                Debug.LogError("Alleen getallen kunnen met elkaar verrekend worden.");
                return 0;
            }

        float value = _arguments[0].GetValue();

        switch (_calculation) {
            case Calculations.Keer:
                for (int i = 1; i < count; i++)
                    value *= _arguments[i].GetValue();
                break;

            case Calculations.GedeeldDoor:
                for (int i = 1; i < count; i++) {
                    if (_arguments[i].GetValue() == 0) {
                        Debug.LogError("Je kan niet delen door 0.");
                        return -987654321;
                    }
                    value /= _arguments[i].GetValue();
                }
                break;

            case Calculations.Min:
                for (int i = 1; i < count; i++)
                    value -= _arguments[i].GetValue();
                break;

            default:
                for (int i = 0; i < count; i++)
                    value += _arguments[i].GetValue();
                break;
        }
        return value;
    }

    protected override void Start() {
        List<string> names = new List<string>(Enum.GetNames(typeof(Calculations)));
        _dropdown.AddOptions(names);
    }

    public override string GetText() {
        string output = "";
        int count = _arguments.Count;
        if (count > 0) {
            output += "(" + _arguments[0].GetText();
            for (int i = 1; i < count; i++) {
                switch (_calculation) {
                    case Calculations.GedeeldDoor:
                        output += " ÷ ";
                        break;
                    case Calculations.Keer:
                        output += " × ";
                        break;
                    case Calculations.Min:
                        output += " - ";
                        break;
                    default:
                        output += " + ";
                        break;
                }
                output += _arguments[i].GetText();
            }
            output += ")";
        }
        return output;
    }
}
