using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cameras;

//this script controls the gun inputs, breathing, and countdown required to fire at Helene
public class Gun : AudioHandler
{
    Camera mainCam;
    CameraManager camManager;

    [Header("Gun Settings")]
    public GameObject bullet;
    public AudioClip fireWeapon;
    public AudioClip outOfAmmo;
    public int bulletCount = 6;
    public ParticleSystem gunSmoke, bang;
    public Light flash;
    public GameCamera heleneDead;
    public bool dead;
    
    void Start()
    {
        mainCam = Camera.main;
        camManager = FindObjectOfType<CameraManager>();
    }
    
    void Update()
    {
        //behavior for player as Andy 
        if (listener == false)
        {
            AimGun();

            //only can shoot once countdown reaches 0
            if (Input.GetMouseButtonDown(0) && andyCountdown.countdown <= 0)
            {
                SpawnBullet(currentShotPos);
                bang.Play();
                flash.intensity = 15f;
            }
        }
        else
        {
            //behavior for tom
            currentShotPos = helene.position;
        }
    }

    //andy / player shoots at something 
    void SpawnBullet(Vector3 aimSpot)
    {
        if(bulletCount > 0)
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);

            Bullet bulletScript = bulletClone.GetComponent<Bullet>();

            bulletScript.shotDest = aimSpot;

            PlaySoundRandomPitch(fireWeapon, 1f);

            bulletCount--;

            gunFired.Invoke();
        }
        else
        {
            gunFired.Invoke();

            PlaySoundRandomPitch(outOfAmmo, 1f);

            Kill();
        }
    }

    void Kill()
    {
        //one time death transition 
        if (!dead)
        {
            gunSmoke.Play();

            character.SetAnimator("dead");

            camManager.Set(heleneDead);

            dead = true;
                
        }
    }

    [Header("Gun Aiming Settings")]
    public Vector3 currentShotPos;
    public LayerMask shootable;
    public float gunRange = 150f;
    public Transform helene;
  
    public CharacterAnimator character;

    //sends a raycast forward
    void AimGun()
    {
        //forward
        Vector3 dir = mainCam.transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        //raycast   
        if (Physics.Raycast(mainCam.transform.position, dir, out hit, gunRange, shootable))
        {
            //check if it has gaze tp
            if (hit.transform.gameObject.tag == "Character")
            {
                currentShotPos = hit.point;
            }
            else
            {
                currentShotPos = helene.position;
            }
        }
        else
        {
            currentShotPos = helene.position;
        }
    }
    
    [Header("Gun Listener behavior for Tom")]
    public BreathingCountdown andyCountdown;
    public UnityEvent gunFired;
    public bool listener;
    public Gun gunLeader;
    public Transform matthew;
    
    //for toms gun
    public void ShootCurrentPos()
    {
        if (bulletCount > 0)
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);

            Bullet bulletScript = bulletClone.GetComponent<Bullet>();

            bulletScript.shotDest = currentShotPos;

            PlaySoundRandomPitch(fireWeapon, 1f);

            bulletCount--;

            gunSmoke.Play();
        }
        else
        {
            gunSmoke.Play();

            character.SetAnimator("dead");
        }
    }
    
    
}
