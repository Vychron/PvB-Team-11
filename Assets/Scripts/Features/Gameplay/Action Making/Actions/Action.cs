using UnityEngine;

/// <summary>
/// Abstract class for all action blocks that can be executed.
/// </summary>
public abstract class Action : Blockly {

    [SerializeField]
    protected int
        _hunger = 0,
        _boredom = 0,
        _satisfaction = 0,
        _appreciation = 0;

    /// <summary>
    /// Returns the hunger, boredom, satisfaction and appreciation values of the action as Vector4.
    /// </summary>
    public Vector4 GetVillagerCosts {
        get { return new Vector4(_hunger, _boredom, _satisfaction, _appreciation); }
    }

    /// <summary>
    /// Execute the block's action.
    /// </summary>
    public abstract void Execute(Villager villager = null);
}
