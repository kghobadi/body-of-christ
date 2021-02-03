using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfObjects : MonoBehaviour
{

    public Transform obj1, obj2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = (obj1.position + obj2.position) / 2;
        transform.position = Vector3.Lerp(transform.position, center, Time.deltaTime);
    }
}
