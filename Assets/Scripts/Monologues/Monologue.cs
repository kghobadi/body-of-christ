﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cameras;

//Can use this scriptable object to create various types of tasks for NPCs to assign the player from their Task Manager
[CreateAssetMenu(fileName = "MonologueData", menuName = "ScriptableObjects/MonologueScriptable", order = 1)]
public class Monologue : ScriptableObject
{
    [Header("Info for Monologue Manager")]
    public TextAsset monologue;
    public int worldMonoIndex;

    [Tooltip("Check this to wait timeUntilStart from trigger Activation to enable Monologue")]
    public bool waitToStart;
    public float timeUntilStart;
   
    [Tooltip("Check this to lock your player's movement")]
    public bool lockPlayer = true;
    [Tooltip("The Monologue Manager will repeat this monologue until further notice")]
    public bool repeatsAtFinish;
    [Tooltip("A condensed version of the Task assignment for repeating")]
    public TextAsset condensedMonologue;
    public bool advancesScene;

    [Header("New Monologues to Enable")]
    [Tooltip("Use this for Monologues that do not assign tasks to activate a new dialogue after a certain amount of time")]
    public bool triggersMonologues;
    [Tooltip("Indeces of the Monologue Triggers to activate from within WorldMonoManager")]
    public int[] monologueIndeces;
    [Tooltip("How long should these Monologue Triggers wait to activate?")]
    public float[] monologueWaits;
   
   
    [Header("Cinematics")]
    [Tooltip("After this Monologue finishes, the manager will play a cinematic")]
    public bool playsCinematic;
    public Cinematic cinematic;
    [Tooltip("After this Monologue finishes, the manager will enable a cinematic triggers somewhere in the game")]
    public bool enablesCinematicTriggers;
    public Cinematic [] cTriggers;
  
    [Header("Info for Monologue Reader")]
    public float timeBetweenLetters = 0.1f;
    public float timeBetweenLines = 3f;
    [Tooltip("Check this and fill in array below so that each line of text can be assigned a different wait")]
    public bool conversational;
    public float[] waitTimes;
}