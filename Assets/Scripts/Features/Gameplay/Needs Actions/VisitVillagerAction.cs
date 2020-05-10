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

    private Villager _selectedVillager => _villagers[_dropdown.value];

    private Villager _controllerChosenVillager = null;

    private void Start() {
        _villagers = ResourceContainer.Villagers;
        foreach (Villager v in _villagers)
            if (!v.Available)
                _villagers.Remove(v);

        int count = _villagers.Count;

        VillagerSpecifiedController controller = GetComponentInParent<VillagerSpecifiedController>();
        if (controller != null)
            if (_villagers.Contains(controller.GetSelectedVillager))
                _villagers.Remove(controller.GetSelectedVillager);

        if (count < 2)
            //Destroy(gameObject);
            return;

        _villagerNames = new List<string>();
        for (int i = 0; i < count; i++)
            _villagerNames.Add(_villagers[i].name);

        _dropdown.AddOptions(_villagerNames);
    }

    public override void Execute(Villager villager = null) {
        if (
            _selectedVillager == null ||
            _selectedVillager == villager
           )
            return;

        if (villager != null)
            _controllerChosenVillager = villager;
        else {
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            villagers.Remove(_selectedVillager);
            int count = villagers.Count;
            for (int i = count - 1; i >= 0; i--)
                if (!villagers[i].Available)
                    villagers.RemoveAt(i);
            count = villagers.Count;
            if (count > 0)
                _controllerChosenVillager = villagers[Random.Range(0, count)];
            else
                return;
        }
        if (_controllerChosenVillager.Available)
            VisitHouse();
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "VisitVillager(Resources." + _selectedVillager.name + ");";
    }

    private void VisitHouse() {
        VillagerAPI.MovementAssigned(_controllerChosenVillager, (Vector2)_selectedVillager.Home.transform.position + _selectedVillager.Home.entrance);
        _selectedVillager.Home.AddTask(new GatherTask(_controllerChosenVillager, ResourceTypes.Geen, 0, _hunger, _boredom, _satisfaction, _appreciation));
        VillagerAPI.MovementAssigned(_selectedVillager, (Vector2)_selectedVillager.Home.transform.position + _selectedVillager.Home.entrance);
        _selectedVillager.Home.AddTask(new GatherTask(_selectedVillager, ResourceTypes.Geen, 0, _hunger, _boredom, _satisfaction, _appreciation));
    }
}
