using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject retryPanel;

    private int score = 0;
    private int highScore = 0;

    public TextMeshProUGUI inGameScoreText;
    public TextMeshProUGUI inGameHighScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    void Start()
    {
        Time.timeScale = 1.0f;

        highScore = GameManager.LoadHighScore();
        highScoreText.text = "High Score: " + highScore.ToString();

        BirdController.Score += UpdateScore;
        BirdController.Die += EnableRetryPanel;
    }

    private void UpdateScore()
    {
        score++;
        inGameScoreText.text = "Score: " + score.ToString();

        if (score > highScore)
        {
            highScore = score;
        }
    }

    private void EnableRetryPanel()
    {
        Time.timeScale = 0.0f;
        retryPanel.SetActive(true);

        scoreText.text = "Score: " + score.ToString();

        if (score > highScore)
        {
            highScore = score;
            GameManager.SaveHighScore(highScore);
        }

        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void StartGame() => SceneManager.LoadScene("GameScene");
    public void RetryButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ReturnButton() => SceneManager.LoadScene("MainMenu");
    public void QuitGame() => Application.Quit();

    private void OnDisable()
    {
        BirdController.Score -= UpdateScore;
        BirdController.Die -= EnableRetryPanel;
    }

}
