using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Button")]
    public Button _playBtn;

    [SerializeField] private Button _menuCarBtn;

    [Header("GameObject")]
    [SerializeField] private GameObject _SelectCarPanel;
    [SerializeField] private GameObject _MenuPanel;
    public void PlayGame()
    {
        if (!PlayerPrefs.HasKey("SelectedVehicle")) return;

        SceneManager.LoadScene("GamePlay");
    }
    public void OpenMenuCar()
    {
        _SelectCarPanel.SetActive(true);
        _MenuPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
