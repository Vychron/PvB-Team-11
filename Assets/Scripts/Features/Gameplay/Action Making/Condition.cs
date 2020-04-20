using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for action controllers with conditions.
/// </summary>
public abstract class Condition : ActionController {

    protected List<Argument> _arguments;

    [SerializeField]
    protected Transform _argumentsContainer = null;

    /// <summary>
    /// Add a new argument to the arguments list.
    /// </summary>
    /// <param name="a">argument to be added to the list.</param>
    public virtual void AddToArguments(Argument a) {
        if (_arguments.Contains(a))
            return;
        if (_arguments == null)
            _arguments = new List<Argument>();
        _arguments.Add(a);
        a.transform.parent = _actionsContainer;
    }

    /// <summary>
    /// Remove an argument from the arguments list.
    /// </summary>
    /// <param name="a">The argument to be removed from the list.</param>
    public virtual void RemoveFromArguments(Argument a) {
        if (!_arguments.Contains(a))
            return;
        _arguments.Remove(a);
        a.transform.parent = transform.root;
    }
}
