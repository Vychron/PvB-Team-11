using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement component of the villagers.
/// </summary>
public class VillagerMovement : MonoBehaviour {

    private Villager _villager;

    private Timer _moveTimer = null;

    private List<Node> _path;

    private void SetNewMoveTimer() {
        TimerAPI.OnTimerEnd -= Move;
        float rand = Random.Range(5f, 20f);
        Debug.Log(rand);
        _moveTimer = Timers.Instance.CreateTimer(rand);

        TimerAPI.OnTimerEnd += Move;
    }

    private void Move(Timer timer) {
        if (timer != _moveTimer)
            return;

        _path = Pathfinder.Instance.GetPath(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        Debug.LogError("Destination is at " + _path[_path.Count - 1].position);
        StartCoroutine(MoveToNextNode(_path));
    }

    private IEnumerator MoveToNextNode(List<Node> nodes) {
        int count = nodes.Count;
        if (count > 0) {
            Debug.LogError("Next node is at " + nodes[0].position);
            while (Vector2.Distance(transform.position, nodes[0].position) > 0.0025f) {
                transform.position += Vector3.Normalize(new Vector3(nodes[0].position.x, nodes[0].position.y) - transform.position) * 0.005f;
                yield return new WaitForEndOfFrame();
            }
            nodes.RemoveAt(0);
            yield return new WaitForEndOfFrame();
            if (nodes.Count < 1)
                SetNewMoveTimer();
            else
                StartCoroutine(MoveToNextNode(nodes));
        }
    }

    /// <summary>
    /// Find a path from the current position to the given destination.
    /// </summary>
    /// <param name="destination">The destination of the path.</param>
    public void FindPath(Vector2Int destination) {
        bool targetEntrance = false;
        if (LevelGrid.Instance.GetTile(destination).tileType == TileTypes.Entrance) {
            destination.y--;
            targetEntrance = true;
        }
        Vector2Int position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        List<Node> path = Pathfinder.Instance.GetPath(position, destination);
        if (targetEntrance) {
            destination.y++;
            path.Add(LevelGrid.Instance.GetTile(destination));
        }
        StartCoroutine(MoveToNextNode(path));
    }
}
