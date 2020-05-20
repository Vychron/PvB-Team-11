using UnityEngine;

/// <summary>
/// The foundation will have a villager tasked to build it, and will replace itself with
/// the desired structure.
/// </summary>
public class Foundation : Structure {

    [SerializeField]
    private GameObject _building = null;

    private BuildTask _task = null;

    [SerializeField]
    private float _duration = 10f;

    [SerializeField]
    private TimerGauge _gauge = null;

    private Timer _timer = null;

    /// <summary>
    /// One-time setter for the assigned builder of the foundation.
    /// </summary>
    public Villager Builder {
        set {
            if (_builder == null) {
                _builder = value;
                AssignTask();
            }
        }
    }

    private Villager _builder = null;

    private void AssignTask() {
        TaskAPI.OnArriveAtTaskLocation += StartTask;
        TimerAPI.OnTimerEnd += FinishTask;
        _task = new BuildTask(_builder);
    }

    private void StartTask(Villager villager) {
        if (villager != _task.Assignee)
            return;
        _timer = Timers.Instance.CreateTimer(_duration);
        TimerGauge gauge = Instantiate(_gauge, transform.position + ((Vector3)size / 2), Quaternion.identity, GameObject.FindWithTag("Canvas").transform);
        gauge.AssignedTimer = _timer;
        _task.timer = _timer;
    }

    private void FinishTask(Timer timer) {
        if (timer != _timer)
            return;
        GameObject building = Instantiate(_building, transform.position, Quaternion.identity, transform.parent) as GameObject;
        LevelGrid.Instance.GetStructures.Remove(this);
        LevelGrid.Instance.GetStructures.Add(building.GetComponent<Structure>());
        DestroySelf();
    }

    private void DestroySelf() {
        TaskAPI.OnArriveAtTaskLocation -= StartTask;
        TimerAPI.OnTimerEnd -= FinishTask;
        Destroy(gameObject);
    }

}
