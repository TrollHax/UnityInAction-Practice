using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeatherManager))]

public class Manager : MonoBehaviour
{
    public static WeatherManager Weather { get; private set; }

    private List<IGameManager> startSquence;

    void Awake()
    {
        Weather = GetComponent<WeatherManager>();

        startSquence = new List<IGameManager>();
        startSquence.Add(Weather);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();

        foreach (IGameManager manager in startSquence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = startSquence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in startSquence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log($"Progress: {numReady}/{numModules}");
            yield return null;
        }

        Debug.Log("All managers started up");
    }
}
