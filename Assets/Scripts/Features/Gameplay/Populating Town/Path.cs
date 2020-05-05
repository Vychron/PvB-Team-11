using UnityEngine;

/// <summary>
/// Data container of a path structure.
/// </summary>
public class Path : Structure {

    [SerializeField]
    private SpriteRenderer _sprite = null;

    private bool[] _neighbours;

    private void Start() {
        _neighbours = new bool[8];
        GridAPI.OnGridUpdated += UpdateConnections;
        UpdateConnections();
    }

    private void UpdateConnections() {

        Node[][] grid = LevelGrid.Instance.GetGrid;

        /*
         * Find all neighbouring tiles.
         * Neighbours of tile T are numbered as pictured below:
         * 
         * 5 6 7
         * 3 T 4
         * 0 1 2
        */
        for (int i = 0; i < 8; i++)
            _neighbours[i] = true;

        // Set all neighbour locations outside of the grid to false.
        if (transform.position.x < 1) {
            _neighbours[0] = false;
            _neighbours[1] = false;
            _neighbours[2] = false;
        }

        if (transform.position.y < 1) {
            _neighbours[0] = false;
            _neighbours[3] = false;
            _neighbours[5] = false;
        }

        if (transform.position.x == grid.Length - 1) {
            _neighbours[2] = false;
            _neighbours[4] = false;
            _neighbours[7] = false;
        }

        if (transform.position.y == grid.Length - 1) {
            _neighbours[5] = false;
            _neighbours[6] = false;
            _neighbours[7] = false;
        }

        // Set horizontal and vertical neighbours to false if they can't connect paths.
        if (_neighbours[1])
            if (grid[(int)transform.position.x][(int)transform.position.y - 1].tileType != TileTypes.Path)
                _neighbours[1] = false;

        if (_neighbours[3])
            if (grid[(int)transform.position.x - 1][(int)transform.position.y].tileType != TileTypes.Path)
                _neighbours[3] = false;

        if (_neighbours[4])
            if (grid[(int)transform.position.x + 1][(int)transform.position.y].tileType != TileTypes.Path)
                _neighbours[4] = false;

        if (_neighbours[6])
            // Also check if the tile above it isn't an entrance tile.
            if (
                grid[(int)transform.position.x][(int)transform.position.y + 1].tileType != TileTypes.Path &&
                grid[(int)transform.position.x][(int)transform.position.y + 1].tileType != TileTypes.Entrance
               )
                _neighbours[6] = false;

        // Set all diagonal neighbours that are adjecent to 2 neighboured paths.
        if (
            _neighbours[1] &&
            _neighbours[3]
           ) {

            if (grid[(int)transform.position.x - 1][(int)transform.position.y - 1].tileType != TileTypes.Path)
                _neighbours[0] = false;
        }
        else
            _neighbours[0] = false;

        if (
            _neighbours[1] &&
            _neighbours[4]
           ) {
            if (grid[(int)transform.position.x + 1][(int)transform.position.y - 1].tileType != TileTypes.Path)
                _neighbours[2] = false;
        }
        else
            _neighbours[2] = false;

        if (
            _neighbours[3] &&
            _neighbours[6]
           ) {
            if (grid[(int)transform.position.x - 1][(int)transform.position.y + 1].tileType != TileTypes.Path)
                _neighbours[5] = false;
        }
        else
            _neighbours[5] = false;

        if (
            _neighbours[4] &&
            _neighbours[6]
           ) {
            if (grid[(int)transform.position.x + 1][(int)transform.position.y + 1].tileType != TileTypes.Path)
                _neighbours[7] = false;
        }
        else
            _neighbours[7] = false;

        // Set the correct image for the path.
        string path = "Buildings/Paths/Path";

        for (int i = 0; i < 8; i++)
            if (_neighbours[i])
                path += "-" + i.ToString();

        _sprite.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
    }
}
