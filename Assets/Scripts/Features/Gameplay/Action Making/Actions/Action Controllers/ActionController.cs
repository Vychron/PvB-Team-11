using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class for every block that executes actions.
/// </summary>
public abstract class ActionController : Action {

    [SerializeField]
    protected ContentSizeFitter[] _fitters;

    [SerializeField]
    protected List<Action> _actions = null;

    [SerializeField]
    protected Transform _actionsContainer = null;

    /// <summary>
    /// Add a new action to the actions list.
    /// </summary>
    /// <param name="a">Action to be added to the list.</param>
    public virtual void AddToActions(Action a) {
        if (_actions.Contains(a))
            return;
        if (_actions == null)
            _actions = new List<Action>();
        _actions.Add(a);
        a.transform.SetParent(_actionsContainer);
        ResourceAPI.UpdateResourceCosts();
        Invoke("Resize", .1f);
    }

    /// <summary>
    /// Remove an action from the actions list.
    /// </summary>
    /// <param name="a">The action to be removed from the list.</param>
    public virtual void RemoveFromActions(Action a) {
        if (!_actions.Contains(a))
            return;
        _actions.Remove(a);
        a.transform.SetParent(transform.root);
        ResourceAPI.UpdateResourceCosts();
        Invoke("Resize", .1f);
    }

    public virtual void Resize() {
        foreach (ContentSizeFitter f in _fitters) {
            // Reconstrain the fit to resize the component.
            f.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            f.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}
