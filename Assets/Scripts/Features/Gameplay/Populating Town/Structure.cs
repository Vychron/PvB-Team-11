using UnityEngine;

/// <summary>
/// Abstract structure class.
/// </summary>
public class Structure : MonoBehaviour {

    /// <summary>
    /// The size of the structure.
    /// </summary>
    public Vector2 size;

    /// <summary>
    /// Type of the tiles that the structure covers.
    /// </summary>
    public TileTypes tileType;

    /// <summary>
    /// The entrance location of the structure relative to it's position.
    /// </summary>
    public Vector2 entrance;

    /// <summary>
    /// Resources required to build this structure.
    /// X value is wood,
    /// Y value is stone,
    /// Z value is food.
    /// </summary>
    public Vector3Int buildCost = Vector3Int.zero;
}
