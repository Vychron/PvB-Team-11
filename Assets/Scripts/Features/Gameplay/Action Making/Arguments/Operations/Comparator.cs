using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Comparator is used to compare arguments with each other and returns a boolean based on the outcome.
/// </summary>
public class Comparator : Operation {

    [SerializeField]
    private Comparators _comparator = Comparators.GelijkAan;

    public override float GetValue() {
        int count = _arguments.Count;

        if (count < 2) {
            Debug.LogError("Er zijn te weinig argumenten om met elkaar te vergelijken.");
            return 2;
        }
        /*
         *  The "pass" int is used as a boolean with an extra state for exceptions.
         *  The states of the pass are as following:
         *  0 = false
         *  1 = true
         *  2 = exception - attention is required.
        */
        int pass = 1;
        switch (_comparator) {
            case Comparators.GroterDanOfGelijkAan:
                for (int i = 0; i < count; i++)
                    if (
                        _arguments[i].GetType() != typeof(Number) &&
                        _arguments[i].GetType() != typeof(Calculation)
                       ) {
                        Debug.LogError("Argument nummer " +  i+1  + " heeft geen geldige waarde.");
                        return 2;
                    }
                for (int i = 0; i < count - 1; i++)
                    if (_arguments[i].GetValue() < _arguments[i + 1].GetValue())
                        pass = 0;
                break;

            case Comparators.GroterDan:
                for (int i = 0; i < count; i++)
                    if (
                        _arguments[i].GetType() != typeof(Number) &&
                        _arguments[i].GetType() != typeof(Calculation)
                       ) {
                        Debug.LogError("Argument nummer " + i + 1 + " heeft geen geldige waarde.");
                        return 2;
                    }
                for (int i = 0; i < count - 1; i++)
                    if (_arguments[i].GetValue() <= _arguments[i + 1].GetValue())
                        pass = 0;
                break;

            case Comparators.KleinerDanOfGelijkAan:
                for (int i = 0; i < count; i++)
                    if (
                        _arguments[i].GetType() != typeof(Number) &&
                        _arguments[i].GetType() != typeof(Calculation)
                       ) {
                        Debug.LogError("Argument nummer " + i + 1 + " heeft geen geldige waarde.");
                        return 2;
                    }
                for (int i = 0; i < count - 1; i++)
                    if (_arguments[i].GetValue() > _arguments[i + 1].GetValue())
                        pass = 0;
                break;

            case Comparators.KleinerDan:
                for (int i = 0; i < count; i++)
                    if (
                        _arguments[i].GetType() != typeof(Number) &&
                        _arguments[i].GetType() != typeof(Calculation)
                       ) {
                        Debug.LogError("Argument nummer " + i + 1 + " heeft geen geldige waarde.");
                        return 2;
                    }
                for (int i = 0; i < count - 1; i++)
                    if (_arguments[i].GetValue() >= _arguments[i + 1].GetValue())
                        pass = 0;
                break;

            case Comparators.NietGelijkAan:
                for (int i = 0; i < count - 1; i++) {
                    if (_arguments[i].GetType() != _arguments[i + 1].GetType())
                        if (!ThouroughComparison(i)) {
                            Debug.LogError("De argumenten kunnen niet met elkaar vergeleken worden.");
                            return 2;
                        }
                    if (_arguments[i].GetValue() == _arguments[i + 1].GetValue())
                        pass = 0;
                }
                break;

            default:
                for (int i = 0; i < count - 1; i++) {
                    if(_arguments[i].GetType() != _arguments[i + 1].GetType())
                        if (!ThouroughComparison(i)) {
                            Debug.LogError("De argumenten kunnen niet met elkaar vergeleken worden.");
                            return 2;
                        }
                    if (_arguments[i].GetValue() != _arguments[i + 1].GetValue())
                        pass = 0;
                }
                break;
        }
        return pass;
    }

    protected override void Start() {
        List<string> names = new List<string>(Enum.GetNames(typeof(Comparators)));
        _dropdown.AddOptions(names);
    }

    private bool ThouroughComparison(int index) {
        var type1 = _arguments[index].GetType();
        var type2 = _arguments[index + 1].GetType();
        // Check if the argument types are compatible.
        if
        (
            (type1 == typeof(Number) && type2 != typeof(Calculation)) ||
            (type1 == typeof(Calculation) && type2 != typeof(Number)) ||
            (type1 == typeof(Boolean) && type2 != typeof(Comparator)) ||
            (type1 == typeof(Comparator) && type2 != typeof(Boolean))
        )
            return false;
        else
            return true;
    }

    public override string GetText() {
        string output = "";
        int count = _arguments.Count;
        if (count > 0) {
            output += "(" + _arguments[0].GetText();
            for (int i = 1; i < count; i++) {
                switch (_comparator) {
                    case Comparators.GroterDanOfGelijkAan:
                        output += " >= ";
                        break;
                    case Comparators.GroterDan:
                        output += " > ";
                        break;
                    case Comparators.KleinerDanOfGelijkAan:
                        output += " <= ";
                        break;
                    case Comparators.KleinerDan:
                        output += " < ";
                        break;
                    case Comparators.NietGelijkAan:
                        output += " != ";
                        break;
                    default:
                        output += " == ";
                        break;
                }
                output += _arguments[i].GetText();
            }
            output += ")";
        }
        return output;
    }
}
