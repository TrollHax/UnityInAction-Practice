using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string xmlApi =
        "https://api.openweathermap.org/data/2.5/weather?lat=60.733218366593576&lon=14.988400895765887&appid=ec79f816b7672b805632aa12dbd5a513&mode=xml&units=metric";
    private const string jsonApi =
        "https://api.openweathermap.org/data/2.5/weather?lat=60.733218366593576&lon=14.988400895765887&appid=ec79f816b7672b805632aa12dbd5a513&units=metric";
    private const string webImage =
        "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
    private const string localApi = 
        "http://localhost/uia/api.php";

    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest request = (form == null) ?
            UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, null, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, null, callback);
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud_value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}
