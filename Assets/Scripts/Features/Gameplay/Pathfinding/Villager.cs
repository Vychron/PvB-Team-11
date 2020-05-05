using UnityEngine;

/// <summary>
/// Main villager component that controls the villager.
/// </summary>
public class Villager : MonoBehaviour {


    public bool IsImmune = false;

    public House Home {
        get {
            return _home;
        }
        set {
            if (_home == null)
                _home = value;
        }
    }

    private House _home;

    /// <summary>
    /// Returns if the villager is available for a task or not.
    /// </summary>
    public bool Available {
        get { return _available; }
    }

    private bool _available = true;

    private void Start() {
        VillagerAPI.OnMovementAssigned += MovementAssigned;
        TaskAPI.OnTaskCompleted += TaskCompleted;
    }

    private void MovementAssigned(Villager villager, Vector2 location) {
        if (villager != this)
            return;
        _available = false;
        GetComponent<VillagerMovement>().DefinePath(LevelGrid.Instance.GetTile(location));
    }

    private void TaskCompleted(Villager villager) {
        if (villager != this)
            return;
        _available = true;
    }

}
