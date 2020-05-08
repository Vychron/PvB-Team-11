using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and keeps track of timers.
/// </summary>
public class Timers : MonoBehaviour {

    /// <summary>
    /// Static reference to itself.
    /// </summary>
    public static Timers Instance;

    private List<Timer> _timers = null;
    private List<Timer> _removeQueue = null;

    private int _timerCount = 0;
    private int _deleteCount = 0;

    private void Start() {
        _timers = new List<Timer>();
        _removeQueue = new List<Timer>();

        //Check if the reference has already been set, and if it refers to itself.
        if (Instance && Instance != this) {
            Destroy(this);
            return;
        }

        Instance = this;
        TimerAPI.OnTimerEnd += RemoveTimer;
    }

    private void Update() {

        // First check if there are any timers to remove from the list.
        _deleteCount = _removeQueue.Count;
        if (_deleteCount > 0) {
            /*
             * Then remove them from the list before updating the timers,
             * iterating backwards to prevent shifting.
            */
            for (int i = _deleteCount - 1; i >= 0; i--)
                    _timers.Remove(_removeQueue[i]);
            // Afterwards, clear the list of timers to be removed, also iterating backwards.
            for (int i = _deleteCount - 1; i >=0; i--)
                    _removeQueue.Remove(_removeQueue[i]);
        }

        // Update all timers (the timers don't derive from MonoBehaviour and have to be updated manually).
        _timerCount = _timers.Count;
        if (_timerCount >= 1)
            for (int i = 0; i < _timerCount; i++)
                _timers[i].Update();
    }

    private void RemoveTimer(Timer timer) {
        // Queue the timer for removal, rather than removing it while the list of timers is still updating.
        _removeQueue.Add(timer);
    }

    /// <summary>
    /// Create a new timer with a duration of n seconds, and return it to the requesting object.
    /// </summary>
    /// <param name="duration">Time in seconds the timer will run.</param>
    /// <returns>Returns the created timer.</returns>
    public Timer CreateTimer(float duration) {
        Timer timer = new Timer(duration);
        _timers.Add(timer);
        TimerAPI.CreateTimer(timer);
        return timer;
    }
}
