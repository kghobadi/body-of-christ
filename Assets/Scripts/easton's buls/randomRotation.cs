using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour
{
    float timer;
    public Material lightAng;
    float speed;

    void Start()
    {
        timer = .5f;
        speed = 1;
    }

    void Update()
    {
        if(timer < 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.Range(0, 360), 0);
            timer = Random.Range(0.5f, 1.5f);
            speed = Random.Range(-2f, 2f);
        }

        transform.localPosition += new Vector3(0, -50f * Input.GetAxis("Mouse Y") * Time.deltaTime, 0);
        lightAng.SetVector("_LightDir", transform.localEulerAngles);
        timer -= Time.deltaTime;
    }
}
