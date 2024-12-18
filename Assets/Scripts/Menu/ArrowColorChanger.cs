using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class ArrowColorChanger : MonoBehaviour
{
    [Header("Image Change Color:")]
    [SerializeField] private Image _arrowImage;

    [SerializeField] private float _timeChangeColor = 1f;
    private float _timer;

    void Start()
    {
        if (_arrowImage == null)
            _arrowImage = GetComponent<Image>();
        ChangeColor();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeChangeColor)
        {
            ChangeColor();
            _timer = 0f;
        }
    }
    private void ChangeColor()
    {
        Color rdColor = new Color(Random.value, Random.value, Random.value, 1f);
        _arrowImage.color = rdColor;
    }
}
