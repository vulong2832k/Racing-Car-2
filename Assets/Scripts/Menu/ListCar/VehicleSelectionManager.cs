using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelectionManager : MonoBehaviour
{
    [Header("Data:")]
    [SerializeField] private Image _vehicleDisplay;
    [SerializeField] private VehicleData[] _vehicles;
    [SerializeField] private MenuController _menuController;
    [Header("GameObject:")]
    [SerializeField] private GameObject _SelectCarPanel;
    [SerializeField] private GameObject _MenuPanel;
    [Header("Selection:")]
    [SerializeField] private RectTransform _selectionTick;
    //[SerializeField] private RectTransform[] _vehiclePositions;
    [Header("Button:")]
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _PreviousBtn;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private Button _exitPanelSelectCarBtn;

    private int _currentVehicleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVehicleDisplay();
        ResetVehicleSelection();
        _menuController._playBtn.interactable = PlayerPrefs.HasKey("SelectedVehicle");
        UpdateSelectionTick();
        _selectionTick.gameObject.SetActive(false);
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
        UpdateSelectionTick();
    }
    public void PreviousVehicle()
    {
        _currentVehicleIndex = (_currentVehicleIndex - 1 + _vehicles.Length) % _vehicles.Length;
        UpdateVehicleDisplay();
        UpdateSelectionTick();
    }
    public void ConfirmSelection()
    {
        PlayerPrefs.SetString("SelectedVehicle", _vehicles[_currentVehicleIndex].name);
        _menuController._playBtn.interactable = true;
        UpdateSelectionTick();
    }
    private void ResetVehicleSelection()
    {
        if (PlayerPrefs.HasKey("SelectedVehicle"))
        {
            PlayerPrefs.DeleteKey("SelectedVehicle");
        }
        _menuController._playBtn.interactable = false;
        _selectionTick.gameObject.SetActive(false);
    }
    public void ExitPanelSelectCar()
    {
        _SelectCarPanel.SetActive(false);
        _MenuPanel.SetActive(true);
    }
    private void UpdateSelectionTick()
    {
        if (PlayerPrefs.HasKey("SelectedVehicle"))
        {
            _selectionTick.gameObject.SetActive(true);
            _selectionTick.position = _vehicleDisplay.rectTransform.position;
        }
        else
        {
            _selectionTick.gameObject.SetActive(false);
        }
    }
}
