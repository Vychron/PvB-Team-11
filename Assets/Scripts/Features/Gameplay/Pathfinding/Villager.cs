using UnityEngine;

/// <summary>
/// Main villager component that controls the villager.
/// </summary>
public class Villager : MonoBehaviour {

    /// <summary>
    /// Whether or not the villager has immunity to negative satisfaction.
    /// </summary>
    public bool IsImmune = false;

    /// <summary>
    /// The home the villager lives in.
    /// </summary>
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
        string[] names = VillagerNames.Instance.Names;
        name = names[Random.Range(0, names.Length)];
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

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
            NeedsDisplayer.Instance.Enable(this);
    }

}
