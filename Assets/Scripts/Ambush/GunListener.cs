
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the behavior of tom's gun, which listens to Andy's Gun.cs for event commands
public class GunListener : AudioHandler
{
    [Header("Gun Listener Settings")]
    public Gun gunLeader;
    public BreathingCountdown andyCountdown;
    public Transform myTarget;
    public AudioClip fireWeapon;
    public ParticleSystem gunSmoke, bang;
    public CharacterAnimator character;

    public bool killed;
    void Start()
    {
        gunLeader.gunFired.AddListener(ShootCurrentPos);
    }
    
    //for toms gun
    public void ShootCurrentPos()
    {
        if (gunLeader.bulletCount > 0)
        {
            GameObject bulletClone = Instantiate(gunLeader.bullet, transform.position, Quaternion.identity);

            Bullet bulletScript = bulletClone.GetComponent<Bullet>();

            bulletScript.shotDest = myTarget.position;

            PlaySoundRandomPitch(fireWeapon, 1f);

            bang.Play();
        }
        else
        {
            Kill();
        }
    }

    void Kill()
    {
        if (!killed)
        {
            gunSmoke.Play();

            character.SetAnimator("dead");

            killed = true;
        }
    }
}

