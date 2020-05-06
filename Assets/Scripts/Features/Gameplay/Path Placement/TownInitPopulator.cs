using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Populator for the grid when the game starts.
/// </summary>
public class TownInitPopulator : MonoBehaviour {

    private List<Structure> structures => LevelGrid.Instance.GetStructures;

    [SerializeField]
    private GameObject
        _home,
        _path,
        _mayor;

    [SerializeField]
    private int
        _x = 0,
        _y = 0;

    private void Start() {
        GridAPI.OnGridCreated += Init;
    }

    private void Init() {
        _home = PlaceObject(_home, _x, _y);
        House home = (House)structures[0];
        _x += (int)home.entrance.x;
        _y += (int)home.entrance.y - 1;
        _path = PlaceObject(_path, _x, _y);
        _mayor = Instantiate(_mayor, structures[1].transform.position, Quaternion.identity, null);
        Villager vil = _mayor.GetComponent<Villager>();
        ResourceContainer.AddVillager(vil);
        vil.IsImmune = true;
        home.AddVillager(vil);
        vil.Home = home;

        VillagerAPI.JoinVillage(vil);
        //VillagerAPI.FinishMoving(vil);
    }

    private GameObject PlaceObject(GameObject obj, int x, int y) {
        Structure str = obj.GetComponent<Structure>();
        GameObject placedObj = null;
        if (str == null)
            placedObj = Instantiate(obj, new Vector2(x, y), Quaternion.identity, LevelGrid.Instance.transform);

        if (str.GetType() == typeof(House)) {
            House home = (House)str;
            LevelGrid.Instance.TryPlace(x, y, obj, home.entrance);
        }
        else
            LevelGrid.Instance.TryPlace(x, y, obj, Vector2Int.zero);
        return placedObj;
    }

}
