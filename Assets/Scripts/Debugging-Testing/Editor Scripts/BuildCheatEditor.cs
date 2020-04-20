using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildCheat))]
[CanEditMultipleObjects]

/// <summary>
/// Editor class to add inspector buttons to the build cheat script.
/// </summary>
public class BuildCheatEditor : Editor {

    public override void OnInspectorGUI() {

        if (GUILayout.Button("Place structure.", EditorStyles.miniButton))
            ((BuildCheat)target).PlaceBuilding();

        DrawDefaultInspector();
    }
}