using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocklyCanvasController : MonoBehaviour {

    [SerializeField]
    private List<Controller> _controllers;

    private int _controllerCount => _controllers.Count;

    private void Start() {
        _controllers = new List<Controller>();
    }

    public void AddToList(Controller c) {
        if (!_controllers.Contains(c))
            _controllers.Add(c);
    }

    public void RemoveFromList(Controller c) {
        if (_controllers.Contains(c))
            _controllers.Remove(c);
    }


    public void Execute() {
        for (int i = 0; i < _controllerCount; i++) {
            _controllers[i].Execute();
        }
        DisableCanvas();
    }

    public void DisableCanvas() {
        for (int i = 0; i < _controllerCount; i++) {
            Destroy(_controllers[i].gameObject);
        }
        _controllers = new List<Controller>();
        transform.root.gameObject.SetActive(false);
    }

}
