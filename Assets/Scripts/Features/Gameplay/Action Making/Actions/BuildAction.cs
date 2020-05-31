using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Action for placing buildings.
/// </summary>
public class BuildAction : Action {

    private int _x => int.Parse(_xField.text);
    private int _y => int.Parse(_yField.text);

    [SerializeField]
    private Dropdown _object = null;

    [SerializeField]
    private List<GameObject> _buildings = null;

    private string _objectName = null;

    /// <summary>
    /// Returns the name of the selected building as string.
    /// </summary>
    public string GetObjectName {
        get { return _objectName; }
    }

    [SerializeField]
    private InputField
        _xField = null,
        _yField = null;

    private void Start() {
        _object?.onValueChanged.AddListener(delegate { SetBuilding(); });
        List<string> objNames = new List<string>();
        foreach (GameObject g in _buildings) {
            objNames.Add(g.name);
        }

        _object?.AddOptions(objNames);
        SetBuilding();
    }

    private void SetBuilding() {
        if (_object?.options.Count > 0)
            _objectName = _buildings[_object.value]?.name;
        else
            _objectName = null;
    }

    public override void Execute(Villager villager = null) {
        PlaceBuilding();
    }

    /// <summary>
    /// Try to place a building.
    /// </summary>
    /// <param name="villager">The villager to build the structure.</param>
    /// <param name="fromString">Whether or not the building information is read from a string.</param>
    /// <param name="objectName">The name of the building.</param>
    /// <param name="xPosition">The x position of the building.</param>
    /// <param name="yPosition">The y position of the building.</param>
    public void PlaceBuilding(Villager villager = null, bool fromString = false, string objectName = null, int xPosition = 0, int yPosition = 0) {

        if (villager == null) {
            List<Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            foreach (Villager v in villagers)
                if (!v.Available)
                    villagers.Remove(v);

            int count = villagers.Count;
            if (count == 0)
                return;
            villager = villagers[Random.Range(0, count)];
        }

        string objName;
        int x;
        int y;

        if (fromString) {
            objName = objectName;
            x = xPosition;
            y = yPosition;
        }
        else {
            objName = _objectName;
            x = _x;
            y = _y;
        }

        GameObject obj = Resources.Load("Prefabs/Buildings/" + objName + " Foundation") as GameObject;
        if (obj) {
            GameObject structure = null;
            Structure str = obj.GetComponent<Structure>();
            if (str.entrance != Vector2.one * -1f)
                structure = LevelGrid.Instance.TryPlace(x, y, obj, str.entrance);
            else
                structure = LevelGrid.Instance.TryPlace(x, y, obj, Vector2Int.zero);

            if (structure.GetComponent<Structure>().GetType() == typeof(Foundation)) {
                structure.GetComponent<Foundation>().Builder = villager;
                VillagerAPI.MovementAssigned(villager, (Vector2)structure.transform.position + new Vector2(Mathf.Floor(structure.GetComponent<Structure>().size.x / 2), -1f));
            }
        }
        else
            Debug.LogWarning("Geen object gevonden om te plaatsen.");

        
    }

    public override string GetText() {
        return "Bouw(" + _objectName + ", " + _x.ToString() + ", " + _y.ToString() + ");";
    }
}
