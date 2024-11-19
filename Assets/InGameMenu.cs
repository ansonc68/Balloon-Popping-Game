using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool isPaused = false;  // Track if the game is paused

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pause the game
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  // Freeze game time
    }

    // Resume the game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Unfreeze game time
    }

    // Go to the Main Menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PersistentData.Instance.ResetTotalScore();
        PersistentData.Instance.ResetShotsFired();
    }
}
