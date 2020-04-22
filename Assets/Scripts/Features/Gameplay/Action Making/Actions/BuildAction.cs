using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Action for placing buildings.
/// </summary>
public class BuildAction : Action {

    private int _x => int.Parse(_xField.text);
    private int _y => int.Parse(_yField.text);
    private string _objectName => _nameField.text;

    [SerializeField]
    private InputField
        _xField = null,
        _yField = null,
        _nameField = null;

    public override void Execute() {
        PlaceBuilding();
    }

    /// <summary>
    /// Try to place a building.
    /// </summary>
    public void PlaceBuilding() {
        GameObject obj = null;
        obj = Resources.Load("Prefabs/Buildings/" + _objectName) as GameObject;
        if (obj)
            LevelGrid.Instance.TryPlace(_x, _y, obj, Vector2Int.zero);
        else
            Debug.LogWarning("Geen object gevonden om te plaatsen.");
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "Build(" + _x.ToString() + ", " + _y.ToString() + ", \"" + _objectName + "\");";
    }
}
