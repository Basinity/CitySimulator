using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text downfallText;
    [SerializeField] private TMP_Text deltaTimeText;
    [SerializeField] private Slider deltaTimeSlider;

    public void OnSliderUpdate()
    {
        Time.timeScale = deltaTimeSlider.value;
        deltaTimeText.text = $"TimeScale: {Math.Round(deltaTimeSlider.value, 2)}x";
    }

    public void OnButtonPress()
    {
        WeatherManager.Instance.ToggleRain(!WeatherManager.Instance.isRaining);
        downfallText.text = WeatherManager.Instance.isRaining ? "Disable" : "Enable";
    }
}
