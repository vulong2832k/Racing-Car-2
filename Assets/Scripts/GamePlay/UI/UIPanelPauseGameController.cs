using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPanelPauseGameController : MonoBehaviour
{
    [Header("Button:")]
    [SerializeField] private Button _playGameBtn;
    [SerializeField] private Button _resetGameBtn;
    [SerializeField] private Button _exitGameBtn;
    [SerializeField] private Button _closePanelBtn;
    [Header("GameObject:")]
    [SerializeField] private GameObject _pausePanel;

    private bool _isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        AddEventToButton();
        _pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }
    public void AddEventToButton()
    {
        _playGameBtn.onClick.AddListener(ResumeGame);
        _resetGameBtn.onClick.AddListener(ResetGame);
        _exitGameBtn.onClick.AddListener(ExitToMenu);
        _closePanelBtn.onClick.AddListener(ClosePausePanel);
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        _isPaused = true;
        _pausePanel.SetActive(true);
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
        _isPaused = false;
        _pausePanel.SetActive(false);
    }
    private void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    private void ClosePausePanel()
    {
        _pausePanel.SetActive(false);
    }
}
