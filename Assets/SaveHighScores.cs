using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveHighScores : MonoBehaviour
{
    [SerializeField] const int NUM_HIGH_SCORES = 5;
    [SerializeField] const string SCORE_KEY = "HighScore";
    [SerializeField] const string SHOTSFIRED_KEY = "HighScoreShotsFired";

    [SerializeField] int shotsFired;  // To track shots fired for this session
    [SerializeField] int playerScore; // To track player score

    [SerializeField] TextMeshProUGUI[] scoreTexts;  // Array for displaying scores and shots fired

    // Start is called before the first frame update
    void Start()
    {
        // Get the number of shots fired and score from PersistentData instance
        shotsFired = PersistentData.Instance.GetShotsFired();
        playerScore = PersistentData.Instance.GetTotalScore();  // Using score from PersistentData

        SaveScore();  // Save the score and shots fired
        DisplayHighScores();  // Display the high scores in the UI
    }

    private void SaveScore()
    {
        // Go through each high score slot and check if we need to insert the current score
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            string currentScoreKey = SCORE_KEY + i;
            string currentShotsFiredKey = SHOTSFIRED_KEY + i;

            if (PlayerPrefs.HasKey(currentScoreKey))  // If score exists in the slot
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                int currentShotsFired = PlayerPrefs.GetInt(currentShotsFiredKey);

                // Check if the new score is better, or if the score is the same but fewer shots fired
                if (playerScore > currentScore || (playerScore == currentScore && shotsFired < currentShotsFired))
                {
                    // Store the new score and shots fired
                    int tempScore = currentScore;
                    int tempShotsFired = currentShotsFired;

                    PlayerPrefs.SetInt(currentScoreKey, playerScore);
                    PlayerPrefs.SetInt(currentShotsFiredKey, shotsFired);

                    // Swap the old score and shots fired with the new ones for the next iteration
                    playerScore = tempScore;
                    shotsFired = tempShotsFired;
                }
            }
            else
            {
                // If the slot is empty, simply store the player's score and shots fired
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                PlayerPrefs.SetInt(currentShotsFiredKey, shotsFired);
                return;  // No need to continue further once the first empty slot is found
            }
        }
    }

    public void DisplayHighScores()
    {
        // Display the high scores and corresponding shots fired
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            // Fetch the score and shots fired from PlayerPrefs
            int score = PlayerPrefs.GetInt(SCORE_KEY + i);
            int shotsFired = PlayerPrefs.GetInt(SHOTSFIRED_KEY + i);

            // Format and display the score and shots fired together in the UI
            scoreTexts[i].text = "Score: " + score.ToString() + " - Shots Fired: " + shotsFired.ToString();
        }
    }

    // Method to clear all high scores
    public void ClearScores()
    {
        // Iterate over all high score slots and clear the values
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            PlayerPrefs.DeleteKey(SCORE_KEY + i);  // Delete score data
            PlayerPrefs.DeleteKey(SHOTSFIRED_KEY + i);  // Delete shots fired data
        }

        DisplayHighScores();  // Re-display the UI, which will now be empty or show default values
    }
}
