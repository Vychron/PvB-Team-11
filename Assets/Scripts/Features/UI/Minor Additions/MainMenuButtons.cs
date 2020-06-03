using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the buttons of the main menu.
/// </summary>
public class MainMenuButtons : MonoBehaviour {

    [SerializeField]
    private Transform _closeButton = null;

#if UNITY_WEBGL
    private void Start() {
        _closeButton?.gameObject.SetActive(false);
    }
#endif

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void CloseButton() {
        Application.Quit();
    }

}
