using System;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public float cloudValue { get; private set; }

    //Add cloud value here (listing 10.8)
    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        network = service;
        StartCoroutine(network.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Initializing;
    }

    public void OnJSONDataLoaded(string data)
    {
        JObject root = JObject.Parse(data);

        JToken clouds = root["clouds"];
        cloudValue = (float)clouds["all"] / 100f;
        Debug.Log($"Value: {cloudValue}");

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }
}
