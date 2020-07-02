using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cameras;

//this script can be used on a spotlight to allow user to click 
//to expand it or right click to retract 
public class ExpandSpotlight : MonoBehaviour
{
    CameraManager camManager;
    Light spotlight;
    public float angleSpeed;
    public float intensitySpeed;

    [Header("Min Maxes")]
    public float angleMin = 10f;
    public float angleMax = 180f;
    public float intensityMin = 1f, intensityMax = 20f;

    [Header("Monologue Activation")]
    [Tooltip("Does this spotlight trigger a monologue?")]
    public bool triggersMono;
    [Tooltip("At what angle does it trigger?")]
    public float angleToTrigger;
    [Tooltip("Monologue Manager to trigger")]
    public MonologueManager monoMan;
    [Tooltip("Index in the monologue to activate")]
    public int monoToActivate;
    public bool hasActivated;

    [Header("Camera Activation")]
    public GameCamera newCamera;

    void Start()
    {
        spotlight = GetComponent<Light>();
        camManager = FindObjectOfType<CameraManager>();
    }
    
    void Update()
    {
        //left click to expand
        if (Input.GetMouseButton(0))
        {
            ExpandLight();
        }

        //right click to retract
        if (Input.GetMouseButton(1))
        {
            RetractLight();
        }
    }

    //light grows 
    public void ExpandLight()
    {
        if(spotlight.spotAngle < angleMax)
            spotlight.spotAngle += angleSpeed * Time.deltaTime;
        if(spotlight.intensity < intensityMax)
            spotlight.intensity += intensitySpeed * Time.deltaTime;

        //we are going to trigger smth
        if (triggersMono && !hasActivated)
        {
            //bigger than the angle necessary to trigger?
            if(spotlight.spotAngle > angleToTrigger)
            {
                //trigger!
                monoMan.SetMonologueSystem(monoToActivate);
                monoMan.EnableMonologue();
             
                //set new camera as well
                if (newCamera)
                    camManager.Set(newCamera);

                //so we cant reactivate
                hasActivated = true;
            }
        }
    }

    //shrink light
    public void RetractLight()
    {
        if(spotlight.spotAngle > angleMin)
            spotlight.spotAngle -= angleSpeed * Time.deltaTime;
        if(spotlight.intensity > intensityMin)
            spotlight.intensity -= intensitySpeed * Time.deltaTime;
    }
}
