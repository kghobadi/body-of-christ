using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float bulletSpeed;
    public float speedOverTime = 10f;
    public float origSpeed;

    [Tooltip("Set while gun is aiming and locked before shooting bullet")]
    public Vector3 shotDest;
    public float shotDist = 500f;
   
    [HideInInspector]
    public TrailRenderer bulletTrail;

    void Awake()
    {
        bulletTrail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        origSpeed = bulletSpeed;
    }

    void Update () {
        //move forward on Z axis 
        transform.position = Vector3.MoveTowards(transform.position, shotDest, bulletSpeed * Time.deltaTime);
        bulletSpeed += speedOverTime;
	}

    void OnTriggerEnter(Collider other)
    {
        //return bullet and death cloud to their pools on impact 
        if(other.gameObject.tag == "Character")
        {
            //reset cloud scale && send to poolers
            ResetBullet();
        }
    }

    //can be called by Deities
    public void ResetBullet()
    {
        Destroy(gameObject);
        //bulletSpeed = origSpeed;
        //bulletTrail.Clear();
    }
}
