using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Abstract base of all blocks.
/// Every block will derive from this class.
/// </summary>
public abstract class Blockly : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    protected int _depth = 0;

    protected static Text _hText => HoverText.Instance?.GetComponentInChildren<Text>();

    protected static Image _hImage => HoverText.Instance?.GetComponent<Image>();

    /// <summary>
    /// Pointer event for hovering over a block.
    /// </summary>
    public virtual void OnPointerEnter(PointerEventData eventData) {
        SetText();
        StartCoroutine(ShowText());
    }

    /// <summary>
    /// Short delay before showing the hover text.
    /// </summary>
    public static IEnumerator ShowText() {
        yield return new WaitForSeconds(0.5f);
        _hText.transform.parent.position = Input.mousePosition;
        _hText.enabled = true;
        _hImage.enabled = true;
    }

    /// <summary>
    /// Pointer event for when moving the mouse off of a block.
    /// </summary>
    public virtual void OnPointerExit(PointerEventData eventData) {
        _hText.enabled = false;
        _hImage.enabled = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// Gets the text that will be displayed as hover text.
    /// </summary>
    /// <returns>Returns a string containing the hover text to be displayed.</returns>
    public virtual string GetText() {
        return " ";
    }

    /// <summary>
    /// Sets the hover text's text.
    /// </summary>
    public virtual void SetText() {
        _hText.text = GetText();
    }

    /// <summary>
    /// Gets how deep the block is nested in a blockly tree.
    /// </summary>
    /// <returns>Returns the nested depth of the block.</returns>
    public virtual int GetDepth() {
        int currentDepth = 0;
        Blockly parent = transform.parent.parent?.GetComponent<Blockly>();
        if (!parent)
            return 0;
        currentDepth += 2;
        return currentDepth + parent.GetDepth();
    }

    protected virtual void OnDestroy() {
        _hText.enabled = false;
        _hImage.enabled = false;
        StopAllCoroutines();
    }

}
