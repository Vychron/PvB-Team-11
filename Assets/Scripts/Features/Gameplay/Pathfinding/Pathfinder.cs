using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all pathfinding functionality required to get a path between 2 points.
/// </summary>
public class Pathfinder : MonoBehaviour {

    /// <summary>
    /// Static reference to the Pathfinder instance.
    /// </summary>
    public static Pathfinder Instance;

    private void Start() {
        if (Instance && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Get a path to a random position.
    /// </summary>
    /// <param name="from">Starting position of the path.</param>
    /// <returns>Returns a node List of coördinates in the order of the path.</returns>
    public List<Node> GetPath(Vector2Int from) {
        Vector2Int to = GetRandomPosition().position;
        return GetPath(from, to);
    }

    /// <summary>
    /// Get a path to a chosen position.
    /// </summary>
    /// <param name="from">Starting location of the path.</param>
    /// <param name="to">End position of the path.</param>
    /// <returns>Returns a node list of coördinates in the order of the path.</returns>
    public List<Node> GetPath(Vector2Int from, Vector2Int to) {

        Node[][] grid = LevelGrid.Instance.GetGrid;

        // Convert the positions to nodes.
        Node fromNode = grid[from.x][from.y];
        Node toNode = grid[to.x][to.y];

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        fromNode.H = Mathf.Abs(fromNode.position.x - toNode.position.x) + Mathf.Abs(fromNode.position.y - toNode.position.y);
        fromNode.F = fromNode.G + fromNode.H;
        openList.Add(fromNode);
        return SetChildNodes(fromNode, toNode, openList, closedList, grid);
    }

    private List<Node> SetChildNodes(Node from, Node to, List<Node> openList, List<Node> closedList, Node[][] grid) {
        List<Node> neighbours = GetChildren(from, grid, closedList);
        int count = neighbours.Count;
        Node currentNode = null;
        if (count > 0)
            for (int i = 0; i < count; i++) {
                currentNode = neighbours[i];
                if (!openList.Contains(currentNode))
                    openList.Add(currentNode);

                if (currentNode.G < (from.G + 1)) {
                    currentNode.parent = from;
                    currentNode.G = from.G + 1;
                    currentNode.H = (int)Mathf.Pow(Mathf.Abs(currentNode.position.x - to.position.x), 2) +
                        (int)Mathf.Pow(Mathf.Abs(currentNode.position.y - to.position.y), 2);
                    currentNode.F = currentNode.G + currentNode.H;

                    if (currentNode.tileType == TileTypes.Empty)
                        currentNode.F += 20;
                }
            }

        int openCount = openList.Count;
        Node lowest = openList[0];
        for (int i = 1; i < openCount; i++) {
            if (openList[i].F < lowest.F) {
                lowest = openList[i];
                continue;
            }

            if (openList[i].F == lowest.F)
                if (openList[i].parent != null)
                    if (openList[i].parent.parent != null)
                        if (
                            openList[i].parent.parent.position.x == openList[i].position.x ||
                            openList[i].parent.parent.position.y == openList[i].position.y
                           )
                            lowest = openList[i];
        }

        closedList.Add(lowest);
        openList.Remove(lowest);
        if (lowest != to)
            return SetChildNodes(lowest, to, openList, closedList, grid);

        /*
         * There's a minor bug in the algorithm that causes the villagers to first move to a different node
         * next to the destination, causing the villager to move over the destination, before moving back to it,
         * therefore, the second-last node is removed from the list.
        */
        int closedCount = closedList.Count;
        if (closedCount > 1)
            closedList.RemoveAt(closedCount - 2);

        return closedList;
    }

    private List<Node> GetChildren(Node parent, Node[][] grid, List<Node> closedList) {

        int length = grid.Length;
        Node currentChild = null;

        List<Node> children = new List<Node>();

        if (parent.position.x > 0) {
            currentChild = grid[parent.position.x - 1][parent.position.y];
            children = TryAddChild(children, currentChild, closedList);
        }

        if (parent.position.x < length - 1) {
            currentChild = grid[parent.position.x + 1][parent.position.y];
            children = TryAddChild(children, currentChild, closedList);
        }

        if (parent.position.y > 0) {
            currentChild = grid[parent.position.x][parent.position.y - 1];
            children = TryAddChild(children, currentChild, closedList);
        }

        if (parent.position.y < length - 1) {
            currentChild = grid[parent.position.x][parent.position.y + 1];
            children = TryAddChild(children, currentChild, closedList);
        }

        return children;
    }

    private List<Node> TryAddChild(List<Node> list, Node child, List<Node> closedList) {
        if (
            (child.tileType == TileTypes.Empty ||
            child.tileType == TileTypes.Path) &&
            !closedList.Contains(child)
           )
            list.Add(child);

        return list;
    }

    private Node GetRandomPosition() {
        List<Node> positions = GetAvailablePositions();

        // Generate a list of positions filtered on paved with path.
        List<Node> pavedPositions = new List<Node>();
        int count = positions.Count;
        for (int i = 0; i < count; i++)
            if (positions[i].tileType == TileTypes.Path)
                pavedPositions.Add(positions[i]);

        int pavedCount = pavedPositions.Count;
        if (pavedCount > 0) {
            int rand = Random.Range(0, pavedCount);
            return pavedPositions[rand];      
        }

        // If there are no paved tiles, select a random empty tile.
        else {
            int rand = Random.Range(0, count);
            return positions[rand];
        }
    }

    private List<Node> GetAvailablePositions() {
        List<Node> walkablePositions = new List<Node>();
        Node[][] grid = LevelGrid.Instance.GetGrid;

        for (int i = 0; i < grid.Length; i++)
            for (int j = 0; j < grid[i].Length; j++)
                if (
                    grid[i][j].tileType == TileTypes.Empty ||
                    grid[i][j].tileType == TileTypes.Path
                   )
                    walkablePositions.Add(grid[i][j]);

        return walkablePositions;
    }
}
