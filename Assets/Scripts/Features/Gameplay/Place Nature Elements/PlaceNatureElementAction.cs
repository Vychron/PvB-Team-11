using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Action for placing nature elements.
/// </summary>
public class PlaceNatureElementAction : Action {

    private int _x => int.Parse(_xField.text);
    private int _y => int.Parse(_yField.text);
    private string _objectName => _nameField.text;

    [SerializeField]
    private InputField
        _xField = null,
        _yField = null,
        _nameField = null;

    public override void Execute(Villager villager = null) {
        PlaceNatureElement();
    }

    /// <summary>
    /// Try to place an element.
    /// </summary>
    public void PlaceNatureElement() {
        GameObject obj = Resources.Load("Prefabs/Nature/" + _objectName) as GameObject;
        if (obj) {
            GameObject structure = null;
            Structure str = obj.GetComponent<Structure>();
            structure = LevelGrid.Instance.TryPlace(_x, _y, obj, Vector2Int.zero);
        }
        else
            Debug.LogWarning("Geen object gevonden om te plaatsen.");
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "PlaceNatureElement(" + _x.ToString() + ", " + _y.ToString() + ", \"" + _objectName + "\");";
    }
}
