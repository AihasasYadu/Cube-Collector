using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI streakText;
    private Player scoreData;
    private int highScore;
    private int currentScore;
    private int scoreStreak;
    private void Start()
    {
        currentScore = 0;
        scoreStreak = 1;
        UpdateScore();
        EventManager.Collected += IncrementScore;
        EventManager.StreakUpdate += UpdateStreak;
    }

    private void UpdateScore()
    {
        currentScoreText.SetText(currentScore.ToString());
        scoreData = JSonManager.Instance.GetSaveData;
        highScore = scoreData.score;
        if(currentScore > highScore)
        {
            highScore = currentScore;
            scoreData.score = highScore;
            JSonManager.Instance.SetSaveData = scoreData;
        }
        highScoreText.SetText(highScore.ToString());
        streakText.SetText("x" + scoreStreak.ToString());
    }

    private void IncrementScore(int update)
    {
        currentScore += (update * scoreStreak);
        UpdateScore();
    }

    private void UpdateStreak(int streak)
    {
        scoreStreak = streak;
    }

    private void OnDestroy()
    {
        EventManager.Collected -= IncrementScore;
        EventManager.StreakUpdate -= UpdateStreak;
    }
}
