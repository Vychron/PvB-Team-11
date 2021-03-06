﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Moves the camera to a position determined by given input fields.
/// </summary>
public class ActionsCameraMover : MonoBehaviour {

    [SerializeField]
    private InputField
        _x = null,
        _y = null;

    private bool _selected = false;

    private Camera _cam => Camera.main;

    private Vector2 _position = Vector2.one * 16;

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
                _selected = true;

            else {
                _selected = false;

                foreach (Transform t in transform.GetComponentsInChildren<Transform>()) {
                    if (
                        t.GetComponent<ActionsCameraMover>() != null &&
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

        if (
            _x != null &&
            _y != null
           ) {

            if (
                _x.text == "" ||
                _y.text == "" ||
                _x.text[0] == '-' ||
                _y.text[0] == '-'
               )
                return;

            _position = new Vector2(int.Parse(_x.text), int.Parse(_y.text));
            MoveCamera(_position);
        }

    }
    private void MoveCamera(Vector2 position) {
        Vector3 newPos = new Vector3(position.x, position.y, _cam.transform.position.z);

        _cam.transform.position = Vector3.Lerp(_cam.transform.position, newPos, 2f * Time.deltaTime);

    }
}
