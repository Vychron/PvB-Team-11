using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Action for placing buildings.
/// </summary>
public class BuildAction : Action {

    private int _x => int.Parse(_xField.text);
    private int _y => int.Parse(_yField.text);
    private string _objectName => _nameField.text;

    [SerializeField]
    private InputField
        _xField = null,
        _yField = null,
        _nameField = null;

    public override void Execute(Villager villager = null) {
        if (villager == null) {
            List <Villager> villagers = new List<Villager>(ResourceContainer.Villagers);
            foreach (Villager v in villagers)
                if (!v.Available)
                    villagers.Remove(v);

            int count = villagers.Count;
            if (count == 0)
                return;
            villager = villagers[Random.Range(0, count)];
        }

        PlaceBuilding(villager);
    }

    /// <summary>
    /// Try to place a building.
    /// </summary>
    public void PlaceBuilding(Villager villager) {
        GameObject obj = Resources.Load("Prefabs/Buildings/" + _objectName + " Foundation") as GameObject;
        if (obj) {
            GameObject structure = null;
            Structure str = obj.GetComponent<Structure>();
            if (str.entrance != Vector2.one * -1f)
                structure = LevelGrid.Instance.TryPlace(_x, _y, obj, str.entrance);
            else
                structure = LevelGrid.Instance.TryPlace(_x, _y, obj, Vector2Int.zero);

            if (structure.GetComponent<Structure>().GetType() == typeof(Foundation)) {
                structure.GetComponent<Foundation>().Builder = villager;
                VillagerAPI.MovementAssigned(villager, (Vector2)structure.transform.position + new Vector2(Mathf.Floor(structure.GetComponent<Structure>().size.x / 2), -1f));
            }
        }
        else
            Debug.LogWarning("Geen object gevonden om te plaatsen.");
    }

    public override string GetText() {
        string indent = "";
        _depth = GetDepth();
        for (int i = 0; i < _depth; i++)
            indent += " ";
        return indent + "Build(" + _x.ToString() + ", " + _y.ToString() + ", \"" + _objectName + "\");";
    }
}
