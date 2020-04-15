using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public TileTypes tileType;

    public Vector2Int position;

    public int F {
        get;
        set;
    }

    public int G {
        get;
        set;
    }

    public int H {
        get;
        set;
    }

    public Node parent;

    public Node() {

    }

}
