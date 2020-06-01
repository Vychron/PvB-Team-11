using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Creates actions for a villager to visit another one.
/// </summary>
public class VisitVillagerAction : Action {

    private List<Villager> _villagers = null;

    [SerializeField]
    private Dropdown _dropdown = null;

    private List<string> _villagerNames = null;

    private Villager _selectedVillager = null;

    private Villager _controllerChosenVillager = null;

    private void Start() {
        if (_dropdown != null) {
            _dropdown.onValueChanged.AddListener(delegate { SetSelectedVillager(); });
            _villagers = new List<Villager>(ResourceContainer.Villagers);
            int count = _villagers.Count;
            if (count > 0)
                for (int i = count - 1; i >= 0; i--)
                    if (!_villagers[i].Available)
                        _villagers.RemoveAt(i);

            VillagerSpecifiedController controller = GetComponentInParent<VillagerSpecifiedController>();
            if (controller != null)
                if (_villagers.Contains(controller.GetSelectedVillager))
                    _villagers.Remove(controller.GetSelectedVillager);

            count = _villagers.Count;

            if (count < 2)
                return;

            _villagerNames = new List<string>();
            for (int i = 0; i < count; i++)
                _villagerNames.Add(_villagers[i].name);
            if (count > 1)
                _dropdown.AddOptions(_villagerNames);
        }
    }

    private void SetSelectedVillager() {
        if (_dropdown.options.Count > 0)
            _selectedVillager = _villagers[_dropdown.value];
        else
            _selectedVillager = null;
    }

    public override void Execute(Villager villager = null) {
        VisitVillager(villager);
    }

    /// <summary>
    /// Visit a villager.
    /// </summary>
    /// <param name="villager">The villager that will visit another villager.</param>
    /// <param name="fromString">Whether or not the information is read from a string.</param>
    /// <param name="hostingVillager">The villager to be visited.</param>
    public void VisitVillager(Villager villager = null, bool fromString = false, Villager hostingVillager = null) {

        if (!fromString) {
            if (_dropdown.options.Count == 0)
                return;
            hostingVillager = _villagers[_dropdown.value];
        }

        if (
            hostingVillager == villager ||
            hostingVillager == null
           )
            return;

        else {
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            villagers.Remove(hostingVillager);
            int count = villagers.Count;
            for (int i = count - 1; i >= 0; i--)
                if (!villagers[i].Available)
                    villagers.RemoveAt(i);
            count = villagers.Count;

            if (count > 0)
                villager = villagers[Random.Range(0, count)];
            else
                return;
        }
        if (villager.Available)
            VisitHouse(hostingVillager, villager);
    }

    public override string GetText() {
        return "Bezoek(" + _selectedVillager?.name + ");";
    }

    private void VisitHouse(Villager host, Villager visitor) {
        VillagerAPI.MovementAssigned(visitor, (Vector2)host.Home.transform.position + host.Home.entrance);
        host.Home.AddTask(new GatherTask(visitor, ResourceTypes.Geen, 0, _hunger, _boredom, _satisfaction, _appreciation));
        VillagerAPI.MovementAssigned(host, (Vector2)host.Home.transform.position + host.Home.entrance);
        host.Home.AddTask(new GatherTask(host, ResourceTypes.Geen, 0, _hunger, _boredom, _satisfaction, _appreciation));
    }
}
