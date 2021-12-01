using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLighting : MonoBehaviour
{
    float timer;
    Light lite;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        timer = 5f;
        speed = 1;
        lite = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            lite.intensity = Random.Range(150f, 900f);
            timer = 1f;
            if(speed < 5) { speed += 0.5f; }
           
        }


        timer -= speed*Time.deltaTime;
    }
}
