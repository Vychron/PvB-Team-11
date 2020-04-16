using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all pathfinding functionality required to get a path between 2 points.
/// </summary>
public class Pathfinder : MonoBehaviour {

    private bool _endFound = false;

    private Node[][] _grid;

    private List<Node> _openList;
    private List<Node> _closedList;
    private List<Node> _blackList;

    private Node _from;
    private Node _to;

    /// <summary>
    /// Get a path to a random, valid location.
    /// </summary>
    /// <param name="from">The starting position of the path.</param>
    public void GetPath(Vector2 from) {
        GetPath(from, GetRandomNode());
    }

    private Vector2 GetRandomNode() {
        Node[][] grid = LevelGrid.Instance.GetGrid;
        List<Node> nodes = new List<Node>();
        int length = grid.Length;
        for (int i = 0; i < length; i++)
            for (int j = 0; j < length; j++)
                if (
                    grid[i][j].tileType == TileTypes.Empty ||
                    grid[i][j].tileType == TileTypes.Path
                   )
                    nodes.Add(grid[i][j]);

        int rand = Random.Range(0, nodes.Count);
        return nodes[rand].position;
    }

    /// <summary>
    /// Get a path towards a specified location.
    /// </summary>
    /// <param name="from">Starting location of the path.</param>
    /// <param name="to">Destination of the path.</param>
    public void GetPath(Vector2 from, Vector2 to) {
        _endFound = false;
        _blackList = new List<Node>();

        _grid = LevelGrid.Instance.GetGrid;
        int size = _grid.Length;
        for (int i = 0; i < _grid.Length; i++) {
            for (int j = 0; j < size; j++) {
                if (
                    _grid[i][j].tileType == TileTypes.Structure ||
                    _grid[i][j].tileType == TileTypes.Entrance
                   )
                    _blackList.Add(_grid[i][j]);
            }
        }

        // Convert from and to positions to nodes.
        _from = _grid[Mathf.RoundToInt(from.x)][Mathf.RoundToInt(from.y)];
        _to = _grid[Mathf.RoundToInt(to.x)][Mathf.RoundToInt(to.y)];
        // Make sure the destination isn't blacklisted.
        if (_blackList.Contains(_to))
            _blackList.Remove(_to);

        _openList = new List<Node>();
        _closedList = new List<Node>();
        _closedList.Add(_from);
        GetNodes();
    }

    private void GetNodes() {
        List<Node> neighbours = GetNeighbours();
        int neighbourCount = neighbours.Count;
        Node current;

        if (neighbourCount > 0) {
            for (int i = 0; i < neighbourCount; i++)
                if (neighbours[i] == _to) {
                    _to.parent = _from;
                    _endFound = true;
                    break;
                }
            
            if (!_endFound)
                for (int i = 0; i < neighbourCount; i++) {
                    current = neighbours[i];
                    if (_openList.Contains(current)) {
                        if (current.G >= _from.G + 1)
                            continue;
                    }
                    else
                        _openList.Remove(current);
                    current.parent = _from;
                    current.H = Mathf.Abs(current.position.x - _to.position.x) + Mathf.Abs(current.position.y - _to.position.y);
                    current.G = _from.G + 1;
                    current.F = current.G + current.H;
                    if (current.tileType == TileTypes.Empty)
                        current.F += 100;
                    _openList.Add(current);
                }
        }

        if (
            _endFound ||
            _openList.Count < 1
           ) {
            List<Node> path = new List<Node>();
            Node currentNode = _to;
            while (currentNode.parent != null) {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Add(_closedList[0]);
            GetComponent<VillagerMovement>().OnPathFound(ReverseOrder(path));
            for (int i = 0; i < _grid.Length; i++)
                for (int j = 0; j < _grid.Length; j++) {
                    _grid[i][j].F = 0;
                    _grid[i][j].G = 0;
                    _grid[i][j].H = 0;
                    _grid[i][j].parent = null;
                }
            return;
        }

        int openCount = _openList.Count;
        if (openCount > 0) {
            Node lowest = _openList[0];
            for (int i = 0; i < openCount; i++) {
                current = _openList[i];
                if (
                    current.position.x != _from.position.x &&
                    current.position.y != _from.position.y
                   )
                    continue;
                if (lowest != current) {
                    if (current.F < lowest.F)
                        lowest = current;
                    if (lowest.F == current.F)
                        if (
                            current.position.x == _from.position.x ||
                            current.position.y == _from.position.y
                           )
                            lowest = current;
                }
            }
            _from = lowest;
        }
        else
            Debug.LogError("openList is empty");
        _closedList.Add(_from);
        _openList.Remove(_from);
        GetNodes();
    }

    private List<Node> GetNeighbours() {
        List<Node> neighbours = new List<Node>();
        Vector2Int originPos = _from.position;
        Node current;

        if (originPos.x > 0) {
           current = _grid[originPos.x - 1][originPos.y];
            if (
                !_blackList.Contains(current) &&
                !_closedList.Contains(current)
               )
                neighbours.Add(current);
        }
        if (originPos.x < _grid.Length - 1) {
            current = _grid[originPos.x + 1][originPos.y];
            if (
                !_blackList.Contains(current) &&
                !_closedList.Contains(current)
               )
                neighbours.Add(current);
        }
        if (originPos.y > 0) {
            current = _grid[originPos.x][originPos.y - 1];
            if (
                !_blackList.Contains(current) &&
                !_closedList.Contains(current)
               )
                neighbours.Add(current);
        }
        if (originPos.y < _grid.Length - 1) {
            current = _grid[originPos.x][originPos.y + 1];
            if (
                !_blackList.Contains(current) &&
                !_closedList.Contains(current)
               )
                neighbours.Add(current);
        }

        return neighbours;
    }

    private Vector2[] ReverseOrder(List<Node> list) {
        int count = list.Count;
        Vector2[] reversedList = new Vector2[count];
        for (int i = count - 1; i >= 0; i--)
            reversedList[count - i - 1] = list[i].position;
        return reversedList;
    }
}
