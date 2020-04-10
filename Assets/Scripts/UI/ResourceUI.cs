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
        ResourceAPI.OnUpdateResources += UpdateResources;
        UpdateResources();
    }

    private void UpdateResources() {
        _populationText.text = "Inwoners: " + ResourceContainer.PopulationCount.ToString() + "/" + ResourceContainer.PopulationCap.ToString();
        _appreciationText.text = "Waardering: " + ResourceContainer.Appreciation.ToString();
        _woodText.text = "Hout: " + ResourceContainer.Wood.ToString();
        _stoneText.text = "Steen: " + ResourceContainer.Stone.ToString();
        _foodText.text = "Eten: " + ResourceContainer.Food.ToString();
    }
}
