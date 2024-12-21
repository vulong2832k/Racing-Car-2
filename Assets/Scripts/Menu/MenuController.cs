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
    [SerializeField] private GameObject _selectCarPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _menuSelectionMap;
    private void Start()
    {
        _menuPanel.SetActive(true);
        _menuSelectionMap.SetActive(false);
        _selectCarPanel.SetActive(false);

    }
    public void PlayGame()
    {
        if (!PlayerPrefs.HasKey("SelectedVehicle")) return;

        _menuSelectionMap.SetActive(true);
        _menuPanel.SetActive(false);
        _selectCarPanel.SetActive(false);

    }
    public void OpenMenuCar()
    {
        _selectCarPanel.SetActive(true);
        _menuPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
