using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Action for placing paths on a given area.
/// </summary>
public class PlacePathAction : Action {

    private int _x => int.Parse(_xField.text);
    private int _y => int.Parse(_yField.text);
    private int _width => int.Parse(_widthField.text);
    private int _height => int.Parse(_heightField.text);

    [SerializeField]
    private GameObject _pathPrefab = null;

    [SerializeField]
    private InputField
        _xField = null,
        _yField = null,
        _widthField = null,
        _heightField = null;

    public override void Execute(Villager villager = null) {
        PlacePaths();
    }

    /// <summary>
    /// Try to place paths on the given area.
    /// </summary>
    public void PlacePaths() {
        bool available = LevelGrid.Instance.CheckArea(_x, _y, _width, _height, _pathPrefab.GetComponent<Structure>());
        if (!available) {
            Debug.LogWarning("Gebied is niet vrij.");
            return;
        }

        if (
            _x < 0 ||
            _y < 0 ||
            _width < 1 ||
            _height < 1
           ) {
            Debug.LogWarning("Gebied is te klein of steekt buiten het dorp uit.");
            return;
        }
        Vector3Int tileCost = _pathPrefab.GetComponent<Structure>().buildCost;
        Vector3Int cost = _width * _height * tileCost;
        if (
            cost.x > ResourceContainer.Wood ||
            cost.y > ResourceContainer.Stone ||
            cost.z > ResourceContainer.Food
           ) {
            Debug.LogWarning("Je hebt niet voldoende grondstoffen om deze actie uit te voeren.");
            return;
        }

        for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
                LevelGrid.Instance.TryPlace(_x + i, _y + j, _pathPrefab, Vector2.zero);
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "PlacePath(" + _x.ToString() + ", " + _y.ToString() + ", " + _width.ToString() + ", " + _height.ToString() + ");";
    }
}
