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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Execute();
            _inputField.text = "";
        }
    }

    private void Execute() {
        string input = _inputField.text;
        if (!input.EndsWith(";")) {
            input += ";";
        }
        string villagerName = null;
        List<string> villagerNames = new List<string>(VillagerNames.Instance.GetUsedNames());
        for (int i = 0; i < villagerNames.Count; i++) {
            if (input.StartsWith(villagerNames[i])) {
                villagerName = villagerNames[i];
                break;
            }
        }

        Villager villager = null;
        if (villagerName != null) {
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            foreach (Villager v in villagers) {
                if (v.name == villagerName) {
                    villager = v;
                    input = input.Remove(0, villagerName.Length + 1);
                    break;
                }
            }
        }

        string action = "";
        for (int i = 0; i < input.Length; i++) {
            if (input[i] == '(') {
                action = input.Substring(0, i);
                break;
            }
        }

        string[] arguments;

        switch (action) {
            case "Bouw":
                input = input.Remove(0, 5);
                arguments = GetArguments(input, 3);
                _buildAction.PlaceBuilding(villager, true, arguments[0], int.Parse(arguments[1]), int.Parse(arguments[2]));
                break;

            case "PlaatsPad":
                input = input.Remove(0, 11);
                arguments = GetArguments(input, 4);
                _pathAction.PlacePaths(true, int.Parse(arguments[0]), int.Parse(arguments[1]), int.Parse(arguments[2]), int.Parse(arguments[3]));
                break;
            case "PlaatsNatuur":
                input = input.Remove(0, 13);
                arguments = GetArguments(input, 3);
                _natureAction.PlaceNatureElement(true, arguments[0], int.Parse(arguments[1]), int.Parse(arguments[2]));
                break;

            case "Verzamel":
                input = input.Remove(0, 9);
                arguments = GetArguments(input, 2);
                _gatherAction.GatherResource((ResourceTypes)Enum.Parse(typeof(ResourceTypes), arguments[0], true), int.Parse(arguments[1]), villager);
                break;

            case "Ruil":
                input = input.Remove(0, 5);
                arguments = GetArguments(input, 3);
                _tradeAction.TradeResource((ResourceTypes)Enum.Parse(typeof(ResourceTypes), arguments[0], true), (ResourceTypes)Enum.Parse(typeof(ResourceTypes), arguments[1], true), int.Parse(arguments[2]), villager);
                break;

            case "Eet":
                _eatAction.Execute(villager);
                break;

            case "Bezoek":
                input = input.Remove(0, 7);
                arguments = GetArguments(input, 1);
                Villager host = null;
                List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
                foreach (Villager v in villagers) {
                    if (v.name == villagerName) {
                        host = v;
                        break;
                    }
                }
                _visitAction.VisitVillager(villager, true, host);
                break;

            default:
                return;
        }
    }

    private string[] GetArguments(string text, int argCount) {
        string[] arguments = new string[argCount];
        int currentLength = 0;
        for (int i = 0; i < argCount; i++) {
            for (int j = 0; j < text.Length; j++) {
                if (
                    text[j] == ',' ||
                    text[j] == ')'
                   ) {
                    currentLength = j;
                    arguments[i] = text.Substring(0, currentLength);
                    text = text.Remove(0, currentLength + 2);
                    Debug.LogError(arguments[i]);
                    break;
                }
            }
        }
        return arguments;
    }
}
