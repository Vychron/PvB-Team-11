using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the costs/earns of the action.
/// </summary>
public class ActionCosts : Costs {

    [SerializeField]
    protected InputField[] _inputFields = null;

    [SerializeField]
    protected InputField[] _sizeMultipliers = null;

    [SerializeField]
    private InputField _buildingPath = null;

    [SerializeField]
    private GameObject _prefab = null;

    protected virtual void Start() {
        for (int i = 0; i < _inputFields.Length; i++)
            _inputFields[i].onValueChanged.AddListener(delegate { ValueChanged(); });
        ValueChanged();
    }

    protected virtual void OnDestroy() {
        for (int i = 0; i < _inputFields.Length; i++)
            _inputFields[i].onValueChanged.RemoveListener(delegate { ValueChanged(); });
    }

    protected virtual void ValueChanged() {
        GameObject obj = null;

        if (_prefab != null)
            obj = _prefab;
        else
            obj = Resources.Load("Prefabs/Buildings/" + _buildingPath.text) as GameObject;

        if (obj != null)
            _resources = obj.GetComponent<Structure>().buildCost * -1;
        else
            _resources = Vector3Int.zero;

        if (_sizeMultipliers.Length != 0)
            for (int i = 0; i < _sizeMultipliers.Length; i++)
                _resources *= int.Parse(_sizeMultipliers[i].text);

        UpdateValues();
    }

    protected override void UpdateValues() {
        base.UpdateValues();
        ResourceAPI.UpdateResourceCosts();
    }
}
