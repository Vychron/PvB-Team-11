using UnityEngine;

/// <summary>
/// Debug script to test timers and their visualization.
/// </summary>
public class TimerSpawner : MonoBehaviour {

    [SerializeField]
    private TimerGauge _gauge = null;

    [SerializeField]
    private Transform _transform = null;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Timer timer = Timers.Instance.CreateTimer(5f);
            TimerGauge gauge = Instantiate(_gauge, _transform);
            gauge.AssignedTimer = timer;
        }
    }
}
