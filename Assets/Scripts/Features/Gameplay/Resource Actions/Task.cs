/// <summary>
/// Abstract class for the different types of tasks.
/// </summary>
public abstract class Task {

    protected Villager _assignee;

    /// <summary>
    /// Getter for the villager assigned to the task.
    /// </summary>
    public Villager Assignee {
        get { return _assignee; }
    }   

}
