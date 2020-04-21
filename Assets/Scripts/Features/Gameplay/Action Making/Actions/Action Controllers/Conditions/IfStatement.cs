﻿using UnityEngine;

/// <summary>
/// IfStatement Executes actions when all conditions are met.
/// </summary>
public class IfStatement : Condition {

    public override void Execute() {
        int argsCount = _arguments.Count;
        if (argsCount == 0) {
            Debug.LogError("Er zijn geen voorwaarden gegeven aan de actie(s).");
            return;
        }
        /*
         *  The "pass" int is used as a boolean with an extra state for exceptions.
        */
        int pass = 1;
        for (int i = 0; i < argsCount; i++)
            pass = (int)_arguments[i].GetValue();

        if (pass != 1) {
            Debug.LogError("De ingestelde voorwaarden voorkomen het uitvoeren van de actie(s).");
            if (pass == 2)
                Debug.LogError("Controleer de voorwaarden voor de actie(s).");
            return;
        }
        int actionCount = _actions.Count;
        if (actionCount == 0) {
            Debug.LogError("Er zijn geen acties om uit te voeren.");
            return;
        }
        for (int i = 0; i < actionCount; i++)
            _actions[i].Execute();
    }
}