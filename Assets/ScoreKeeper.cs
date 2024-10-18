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
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int level;

    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        DisplayScore();
        DisplayLevel();
    }

    public void AddPoints()
    {
        AddPoints(DEFAULT_POINTS);
    }

    public void AddPoints(int value)
    {
        score += value;
        DisplayScore();
        if (score >= SCORE_THRESHOLD)
            AdvanceLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void DisplayLevel()
    {
        levelText.text = "Level: " + (level+1);
    }

    private void AdvanceLevel()
    {
        SceneManager.LoadScene(level + 1);
    }
}
