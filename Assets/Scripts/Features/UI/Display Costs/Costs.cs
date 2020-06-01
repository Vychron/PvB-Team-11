using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the costs/earns of the actions inside of the controller.
/// </summary>
public class Costs : MonoBehaviour {

    protected int
        _hunger = 0,
        _boredom = 0,
        _satisfaction = 0,
        _appreciation = 0;

    protected Vector3Int _resources = Vector3Int.zero;

    [SerializeField]
    protected Transform
        _hungerContainer = null,
        _boredomContainer = null,
        _satisfactionContainer = null,
        _woodContainer = null,
        _stoneContainer = null,
        _foodContainer = null,
        _appreciationContainer = null;

    private void Start() {
        ResourceAPI.OnUpdateResourceCost += GetCostValues;
        GetCostValues();
    }

    private void OnDestroy() {
        ResourceAPI.OnUpdateResourceCost -= GetCostValues;
    }

    private void GetCostValues() {
        Transform[] tfs = transform.parent.GetComponentsInChildren<Transform>();
        _hunger = 0;
        _boredom = 0;
        _satisfaction = 0;
        _appreciation = 0;
        _resources = Vector3Int.zero;
        ActionCosts c = null;
        foreach (Transform t in tfs) {
            c = t.GetComponent<ActionCosts>();
            if (c != null) {
                _hunger += c._hunger;
                _boredom += c._boredom;
                _satisfaction += c._satisfaction;
                _appreciation += c._appreciation;
                _resources += c._resources;
            }
        }
        UpdateValues();
    }

    protected virtual void UpdateValues() {
        SetValue(_hungerContainer, _hunger);
        SetValue(_boredomContainer, _boredom, true);
        SetValue(_satisfactionContainer, _satisfaction);
        SetValue(_woodContainer, _resources.x, false, ResourceTypes.Hout);
        SetValue(_stoneContainer, _resources.y, false, ResourceTypes.Steen);
        SetValue(_foodContainer, _resources.z, false, ResourceTypes.Eten);
        SetValue(_appreciationContainer, _appreciation);
    }

    protected void SetValue(Transform trans, int value, bool invert = false, ResourceTypes resource = ResourceTypes.Geen) {
        if (value == 0)
            trans.gameObject.SetActive(false);
        else {
            trans.gameObject.SetActive(true);
            Text txt = trans.GetComponentInChildren<Text>();
            txt.text = "";
            if (value > 0)
                txt.text = "+";

            txt.text += (value).ToString();
            txt.color = new Color(1, 1, 1);
            switch (resource) {
                case ResourceTypes.Hout:
                    if (Mathf.Abs(value) > ResourceContainer.Wood)
                        SetColor(txt);
                    break;
                case ResourceTypes.Steen:
                    if (Mathf.Abs(value) > ResourceContainer.Stone)
                        SetColor(txt);
                    break;
                case ResourceTypes.Eten:
                    if (Mathf.Abs(value) > ResourceContainer.Food)
                        SetColor(txt);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetColor(Text txt) {
            if (!txt.text.StartsWith("+"))
                txt.color = new Color(1, 0, 0);
            else
                txt.color = new Color(0, 1, 0);
    }

}
