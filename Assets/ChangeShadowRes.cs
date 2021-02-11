using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShadowRes : MonoBehaviour
{
    public Vector3 shade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.RandomRange(0f, .98f);
        float y = Random.RandomRange(x, .99f);
        float z = Random.RandomRange(y, 1f);
        QualitySettings.shadowCascade4Split = shade;
    }
}
