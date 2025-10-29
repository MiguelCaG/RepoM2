using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        BirdController.Score += UpdateScore;
    }

    void Update()
    {

    }

    private void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void OnDisable()
    {
        BirdController.Score -= UpdateScore;
    }
}
