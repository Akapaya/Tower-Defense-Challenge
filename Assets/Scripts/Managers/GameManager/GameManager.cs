using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasInGameManager _canvasManager;

    [Header("Values")]
    [SerializeField] private int _score = 0;
    [SerializeField] private int _lives = 3;    
    [SerializeField] private bool InGame = true;

    [Header("SummonPoints")]
    [SerializeField] private int _summonPoints = 10;
    [SerializeField] private int _summonPointsPerTime = 1;
    [SerializeField] private float _timerToGetSummonPoints;
    [SerializeField] private float _timeToIncreasePoints = 5f;

    [Header("Tower Cost")]
    [SerializeField] private int _summonCost = 10;
    [SerializeField] private int _summonCostIncreasedPerThreeTowers = 20;
    [SerializeField] private int _countToIncreaseCost = 0;
    [SerializeField] private int _maxCountToIncrease = 3;
    

    #region Delegates
    public delegate void OnEnemyKilledEvent();
    public static OnEnemyKilledEvent OnEnemyKilledHandle;

    public delegate void OnEnemyPassedEvent();
    public static OnEnemyPassedEvent OnEnemyPassedHandle;

    public delegate void OnPickupSummonPointEvent();
    public static OnPickupSummonPointEvent OnPickupSummonPointHandle;
    #endregion

    #region Unity Events
    public UnityEvent OnGameOver = new UnityEvent();
    public UnityEvent OnAvailableCurrency = new UnityEvent();
    public UnityEvent OnUnavailableCurrency = new UnityEvent();
    #endregion

    #region StartMethods
    private void OnEnable()
    {
        OnEnemyKilledHandle += OnEnemyKilled;
        OnEnemyPassedHandle += OnEnemyPassed;
        OnPickupSummonPointHandle += IncreaseSummonPointsByOne;
    }

    private void OnDisable()
    {
        OnEnemyKilledHandle -= OnEnemyKilled;
        OnEnemyPassedHandle -= OnEnemyPassed;
        OnPickupSummonPointHandle -= IncreaseSummonPointsByOne;
    }

    private void Start()
    {
        _canvasManager.UpdateSummonPointsText(_summonPoints.ToString());
    }
    #endregion

    void Update()
    {
        if (InGame)
        {
            _timerToGetSummonPoints += Time.deltaTime;
            

            if (_timerToGetSummonPoints >= _timeToIncreasePoints)
            {
                IncreaseSummonPoints();
                _timerToGetSummonPoints = 0f;
            }

            float t = Time.time;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            _canvasManager.UpdateTimerText(minutes + ":" + seconds);
        }
    }

    private void OnEnemyKilled()
    {
        _score++;
        _canvasManager.UpdateScoreText(_score.ToString("0000"));
        IncreaseSummonPointsByOne() ;
    }

    private void OnEnemyPassed()
    {
        _lives--;
        _canvasManager.UpdateLivesIcons(_lives);

        if( _lives == 0 )
        {
            _canvasManager.UpdateGameOverText(_score.ToString("0000"));
            OnGameOver.Invoke();
        }
    }

    #region SummonPoints Methods
    private void IncreaseSummonPoints()
    {
        _summonPoints += _summonPointsPerTime;
        _canvasManager.UpdateSummonPointsText(_summonPoints.ToString() + "/" + _summonCost.ToString());
        CheckIfCanBuildTower();
    }   

    private void IncreaseSummonPointsByOne()
    {
        _summonPoints ++;
        _canvasManager.UpdateSummonPointsText(_summonPoints.ToString() + "/" + _summonCost.ToString());
        CheckIfCanBuildTower();
    }

    public void DecreaseSummonPoints()
    {
        _summonPoints -= _summonCost;
        _canvasManager.UpdateSummonPointsText(_summonPoints.ToString() + "/" + _summonCost.ToString());
        CheckIfIncreaseCost();
        CheckIfCanBuildTower();
    }
    #endregion

    #region Check Methods
    private void CheckIfCanBuildTower()
    {
        if(_summonPoints >= _summonCost)
        {
            OnAvailableCurrency.Invoke();
        }
        else
        {
            OnUnavailableCurrency.Invoke();
        }
    }

    private void CheckIfIncreaseCost()
    {
        _countToIncreaseCost++;
        if (_countToIncreaseCost >= _maxCountToIncrease)
        {
            _countToIncreaseCost = 0;
            _summonCost += _summonCostIncreasedPerThreeTowers;
        }
    }
    #endregion

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
