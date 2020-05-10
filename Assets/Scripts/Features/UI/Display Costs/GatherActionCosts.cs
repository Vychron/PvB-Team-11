using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the costs/earns of the gather action.
/// </summary>
public class GatherActionCosts : ActionCosts {

    [SerializeField]
    private Dropdown _resource = null;

    private int _boredomRestoreValue = 0;

    protected override void Start() {
        SetVillagerCosts();
        _boredomRestoreValue = _boredom;
        base.Start();
        _resource?.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        _resource?.onValueChanged.RemoveListener(delegate { ValueChanged(); });
    }

    protected override void ValueChanged() {
        _boredom = _boredomRestoreValue;
        _resources = Vector3Int.zero;
        if (_resource != null)
            switch (_resource.value) {
                case 0:
                    _resources.x = 1;
                    break;
                case 1:
                    _resources.y = 1;
                    break;
                case 2:
                    _resources.z = 1;
                    break;
                default:
                    _boredom = 0;
                    break;
            }
        SetVillagerCosts();

        foreach (InputField i in _sizeMultipliers) {
            _resources *= int.Parse(i.text);
            _boredom *= int.Parse(i.text);
        }

        UpdateValues();
    }
}
