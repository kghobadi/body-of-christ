using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimation : MonoBehaviour
{
    Renderer rend;
    public bool turnedOn;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        turnedOn = false;
        SwitchStates();
    }

    private void OnMouseDown()
    {
        SwitchStates();
        
    }

    void SwitchStates()
    {
        turnedOn = !turnedOn;
        if (turnedOn)
        {
            rend.material.color = Color.red;
        }
        else
        {
            rend.material.color = Color.green;
        }
    }
}
