using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the blockly canvas.
/// Allows execution of blocks and clears all blocks when closing.
/// </summary>
public class BlocklyCanvasController : MonoBehaviour {

    [SerializeField]
    private List<Controller> _controllers;

    private int _controllerCount => _controllers.Count;

    /// <summary>
    /// Enables the canvas.
    /// </summary>
    public void EnableCanvas() {
        CanvasAPI.CanvasEnabled();
        _controllers = new List<Controller>();
    }

    /// <summary>
    /// Adds an action controller to the controller list.
    /// </summary>
    /// <param name="c">Controller to be added to the list.</param>
    public void AddToList(Controller c) {
        if (!_controllers.Contains(c))
            _controllers.Add(c);
    }

    /// <summary>
    /// Removes an action controller from the controller list.
    /// </summary>
    /// <param name="c">Controller to be removed from the list.</param>
    public void RemoveFromList(Controller c) {
        if (_controllers.Contains(c))
            _controllers.Remove(c);
    }

    /// <summary>
    /// Executes all controllers on the canvas.
    /// </summary>
    public void Execute() {
        for (int i = 0; i < _controllerCount; i++) {
            _controllers[i].Execute();
        }
        DisableCanvas();
    }

    /// <summary>
    /// Disables the canvas.
    /// </summary>
    public void DisableCanvas() {
        CanvasAPI.CanvasDisabled();
        if (_controllerCount > 0)
            for (int i = 0; i < _controllerCount; i++)
                Destroy(_controllers[i].gameObject);

        transform.root.gameObject.SetActive(false);
    }

}
