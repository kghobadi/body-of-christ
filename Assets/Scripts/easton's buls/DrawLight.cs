using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLight : MonoBehaviour
{
    public Vector3[] dist = new Vector3[2];
    public GameObject meshObj;
    MeshFilter mf;
    Mesh origMesh;
    Mesh newMesh;
    // Start is called before the first frame update
    void Start()
    {
        dist = new Vector3[2];
        for (int i = 0; i < 2; i++)
        {
            
            dist[i] = 100 * Random.onUnitSphere;
            dist[i] = new Vector3(dist[i].x, Mathf.Abs(dist[i].y), dist[i].z);
            
        }

        mf = meshObj.GetComponent<MeshFilter>();
        origMesh = mf.sharedMesh;
        newMesh = new Mesh();
        newMesh.vertices = origMesh.vertices;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        newMesh.name = "clone";
        
        
        for (int i = 0; i < 2; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position+dist[i], transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position+dist[i], transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
                //send shader coordinates hit.textureCoord
                
                newMesh.vertices[i] = transform.position + dist[i];
                newMesh.vertices[i + 2] = hit.point;
                
            }
        }
        newMesh.triangles = origMesh.triangles;
        newMesh.normals = origMesh.normals;
        newMesh.uv = origMesh.uv;

        newMesh.RecalculateNormals();
        meshObj.GetComponent<MeshFilter>().mesh = newMesh;
        meshObj.GetComponent<MeshCollider>().sharedMesh = newMesh;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < 3; i++)
        {
            //Gizmos.DrawSphere(transform.position + dist[i], 5f);
        }
    }
}
