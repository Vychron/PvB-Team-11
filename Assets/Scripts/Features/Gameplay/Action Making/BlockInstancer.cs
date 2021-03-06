﻿using UnityEngine;

/// <summary>
/// Handles instancing new blocks.
/// </summary>
public class BlockInstancer : MonoBehaviour {

    [SerializeField]
    private Blockly _instance = null;

    private Blockly _instancedObject = null;

    private DragDrop _dragDrop => GetComponentInParent<DragDrop>();

    /// <summary>
    /// Execute when a click on the object is detected.
    /// </summary>
    public void OnClick() {
        _instancedObject = Instantiate(_instance, Input.mousePosition, Quaternion.identity, transform.root);
        _dragDrop.SetBlock(_instancedObject);
    }

    /// <summary>
    /// Add the controller to the canvas controller's controller list.
    /// </summary>
    public void AddToList() {
        GetComponentInParent<BlocklyCanvasController>().AddToList((Controller)_instancedObject);
    }
}
