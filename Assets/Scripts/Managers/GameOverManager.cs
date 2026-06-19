using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;

    void Start()
    {
        GameOverPanel.SetActive(false);
    }
    void OnEnable()
    {
        GameEvents.OnPlayerDeath += HandleGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerDeath -= HandleGameOver;
    }

    void HandleGameOver()
    {
        // Load the Game Over scene
        GameOverPanel.SetActive(true);

    }

    public void RestartLevel()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restarting level...");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
