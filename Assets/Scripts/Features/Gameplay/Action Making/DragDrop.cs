using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the drag-drop functionality of the blockly system.
/// </summary>
public class DragDrop : MonoBehaviour
{
    private Blockly _block;

    [SerializeField]
    private GraphicRaycaster _raycaster = null;

    [SerializeField]
    private EventSystem _evSys = null;

    private PointerEventData _data = null;

    private Vector3 _mousePosition => Input.mousePosition;
    private Vector3 _mousePrevious = Vector3.zero;

    private void SaveOriginPositions() {
        _mousePrevious = _mousePosition;
    }

    public void SetBlock(Blockly block) {
        _block = block;
    }

    private void MoveObject() {
        if (_block == null)
            return;

        // Mouse position relative to it's previous position is being determined.
        Vector2 newPos = _mousePosition - _mousePrevious;

        _block.transform.position += new Vector3
        (
            newPos.x,
            newPos.y,
            0
        );

        SaveOriginPositions();
    }

    public void Update() {
        _data = new PointerEventData(_evSys);
        _data.position = _mousePosition;

        if (Input.GetMouseButtonDown(0)) {
            List<RaycastResult> results = new List<RaycastResult>();
            _raycaster.Raycast(_data, results);
            foreach (RaycastResult r in results) {
                _block = r.gameObject.GetComponent<Blockly>();
                if (_block != null) {
                    break;
                }
            }
            if (_block != null) {
                if (_block.transform.parent != _block.transform.root) {
                    Action action = null;
                    Argument argument = null;
                    if (_block.GetType().IsSubclassOf(typeof(Action))) {
                        action = (Action)_block;
                        action.transform.parent.parent.GetComponent<ActionController>().RemoveFromActions(action);
                    }
                    else if (_block.GetType().IsSubclassOf(typeof(Argument))) {
                        argument = (Argument)_block;
                        if (argument.transform.parent.parent.GetType().IsSubclassOf(typeof(Condition)))
                            argument.transform.parent.parent.GetComponent<Condition>().RemoveFromArguments(argument);
                        else if (argument.transform.parent.parent.GetType().IsSubclassOf(typeof(Operation)))
                            argument.transform.parent.parent.GetComponent<Operation>().RemoveFromArguments(argument);
                    }
                }
            }
            SaveOriginPositions();
        }

        if (Input.GetMouseButtonUp(0)) {
            List<RaycastResult> results = new List<RaycastResult>();
            _raycaster.Raycast(_data, results);
            if (_block != null) {
                foreach (RaycastResult r in results) {
                    if (r.gameObject == _block.gameObject)
                        continue;
                    Blockly block = r.gameObject.GetComponent<Blockly>();
                    if (block != null) {
                        Action action = null;
                        Argument argument = null;
                        ActionController controller = null;
                        Condition condition = null;
                        Operation operation = null;
                        if (_block.GetType().IsSubclassOf(typeof(Action)))
                            action = (Action)_block;
                        else if (_block.GetType() == typeof(Argument))
                            argument = (Argument)_block;
                        if (block.GetType().IsSubclassOf(typeof(ActionController))) {
                            controller = (ActionController)block;
                            controller.AddToActions(action);
                            break;
                        }
                        if (block.GetType().IsSubclassOf(typeof(Condition))) {
                            condition = (Condition)block;
                            condition.AddToArguments(argument);
                            break;
                        }
                        else if (block.GetType().IsSubclassOf(typeof(Operation))) {
                            operation = (Operation)block;
                            operation.AddToArguments(argument);
                            break;
                        }
                    }
                }
            }
            _block = null;
            SaveOriginPositions();
        }

        if (Input.GetMouseButton(0))
            if (_block != null)
                MoveObject();
    }
}
