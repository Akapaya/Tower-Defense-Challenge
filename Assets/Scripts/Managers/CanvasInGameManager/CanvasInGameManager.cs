using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInGameManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _summonPointsText;
    [SerializeField] private Image[] _livesIcons;
    [SerializeField] private Sprite _livesIconOn;
    [SerializeField] private Sprite _livesIconOff;

    [Header("GameOver Values")]
    [SerializeField] private TMP_Text _gameoverStatsText;

    public void UpdateTimerText(string timer)
    {
        _timerText.text = "Timer: " + timer;
    }

    public void UpdateLivesIcons(int lives)
    {
        for (int i = 0; i < _livesIcons.Length; i++)
        {
            _livesIcons[i].sprite = (i < lives) ? _livesIconOn : _livesIconOff;
        }
    }

    public void UpdateScoreText(string score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateSummonPointsText(string summonPoints)
    {
        _summonPointsText.text = "SP: " + summonPoints;
    }

    public void UpdateGameOverText(string score)
    {
        _gameoverStatsText.text = _timerText.text + "\n" + "Score: " + score;
    }
}
