using UnityEngine;
using UnityEngine.UI; // Include this for Button
using UnityEngine.SceneManagement; // For scene management

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Reference to the pause menu UI
    [SerializeField] private Button resumeButton; // Reference to the Resume button
    [SerializeField] private Button mainMenuButton; // Reference to the Main Menu button
    [SerializeField] private Button quitButton; // Reference to the Quit button

    private bool isPaused = false; // Tracks if the game is paused

    void Start()
    {
        // Initially hide the pause menu
        pauseMenuUI.SetActive(false);
        // Assign button click listeners
        resumeButton.onClick.AddListener(Resume);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        // Check for pause input (Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming game...");
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game
        isPaused = false; // Update the pause state
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Freeze the game
        isPaused = true; // Update the pause state
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Make sure the game is unpaused
        SceneManager.LoadScene("main_menu"); // Replace with your main menu scene name
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops the play mode in the editor
#else
        Application.Quit(); // Quit the application
#endif
    }
}
