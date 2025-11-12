using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public ParticleSystem[] particles;


    void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;

        optionsPanel.transform.GetChild(3).GetComponent<Slider>().value = audioManager.GetMusicVolume();
        optionsPanel.transform.GetChild(4).GetComponent<Slider>().value = audioManager.GetSFXVolume();

        Time.timeScale = 1.0f;

        highScore = gameManager.LoadHighScore();
        highScoreText.text = "High Score: " + highScore.ToString();

        if (inGameHighScoreText)
        {
            inGameHighScoreText.text = "High Score: " + highScore.ToString();
        }

        gameManager.Score += UpdateScore;
        gameManager.Die += EnableRetryPanel;
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
            if(inGameScoreText.color != inGameHighScoreText.color)
            {
                particles[0].Play();
                particles[1].Play();
                inGameScoreText.color = inGameHighScoreText.color;
            }
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
        gameManager.Score -= UpdateScore;
        gameManager.Die -= EnableRetryPanel;
    }

}
