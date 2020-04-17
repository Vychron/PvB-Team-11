using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {

    [SerializeField]
    private Text
        _populationText = null,
        _appreciationText = null,
        _woodText = null,
        _stoneText = null,
        _foodText = null;

    private void Start() {
        GridAPI.OnGridCreated += Init;
    }

    private void Init() {
        ResourceAPI.OnUpdateResources += UpdateResources;
        UpdateResources();
    }

    private void UpdateResources() {
        _populationText.text = ResourceContainer.PopulationCount.ToString() + "/" + ResourceContainer.PopulationCap.ToString();
        _appreciationText.text = ResourceContainer.Appreciation.ToString();
        _woodText.text = ResourceContainer.Wood.ToString();
        _stoneText.text = ResourceContainer.Stone.ToString();
        _foodText.text = ResourceContainer.Food.ToString();
    }
}
