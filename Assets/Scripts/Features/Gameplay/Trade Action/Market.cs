using System.Collections.Generic;

/// <summary>
/// A market structure that adds the ability to trade resources.
/// </summary>
public class Market : Structure {

    private List<GatherTask> _tasks;

    private void Start() {
        _tasks = new List<GatherTask>();
        TaskAPI.OnArriveAtTaskLocation += FinishTask;
    }

    private void FinishTask(Villager villager) {
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

        TaskAPI.TaskCompleted(task.Assignee);
        switch (task.Resource) {
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
        RemoveTask(task);
    }

    public void AddTask(GatherTask task) {
        if (_tasks == null)
            _tasks = new List<GatherTask>();

        if (!_tasks.Contains(task))
            _tasks.Add(task);
    }

    private void RemoveTask(GatherTask task) {
        if (_tasks.Contains(task))
            _tasks.Remove(task);
    }
}
