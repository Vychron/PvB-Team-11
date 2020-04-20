using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GatherResourceAction))]
[CanEditMultipleObjects]

/// <summary>
/// Editor class to add inspector buttons to the build cheat script.
/// </summary>
public class GatherResourceActionEditor : Editor {

    public override void OnInspectorGUI() {

        if (GUILayout.Button("Gather Resource.", EditorStyles.miniButton)) {
            ((GatherResourceAction)target).GatherResource(((GatherResourceAction)target).resource, ((GatherResourceAction)target).amount);
        }
        DrawDefaultInspector();
    }
}