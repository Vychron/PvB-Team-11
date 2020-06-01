using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Interpreter script for writing actions ingame.
/// </summary>
public class StringInterpreter : MonoBehaviour {

    [SerializeField]
    private BuildAction _buildAction = null;

    [SerializeField]
    private PlacePathAction _pathAction = null;

    [SerializeField]
    private PlaceNatureElementAction _natureAction = null;

    [SerializeField]
    private GatherResourceAction _gatherAction = null;

    [SerializeField]
    private TradeResourcesAction _tradeAction = null;

    [SerializeField]
    private EatAction _eatAction = null;

    [SerializeField]
    private VisitVillagerAction _visitAction = null;

    [SerializeField]
    private InputField _inputField;

    [SerializeField]
    private char[]
        _validCharacters = null,
        _separators = null;

    private void Update() {
        if (
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter)
           ) {
            Execute();
            _inputField.text = "";
        }
    }

    private void Execute() {
        string input = _inputField.text.ToLowerInvariant();

        string cleanedInput = "";

        for (int i = 0; i < input.Length; i++) {
            bool nextCharFound = false;
            for (int j = 0; j < _validCharacters.Length; j++) {
                for (int k = 0; k < _separators.Length; k++) {
                    if (input[i] == _validCharacters[j] || input[i] == _separators[k]) {
                        cleanedInput += input[i];
                        nextCharFound = true;
                        break;
                    }
                }
                if (nextCharFound)
                    break;
            }
        }

        input = cleanedInput;

        string villagerName = null;
        List<string> villagerNames = new List<string>(VillagerNames.Instance.GetUsedNames());
        for (int i = 0; i < villagerNames.Count; i++)
            if (input.StartsWith(villagerNames[i].ToLowerInvariant())) {
                villagerName = villagerNames[i];
                break;
            }

        Villager villager = null;
        if (villagerName != null) {
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            foreach (Villager v in villagers)
                if (v.name == villagerName) {
                    villager = v;
                    input = input.Substring(villagerName.Length);
                    break;
                }
        }

        bool trimmed = false;
        while (!trimmed) {
            bool isTrimmed = false;
            for (int i = 0; i < _separators.Length; i++) {
                if (input[0] == _separators[i]) {
                    input = input.Substring(1);
                    isTrimmed = true;
                }
            }
            if (!isTrimmed) {
                trimmed = true;
            }
        }

        string[] arguments;
        Debug.LogError(input);
        if (input.StartsWith("bouw")) {
            input = input.Substring(4);
            arguments = GetArguments(input, 3);
            Debug.LogError(input);
            for (int i = 0; i < arguments.Length; i++)
                Debug.LogWarning(arguments[i]);
            _buildAction.PlaceBuilding(villager, true, arguments[0], int.Parse(arguments[1]), int.Parse(arguments[2]));
        }

        if (input.StartsWith("plaats")) {
            input = input.Substring(6);
            trimmed = false;
            while (!trimmed) {
                bool isTrimmed = false;
                for (int i = 0; i < _separators.Length; i++) {
                    if (input[0] == _separators[i]) {
                        input = input.Substring(1);
                        isTrimmed = true;
                    }
                }
                if (!isTrimmed) {
                    trimmed = true;
                }
            }

            if (input.StartsWith("pad")) {
                input = input.Substring(3);
                arguments = GetArguments(input, 4);
                _pathAction.PlacePaths(true, int.Parse(arguments[0]), int.Parse(arguments[1]), int.Parse(arguments[2]), int.Parse(arguments[3]));
            }

            if (input.StartsWith("natuur")) {
                input = input.Substring(6);
                arguments = GetArguments(input, 4);
                _natureAction.PlaceNatureElement(true, arguments[0] + " " + arguments[1], int.Parse(arguments[2]), int.Parse(arguments[3]));
            }
        }

        if (input.StartsWith("verzamel")) {
            input = input.Substring(8);
            arguments = GetArguments(input, 2);
                _gatherAction.GatherResource((ResourceTypes)Enum.Parse(typeof(ResourceTypes), arguments[0], true), int.Parse(arguments[1]), villager);
        }

        if (input.StartsWith("ruil")) {
            input = input.Substring(4);
            arguments = GetArguments(input, 3);
                _tradeAction.TradeResource((ResourceTypes)Enum.Parse(typeof(ResourceTypes), arguments[0], true), (ResourceTypes)Enum.Parse(typeof(ResourceTypes), arguments[1], true), int.Parse(arguments[2]), villager);
        }

        if (input.StartsWith("eet"))
            _eatAction.Execute(villager);

        if (input.StartsWith("bezoek")) {
            input = input.Substring(6);
            arguments = GetArguments(input, 1);
            Villager host = null;
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            foreach (Villager v in villagers)
                if (v.name == villagerName) {
                    host = v;
                    break;
                }
            _visitAction.VisitVillager(villager, true, host);
        }
    }

    private string[] GetArguments(string text, int argCount) {
        string[] arguments = new string[argCount];
        
        for (int i = 0; i < _separators.Length; i++)
            if (text[0] == _separators[i])
                text = text.Substring(1);

        int currentChar = 0;

        for (int i = 0; i < argCount; i++) {
            bool separatorFound = false;
            bool nextCharFound = false;
            arguments[i] = "";
            for (int j = currentChar; j < text.Length; j++) {
                for (int k = 0; k < _separators.Length; k++) {
                    if (
                        text[j] == _separators[k] &&
                        nextCharFound
                       ) {
                        separatorFound = true;
                        break;
                    }
                }
                nextCharFound = true;
                arguments[i] += text[j];
                if (separatorFound) {
                    currentChar = j + 1;
                    arguments[i] = arguments[i].Substring(0, arguments[i].Length - 1);
                    break;
                }
            }
        }

        return arguments;
    }
}
