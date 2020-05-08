using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Controls the highlight object, tells it where it needs to be positioned,
/// the size it needs to have, and what color the highlighted area should be.
/// </summary>
public class ActionHighlight : MonoBehaviour {

    [SerializeField]
    private InputField
        _x = null,
        _y = null,
        _width = null,
        _height = null,
        _object = null;

    private Vector3 _size = Vector3.zero;

    private bool _selected = false;

    private Transform _highlight => Highlighter.Instance.transform;

    // Update is called once per frame
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
                _selected = true;

            else {
                _selected = false;
                _highlight.localScale = Vector3.zero;
                foreach (Transform t in transform.GetComponentsInChildren<Transform>()) {
                    if (
                        t.GetComponent<ActionHighlight>() != null &&
                        t.gameObject != gameObject
                       )
                        return;
                    if (t.gameObject == EventSystem.current.currentSelectedGameObject)
                        _selected = true;
                }
            }
        }

        if (!_selected)
            return;

        _highlight.position = new Vector3(int.Parse(_x.text), int.Parse(_y.text));

        if (
            _width == null ||
            _height == null
            ) {
            if (_object != null) {
                GameObject obj = Resources.Load("Prefabs/Buildings/" + _object.text) as GameObject;
                if (obj != null)
                    _size = obj.GetComponent<Structure>().size;
            }
        }
        else {
            _size = new Vector3(int.Parse(_width.text), int.Parse(_height.text));
        }
        _highlight.localScale = _size;

        if (LevelGrid.Instance.CheckArea(int.Parse(_x.text), int.Parse(_y.text), (int)_size.x, (int)_size.y))
            _highlight.GetComponent<RawImage>().color = new Color(0, 255, 0, 0.5f);
        else
            _highlight.GetComponent<RawImage>().color = new Color(255, 0, 0, 0.5f);
    }
}
