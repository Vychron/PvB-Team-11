using UnityEngine;

/// <summary>
/// Debug script to test placing buildings.
/// </summary>
public class BuildCheat : MonoBehaviour {

    [SerializeField]
    private int
        _x = 0,
        _y = 0,
        _length = 0,
        _width = 0;

    [SerializeField]
    private string _objectName = "";

    [SerializeField]
    private TileTypes _tileType = TileTypes.Structure;

    /// <summary>
    /// Try to place a building.
    /// </summary>
    public void PlaceBuilding() {
        GameObject obj = null;
        obj = Resources.Load("Prefabs/Buildings/" + _objectName) as GameObject;
        if (obj)
            LevelGrid.Instance.TryPlace(_x, _y, _length, _width, obj, _tileType);
        else
            Debug.LogWarning("Geen object gevonden om te plaatsen.");
    }
}
