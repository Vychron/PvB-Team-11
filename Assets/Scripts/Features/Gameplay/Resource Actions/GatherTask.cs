/// <summary>
/// Contains data used for gather tasks.
/// </summary>
public class GatherTask : Task {

    /// <summary>
    /// Resource to be gathered.
    /// </summary>
    public ResourceTypes Resource {
        get { return _resource; }
    }

    private ResourceTypes _resource;

    /// <summary>
    /// Amount of the resource to be gathered.
    /// </summary>
    public int Amount {
        get { return _amount; }
    }

    private int _amount;

    /// <summary>
    /// Timer for the duration of the task.
    /// </summary>
    public Timer timer;

    /// <summary>
    /// Constructor of a gathering task.
    /// </summary>
    /// <param name="villager">The villager assigned to the task.</param>
    /// <param name="resource">The resource type to be gathered.</param>
    /// <param name="amount">The amount of the resource to be gathered.</param>
    public GatherTask(Villager villager, ResourceTypes resource, int amount) {
        _assignee = villager;
        _resource = resource;
        _amount = amount;
    }
}
