using UnityEngine;

/// <summary>
/// Main villager component that controls the villager.
/// </summary>
public class Villager : MonoBehaviour {

    public House Home {
        get {
            return _home;
        }
        set {
            if (_home == null)
                _home = value;
        }
    }

    private House _home;

}
