using UnityEngine;
[ExecuteInEditMode]
public class LightCasting : MonoBehaviour
{
    [Range(0, 360)]
    public int rayCount = 50;
    int layerMask = 1 << 10;
    [Range(0.01f, 5f)]
    public float coneAngle = 1f;
    public float distanceBetweenCasts;

    public Vector3[] verts;
    public Vector3[] lastSafe;
    public float[] distance;
    public float[] difference;
    public int[] tris;
    public MeshFilter filter;
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenCasts = 360/rayCount;
        verts = new Vector3[rayCount];
        lastSafe = new Vector3[rayCount];
        distance = new float[rayCount];
        difference = new float[rayCount];
        mesh = new Mesh();
        mesh.name = "Light";
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < rayCount; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward + (transform.right * Mathf.Cos(i * distanceBetweenCasts * Mathf.Deg2Rad) * coneAngle) + (transform.up * Mathf.Sin(i * distanceBetweenCasts * Mathf.Deg2Rad) * coneAngle), out hit, 500, layerMask))
            {
                Debug.DrawRay(transform.position, (transform.forward + (transform.right * Mathf.Cos(i * distanceBetweenCasts*Mathf.Deg2Rad) * coneAngle) + (transform.up * Mathf.Sin(i * distanceBetweenCasts * Mathf.Deg2Rad) * coneAngle)) * hit.distance, Color.black);
                verts[i] = hit.point;
                lastSafe[i] = hit.point;
                distance[i] = hit.distance;
            }
            else {
                Debug.DrawRay(transform.position, transform.forward + (transform.right * Mathf.Cos(i * distanceBetweenCasts) * coneAngle) + (transform.up * Mathf.Sin(i * distanceBetweenCasts) * coneAngle), Color.red);
                verts[i] = lastSafe[i];
            }
        }

        for(int i = 0; i < rayCount; i++)
        {
            if (i == rayCount - 1) {
                difference[i] = distance[i] - distance[0];
                    } else {
                difference[i] = distance[i] - distance[i + 1];
            }
        }

        verts[verts.Length-1] = transform.position;

        MeshGen(verts);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, .5f);
        for (int i = 0; i < rayCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(verts[i], .5f);
        }
    }

    void MeshGen(Vector3[] verts)
    {
        mesh.vertices = verts;
        tris = new int[rayCount * 3];

        for(int i = 0; i < tris.Length-1; i++)
        {

         if(i%3 == 0)
            {
                if (i >= tris.Length-2)
                {
                    tris[i] = 0;
                }
                else
                {
                    tris[i] = (i / 3) + 1;
                }
            }
            else if (i%3 == 1)
            {
                tris[i] = i / 3;
                
                
            }
            else
            {
                tris[i] = rayCount;
            }
        }
        
        mesh.triangles = tris;
        
        filter.mesh = mesh;
    }
}
