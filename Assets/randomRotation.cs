using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour
{
    float timer;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        timer = .5f;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.RandomRange(0, 360), 0);
            timer = Random.Range(0.5f, 1.5f);
            speed = Random.Range(-2f, 2f);
        }

        transform.localEulerAngles += new Vector3(0, speed, 0);

        timer -= Time.deltaTime;
    }
}
