using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the cost display of a trade action.
/// </summary>
public class TradeActionCosts : ActionCosts {

    [SerializeField]
    private Dropdown
        _in = null,
        _out = null;


    protected override void Start() {
        SetVillagerCosts();
        base.Start();
        _in?.onValueChanged.AddListener(delegate { ValueChanged(); });
        _out?.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    protected override void ValueChanged() {
        _resources = Vector3Int.zero;
        if (_in != null)
            switch (_in.value) {
                case 0:
                    _resources.x += -1 * int.Parse(_sizeMultipliers[0].text);
                    break;
                case 1:
                    _resources.y += -1 * int.Parse(_sizeMultipliers[0].text);
                    break;
                case 2:
                    _resources.z += -1 * int.Parse(_sizeMultipliers[0].text);
                    break;
                default:
                    break;
            }

        if (_out != null)
            switch (_out.value) {
                case 0:
                    _resources.x += Mathf.FloorToInt(0.5f * int.Parse(_sizeMultipliers[0].text));
                    ;
                    break;
                case 1:
                    _resources.y += Mathf.FloorToInt(0.5f * int.Parse(_sizeMultipliers[0].text));
                    break;
                case 2:
                    _resources.z += Mathf.FloorToInt(0.5f * int.Parse(_sizeMultipliers[0].text));
                    break;
                default:
                    break;
            }
        SetVillagerCosts();

        UpdateValues();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        _in?.onValueChanged.RemoveListener(delegate { ValueChanged(); });
        _out?.onValueChanged.RemoveListener(delegate { ValueChanged(); });
    }
}
