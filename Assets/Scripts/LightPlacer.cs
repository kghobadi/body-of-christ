using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightPlacer : MonoBehaviour
{
    [SerializeField] float lifetime;

    public List<float> lifetimes = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lifetimes.Add(lifetime);
        }

        while(lifetimes.Count > 0)
        {
            for(int i = 0; i < lifetimes.Count; i++)
            {

            }
        }
    }
}


