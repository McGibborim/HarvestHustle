using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    [SerializeField]  Button playButton;             
    [SerializeField]  Button quitButton;           
    [SerializeField]  Button optionsButton;          

    
    public void Start()
    {
        playButton.onClick.AddListener(StartGame); 
        quitButton.onClick.AddListener(QuitGame);
        optionsButton.onClick.AddListener(OpenOptions);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level_wheat"); 
    }

    
    public void OpenOptions()
    {
    
        Debug.Log("Options Menu opened");
    }

 
    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
        Application.Quit(); 
#endif
    }
}
