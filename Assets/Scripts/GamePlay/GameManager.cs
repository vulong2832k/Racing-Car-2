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

    //UI GameOver
    public GameObject gameOverObj;
    public GameObject timeGameOverObj;
    public GameObject speedTextObj;
    public GameObject winGameObj;

    //Player
    [SerializeField] private Vector3 _initialPlayerPosition;
    [SerializeField] private Quaternion _initialPlayerRotation;
    [SerializeField] private PlayerController _playerController;

    // Out of bounds check
    private float _offTrackTimer = 0f;
    private float _maxOffTrackTime = 5f;

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
    private void Start()
    {
        StartCoroutine(FindPlayerAfterDelay());
        if (_playerController == null) return;
        _initialPlayerPosition = _playerController.transform.position;
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
            CheckOutOfBounds();
        }
        if (_isWinGame)
        {
            timeGameOverObj.SetActive(false);
            winGameObj.SetActive(true);
        }
    }
    private IEnumerator FindPlayerAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        FindPlayerController();
    }
    private void FindPlayerController()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null) return;
        {
            _playerController = player.GetComponentInChildren<PlayerController>();
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
    public void ResetPlayerPosition()
    {
        if (_playerController != null)
        {
            Rigidbody playerRigidbody = _playerController.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Reset Rigidbody
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;

                // Di chuyển player đến vị trí ban đầu
                playerRigidbody.MovePosition(_initialPlayerPosition);
                playerRigidbody.MoveRotation(_initialPlayerRotation.normalized);
            }
            else
            {
                _playerController.transform.position = _initialPlayerPosition;
                _playerController.transform.rotation = _initialPlayerRotation.normalized;
            }

            Debug.Log("Player position reset to: " + _initialPlayerPosition);
            Debug.Log("Player rotation reset to: " + _initialPlayerRotation);
        }
        else
        {
            Debug.LogError("Player object is not assigned in the GameManager.");
        }
    }
    private void CheckOutOfBounds()
    {
        if (_playerController == null) return;

        if (!Physics.Raycast(_playerController.transform.position, Vector3.down, 10f))
        {
            _offTrackTimer += Time.deltaTime;

            if (_offTrackTimer >= _maxOffTrackTime)
            {
                ResetPlayerPosition();
                _offTrackTimer = 0;
            }
        }
        else
        {
            _offTrackTimer = 0;
        }
    }
}
