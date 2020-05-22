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
    /// Whether or not the villager is leaving town.
    /// </summary>
    public bool IsLeavingTown = false;

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

    public bool GetGender {
        get { return _gender; }
    }

    private bool _gender = false; // False (0) = female, true (1) = male.

    /// <summary>
    /// Returns if the villager is available for a task or not.
    /// </summary>
    public bool Available {
        get { return _available; }
    }

    private bool _available = true;

    private void Start() {
        if (Random.value >= 0.5f)
            _gender = true;
        else
            _gender = false;

        VillagerAPI.OnMovementAssigned += MovementAssigned;
        TaskAPI.OnTaskCompleted += TaskCompleted;
        VillagerAPI.OnLeaveVillage += LeaveVillage;

        string[] names;
        if (_gender)
            names = VillagerNames.Instance.MaleNames;
        else
            names = VillagerNames.Instance.FemaleNames;

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
            NeedsDisplayer.Instance?.Enable(this);
    }

    private void LeaveVillage(Villager villager) {
        if (villager != this)
            return;
        GetComponent<VillagerMovement>().DefinePath(LevelGrid.Instance.GetTile(new Vector2(LevelGrid.Instance.GetGrid.Length / 2, 0)));
        IsLeavingTown = true;
        _home = null;
        VillagerAPI.OnMovementAssigned -= MovementAssigned;
        TaskAPI.OnTaskCompleted -= TaskCompleted;
        VillagerAPI.OnLeaveVillage -= LeaveVillage;
    }

}
