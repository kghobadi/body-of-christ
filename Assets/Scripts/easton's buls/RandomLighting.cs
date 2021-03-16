using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLighting : MonoBehaviour
{
    public Light light;
    public float speed, angleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //light = GetComponent<Renderer>().material;
        //lightDir = Random.insideUnitSphere;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.PerlinNoise(Time.time * speed, 0) + 0.5f;
        light.spotAngle = (Mathf.PerlinNoise(Time.time * angleSpeed, 1) * 15f) + 30;
    }
}
