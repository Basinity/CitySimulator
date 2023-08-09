using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;

    public void OnSliderUpdate()
    {
        Time.timeScale = slider.value;
        text.text = $"TimeScale: {Math.Round(slider.value, 2)}x";
    }
}
