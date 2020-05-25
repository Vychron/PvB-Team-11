using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Action for placing nature elements.
/// </summary>
public class PlaceNatureElementAction : Action {

    private int _x => int.Parse(_xField.text);
    private int _y => int.Parse(_yField.text);

    [SerializeField]
    private Dropdown _object = null;

    [SerializeField]
    private List<GameObject> _objects = null;

    private string _objectName = null;

    public string GetObjectName {
        get { return _objectName; }
    }

    [SerializeField]
    private InputField
        _xField = null,
        _yField = null;

    private void Start() {
        _object?.onValueChanged.AddListener(delegate { SetElement(); });
        List<string> objNames = new List<string>();
        foreach (GameObject g in _objects) {
            objNames.Add(g.name);
        }

        _object?.AddOptions(objNames);
        SetElement();
    }

    private void SetElement() {
        if (_object?.options.Count > 0)
            _objectName = _objects[_object.value]?.name;
        else
            _objectName = null;
    }

    public override void Execute(Villager villager = null) {
        PlaceNatureElement();
    }

    /// <summary>
    /// Try to place an element.
    /// </summary>
    public void PlaceNatureElement(bool fromString = false, string objName = null, int xPosition = 0, int yPosition = 0) {

        string objectName;
        int x;
        int y;
        if (fromString) {
            objectName = objName;
            x = xPosition;
            y = yPosition;
        }
        else {
            objectName = _objectName;
            x = _x;
            y = _y;
        }

        GameObject obj = Resources.Load("Prefabs/Nature/" + objectName) as GameObject;
        if (obj) {
            GameObject structure = null;
            Structure str = obj.GetComponent<Structure>();
            structure = LevelGrid.Instance.TryPlace(x, y, obj, Vector2Int.zero);
        }
        else
            Debug.LogWarning("Geen object gevonden om te plaatsen.");
    }

    public override string GetText() {
        return "PlaatsNatuur(" + _objectName + ", " + _x.ToString() + ", " + _y.ToString() + ");";
    }
}
