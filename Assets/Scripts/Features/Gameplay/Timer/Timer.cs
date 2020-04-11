using UnityEngine;

/// <summary>
/// A simple timer that can be used for timed events.
/// </summary>
public class Timer {

    private float _startTime;
    private float _duration;

    private bool _ended;

    /// <summary>
    /// Constructor for a timer object, will last for X seconds.
    /// </summary>
    /// <param name="duration">Time in seconds before the timer finishes.</param>
    public Timer(float duration) {
        _duration = duration;
        _startTime = Time.time;
        _ended = false;
    }

    /// <summary>
    /// Since the timer doesn't derive from MonoBehaviour, it requires manual updating.
    /// </summary>
    public void Update() {
        if (!_ended && Time.time >= _startTime + _duration) {
            _ended = true;
            TimerAPI.EndTimer(this);
        }
    }

    /// <summary>
    /// Returns the how much time on the timer has passed.
    /// </summary>
    /// <returns>Returns the time passed since the timer has been created.</returns>
    public float GetSpentTime() {
        return Time.time - _startTime;
    }

    /// <summary>
    /// Returns the total duration of the timer.
    /// </summary>
    /// <returns>Returns the duration of the timer.</returns>
    public float GetDuration() {
        return _duration;
    }
}
