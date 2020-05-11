/// <summary>
/// Abstract class for the different types of tasks.
/// </summary>
public abstract class Task {

    protected Villager _assignee;

    protected int
        _hunger,
        _boredom,
        _satisfaction,
        _appreciation;

    /// <summary>
    /// Getter for the villager assigned to the task.
    /// </summary>
    public Villager Assignee {
        get { return _assignee; }
    }   

    /// <summary>
    /// Getter for the hunger value of the task.
    /// </summary>
    public int Hunger {
        get { return _hunger; }
    }

    /// <summary>
    /// Getter for the boredom value of the task.
    /// </summary>
    public int Boredom {
        get { return _boredom; }
    }

    /// <summary>
    /// Getter for the satisfaction value of the task.
    /// </summary>
    public int Satisfaction {
        get { return _satisfaction; }
    }

    /// <summary>
    /// Getter for the appreciation value of the task.
    /// </summary>
    public int Appreciation {
        get { return _appreciation; }
    }

}
