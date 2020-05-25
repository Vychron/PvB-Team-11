using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates actions for villagers to go home and eat.
/// </summary>
public class EatAction : Action {

    private Villager _villager = null;

    public override void Execute(Villager villager = null) {
        Villager vil = null;
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
                vil = villagers[Random.Range(0, count)];
            else
                return;
        }
        else
            vil = villager;

        if (vil.Available)
            Dine(vil);
        
    }

    private void Dine(Villager villager) {
        VillagerAPI.MovementAssigned(villager, (Vector2)villager.Home.transform.position + villager.Home.entrance);
        villager.Home.AddTask(new GatherTask(villager, ResourceTypes.Eten, -1, _hunger, _boredom, _satisfaction, _appreciation));
    }

    public override string GetText() {
        return "Eet();";
    }
}
