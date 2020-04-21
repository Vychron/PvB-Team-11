using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class for argument operations.
/// </summary>
public abstract class Operation : Argument {

    [SerializeField]
    protected List<Argument> _arguments;

    [SerializeField]
    protected Transform _argumentsContainer = null;

    [SerializeField]
    protected Dropdown _dropdown;

    protected abstract void Start();

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
        a.transform.SetParent(_argumentsContainer);
    }

    /// <summary>
    /// Remove an argument from the arguments list.
    /// </summary>
    /// <param name="a">The argument to be removed from the list.</param>
    public virtual void RemoveFromArguments(Argument a) {
        if (!_arguments.Contains(a))
            return;
        _arguments.Remove(a);
        a.transform.SetParent(transform.root);
    }
}
