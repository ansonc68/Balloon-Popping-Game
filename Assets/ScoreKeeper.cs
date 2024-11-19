using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int score = 0;
    const int DEFAULT_POINTS = 1;
    const int SCORE_THRESHOLD = 15;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI shotsFiredText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        DisplayScore();
        DisplayLevel();
        DisplayTotalScore();
        DisplayShotsFired();
    }

    public void AddPoints()
    {
        AddPoints(DEFAULT_POINTS);
    }

    public void AddPoints(int value)
    {
        score += value;
        DisplayScore();
        PersistentData.Instance.AddTotalScore(value);
        DisplayTotalScore();
        if (score >= SCORE_THRESHOLD)
            AdvanceLevel();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayShotsFired();
    }

    private void DisplayScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void DisplayTotalScore()
    {
        totalScoreText.text = "Total Score: " + PersistentData.Instance.GetTotalScore();
    }

    private void DisplayShotsFired()
    {
        shotsFiredText.text = "Shots Fired: " + PersistentData.Instance.GetShotsFired();
    }

    private void DisplayLevel()
    {
        levelText.text = "Level: " + (level);
    }

    private void AdvanceLevel()
    {
        SceneManager.LoadScene(level + 1);
    }
}
