using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    private Light light;
    public SceneTrigger sceneAdvance;
    public float transitionVal;
    public float normalVal = 45f;
    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        //while pressing mouse, shrink light
        if (Input.GetMouseButton(0))
        {
            light.range = Mathf.Lerp(light.range, 0f, Time.deltaTime);

            //check for scene transition
            if (light.range < transitionVal)
            {
                if (sceneAdvance)
                {
                    sceneAdvance.SetTrigger();
                }
            }
        }
        //reset light when not pressing 
        else
        {
            light.range = Mathf.Lerp(light.range, normalVal, Time.deltaTime);
        }
    }
}
