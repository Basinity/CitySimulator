using StateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AIManager AIManager;
    [SerializeField] private WeatherManager WeatherManager;
    [SerializeField] private UIManager UIManager;
    private float countdown;
    
    private void Awake()
    {
        WeatherManager.Initialize();

        countdown = WeatherManager.secondsToToggleRain;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown < 0) WeatherManager.ToggleRain(!WeatherManager.isRaining);

        if (WeatherManager.isRaining) countdown = WeatherManager.secondsToToggleRain;
    }
}
