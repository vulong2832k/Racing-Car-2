using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    // Update is called once per frame
    void Update()
    {
        UpdateTimeText();
    }
    private void UpdateTimeText()
    {
        int totalSeconds = Mathf.FloorToInt(GameManager.Instance._timeComplete);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        _timeText.SetText($"Time: {minutes:D2}:{seconds:D2}");
    }
}
