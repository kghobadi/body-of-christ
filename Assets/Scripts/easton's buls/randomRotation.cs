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
    Vector3 originalPos;
    void Start()
    {
        timer = .5f;
        speed = 1;
        amount = 1f;
        originalRotation = transform.localEulerAngles;
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if(timer < 0)
        {
            transform.localEulerAngles = new Vector3(originalRotation.x + Random.Range(-amount, amount), originalRotation.y + Random.Range(-amount, amount), originalRotation.z + Random.Range(-amount, amount));
            //transform.localPosition = new Vector3(originalPos.x + Random.Range(-amount, amount), originalPos.y + Random.Range(-amount, amount), originalPos.z + Random.Range(-amount, amount));
            //timer = Random.Range(0.5f, 1.5f);
            //speed = Random.Range(-2f, 2f);
           // amount += 1f;
        }

       
        timer -= Time.deltaTime;
    }
}
