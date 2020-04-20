using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls Gathering of a resource.
/// </summary>
public class ResourceSite : Structure {

    /// <summary>
    /// The type of resource villagers can gather at this structure.
    /// </summary>
    public ResourceTypes resourceType;

    /// <summary>
    /// The location of the entrance relative to the structure's position.
    /// </summary>
    public Vector2 entrance;

    private List<Timer> _timers;

    private List<GatherTask> _tasks;

    [SerializeField]
    private float _gatherTime = 5f;

    [SerializeField]
    private TimerGauge _gauge;

    private void Start() {
        _tasks = new List<GatherTask>();
        TaskAPI.OnArriveAtTaskLocation += StartTask;
        TimerAPI.OnTimerEnd += FinishTask;
        _timers = new List<Timer>();
    }

    /// <summary>
    /// Add a task to the resource site.
    /// </summary>
    /// <param name="task"></param>
    public void AddTask(GatherTask task) {
        _tasks.Add(task);
    }

    private void RemoveTask(GatherTask task) {
        _tasks.Remove(task);
    }

    private void StartTask(Villager villager) {
        Villager assignee = null;
        GatherTask task = null;
        foreach (GatherTask t in _tasks) {
            if (t.Assignee == villager) {
                assignee = villager;
                task = t;
                break;
            }
        }
        if (assignee == null)
            return;

        Timer taskTimer = Timers.Instance.CreateTimer(_gatherTime * task.Amount);
        TimerGauge gauge = Instantiate(_gauge, transform.GetChild(0).position, Quaternion.identity, GameObject.FindWithTag("Canvas").transform);
        gauge.AssignedTimer = taskTimer;

        _timers.Add(taskTimer);
        task.timer = taskTimer;
    }

    private void FinishTask(Timer timer) {
        if (!_timers.Contains(timer))
            return;
        GatherTask task = null;
        foreach (GatherTask t in _tasks) {
            if (t.timer == timer) {
                task = t;
                break;
            }
        }
        if (task == null)
            return;

        TaskAPI.TaskCompleted(task.Assignee);
        switch (resourceType) {
            case ResourceTypes.Food:
                ResourceContainer.Food += task.Amount;
                break;
            case ResourceTypes.Wood:
                ResourceContainer.Wood += task.Amount;
                break;
            case ResourceTypes.Stone:
                ResourceContainer.Stone += task.Amount;
                break;
            default:
                break;
        }
        RemoveTask(task);
        _timers.Remove(timer);
    }
}
