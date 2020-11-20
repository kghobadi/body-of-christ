using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour
{
    float timer;
    public Material lightAng;
    float mouseMovement;
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
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.RandomRange(0, 360), 0);
            //timer = Random.Range(0.5f, 1.5f);
            //speed = Random.Range(-2f, 2f);
        }
        mouseMovement += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));
        transform.localEulerAngles += new Vector3(0, 5f*Input.GetAxis("Mouse X"), 0);
        //transform.localPosition += new Vector3(0, -50f * Input.GetAxis("Mouse Y") * Time.deltaTime, 0);
        //lightAng.SetVector("_LightDir", transform.localEulerAngles);
        lightAng.SetFloat("_MouseInput", mouseMovement);
        //timer -= Time.deltaTime;
    }
}
