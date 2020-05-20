/// <summary>
/// Contains data used for build tasks.
/// </summary>
public class BuildTask : Task {

    /// <summary>
    /// Timer for the duration of the task.
    /// </summary>
    public Timer timer = null;

    /// <summary>
    /// Creates a new Build task with an assigned villager.
    /// </summary>
    /// <param name="villager">The villager assinged to the task.</param>

    public BuildTask(Villager villager) {
        _assignee = villager;
    }
}
