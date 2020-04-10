using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceCheat))]
[CanEditMultipleObjects] // only if you handle it properly

/// <summary>
/// Editor class to add inspector buttons to the resource cheat script.
/// </summary>
public class ResourceCheatEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
        if (GUILayout.Button("Increase population capacity by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCap(1);
        }
        if (GUILayout.Button("Increase population capacity by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCap(10);
        }
        if (GUILayout.Button("Decrease population capacity by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCap(-1);
        }
        if (GUILayout.Button("Decrease population capacity by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCap(-10);
        }

        if (GUILayout.Button("Increase population by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCount(1);
        }
        if (GUILayout.Button("Increase population by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCount(10);
        }
        if (GUILayout.Button("Decrease population by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCount(-1);
        }
        if (GUILayout.Button("Decrease population by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditPopulationCount(-10);
        }

        if (GUILayout.Button("Increase apppreciation by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditAppreciation(1);
        }
        if (GUILayout.Button("Increase apppreciation by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditAppreciation(10);
        }
        if (GUILayout.Button("Decrease apppreciation by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditAppreciation(-1);
        }
        if (GUILayout.Button("Decrease apppreciation by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditAppreciation(-10);
        }

        if (GUILayout.Button("Increase wood by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditWood(1);
        }
        if (GUILayout.Button("Increase wood by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditWood(10);
        }
        if (GUILayout.Button("Decrease wood by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditWood(-1);
        }
        if (GUILayout.Button("Decrease wood by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditWood(-10);
        }

        if (GUILayout.Button("Increase stone by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditStone(1);
        }
        if (GUILayout.Button("Increase stone by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditStone(10);
        }
        if (GUILayout.Button("Decrease stone by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditStone(-1);
        }
        if (GUILayout.Button("Decrease stone by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditStone(-10);
        }

        if (GUILayout.Button("Increase food by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditFood(1);
        }
        if (GUILayout.Button("Increase food by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditFood(10);
        }
        if (GUILayout.Button("Decrease food by 1", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditFood(-1);
        }
        if (GUILayout.Button("Decrease food by 10", EditorStyles.miniButton)) {
            ((ResourceCheat)this.target).EditFood(-10);
        }
        DrawDefaultInspector();
    }
}