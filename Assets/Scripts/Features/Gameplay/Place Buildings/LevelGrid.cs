using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The grid on which the player can build.
/// Also contains the generation of it.
/// </summary>
public class LevelGrid : MonoBehaviour {

    [SerializeField]
    private int _gridSize = 32;

    private TileTypes[][] _grid = null;

    private List<GameObject> _structures = null;

    /// <summary>
    /// public getter for the structure list.
    /// </summary>
    public List<GameObject> GetStructures {
        get { return _structures; }
    }

    /// <summary>
    /// Static reference to the level grid.
    /// </summary>
    public static LevelGrid Instance;

    // Start is called before the first frame update
    private void Awake() {

        _structures = new List<GameObject>();

        // Check if the level grid component already exists, and if it refers to itself.
        if (Instance && Instance != this) {
            Destroy(this);
            return;
        }

        // Set the reference of the grid to itself.
        Instance = this;
        
        // Wait half a second before invoking to make sure every object has been loaded.
        Invoke("GenerateGrid", .5f);
    }

    private void GenerateGrid() {

        _grid = new TileTypes[_gridSize][];
        for (int i = 0; i < _gridSize; i++) {
            _grid[i] = new TileTypes[_gridSize];
            for (int j = 0; j < _gridSize; j++) {
                _grid[i][j] = TileTypes.Empty;
            }
        }
        GridAPI.InitializeGrid();
    }

    /// <summary>
    /// Checks if the structure can be placed at the selected location.
    /// 
    /// </summary>
    /// <param name="x">X position of the structure's origin.</param>
    /// <param name="y">Y position of the structure's origin.</param>
    /// <param name="length">Length of the structure.</param>
    /// <param name="width">Width of the structure.</param>
    /// <param name="structure">The structure you want to place.</param>
    /// <returns>Returns if the position is available.</returns>
    public bool TryPlace(int x, int y, int length, int width, GameObject structure) {

        // Check if the area is inside of the grid.
        if (
            x < 0 ||
            y < 0 ||
            x + width > _gridSize ||
            y + length > _gridSize
           ) {
            return false;
        }

        // Check if the area is available.
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                if (_grid[i + x][j + y] != TileTypes.Empty) {
                    return false;
                }
            }
        }

        // Place the structure.
        GameObject obj = Instantiate(structure, transform);
        // Use y value as z coördinate to properly translate to 2D location.
        obj.transform.position = new Vector3(x, 0, y);

        // Mark the area as structure.
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                _grid[i + x][j + y] = TileTypes.Structure;
            }
        }

        _structures.Add(obj);

        return true;
    }
}
