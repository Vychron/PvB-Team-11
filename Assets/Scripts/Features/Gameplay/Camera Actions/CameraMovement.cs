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

    [SerializeField]
    private float _verticalBoundsOffset = 0f;

    private void Start() {
        _leftBoundary = transform.position.x - _boundary;
        _rightBoundary = transform.position.x + _boundary;
        _topBoundary = transform.position.x + _boundary + _verticalBoundsOffset;
        _bottomBoundary = transform.position.x - _boundary + _verticalBoundsOffset;
    }

    private void SaveOriginPositions() {
        _oldPos = transform.position;
        _mouseOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    // Moves the camera around based on the mouse movemnt.
    private void MoveObject() {

        // Mouse position relative to it's previous position is being determined.
        Vector3 newPos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _mouseOrigin;

        // Position changes depending on the mouse movement.
        transform.position = new Vector3
        (
            _oldPos.x + -newPos.x * (_dragSpeed * 0.75f * transform.position.y),
            transform.position.y,
            _oldPos.z + -newPos.y * (_dragSpeed * 0.75f * transform.position.y)
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

    // Clamp the object to the bounds depending on the height of the camera.
    private void ClampToBounds() {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3
            (
                Mathf.Clamp(transform.position.x, _leftBoundary, _rightBoundary),
                transform.position.y,
                Mathf.Clamp(transform.position.z, _bottomBoundary + (transform.position.y * _verticalBoundsOffset * 0.25f), _topBoundary + (transform.position.y * _verticalBoundsOffset * 0.25f))
            ),
            .5f);
    }
}
