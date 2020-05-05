using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The grid on which the player can build.
/// Also contains the generation of it.
/// </summary>
public class LevelGrid : MonoBehaviour {

    [SerializeField]
    private int _gridSize = 32;

    /// <summary>
    /// Public getter for the level grid.
    /// </summary>
    public Node[][] GetGrid => _grid;

    private Node[][] _grid = null;

    /// <summary>
    /// public getter for the status of the level grid.
    /// </summary>
    public bool Initialized => _initialized;

    private bool _initialized;

    /// <summary>
    /// Public getter for the structure list.
    /// </summary>
    public List<Structure> GetStructures => _structures;

    private List<Structure> _structures = null;
    
    /// <summary>
    /// Static reference to the level grid.
    /// </summary>
    public static LevelGrid Instance;

    // Start is called before the first frame update
    private void Awake() {

        _structures = new List<Structure>();

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

    /// <summary>
    /// Returns the node of a requested grid tile.
    /// </summary>
    /// <param name="tile">The tile that the node is requested from.</param>
    /// <returns>Returns the node on the requested grid position.</returns>
    public Node GetTile(Vector2 tile) {
        return _grid[Mathf.RoundToInt(tile.x)][Mathf.RoundToInt(tile.y)];
    }

    private void GenerateGrid() {

        _grid = new Node[_gridSize][];
        for (int i = 0; i < _gridSize; i++) {
            _grid[i] = new Node[_gridSize];
            for (int j = 0; j < _gridSize; j++) {
                _grid[i][j] = new Node();
                _grid[i][j].tileType = TileTypes.Empty;
                _grid[i][j].position = new Vector2Int(i, j);
            }
        }

        GridAPI.InitializeGrid();
        _initialized = true;
    }

    /// <summary>
    /// Checks if a given area is empty.
    /// </summary>
    /// <param name="x">Leftmost X coordinate of the area.</param>
    /// <param name="y">Bottom-most Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="length">The Length of the area.</param>
    /// <returns>Returns if the area is empty.</returns>
    public bool CheckArea(int x, int y, int width, int length) {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < length; j++)
                if (_grid[i + x][j + y].tileType != TileTypes.Empty)
                    return false;

        return true;
    }

    /// <summary>
    /// Checks if the structure can be placed at the selected location.
    /// </summary>
    /// <param name="x">X position of the structure's origin.</param>
    /// <param name="y">Y position of the structure's origin.</param>
    /// <param name="structure">The structure you want to place.</param>
    /// <param name="type">The type of tile the structure uses.</param>
    /// <param name="entrance">The entrance location of the structure.</param>
    /// <returns>Returns if the position is available.</returns>
    public bool TryPlace(int x, int y, GameObject structure, Vector2 entrance) {
        Structure structureComponent = structure.GetComponent<Structure>();
        Vector2 size = structureComponent.size;

        // Check if the area of the structure is inside of the grid.
        if (
            x < 0 ||
            y < 0 ||
            x + size.x > _gridSize ||
            y + size.y > _gridSize
           )
            return false;

        // Check if the area is available.
        bool available = CheckArea(x, y, (int)size.x, (int)size.y);
        if (!available)
            return false;

        // Check if the player has the required resources.
        Vector3Int cost = structureComponent.buildCost;
        if (
            structureComponent.buildCost.x > ResourceContainer.Wood ||
            structureComponent.buildCost.y > ResourceContainer.Stone ||
            structureComponent.buildCost.z > ResourceContainer.Food
           ) {
            return false;
        }
        // Take the required resources from the player.
        ResourceContainer.Wood -= cost.x;
        ResourceContainer.Stone -= cost.y;
        ResourceContainer.Food -= cost.z;

        // Place the structure.
        GameObject obj = Instantiate(structure, transform);
        obj.transform.position = new Vector2(x, y);

        // Mark the area as structure.
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                _grid[i + x][j + y].tileType = structureComponent.tileType;

        _structures.Add(obj.GetComponent<Structure>());
        if (entrance != (Vector2.one * -1)) {
            entrance.x += x;
            entrance.y += y;
            SetEntrance(entrance);
        }
        GridAPI.UpdateGrid();
        return true;
        
    }

    /// <summary>
    /// Set the entrance of the building to the right position.
    /// </summary>
    /// <param name="position">The position of the entrance.</param>
    public void SetEntrance(Vector2 position) {
        if (_grid[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)].tileType == TileTypes.Structure)
            _grid[Mathf.RoundToInt(position.x)][Mathf.RoundToInt(position.y)].tileType = TileTypes.Entrance;
    }
}
