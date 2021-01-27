/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the behavior of tom's gun
public class GunListener : MonoBehaviour
{
    public Gun gunLeader;
    public BreathingCountdown andyCountdown;
    public Transform myTarget;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    //for toms gun
    public void ShootCurrentPos()
    {
        if (gunLeader.bulletCount > 0)
        {
            GameObject bulletClone = Instantiate(gunLeader.bullet, transform.position, Quaternion.identity);

            Bullet bulletScript = bulletClone.GetComponent<Bullet>();

            bulletScript.shotDest = gunLeader.currentShotPos;

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
*/
