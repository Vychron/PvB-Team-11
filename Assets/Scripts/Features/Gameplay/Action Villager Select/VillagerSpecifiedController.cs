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

    private Villager _selectedVillager = null;

    /// <summary>
    /// Returns the selected villager.
    /// </summary>
    public Villager GetSelectedVillager {
        get { return _selectedVillager; }
    }

    private void Start() {
        _dropdown.onValueChanged.AddListener(delegate { SetSelectedVillager(); });

        // Get a list of available villagers.
        _villagers = new List<Villager>(ResourceContainer.Villagers);

        int count = _villagers.Count;
        if (count > 0)
            for (int i = count - 1; i >= 0; i--)
                if (!_villagers[i].Available)
                    _villagers.RemoveAt(i);

        count = _villagers.Count;
        /*
         * Fill the list with villager names in the same order
         * so they have the same index numbers in their corresponding lists.
        */
        _villagerNames = new List<string>();
        for (int i = 0; i < count; i++)
            _villagerNames.Add(_villagers[i].name);

        if (count > 0)
            _dropdown.AddOptions(_villagerNames);
    }

    private void SetSelectedVillager() {
        if (_dropdown.options.Count > 0)
            _selectedVillager = _villagers[_dropdown.value];
        else
            _selectedVillager = null;
    }

    public override void Execute(Villager villager = null) {
        if (_dropdown.options.Count == 0)
            return;

        _selectedVillager = _villagers[_dropdown.value];

        if (_actions == null)
            return;

        int count = _actions.Count;
        if (count == 0) {
            Debug.LogError("Er zijn geen acties toegevoegd aan de controller.");
            return;
        }
        for (int i = 0; i < count; i++)
            _actions[i].Execute(_selectedVillager);
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
