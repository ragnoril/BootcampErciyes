using Match3D;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;

    public TMP_Text ScoreText;
    public TMP_Text FinalScoreText;

    public GameObject InGameUI;
    public GameObject WinUI;


    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _gameManager.OnItemMatch += UpdateScore;
        _gameManager.OnGameOver += GameOver;

        InGameUI.SetActive(true);
        WinUI.SetActive(false);

        UpdateScore();
    }

    private void GameOver()
    {
        FinalScoreText.text = ScoreText.text;
        InGameUI.SetActive(false);
        WinUI.SetActive(true);
    }

    private void OnDestroy()
    {
        _gameManager.OnItemMatch -= UpdateScore;
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + _gameManager.Score.ToString();
    }

    public void PlayAgain()
    {
        _gameManager.PlayAgain();
    }


}
