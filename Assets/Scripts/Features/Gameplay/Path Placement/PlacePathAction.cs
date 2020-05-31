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
    /// <param name="fromString">Whether or not the placement information is read from a string.</param>
    /// <param name="xPostition">The x position of the path.</param>
    /// <param name="yPosition">The y position of the path.</param>
    /// <param name="widthValue">The width of the path.</param>
    /// <param name="heightValue">The height of the path.</param>
    public void PlacePaths(bool fromString = false, int xPostition = 0, int yPosition = 0, int widthValue = 0, int heightValue = 0) {

        int x;
        int y;
        int width;
        int height;

        if (fromString) {
            x = xPostition;
            y = yPosition;
            width = widthValue;
            height = heightValue;
        }
        else {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
        }

        bool available = LevelGrid.Instance.CheckArea(x, y, width, height, _pathPrefab.GetComponent<Structure>());
        if (!available) {
            Debug.LogWarning("Gebied is niet vrij.");
            return;
        }

        if (
            x < 0 ||
            y < 0 ||
            width < 1 ||
            height < 1
           ) {
            Debug.LogWarning("Gebied is te klein of steekt buiten het dorp uit.");
            return;
        }
        Vector3Int tileCost = _pathPrefab.GetComponent<Structure>().buildCost;
        Vector3Int cost = width * height * tileCost;
        if (
            cost.x > ResourceContainer.Wood ||
            cost.y > ResourceContainer.Stone ||
            cost.z > ResourceContainer.Food
           ) {
            Debug.LogWarning("Je hebt niet voldoende grondstoffen om deze actie uit te voeren.");
            return;
        }

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                LevelGrid.Instance.TryPlace(x + i, y + j, _pathPrefab, Vector2.zero);
    }

    public override string GetText() {
        return "PlaatsPad(" + _x.ToString() + ", " + _y.ToString() + ", " + _width.ToString() + ", " + _height.ToString() + ");";
    }
}
