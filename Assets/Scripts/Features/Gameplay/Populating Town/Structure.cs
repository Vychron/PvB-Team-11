using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract structure class.
/// </summary>
public abstract class Structure : MonoBehaviour {

    /// <summary>
    /// List of the tiles that the structure covers.
    /// </summary>
    public List<Vector2Int> tiles;

    /// <summary>
    /// Type of the tiles that the structure covers.
    /// </summary>
    public TileTypes tileType;
}
