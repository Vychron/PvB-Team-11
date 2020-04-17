using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceCheat))]
[CanEditMultipleObjects]

/// <summary>
/// Editor class to add inspector buttons to the resource cheat script.
/// </summary>
public class ResourceCheatEditor : Editor {

    public override void OnInspectorGUI() {
        GUILayout.BeginVertical();
        GUILayout.Label("Appreciation");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditAppreciation(-10);
        }
        if (GUILayout.Button("-1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditAppreciation(-1);
        }
        if (GUILayout.Button("+1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditAppreciation(1);
        }
        if (GUILayout.Button("+10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditAppreciation(10);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Wood");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditWood(-10);
        }
        if (GUILayout.Button("-1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditWood(-1);
        }
        if (GUILayout.Button("+1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditWood(1);
        }
        if (GUILayout.Button("+10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditWood(10);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Stone");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditStone(-10);
        }
        if (GUILayout.Button("-1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditStone(-1);
        }
        if (GUILayout.Button("+1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditStone(1);
        }
        if (GUILayout.Button("+10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditStone(10);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Food");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditFood(-10);
        }
        if (GUILayout.Button("-1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditFood(-1);
        }
        if (GUILayout.Button("+1", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditFood(1);
        }
        if (GUILayout.Button("+10", EditorStyles.miniButton)) {
            ((ResourceCheat)target).EditFood(10);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        DrawDefaultInspector();
    }
}