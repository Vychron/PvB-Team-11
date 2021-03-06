﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement component of the villagers.
/// </summary>
public class VillagerMovement : MonoBehaviour {

    private Villager _villager = null;

    private VillagerAnimator _animator = null;

    /// <summary>
    /// Returns the timer the villager uses for movement actions.
    /// </summary>
    public Timer GetMoveTimer {
        get { return _moveTimer; }
    }

    private Timer _moveTimer = null;

    private List<Vector2> _path;

    private Pathfinder _pathfinder;

    private Vector2 _destination;

    private bool _isPathDefined = false;

    private void Start() {
        _villager = GetComponent<Villager>();
        _animator = GetComponent<VillagerAnimator>();
        VillagerAPI.OnMovementCompleted += MovementCompleted;
        VillagerAPI.OnVillagerArrive += OnJoinVillage;
        TaskAPI.OnTaskCompleted += ReadyMovement;
        TimerAPI.OnTimerEnd += Move;
        _pathfinder = GetComponent<Pathfinder>();
    }

    private void MovementCompleted(Villager villager) {
        if (villager != _villager)
            return;
        if (!_villager.Available)
            TaskAPI.ArriveAtTaskLocation(_villager);
        if (villager.IsLeavingTown) {
            VillagerAPI.OnMovementCompleted -= MovementCompleted;
            VillagerAPI.OnVillagerArrive -= OnJoinVillage;
            TimerAPI.OnTimerEnd -= Move;
            _moveTimer = null;
            DestroyImmediate(gameObject);
            return;
        }

        SetNewMoveTimer();
    }

    private void SetNewMoveTimer() {
        float rand = Random.Range(5f, 20f);
        Debug.Log(rand);
        _moveTimer = Timers.Instance.CreateTimer(rand);
    }

    private void OnJoinVillage(Villager villager) {
        if (villager != _villager)
            return;
        DefinePath(LevelGrid.Instance.GetTile((Vector2)_villager.Home.transform.position + _villager.Home.entrance));
    }

    public void DefinePath(Node destination) {
        _destination = destination.position;
        Vector2 target = destination.position;
        if (destination.tileType == TileTypes.Entrance) {
            target.y--;
        }
        _isPathDefined = true;
        _pathfinder.GetPath(transform.position, target);
    }

    private void ReadyMovement(Villager villager) {
        if (villager != _villager)
            return;
        SetNewMoveTimer();
        StartCoroutine(SetVisibility(true));
    }

    private void Move(Timer timer) {
        if (timer != _moveTimer)
            return;
        if (!_villager.Available)
            return;
        _isPathDefined = false;
        _pathfinder.GetPath(transform.position);
    }

    /// <summary>
    /// Action to execute when a path is found.
    /// </summary>
    /// <param name="path">The path that has been found.</param>
    public void OnPathFound(Vector2[] path) {
        _path = new List<Vector2>(path);
        if (_isPathDefined)
            _path.Add(_destination);
        StopAllCoroutines();
        StartCoroutine(MoveToNextNode());
    }

    private IEnumerator SetVisibility(bool visible) {
        SpriteRenderer rend = _villager.GetComponent<SpriteRenderer>();
        int target = 1;
        float fadeValue = 0.025f;
        if (!visible) {
            target = 0;
            fadeValue *= -1;
        }
        while (Mathf.Abs(rend.color.a - target) > Mathf.Abs(fadeValue)) {
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a + fadeValue);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveToNextNode() { 
        int count = _path.Count;
        if (count > 0) {
            while (Vector2.Distance(transform.position, _path[0]) > 0.0025f) {
                Vector2 normalizedDirection = Vector3.Normalize(new Vector3(_path[0].x, _path[0].y) - transform.position) * 0.01f;
                transform.position += (Vector3)normalizedDirection;
                _animator.UpdateSprite(normalizedDirection);
                yield return new WaitForEndOfFrame();
            }
            transform.position = new Vector3(_path[0].x, _path[0].y);
            _path.RemoveAt(0);
            yield return new WaitForEndOfFrame();
            if (_path.Count < 1) {
                VillagerAPI.FinishMoving(_villager);
                if (!_villager.Available) {
                    _animator.UpdateSprite(new Vector2(0, 1));
                    StartCoroutine(SetVisibility(false));
                }
            }
                
            else
                StartCoroutine(MoveToNextNode());
        }
    }
}
