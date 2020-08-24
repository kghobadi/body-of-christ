using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusControl : MonoBehaviour
{

    public List<GameObject> cacti;
    public List<float> cactiAmounts;

    // Start is called before the first frame update
    void Start()
    {


        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;
        int x = 0;
        foreach (GameObject obj in cacti)
        {
            cactiAmounts.Add(Random.Range(50f, 100f));
            props.SetFloat("_Amount", cactiAmounts[x]);

            renderer = obj.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
            x++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;
        for (int i = 0; i < cacti.Count; i++)
        {
            cactiAmounts[i] += Mathf.Sin(Time.time + i) *  Mathf.PerlinNoise(i, Time.time + i);
            props.SetFloat("_Amount", cactiAmounts[i]);
            renderer = cacti[i].GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }
}
