using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStation : MonoBehaviour
{
    [SerializeField]
    private int _counter = 0;
 
    

    // Update is called once per frame
    void Update()
    {
        if(_counter == 1)
        {

        }
        if (_counter == 2)
        {

        }
        if (_counter == 3)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
