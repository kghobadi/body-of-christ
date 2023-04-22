using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringMeshes : MonoBehaviour
{

    public Mesh mesh;
    public Material mat;

    [Range(1, 1024)]
    public int count;

    public Vector2 position;
    Matrix4x4[] bladeMatrices;
    // Start is called before the first frame update
    void Start()
    {
        bladeMatrices = CreateGrassBladeMatrix();
    }

    void OnValidate(){
        //positon
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMeshInstanced(mesh, 0, mat, bladeMatrices);
    }

    Matrix4x4[] CreateGrassBladeMatrix(){
        Matrix4x4[] pos = new Matrix4x4[count];
        for(int i = 0; i < count; i++){
            float size = Random.Range(1f, 4f);
            pos[i] = Matrix4x4.TRS(new Vector3(Random.Range(0, position.x), size/2f, Random.Range(0, position.y)), Quaternion.identity, new Vector3(.2f, size, .01f));
        }

        return pos;
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireCube(transform.position + new Vector3(position.x/2, 0, position.y/2), new Vector3(position.x,1,position.y));
    }
}
