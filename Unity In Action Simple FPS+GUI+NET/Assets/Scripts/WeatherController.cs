using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] Light sun;

    private float fullIntensity;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    // Start is called before the first frame update
    void Start()
    {
        fullIntensity  = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnWeatherUpdated()
    {
        SetOvercast(Manager.Weather.cloudValue);
    }

    void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = fullIntensity - (fullIntensity * value);
    }
}
