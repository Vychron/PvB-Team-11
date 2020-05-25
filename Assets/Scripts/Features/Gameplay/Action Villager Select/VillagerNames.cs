using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the list of names the villagers can have.
/// </summary>
public class VillagerNames : MonoBehaviour {

    /// <summary>
    /// Static reference to the component.
    /// </summary>
    public static VillagerNames Instance = null;

    /// <summary>
    /// List of possible villager names.
    /// </summary>
    public string[]
        MaleNames,
        FemaleNames;

    private List<string> _availableMaleNames = null;
    private List<string> _availableFemaleNames = null;
    private List<string> _usedNames = null;

    private void Awake() {
        
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
        _availableMaleNames = new List<string>(MaleNames);
        _availableFemaleNames = new List<string>(FemaleNames);
        _usedNames = new List<string>();
    }

    /// <summary>
    /// Returns a name that hasn't been taken by a villager yet and removes the name from the available list.
    /// </summary>
    /// <param name="isMale">Whether or not the villager is a male.</param>
    /// <returns>Returns the chosen name for the villager as a string.</returns>
    public string GenerateName(bool isMale) {
        List<string> names;
        string chosenName;
        if (isMale)
            names = _availableMaleNames;
        else
            names = _availableFemaleNames;

        int count = names.Count;
        int rand = Random.Range(0, count);
        chosenName = names[rand];
        _usedNames.Add(names[rand]);
        names.RemoveAt(rand);
        return chosenName;
    }

    /// <summary>
    /// Returns the names of all villager names that are in use.
    /// </summary>
    /// <returns>Retuns a List of strings containing the villager names.</returns>
    public List<string> GetUsedNames() {
        return _usedNames;
    }

    /// <summary>
    /// Adds a name of a villager that has left town back to the available list.
    /// </summary>
    /// <param name="name">The name of the villager.</param>
    /// <param name="isMale">The gender of the villager.</param>
    public void RecycleName(string name, bool isMale) {
        if (isMale)
            _availableMaleNames.Add(name);
        else
            _availableFemaleNames.Add(name);
    }
}