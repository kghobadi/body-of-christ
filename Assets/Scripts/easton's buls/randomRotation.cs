using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour
{
    float timer;
    //public Material lightAng;
    float speed;
    float amount;
    Vector3 originalRotation;

    void Start()
    {
        timer = .5f;
        speed = 1;
        amount = 1;
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if(timer < 0)
        {
            transform.localEulerAngles = new Vector3(originalRotation.x + Random.Range(-amount, amount), originalRotation.y + Random.Range(-amount, amount), originalRotation.z + Random.Range(-amount, amount));
            timer = Random.Range(0.5f, 1.5f);
            speed = Random.Range(-2f, 2f);
            amount += 1f;
        }

       
        timer -= Time.deltaTime;
    }
}
