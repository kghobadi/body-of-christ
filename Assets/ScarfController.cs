using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfController : MonoBehaviour
{

    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed));
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
    }
}
