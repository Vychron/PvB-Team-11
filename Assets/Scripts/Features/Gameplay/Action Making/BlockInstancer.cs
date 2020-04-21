using UnityEngine;

/// <summary>
/// Handles instancing new blocks.
/// </summary>
public class BlockInstancer : MonoBehaviour {

    [SerializeField]
    private RectTransform _blockSpawnLocation = null;

    [SerializeField]
    private Blockly _instance = null;

    private Blockly _instancedObject = null;

    /// <summary>
    /// Execute when a click on the object is detected.
    /// </summary>
    public void OnClick() {
        _instancedObject = Instantiate(_instance, _blockSpawnLocation.position, Quaternion.identity, transform.root);
    }

    /// <summary>
    /// Add the controller to the canvas controller's controller list.
    /// </summary>
    public void AddToList() {
        GetComponentInParent<BlocklyCanvasController>().AddToList((Controller)_instancedObject);
    }
}
