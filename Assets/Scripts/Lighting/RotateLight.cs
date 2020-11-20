using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script allows user to click to rotate a light 
public class RotateLight : MonoBehaviour
{
    public float rotationSpeed;

    public Material lightAng;

    Vector3 origRot;

    public float minX, maxX;
    public float minY, maxY;
    public float minZ, maxZ;

    public Vector3 referencePoint;
    public Vector3 currentMousePos;
    public Vector3 lastMousePos;

    void Start()
    {
        origRot = transform.localEulerAngles;   
    }
    
    void Update()
    {
        TakeInput();

        lightAng.SetVector("_LightDir", transform.localEulerAngles);

    }

    void TakeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //set a ref point when the user clicks 
            referencePoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentMousePos = Input.mousePosition;

            //base y rotation on x dist from ref point
            float rotationYAmount = currentMousePos.x - referencePoint.x;
            float rotationxAmount = currentMousePos.y - referencePoint.y;

            transform.Rotate(0, 0, 0);

            //only rotate when the mouse is moving 
            if(currentMousePos != lastMousePos )
                //transform.Rotate(new Vector3(rotationxAmount * rotationSpeed, 0, 0));

            lastMousePos = currentMousePos;
        }
    }
}
