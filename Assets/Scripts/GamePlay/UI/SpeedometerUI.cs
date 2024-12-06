using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedometerUI : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TextMeshProUGUI _speedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedUIText();
    }
    private void UpdateSpeedUIText()
    {
        if(_playerController != null && _speedText != null)
        {
            float speed = _playerController.GetSpeedInKmh();
            _speedText.text = "Speed: " + Mathf.RoundToInt(speed) + "Km/h";
        }
    }
}
