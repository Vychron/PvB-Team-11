using UnityEngine;
using UnityEngine.UI;

public class TimerGauge : MonoBehaviour {

    private Timer _timer;

    private float _progress = 0f;
    private float _duratrion = 0f;
    private float _currentTime = 0f;

    private Image _image = null;

    public Timer AssignedTimer {
        set { _timer = value; }
    }

    void Start() {
        _image = GetComponent<Image>();
        TimerAPI.OnTimerEnd += Complete;
        _duratrion = _timer.GetDuration();
    }

    void Update() {
        _currentTime = _timer.GetSpentTime();
        _progress = _currentTime / _duratrion;
        _image.fillAmount = _progress;
        _image.color = new Color(1 - _progress, _progress, 0f);
    }

    private void Complete(Timer timer) {
        if (timer == _timer) {
        TimerAPI.OnTimerEnd -= Complete;
        Destroy(gameObject);
        }
    }
}
