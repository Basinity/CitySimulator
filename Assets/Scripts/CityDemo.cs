using System;
using System.Collections;
using StateMachine;
using UnityEngine;

public class CityDemo : MonoBehaviour
{
    [SerializeField] private AIManager AIManager;
    [SerializeField] private WeatherManager weatherManager;
    private float countdown;
    
    private void Awake()
    {
        weatherManager.Initialize();
        StartCoroutine(Initialize());

        countdown = weatherManager.secondsToToggleRain;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown < 0) weatherManager.ToggleRain(!weatherManager.isRaining);

        if (weatherManager.isRaining) countdown = weatherManager.secondsToToggleRain;
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSecondsRealtime(1f);
        
        StartCoroutine(AIManager.Initialize());
    }
}
