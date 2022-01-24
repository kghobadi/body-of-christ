using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingCube : MonoBehaviour
{
    [Serializable]
    public class LightSource
    {
        public Light posi;
        public LightType type;
        public float lightStrength;
    }

    [SerializeField]
    private float _speed;

    [SerializeField]
    private List<LightSource> _lightSources;

    [SerializeField]
    private bool _spotted;

    protected void Update()
    {
        foreach (LightSource _light in _lightSources)
        {


            if (_light.type == LightType.Directional)
            {
                //just check direction from pos
                RaycastHit hit;
                Vector3 testPos = Vector3.zero;
                Vector3 dir = _light.posi.transform.forward * 20f;
                //Debug.DrawRay(_light.posi.transform.position, dir, Color.black);

                testPos = transform.position - dir;
                Debug.DrawRay(testPos, dir);
                if (Physics.Raycast(testPos, dir, out hit))
                {
                    if (hit.transform == transform)
                    {
                        Debug.Log("direct" + hit.collider.name);
                        _spotted = true;
                    }
                    else
                    {
                        _spotted = false;
                    }
                }
            }
            else if (_light.type == LightType.Point)
            {
                //Debug.Log("one point light");
                //just check range from pos
                RaycastHit hit;
                Vector3 dir = transform.position - _light.posi.transform.position;
               // Debug.DrawRay(_light.posi.transform.position, dir, Color.black);
                if (Physics.Raycast(_light.posi.transform.position, dir, out hit, _light.lightStrength))
                {

                    if (hit.transform == transform)
                    {
                        _spotted = true;
                        Debug.Log("point" + hit.collider.name);
                    }
                    else
                    {
                        _spotted = false;
                    }
                }

            }

            }
            /*
             //if in light grow, if not stop
             if (_spotted)
             {
                 transform.localScale += Vector3.up * _speed;
             }
             else
             {

             }*/
        }
    }
