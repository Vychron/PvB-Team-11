using UnityEngine;

///<summary>
/// Manages the movement on the camera, based on how far it is zoomed in.
///</summary>
public class CameraMovement : MonoBehaviour {

    private float
        _topBoundary = 0f,
        _bottomBoundary = 0f,
        _leftBoundary = 0f,
        _rightBoundary = 0f;

    private bool _canDrag = true;

    private Vector3 _oldPos = Vector3.zero;
    private Vector3 _mouseOrigin = Vector3.zero;

    [SerializeField]
    private float _dragSpeed = 4;

    [SerializeField]
    private float _boundary = 15f;

    private void Start() {
        _oldPos = transform.position;
        _leftBoundary = transform.position.x - _boundary;
        _rightBoundary = transform.position.x + _boundary;
        _topBoundary = transform.position.x + _boundary;
        _bottomBoundary = transform.position.x - _boundary;
    }

    private void SaveOriginPositions() {
        _oldPos = transform.position;
        _mouseOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    // Moves the camera around based on the mouse movemnt.
    private void MoveObject() {

        // Mouse position relative to it's previous position is being determined.
        Vector2 newPos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _mouseOrigin;

        float camSize = Camera.main.orthographicSize;
        // Position changes depending on the mouse movement.
        transform.position = new Vector3
        (
            _oldPos.x - newPos.x * _dragSpeed * camSize,
            _oldPos.y - newPos.y * _dragSpeed * camSize,
            transform.position.z
        );
        ClampToBounds();
        SaveOriginPositions();
    }

    private void Update() {
        if (
            Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonUp(0) 
           )
            SaveOriginPositions();

        if (Input.GetMouseButton(0))
            MoveObject();
    }

    private void ClampToBounds() {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3
            (
                Mathf.Clamp(transform.position.x, _leftBoundary, _rightBoundary),
                Mathf.Clamp(transform.position.y, _bottomBoundary, _topBoundary),
                transform.position.z
            ),
            .5f);
    }
}
