using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRotationController : MonoBehaviour
{
    [SerializeField] float rotationAmount, timeRec;
    [SerializeField] float sensitivity;
    [SerializeField] GameObject[] rotationObjects;
    [SerializeField] float[] rotationSpeed;

    [SerializeField] Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            rotationAmount = Input.GetAxis("Mouse X") * sensitivity;
            
        }

        for (int i = 0; i < rotationObjects.Length; i++)
        {
            rotationObjects[i].transform.localEulerAngles += Vector3.up * rotationSpeed[i] * rotationAmount;
        }
        //anim.SetFloat("Direction", -rotationAmount);
        timeRec += rotationAmount;
        timeRec = Mathf.Repeat(timeRec, 1);
        //timeRec *= 3f;
        //timeRec = Mathf.Floor(timeRec);
        //timeRec /= 3f;
        Debug.Log(timeRec);
        anim.Play("Walking", -1, -timeRec);
        //rotationAmount = Mathf.Lerp(rotationAmount, 0, Time.deltaTime * 0.1f);
    }
}
