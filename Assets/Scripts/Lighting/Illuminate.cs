using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class sends raycasts in a light sources direction to see if there are objects which should be illuminated
public class Illuminate : MonoBehaviour
{
    public float maxDistance = 50f;
    public bool search;

    public LayerMask illuminationLayer;

    void Update()
    {
        if (search)
        {
            SearchForObjects();
        }
    }

    void SearchForObjects()
    {
        RaycastHit hit;

        Vector3 origin = transform.position;

        //Debug.DrawRay(origin, transform.forward, Color.red);
        if (Physics.Raycast(origin, transform.forward, out hit, maxDistance, illuminationLayer))
        {
            //get text illumination
            IlluminateText textIllumination = hit.transform.gameObject.GetComponent<IlluminateText>();
            //null check
            if (textIllumination != null)
            {
                textIllumination.Illuminate();
                Debug.Log("called illuminate!");
            }
        }
    }
}
