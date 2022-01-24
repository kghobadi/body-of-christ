using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightEffect : MonoBehaviour
{
    public Light lightComp;

    public enum LightSource { Fire, Moon, Sun, Lightning}

    public LightSource ls;


    [SerializeField]
    private float lightAngle;
    // Start is called before the first frame update
    void Start()
    {
        lightComp = GetComponent<Light>();


    }

    private void Update()
    {
       // lightAngle = Vector3.
    }
    /*
    private void OnValidate()
    {
        if(lightComp == null)
        {
            lightComp = GetComponent<Light>();
        }

        switch (ls)
        {
            case LightSource.Fire:
                lightComp.type = LightType.Point;
                Debug.Log("Is Fire");
                break;
            case LightSource.Moon:
                lightComp.type = LightType.Directional;
                Debug.Log("Is Moon");
                break;
            case LightSource.Sun:
                lightComp.type = LightType.Directional;
                Debug.Log("Is Sun");
                break;
            case LightSource.Lightning:
                lightComp.type = LightType.Spot;
                Debug.Log("Is Lightning");
                break;
        }
    }*/

}
