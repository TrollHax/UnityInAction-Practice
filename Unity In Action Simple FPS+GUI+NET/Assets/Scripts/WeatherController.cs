using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] Light sun;

    private float fullIntensity;
    private float cloudValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fullIntensity  = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        SetOvercast(cloudValue);
        cloudValue += 0.25f * Time.deltaTime;
    }

    private void SetOvercast(float value)
    {
        if (value > 1f)
        {
            cloudValue = 1f;
        }
        sky.SetFloat("_Blend", value);
        sun.intensity = fullIntensity - (fullIntensity * value);
    }
}
