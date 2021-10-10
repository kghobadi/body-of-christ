using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for controlling light movement with mouse movement 
public class ControlLight : MonoBehaviour
{
    //not using this anymore 
    public Material lightAng;

    //cam refs
    private Camera mainCam;
    private Transform camTransform;

    private void Start()
    {
        mainCam = Camera.main;
        camTransform = mainCam.transform;

        //disable cursor 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //when left click
        if (Input.GetMouseButton(0))
        {
            transform.localEulerAngles = camTransform.localEulerAngles;
        }
        
        //mouseMovement += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));
        //transform.localEulerAngles += new Vector3(0, 5f * Input.GetAxis("Mouse X"), 0);
        
    }
}
