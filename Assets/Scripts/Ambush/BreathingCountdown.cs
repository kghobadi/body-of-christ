using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingCountdown : AudioHandler
{
    [Header("Breathing Settings")]
    public int countdown = 3;
    public HandAnimator handAnimator;

    public AudioClip[] breathing;

    public bool resetting;
    public float timer = 0, timeBetweenSpace = 1f;

    void Start()
    {

    }
    
    void Update()
    {
        //input call when not resetting 
        if (!resetting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CountDown();
            }
        }

        //reset 
        CounterReset();
    }

    //called by pressing space 
    void CountDown()
    {
        if(countdown > 0)
        {
            //value
            countdown--;

            //sound
            PlaySoundRandomPitch(breathing[countdown], 1f);

            //animation
            handAnimator.Animator.SetTrigger("hand " + countdown.ToString());

            //reset 
            timer = 0;
            resetting = true;
        }
    }

    //time forced on press between space 
    void CounterReset()
    {
        if (resetting)
        {
            timer += Time.deltaTime;

            if(timer > timeBetweenSpace)
            {
                resetting = false;
            }
        }
    }
}
