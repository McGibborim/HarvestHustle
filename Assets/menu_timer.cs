using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 30f; // Example: 30 seconds
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button quitFinalButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button loadMainMenuButton; // Renamed for clarity

    private bool isGameOver = false;
    private bool isPaused = false;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        resumeButton.onClick.AddListener(TogglePause);
        loadMainMenuButton.onClick.AddListener(LoadMainMenu); // Updated reference
        quitFinalButton.onClick.AddListener(QuitFinal); // Updated to a different method

        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isGameOver && !isPaused) // Only update if not paused or game over
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0;
                isGameOver = true;
                ShowGameOverMenu();
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume before loading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Resume before quitting
        SceneManager.LoadScene("main_menu");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume before loading main menu
        SceneManager.LoadScene("main_menu");
    }

    public void QuitFinal() // Renamed from quitFinalButton to QuitFinal
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops the play mode in the editor
#else
        Application.Quit(); // Quit the application
#endif
    }
}

