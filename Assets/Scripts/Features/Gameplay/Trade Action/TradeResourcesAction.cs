using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An action to make a villager trade resources.
/// </summary>
public class TradeResourcesAction : Action {

    [SerializeField]
    private Dropdown
        _offer = null,
        _return = null;

    [SerializeField]
    private InputField _input = null;

    private int _amount => int.Parse(_input.text);

    public override void Execute(Villager villager = null) {
        TradeResource((ResourceTypes)_return.value, _amount, villager);
    }

    private void TradeResource(ResourceTypes resource, int amount, Villager villager) {
        if (resource == ResourceTypes.Geen)
            return;
        if (_amount == 0)
            return;


        Market market = null;
        List<Structure> structures = LevelGrid.Instance.GetStructures;
        List<Market> markets = new List<Market>();

        foreach (Structure s in structures)
            if (s.GetType() == typeof(Market)) {
                market = (Market)s;
                markets.Add(market);
            }

        int marketCount = markets.Count;
        if (marketCount > 0) {
            int randMarket = UnityEngine.Random.Range(0, marketCount);
            market = markets[randMarket];
        }
        else
            return;

        Villager assignee = villager;
        if (assignee == null) {
            List<Villager> villagers = ResourceContainer.Villagers;
            List<Villager> available = new List<Villager>();
            
            foreach (Villager v in villagers) {
                if (v.Available)
                    available.Add(v);

                int villagerCount = available.Count;
                if (villagerCount > 0) {
                    int randVillager = UnityEngine.Random.Range(0, villagerCount);
                    assignee = available[randVillager];
                }
            }
        }
        if (assignee == null)
            return;

        switch (_offer.value) {
            case (int)ResourceTypes.Hout:
                ResourceContainer.Wood -= _amount;
                break;
            case (int)ResourceTypes.Steen:
                ResourceContainer.Stone -= _amount;
                break;
            case (int)ResourceTypes.Eten:
                ResourceContainer.Food -= _amount;
                break;
            default:
                break;
        }

        VillagerAPI.MovementAssigned(assignee, (Vector2)market.transform.position + market.entrance);
        market.AddTask(new GatherTask(assignee, (ResourceTypes)_return.value, Mathf.FloorToInt(amount / 2f)));

    }

    private void Start() {
        List<string> names = new List<string>(Enum.GetNames(typeof(ResourceTypes)));
        names.Remove(ResourceTypes.Geen.ToString());
        _offer.AddOptions(names);
        _return.AddOptions(names);
    }
}
