using UnityEngine;

/// <summary>
/// Manages the needs of a villager.
/// </summary>
public class VillagerNeeds : MonoBehaviour {

    /// <summary>
    /// Returns the needs of the villager as a Vector3Int.
    /// </summary>
    public Vector3Int GetNeeds {
        get { return new Vector3Int(_hunger, _boredom, _satisfaction); }
    }

    private Timer
        _hungerTimer = null,
        _boredomTimer = null,
        _appreciationTimer = null;

    private int
        _hunger = 100,
        _boredom = 0,
        _satisfaction = 100;

    [SerializeField]
    private int
        _hungerTime = 7,
        _boredomTime = 5,
        _appreciationTime = 10;

    private void Start() {
        TimerAPI.OnTimerEnd += NewHungerTimer;
        TimerAPI.OnTimerEnd += NewBoredomTimer;
        TimerAPI.OnTimerEnd += UpdateAppreciation;
        _hungerTimer = Timers.Instance.CreateTimer(_hungerTime);
        _boredomTimer = Timers.Instance.CreateTimer(_boredomTime);
        _appreciationTimer = Timers.Instance.CreateTimer(_appreciationTime);
    }

    private void NewHungerTimer(Timer timer) {
        if (timer != _hungerTimer)
            return;

        _hunger--;
        _hungerTimer = Timers.Instance.CreateTimer(_hungerTime);
    }

    private void NewBoredomTimer(Timer timer) {
        if (timer != _boredomTimer)
            return;

        _boredom++;
        _boredomTimer = Timers.Instance.CreateTimer(_boredomTime);
    }

    private void UpdateAppreciation(Timer timer) {
        if (timer != _appreciationTimer)
            return;

        _appreciationTimer = Timers.Instance.CreateTimer(_appreciationTime);

        if (_hunger < 0) {
            _hunger = 0;
            _satisfaction -= 5;
        }
        else if (_hunger < 25)
            _satisfaction--;
        else if (_hunger == 100)
            _satisfaction += 5;
        else if (_hunger >= 75)
            _satisfaction++;

        if (_boredom > 100) {
            _boredom = 100;
            _satisfaction -= 5;
        }
        else if (_boredom > 75)
            _satisfaction--;
        else if (_boredom == 0)
            _satisfaction += 5;
        else if (_boredom <= 25)
            _satisfaction++;

        ResourceContainer.Appreciation += Mathf.FloorToInt(_satisfaction / 100);
    }

    public void UpdateNeeds(int hunger = 0, int boredom = 0) {
        _hunger += hunger;
        _boredom += boredom;
    }
}
