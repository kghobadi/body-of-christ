using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private Light flash;
    // Start is called before the first frame update
    void Start()
    {
        flash = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        flash.intensity = Mathf.Lerp(flash.intensity, 5f, Time.deltaTime);
    }
}
