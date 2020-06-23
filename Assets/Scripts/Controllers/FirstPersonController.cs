﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//a first  person controller script designed to work with mouse input, WASD, and controllers 
public class FirstPersonController : MonoBehaviour
{
    //timers and values for speed
    public float currentSpeed, walkSpeed, sprintSpeed;
    public float scrollSpeed = 2.0f;
    float sprintTimer = 0;
    public float sprintTimerMax = 1;

    float footStepTimer = 0;
    public float footStepTimerTotal = 0.5f;

    CharacterController player;
    GroundCamera mouseLook;
    Vector3 movement;
    ResetNearbyAudioSources resetAudio;

    //for footstep sounds
    public AudioClip[] currentFootsteps/*, indoorFootsteps, gardenFootsteps, pathFootsteps*/;
    AudioSource playerAudSource;

    public bool canMove = true;
    public bool moving;

    Vector3 lastPosition;

    //for start of radio room
    public GameObject startCam;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAudSource = GetComponent<AudioSource>();
        mouseLook = GetComponentInChildren<GroundCamera>();
        resetAudio = GetComponent<ResetNearbyAudioSources>();
    }

    void Update()
    {
        //get input device 
        //var inputDevice = InputManager.ActiveDevice;

        if (canMove)
        {
            //controller 
            //if (inputDevice.DeviceClass == InputDeviceClass.Controller)
            //{
            //    ControllerMovement();
            //}
            ////mouse & keyboard 
            //else
            //{
               
            //}
            MouseKeyboardMovement();


            //actual movement
            if (moving)
            {
                movement = transform.rotation * movement;
                player.Move(movement * Time.deltaTime);

                player.Move(new Vector3(0, -0.5f, 0));

                resetAudio.ResetNearbyAudio();
            }
        }
    }

    void ControllerMovement()
    {
        //get input device 
        //var inputDevice = InputManager.ActiveDevice;

        //moving 
        //if (inputDevice.LeftStickY != 0 && inputDevice.LeftStickX != 0)
        //{
        //    moving = true;

        //    float moveForwardBackward = inputDevice.LeftStickY * currentSpeed;
        //    float moveLeftRight = inputDevice.LeftStickX * currentSpeed;

        //    movement = new Vector3(moveLeftRight, 0, moveForwardBackward);

        //    SprintSpeed();

        //}
        ////when not moving
        //else
        //{
        //    moving = false;
        //    movement = Vector3.zero;
        //    currentSpeed = walkSpeed;
        //}
    }

    void MouseKeyboardMovement()
    {
        //when hold mouse 1, you begin to move in that direction
        if (Input.GetMouseButton(0))
        {
            moving = true;

            movement = new Vector3(0, 0, currentSpeed);

            SprintSpeed();
        }
        //move backwards
        else if (Input.GetMouseButton(1))
        {
            moving = true;

            movement = new Vector3(0, 0, -currentSpeed);

            SprintSpeed();
        }
        //WASD controls
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moving = true;

            float moveForwardBackward = Input.GetAxis("Vertical") * currentSpeed;
            float moveLeftRight = Input.GetAxis("Horizontal") * currentSpeed;

            movement = new Vector3(moveLeftRight, 0, moveForwardBackward);

            SprintSpeed();

        }
        //when not moving
        else
        {
            moving = false;
            movement = Vector3.zero;
            currentSpeed = walkSpeed;
        }
    }

    //increases move speed while player is moving over time
    public void SprintSpeed()
    {
        //increment and play footstep sounds
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0)
        {
            PlayFootStepAudio();
            footStepTimer = footStepTimerTotal;
        }

        sprintTimer += Time.deltaTime;
        //while speed is less than sprint, autoAdd
        if (sprintTimer > sprintTimerMax && currentSpeed < sprintSpeed)
        {
            currentSpeed += Time.deltaTime;
        }
    }

    private void PlayFootStepAudio()
    {
        int n = Random.Range(1, currentFootsteps.Length);
        playerAudSource.clip = currentFootsteps[n];
        playerAudSource.PlayOneShot(playerAudSource.clip, 1f);
        // move picked sound to index 0 so it's not picked next time
        currentFootsteps[n] = currentFootsteps[0];
        currentFootsteps[0] = playerAudSource.clip;
    }
}
