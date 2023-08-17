using System;
using UnityEngine;
using Utility;

public class WeatherManager : Singleton<WeatherManager>
{
    [Range(0, 240)] public int secondsToToggleRain;
    [SerializeField] private Camera _Camera;
    [SerializeField] private Transform rain;
    private UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    public bool isRaining;
    public Action<bool> rainingEvent;
    
    public void Initialize()
    {
        var volumeProfile = _Camera.GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (volumeProfile != null) volumeProfile.TryGet(out colorAdjustments);
    }

    public void ToggleRain(bool toggle)
    {
        isRaining = toggle;
        colorAdjustments.active = toggle;
        rain.gameObject.SetActive(toggle);
        rainingEvent?.Invoke(toggle);
    }
}