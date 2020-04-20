using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for argument operations.
/// </summary>
public abstract class Operation : Argument {

    [SerializeField]
    protected List<Argument> _arguments;
}
