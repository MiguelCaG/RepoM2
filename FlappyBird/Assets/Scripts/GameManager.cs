using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject retryPanel;

    void Start()
    {
        Time.timeScale = 1.0f;

        BirdController.Die += EnableRetryPanel;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void EnableRetryPanel()
    {
        Time.timeScale = 0.0f;
        retryPanel.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        BirdController.Die -= EnableRetryPanel;
    }
}
