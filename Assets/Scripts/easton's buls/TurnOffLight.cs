using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            light.range = Mathf.Lerp(light.range, 0f, Time.deltaTime);
        }
        else
        {
            light.range = Mathf.Lerp(light.range, 45f, Time.deltaTime);
        }
    }
}
