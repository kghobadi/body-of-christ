using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script allows user to click to rotate a light 
public class RotateLight : MonoBehaviour
{
    public float rotationSpeed;

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


            //only rotate when the mouse is moving 
            if(currentMousePos != lastMousePos )
                transform.Rotate(new Vector3(0, rotationYAmount * rotationSpeed, 0));

            lastMousePos = currentMousePos;
        }
    }
}
