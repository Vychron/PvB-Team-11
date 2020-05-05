using UnityEngine;

/// <summary>
/// Populator for the grid when the game starts.
/// </summary>
public class TownInitPopulator : MonoBehaviour {

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
        _x += (int)_home.GetComponent<House>().entrance.x;
        _y += (int)_home.GetComponent<House>().entrance.y - 1;
        _path = PlaceObject(_path, _x, _y);
        _mayor = Instantiate(_mayor, _path.transform.position, Quaternion.identity, null);
        Villager vil = _mayor.GetComponent<Villager>();
        ResourceContainer.AddVillager(vil);
        vil.IsImmune = true;
        vil.Home = _home.GetComponent<House>();
        VillagerAPI.FinishMoving(vil);
        _home.GetComponent<House>().AddVillager(vil);
    }

    private GameObject PlaceObject(GameObject obj, int x, int y) {
        GameObject placedObj = Instantiate(obj, new Vector2(x, y), Quaternion.identity, LevelGrid.Instance.transform);

        Structure str = placedObj.GetComponent<Structure>();
        if (str.GetType() == typeof(House)) {
            House home = (House)str;
            LevelGrid.Instance.TryPlace(x, y, placedObj, home.entrance);
        }
        else
            LevelGrid.Instance.TryPlace(x, y, placedObj, Vector2Int.zero);
        return placedObj;
    }

}
