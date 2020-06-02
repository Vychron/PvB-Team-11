using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple gauge to visualize the progress of a timer.
/// </summary>
public class TimerGauge : MonoBehaviour {

    private Timer _timer;

    private float _progress = 0f;
    private float _duratrion = 0f;
    private float _currentTime = 0f;

    [SerializeField]
    private Image
        _frontImage = null,
        _backImage = null;

    /// <summary>
    /// Assigns a timer to the gauge.
    /// </summary>
    public Timer AssignedTimer {
        set { _timer = value; }
    }

    private void Start() {
        TimerAPI.OnTimerEnd += Complete;
        _duratrion = _timer.GetDuration();
    }

    private void Update() {
        _currentTime = _timer.GetSpentTime();
        _progress = _currentTime / _duratrion;
        _frontImage.fillAmount = _progress;
        _backImage.fillAmount = _progress + 0.02f;
        _frontImage.color = new Color(1 - _progress, _progress, 0f);
    }

    private void Complete(Timer timer) {
        if (timer == _timer) {
        TimerAPI.OnTimerEnd -= Complete;
        Destroy(gameObject);
        }
    }
}
