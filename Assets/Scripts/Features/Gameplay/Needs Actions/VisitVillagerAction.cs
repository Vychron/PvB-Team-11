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
        _dropdown.onValueChanged.AddListener(delegate { SetSelectedVillager(); });

        _villagers = ResourceContainer.Villagers;
        int count = _villagers.Count;
        if (count > 0)
            for (int i = count - 1; i >= 0; i--)
                if (!_villagers[i].Available)
                    _villagers.RemoveAt(i);

        count = _villagers.Count;

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
        if (count > 1)
            _dropdown.AddOptions(_villagerNames);
    }

    private void SetSelectedVillager() {
        if (_dropdown.options.Count > 0)
            _selectedVillager = _villagers[_dropdown.value];
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
        SetSelectedVillager();
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "VisitVillager(" + _selectedVillager?.name + ");";
    }

    private void VisitHouse() {
        VillagerAPI.MovementAssigned(_controllerChosenVillager, (Vector2)_selectedVillager.Home.transform.position + _selectedVillager.Home.entrance);
        _selectedVillager.Home.AddTask(new GatherTask(_controllerChosenVillager, ResourceTypes.Geen, 0, _hunger, _boredom, _satisfaction, _appreciation));
        VillagerAPI.MovementAssigned(_selectedVillager, (Vector2)_selectedVillager.Home.transform.position + _selectedVillager.Home.entrance);
        _selectedVillager.Home.AddTask(new GatherTask(_selectedVillager, ResourceTypes.Geen, 0, _hunger, _boredom, _satisfaction, _appreciation));
    }
}
