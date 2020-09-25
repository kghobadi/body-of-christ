using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SendLightPosition : MonoBehaviour
{
    public Transform lightPos;
    public Material light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 normalDir = lightPos.transform.localEulerAngles.normalized;
        light.SetVector("Vector3_EFA8D867", normalDir);
        
    }
}
