using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsVisual : MonoBehaviour
{
    [SerializeField]
    private Renderer _bounds;

    protected void Start()
    {
        //_bounds = GetComponentInChildren<Renderer>();
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_bounds.bounds.center, _bounds.bounds.size);
    }
}
