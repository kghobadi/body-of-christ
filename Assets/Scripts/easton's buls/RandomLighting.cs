using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLighting : MonoBehaviour
{
    public Material light;
    public Vector3 lightDir;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Renderer>().material;
        lightDir = Random.insideUnitSphere;
    }

    // Update is called once per frame
    void Update()
    {
        lightDir = new Vector3(Mathf.PerlinNoise(Time.time, lightDir.x), Mathf.PerlinNoise(Time.time, lightDir.y), Mathf.PerlinNoise(Time.time, lightDir.z));
        light.SetVector("Vector3_EFA8D867", lightDir);
    }
}
