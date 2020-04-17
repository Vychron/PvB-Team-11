using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawner for villagers that attempts to spawn a new villager at semi-random intervals.
/// </summary>
public class VillagerSpawner : MonoBehaviour {

    private Timer _spawnTimer = null;

    private int _gridSize = 0;

    [SerializeField]
    private Villager _villagerPrefab = null;

    private void Start() {
        GridAPI.OnGridCreated += OnGridInitialized;
        TimerAPI.OnTimerEnd += SpawnVillager;
    }

    private void OnGridInitialized() {
        _gridSize = LevelGrid.Instance.GetGrid.Length;
        CreateNewTimer();
    }

    private void CreateNewTimer() {
        float maxDuration = 6f - (0.01f * ResourceContainer.Appreciation);
        float duration = Random.Range(maxDuration / 2f, maxDuration);
        _spawnTimer = Timers.Instance.CreateTimer(duration);
    }

    private void SpawnVillager(Timer timer) {
        StartCoroutine(TrySpawnVillager(timer));
    }

    private IEnumerator TrySpawnVillager(Timer timer) {
        if (timer != _spawnTimer)
            yield break;
        CreateNewTimer();
        if (ResourceContainer.Appreciation > 0 && ResourceContainer.PopulationCount < ResourceContainer.PopulationCap) {
            Vector2Int location = Vector2Int.zero;

            // A couple of random numbers will determine where the villager will be spawned.
            int borderAxis = Random.Range(0, 2);
            int side = Random.Range(0, 2);
            int borderPosition = Random.Range(0, _gridSize);
            // Translate number representing the border side to the actual border position.
            if (side == 1)
                side = _gridSize - 1;

            if (borderAxis == 0) {
                location.x = side;
                location.y = borderPosition;
            }
            else {
                location.x = borderPosition;
                location.y = side;
            }

            // Find a house with an empty bed.
            List<Structure> structures = LevelGrid.Instance.GetStructures;
            int count = structures.Count;
            House house = null;
            for (int i = 0; i < count; i++) {
                if (structures[i].GetType() != typeof(House))
                    continue;

                house = (House)structures[i];
                if (house.VillagerCount < house.VillagerCap)
                    break;
            }
            if (house != null) {
                Debug.LogError(house.name);
                Villager villager = Instantiate(_villagerPrefab, null);
                villager.transform.position = new Vector2(location.x, location.y);
                house.AddVillager(villager);
                villager.Home = house;
                yield return new WaitForEndOfFrame();
                VillagerAPI.JoinVillage(villager);
            }
        }
    }
}
