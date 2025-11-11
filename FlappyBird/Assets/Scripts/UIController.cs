using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject retryPanel;
    public GameObject optionsPanel;

    private float time;

    private int score = 0;
    private int highScore = 0;

    public TextMeshProUGUI inGameScoreText;
    public TextMeshProUGUI inGameHighScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private GameManager gameManager;
    private AudioManager audioManager;


    void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;

        Time.timeScale = 1.0f;

        highScore = gameManager.LoadHighScore();
        highScoreText.text = "High Score: " + highScore.ToString();

        if (inGameHighScoreText)
        {
            inGameHighScoreText.text = "High Score: " + highScore.ToString();
        }

        BirdController.Score += UpdateScore;
        BirdController.Die += EnableRetryPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!optionsPanel.activeSelf)
            {
                gameManager.actualState = GameManager.State.Ready;
                time = Time.timeScale;
                Time.timeScale = 0.0f;
                optionsPanel.SetActive(true);
            }
            else
            {
                gameManager.actualState = GameManager.State.Playing;
                optionsPanel.SetActive(false);
                Time.timeScale = time;
            }

        }
    }

    private void UpdateScore()
    {
        score++;
        inGameScoreText.text = "Score: " + score.ToString();

        if (score > highScore)
        {
            inGameScoreText.color = inGameHighScoreText.color;
            inGameHighScoreText.text = "High Score: " + score.ToString();
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
            gameManager.SaveHighScore(highScore);
        }

        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void StartGame() => SceneManager.LoadScene("GameScene");
    public void RetryButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ReturnButton() => SceneManager.LoadScene("MainMenu");
    public void QuitGame() => Application.Quit();

    public void SetMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioManager.SetSFXVolume(volume);
    }

    private void OnDisable()
    {
        BirdController.Score -= UpdateScore;
        BirdController.Die -= EnableRetryPanel;
    }

}
