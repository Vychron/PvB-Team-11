using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data container of a house structure.
/// </summary>
public class House : ResourceSite {

    protected ResourceTypes _resource = ResourceTypes.Eten;

    /// <summary>
    /// Getter for the villager count of the house.
    /// </summary>
    public int VillagerCount {
        get {
            if (_villagers == null)
                _villagers = new List<Villager>();
            return _villagers.Count; }
    }

    /// <summary>
    /// Getter for the villager capacity of the house.
    /// </summary>
    public int VillagerCap {
        get { return _villagerCap; }
    }

    [SerializeField]
    private int _villagerCap = 1;

    /// <summary>
    /// Getter for a list of villagers living in the house.
    /// </summary>
    public List<Villager> Villagers {
        get {
            if (_villagers == null)
                _villagers = new List<Villager>();
            return _villagers;
        }
    }

    private List<Villager> _villagers = null;

    /// <summary>
    /// Add a villager to the house.
    /// </summary>
    /// <param name="villager">The villager to add to the house.</param>
    public void AddVillager(Villager villager) {
        if (_villagers == null)
            _villagers = new List<Villager>();
        _villagers.Add(villager);
    }

    /// <summary>
    /// Remove a villager from the house.
    /// </summary>
    /// <param name="villager">The villager to be removed from the house.</param>
    public void RemoveVillager(Villager villager) {
        if (!_villagers.Contains(villager))
            return;
        _villagers.Remove(villager);
    }

    private void Start() {
        if (_villagers == null)
            _villagers = new List<Villager>();

        TaskAPI.OnArriveAtTaskLocation += StartTask;
        TimerAPI.OnTimerEnd += FinishTask;
        ResourceAPI.UpdateResources();
        VillagerAPI.OnLeaveVillage += RemoveVillager;
        _tasks = new List<GatherTask>();
    }


}
