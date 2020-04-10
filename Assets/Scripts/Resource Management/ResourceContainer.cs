/// <summary>
/// Contains all the resources the player can have, this includes population capacity.
/// </summary>
public static class ResourceContainer {

    private static int _populationCount = 1;

    /// <summary>
    /// Getter/setter for the population count.
    /// The set method prevents editing the value out of the bounds of the capacity.
    /// </summary>
    public static int PopulationCount {
        get { return _populationCount; }
        set {
            if (value > _populationCap)
                _populationCap = PopulationCap;
            else if (value < 1)
                _populationCap = 1;
            else
                _populationCap = value;
        }
    }

    private static int _populationCap = 1;

    /// <summary>
    /// Getter/setter for the population capacity.
    /// </summary>
    public static int PopulationCap {
        get { return _populationCap; }
        set {
            if (value < 1)
                _populationCap = 1;
            else
                _populationCap = value;
        }
    }

    private static int _appreciation = 100;

    /// <summary>
    /// Getter setter for the appreciation value of the town.
    /// </summary>
    public static int Appreciation {
        get { return _appreciation; }
        set { _appreciation = value; }
    }

    private static int _wood = 20;

    /// <summary>
    /// Getter/setter for the amount of wood the player has.
    /// Setter prevents wood value from being negative.
    /// </summary>
    public static int Wood {
        get { return _wood; }
        set {
            if (value < 0)
                _wood = 0;
            else
                _wood = value;
        }
    }

    private static int _stone = 10;

    /// <summary>
    /// Getter/setter for the amount of stone the player has.
    /// Setter prevents stone value from being negative.
    /// </summary>
    public static int Stone {
        get { return _stone; }
        set {
            if (value < 0)
                _stone = 0;
            else
                _stone = value;
        }
    }

    private static int _food = 10;

    /// <summary>
    /// Getter setter for the amount of food the player has.
    /// Setter prevents food value from being negative.
    /// </summary>
    public static int Food {
        get { return _food; }
        set {
            if (value < 0)
                _food = 0;
            else
                _food = value;
        }
    }
}
