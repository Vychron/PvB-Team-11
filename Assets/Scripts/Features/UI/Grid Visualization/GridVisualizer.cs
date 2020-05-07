using UnityEngine;

public class GridVisualizer : MonoBehaviour {

    private Vector3 _zeroPos = Vector3.zero;

    [SerializeField]
    private Transform
        _hor = null,
        _vert = null,
        _backdrop = null;

    private void Update() {
        _zeroPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _zeroPos.z = 0;
        _zeroPos.x += gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _zeroPos.y += gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _vert.position = new Vector3(-16 + _zeroPos.x, _vert.position.y);
        _hor.position = new Vector3(_hor.position.x, -16 + _zeroPos.y);
    }
}
