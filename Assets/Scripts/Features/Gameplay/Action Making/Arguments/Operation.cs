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
    protected Dropdown _dropdown;

    protected abstract void Start();
}
