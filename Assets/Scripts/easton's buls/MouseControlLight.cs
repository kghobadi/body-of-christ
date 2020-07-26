using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlLight : MonoBehaviour
{
    private Light lit;
    public float spotAng, spotAngOrg;

    // Start is called before the first frame update
    void Start()
    {
        lit = GetComponent<Light>();
        spotAng = lit.range;
        spotAngOrg = spotAng;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetMouseButton(0))
        {
            spotAng = Mathf.Lerp(spotAng, 0, Time.deltaTime);
        }
        else
        {
            spotAng = Mathf.Lerp(spotAng, spotAngOrg, Time.deltaTime);
        }

        lit.range = spotAng;
    }
}
