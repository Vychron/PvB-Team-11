using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Extended version of the controller with a dropdown villager picker for the actions.
/// </summary>
public class VillagerSpecifiedController : Controller {

    private List<Villager> _villagers = null;

    [SerializeField]
    private Dropdown _dropdown = null;

    private List<string> _villagerNames = null;

    private Villager _selectedVillager => _villagers[_dropdown.value];

    /// <summary>
    /// Returns the selected villager.
    /// </summary>
    public Villager GetSelectedVillager {
        get { return _selectedVillager; }
    }

    private void Start() {
        // Get a list of available villagers.
        _villagers = ResourceContainer.Villagers;
        foreach (Villager v in _villagers) {
            if (!v.Available)
                _villagers.Remove(v);
        }

        int count = _villagers.Count;
        /*
         * Fill the list with villager names in the same order
         * so they have the same index numbers in their corresponding lists.
        */
        _villagerNames = new List<string>();
        for (int i = 0; i < count; i++) {
            _villagerNames.Add(_villagers[i].name);
        }

        _dropdown.AddOptions(_villagerNames);
    }

    public override void Execute(Villager villager = null) {
        if (_actions == null)
            return;
        int count = _actions.Count;
        if (count == 0) {
            Debug.LogError("Er zijn geen acties toegevoegd aan de controller.");
            return;
        }
        if (_selectedVillager != null)
            for (int i = 0; i < count; i++)
                _actions[i].Execute(_selectedVillager);

        else
            Debug.LogError("De gekozen dorpeling is niet beschikbaar.");
    }

    public override string GetText() {
        string output = "";
        int count = _actions.Count;

        if (count > 0)
            for (int i = 0; i < count; i++) {
                if (i != 0)
                    output += "\n";
                output += _actions[i].GetText();
            }
        return output;
    }
}
