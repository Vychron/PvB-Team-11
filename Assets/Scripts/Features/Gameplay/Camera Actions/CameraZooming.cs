using UnityEngine;

/// <summary>
/// Manages the camera zooming.
/// </summary>
public class CameraZooming : MonoBehaviour {

    [SerializeField]
    private float _zoomSpeed = 1f;

    [SerializeField]
    private float
        _minZoomLevel = 2f,
        _maxZoomLevel = 17.5f;

    private Camera _cam;

    private float _targetZoomLevel = 0f;

    private void Start() {
        _cam = Camera.main;
        _targetZoomLevel = _cam.orthographicSize;
    }

    private void Update() {
        if (Input.mouseScrollDelta.y > 0) {
            _targetZoomLevel -= _zoomSpeed;
            if (_targetZoomLevel < _minZoomLevel)
                _targetZoomLevel = _minZoomLevel;
        }
        else if (Input.mouseScrollDelta.y < 0) {
            _targetZoomLevel += _zoomSpeed;
            if (_targetZoomLevel > _maxZoomLevel)
                _targetZoomLevel = _maxZoomLevel;
        }
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _targetZoomLevel, .02f);

    }
}
