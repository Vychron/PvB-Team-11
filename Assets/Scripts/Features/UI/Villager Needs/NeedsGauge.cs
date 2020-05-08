using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the size and color of a gauge.
/// </summary>
public class NeedsGauge : MonoBehaviour {

    [SerializeField]
    private Image _image = null;

    [SerializeField]
    private bool _invertGauge = false;

    /// <summary>
    /// Sets the value of the gauge.
    /// </summary>
    public float Need {
        set {
            _need = (float)value / 100f;
            UpdateGauge();
        }
    }

    private float _need = 0f;

    private void UpdateGauge() {
        if (_invertGauge)
            _image.color = new Color(_need, 1f - _need, 0);
        else
            _image.color = new Color(1f - _need, _need, 0);
        if (_need > 1)
            _need = 1f;
        _image.rectTransform.sizeDelta = new Vector2(100f * _need, _image.rectTransform.sizeDelta.y);
    }
}
