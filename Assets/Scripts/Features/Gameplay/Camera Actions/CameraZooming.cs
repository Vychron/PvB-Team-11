using UnityEngine;

/// <summary>
/// Manages the camera zooming.
/// </summary>
public class CameraZooming : MonoBehaviour {

    [SerializeField]
    private float _zoomSpeed = 1f;

    [SerializeField]
    private float
        _minZoomHeight = 5,
        _maxZoomHeight = 25;

    private void Update() {

        if (Input.mouseScrollDelta.y > 0) {
            transform.Translate(Vector3.forward * _zoomSpeed);
            if (transform.position.y < _minZoomHeight) {
                float distance = Mathf.Acos(transform.rotation.x) * Mathf.Abs(_minZoomHeight - transform.position.y);
                transform.Translate(Vector3.back * distance);
            }
        }
        else if (Input.mouseScrollDelta.y < 0) {
            transform.Translate(Vector3.back * _zoomSpeed);
            if (transform.position.y > _maxZoomHeight) {
                float distance = Mathf.Acos(transform.rotation.x) * Mathf.Abs(_maxZoomHeight - transform.position.y);
                transform.Translate(Vector3.forward * distance);
            }
        }

    }
}
