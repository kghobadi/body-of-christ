using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollows : MonoBehaviour
{

    public Transform target;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        target.position = Vector3.Lerp(target.position, transform.position - new Vector3(0, 3, 0), speed * Time.deltaTime);   
    }
}
