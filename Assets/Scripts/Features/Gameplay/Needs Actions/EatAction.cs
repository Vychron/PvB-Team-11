using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates actions for villagers to go home and eat.
/// </summary>
public class EatAction : Action {

    private Villager _villager = null;

    public override void Execute(Villager villager = null) {
        if (villager == null) {
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            int count = villagers.Count;
            if (count == 0)
                return;

            for (int i = count - 1; i >= 0; i--)
                if (!villagers[i].Available)
                    villagers.RemoveAt(i);
            count = villagers.Count;
            if (count > 0)
                _villager = villagers[Random.Range(0, count)];
            else
                return;
        }

        if (_villager.Available)
            Dine();
        
    }

    private void Dine() {
        VillagerAPI.MovementAssigned(_villager, (Vector2)_villager.Home.transform.position + _villager.Home.entrance);
        _villager.Home.AddTask(new GatherTask(_villager, ResourceTypes.Eten, -1, _hunger, _boredom, _satisfaction, _appreciation));
        _villager = null;
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "Eat();";
    }
}
