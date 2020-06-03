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
        TradeResource((ResourceTypes)_offer.value, (ResourceTypes)_return.value, _amount, villager);
    }

    /// <summary>
    /// Tasks a villager to trade resources.
    /// </summary>
    /// <param name="offer">The resource you offer.</param>
    /// <param name="returnedResource">The resource you get in return.</param>
    /// <param name="amount">The amount of resources you offer.</param>
    /// <param name="villager">The villager that will trade the resources.</param>
    public void TradeResource(ResourceTypes offer, ResourceTypes returnedResource, int amount, Villager villager) {
        if (offer == ResourceTypes.Geen)
            return;
        if (returnedResource == ResourceTypes.Geen)
            return;
        if (amount == 0)
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

        switch (offer) {
            case ResourceTypes.Hout:
                ResourceContainer.Wood -= amount;
                break;
            case ResourceTypes.Steen:
                ResourceContainer.Stone -= amount;
                break;
            case ResourceTypes.Eten:
                ResourceContainer.Food -= amount;
                break;
            default:
                break;
        }

        VillagerAPI.MovementAssigned(assignee, (Vector2)market.transform.position + market.entrance);
        market.AddTask(new GatherTask(assignee, returnedResource, Mathf.FloorToInt(amount / 2f)));

    }

    private void Start() {
        List<string> names = new List<string>(Enum.GetNames(typeof(ResourceTypes)));
        names.Remove(ResourceTypes.Geen.ToString());
        _offer?.AddOptions(names);
        _return?.AddOptions(names);
    }

    public override string GetText() {
        return "Ruil(" + ((ResourceTypes)_offer.value).ToString() + ", " + ((ResourceTypes)_return.value).ToString() + ", " + _amount.ToString() + ");";
    }
}
