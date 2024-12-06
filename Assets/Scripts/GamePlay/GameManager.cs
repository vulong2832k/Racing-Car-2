using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UI Timer
    public float _timeComplete;
    private bool _isPlaying = true;
    public bool _isWinGame = false;
    private bool _hasReachedCheckpoint = false;
    private static GameManager instance;
    private float _resetTimeWhenComplete = 30f;

    //UI GameOver
    public GameObject gameOverObj;
    public GameObject timeGameOverObj;
    public GameObject speedTextObj;
    public GameObject winGameObj;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManagerGO = new GameObject("GameManager");
                    instance = gameManagerGO.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private void Update()
    {
        if (_isPlaying)
        {
            _timeComplete -= Time.deltaTime;
            if (_timeComplete <= 0)
            {
                _timeComplete = 0;
                timeGameOverObj.SetActive(false);
                gameOverObj.SetActive(true);
                speedTextObj.SetActive(false);
                EndGame();

            }
        }
        if (_isWinGame)
        {
            timeGameOverObj.SetActive(false);
            winGameObj.SetActive(true);


        }
    }

    private void EndGame()
    {
        _isPlaying = false;
        Time.timeScale = 0;
    }
    public void CheckPoint()
    {
        if (_isPlaying)
        {
            _timeComplete = _resetTimeWhenComplete;
            _hasReachedCheckpoint = true;
        }
    }
    public void CompleteRaceTrack()
    {
        if (_isPlaying && _hasReachedCheckpoint)
        {
            _isWinGame = true;
            _isPlaying = false;
        }
        else if (!_isPlaying)
        {
            Debug.Log("Chưa chạm checkpoint!");
        }
    }
}
