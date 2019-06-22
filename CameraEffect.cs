using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public bool hardMode = false;
    bool redLight = false;
    public Light[] lights;
    Color color = Color.red;

    float time = 2f;
    float curTime = 0f;

    private void Awake()
    {
        lights = Light.GetLights(LightType.Point, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hardMode = true;
        }
        if (hardMode)
        {
            foreach (var light in lights)
            {
                light.enabled = false;
            }
            if (time > curTime)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                redLight = true;
            }
            if (redLight)
            {
                foreach (var light in lights)
                {
                    light.color = color;
                    light.enabled = true;
                }
                Destroy(this);
            }
        }
    }
}
