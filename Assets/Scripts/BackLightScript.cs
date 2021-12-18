using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLightScript : MonoBehaviour
{
    //Dependencies
    private Transform cameraTransform;
    private Light parentLight;

    //For changing the light on triggers
    float defaultLightIntensity;
    float newLightIntensity;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        parentLight = GetComponentInChildren<Light>();

        defaultLightIntensity = parentLight.intensity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cameraTransform);
    }

    public void IntensifyLight(float transitionTime, float intensityMod)
    {
        StartCoroutine(IntensifyLightCoroutine(transitionTime, intensityMod));
    }

    //Increase the intensity of the backlight based on some trigger. For example... getting hit or casting a spell.
    IEnumerator IntensifyLightCoroutine(float transitionTime, float intensityMod)
    {
        newLightIntensity = defaultLightIntensity + intensityMod;

        while (transitionTime > 0)
        {
            parentLight.intensity = Mathf.Lerp(defaultLightIntensity, newLightIntensity, transitionTime);
            transitionTime -= Time.deltaTime;
            yield return null;
        }
    }
}
