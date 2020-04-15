using UnityEngine;

/// <summary>
/// A node representing a grid tile.
/// </summary>
public class Node {

    /// <summary>
    /// The type of tile that the node represents.
    /// </summary>
    public TileTypes tileType;

    /// <summary>
    /// The grid position of the node.
    /// </summary>
    public Vector2Int position;

    /// <summary>
    /// The F cost of the node, used in pathfinding.
    /// </summary>
    public int F {
        get;
        set;
    }

    /// <summary>
    /// The G cost of the node, used in pathfinding.
    /// </summary>
    public int G {
        get;
        set;
    }

    /// <summary>
    /// The H cost of the node, used in pathfinding.
    /// </summary>
    public int H {
        get;
        set;
    }

    /// <summary>
    /// The parent node of the node, used in Pathfinding.
    /// </summary>
    public Node parent;

    /// <summary>
    /// Constructor for a node.
    /// </summary>
    public Node() {

    }

}
