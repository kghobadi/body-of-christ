using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for controlling light movement with mouse movement 
public class ControlLight : MonoBehaviour
{
    public Material lightAng;
    float mouseMovement;

    void Update()
    {
        mouseMovement += Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));
        transform.localEulerAngles += new Vector3(0, 5f * Input.GetAxis("Mouse X"), 0);
        lightAng.SetFloat("_MouseInput", mouseMovement);
    }
}
