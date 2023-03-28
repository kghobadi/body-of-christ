using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    [SerializeField]
    private LightDetection _lightDet;

    [SerializeField]
    private bool _detected;

    [SerializeField]
    private LightDetection _followTarget;

    // Start is called before the first frame update
    void Start()
    {
        _lightDet = GetComponent<LightDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        _detected = _lightDet._spotted;
        Action(_detected);
    }

    void Action(bool _acting)
    {
        if (_acting)
        {
            if (_followTarget._spotted)
            {
                transform.LookAt(_followTarget.transform);
            }
            else
            {
                transform.LookAt(_lightDet._lightSources[0].posi.transform);
            }
        }
        else
        {
            
        }
    }
}
