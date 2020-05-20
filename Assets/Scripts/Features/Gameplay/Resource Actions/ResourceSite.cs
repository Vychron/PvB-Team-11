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

    protected List<Timer> _timers;

    protected List<GatherTask> _tasks;

    [SerializeField]
    protected float _gatherTime = 5f;

    [SerializeField]
    protected TimerGauge _gauge = null;

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
        if (_tasks == null)
            _tasks = new List<GatherTask>();

        _tasks.Add(task);
    }

    protected void RemoveTask(GatherTask task) {
        _tasks.Remove(task);
    }

    protected void StartTask(Villager villager) {
        Villager assignee = null;
        GatherTask task = null;
        if (_tasks.Count > 0)
            foreach (GatherTask t in _tasks) {
                if (t.Assignee == villager) {
                    assignee = villager;
                    task = t;
                    break;
                }
            }
        else
            return;
        if (assignee == null)
            return;

        Timer taskTimer = Timers.Instance.CreateTimer(_gatherTime * Mathf.Max(1, task.Amount));
        TimerGauge gauge = Instantiate(_gauge, transform.position + ((Vector3)size / 2), Quaternion.identity, GameObject.FindWithTag("Canvas").transform);
        gauge.AssignedTimer = taskTimer;
        if (_timers == null)
            _timers = new List<Timer>();

        _timers.Add(taskTimer);
        task.timer = taskTimer;
    }

    protected void FinishTask(Timer timer) {
        if (_timers == null)
            _timers = new List<Timer>();
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
            case ResourceTypes.Eten:
                ResourceContainer.Food += task.Amount;
                break;
            case ResourceTypes.Hout:
                ResourceContainer.Wood += task.Amount;
                break;
            case ResourceTypes.Steen:
                ResourceContainer.Stone += task.Amount;
                break;
            default:
                break;
        }
        task.Assignee.GetComponent<VillagerNeeds>().UpdateNeeds(task.Hunger, task.Boredom, task.Satisfaction);
        ResourceContainer.Appreciation += task.Appreciation;
        RemoveTask(task);
        _timers.Remove(timer);
    }
}
