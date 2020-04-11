/// <summary>
/// Manages the events of
/// </summary>
public static class TimerAPI {

    /// <summary>
    /// Timer event to be called when a timer is created.
    /// </summary>
    public static TimerEvent OnCreateTimer;

    /// <summary>
    /// Timer event to be called when a timer has finished.
    /// </summary>
    public static TimerEvent OnTimerEnd;

    /// <summary>
    /// Tell all listeners that a timer has been created.
    /// </summary>
    /// <param name="timer"></param>
    public static void CreateTimer(Timer timer) {
        OnCreateTimer?.Invoke(timer);
    }

    /// <summary>
    /// Tell all listeners that a timer has finished.
    /// </summary>
    /// <param name="timer"></param>
    public static void EndTimer(Timer timer) {
        OnTimerEnd?.Invoke(timer);
    }
}
