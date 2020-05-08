using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the needs of a villager.
/// </summary>
public class NeedsDisplayer : MonoBehaviour {

    [SerializeField]
    private Vector3Int _needs = Vector3Int.zero;

    [SerializeField]
    private Text
        _name = null,
        _satisfaction = null;

    [SerializeField]
    private NeedsGauge
        _hungerGauge = null,
        _boredomGauge = null;

    private Villager _villager = null;

    private bool _enabled = false;

    /// <summary>
    /// Static reference to the component.
    /// </summary>
    public static NeedsDisplayer Instance = null;

    private void Start() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Enables the display.
    /// </summary>
    /// <param name="villager"></param>
    public void Enable(Villager villager) {
        StartCoroutine(EnableRoutine(villager));
    }

    private IEnumerator EnableRoutine(Villager villager) {
        _villager = villager;
        transform.GetChild(0).gameObject.SetActive(true);
        _enabled = true;
        GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Button>().enabled = true;
        yield break;
    }

    /// <summary>
    /// Disables the display.
    /// </summary>
    public void Disable() {
        _needs = Vector3Int.zero;
        _name.text = "Niemand";
        _satisfaction.text = "-0";
        GetComponent<Button>().enabled = false;
        _enabled = false;
    }

    private void Update() {
        if (!_enabled)
            return;
        _needs = _villager.GetComponent<VillagerNeeds>().GetNeeds;
        _name.text = _villager.name;
        _hungerGauge.Need = _needs.x;
        _boredomGauge.Need = _needs.y;
        _satisfaction.text = _needs.z.ToString();
    }
}
