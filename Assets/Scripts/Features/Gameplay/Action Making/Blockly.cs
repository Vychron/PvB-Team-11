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

    public virtual void OnPointerEnter(PointerEventData eventData) {
        SetText();
        StartCoroutine(ShowText());
    }

    public static IEnumerator ShowText() {
        yield return new WaitForSeconds(0.5f);
        _hText.transform.parent.position = Input.mousePosition;
        _hText.enabled = true;
        _hImage.enabled = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        _hText.enabled = false;
        _hImage.enabled = false;
        StopAllCoroutines();
    }

    public virtual string GetText() {
        return " ";
    }

    public virtual void SetText() {
        _hText.text = GetText();
    }

    public virtual int GetDepth() {
        int currentDepth = 0;
        Blockly parent = transform.parent.parent?.GetComponent<Blockly>();
        if (!parent)
            return 0;
        currentDepth += 2;
        return currentDepth + parent.GetDepth();
    }

}
