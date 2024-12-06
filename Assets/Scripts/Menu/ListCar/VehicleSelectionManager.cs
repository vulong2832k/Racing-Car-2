using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelectionManager : MonoBehaviour
{
    [SerializeField] private VehicleData[] _vehicles;
    [SerializeField] private Image _vehicleDisplay;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _PreviousBtn;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private Button _playBtn;

    private int _currentVehicleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVehicleDisplay();
        _playBtn.interactable = PlayerPrefs.HasKey("SelectedVehicle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateVehicleDisplay()
    {
        _vehicleDisplay.sprite = _vehicles[_currentVehicleIndex].vehicleImage;
    }
    public void NextVehicle()
    {
        _currentVehicleIndex = (_currentVehicleIndex + 1) % _vehicles.Length;
        UpdateVehicleDisplay();
    }
    public void PreviousVehicle()
    {
        _currentVehicleIndex = (_currentVehicleIndex - 1 + _vehicles.Length) % _vehicles.Length;
        UpdateVehicleDisplay();
    }
    public void ConfirmSelection()
    {
        PlayerPrefs.SetString("SelectedVehicle", _vehicles[_currentVehicleIndex].name);
        _playBtn.interactable = true;
    }
    public void PlayGame()
    {
        if (!PlayerPrefs.HasKey("SelectedVehicle")) return;

        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}
