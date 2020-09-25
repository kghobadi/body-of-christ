using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleCamera : MonoBehaviour
{
    public Matrix4x4 originalProjection;
    public Matrix4x4 pers, ortho;
    public bool orth;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalProjection = cam.projectionMatrix;
    }

    void Update()
    {
        
        if (orth)
        {
            originalProjection.m00 = Mathf.Lerp(originalProjection.m00, ortho.m00, Time.deltaTime);
            originalProjection.m11 = Mathf.Lerp(originalProjection.m11, ortho.m11, Time.deltaTime);
            originalProjection.m22 = Mathf.Lerp(originalProjection.m22, ortho.m22, Time.deltaTime);
            originalProjection.m32 = Mathf.Lerp(originalProjection.m32, ortho.m32, Time.deltaTime);
            originalProjection.m33 = Mathf.Lerp(originalProjection.m33, ortho.m33, Time.deltaTime);
        }
        else
        {
            originalProjection.m00 = Mathf.Lerp(originalProjection.m00, pers.m00, Time.deltaTime);
            originalProjection.m11 = Mathf.Lerp(originalProjection.m11, pers.m11, Time.deltaTime);
            originalProjection.m22 = Mathf.Lerp(originalProjection.m22, pers.m22, Time.deltaTime);
            originalProjection.m32 = Mathf.Lerp(originalProjection.m32, pers.m32, Time.deltaTime);
            originalProjection.m33 = Mathf.Lerp(originalProjection.m33, pers.m33, Time.deltaTime);
        }

       
        cam.projectionMatrix = originalProjection;
    }
}
