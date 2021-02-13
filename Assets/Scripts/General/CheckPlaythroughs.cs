using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlaythroughs : MonoBehaviour
{
    public int playthroughs;
    
    [Header("Storyboard Replacement")]
    public Image storyboardImg;
    public Sprite pure;
    public Sprite corrupted;
    
    [Header("Music Replacement")]
    public AudioSource music;
    public AudioClip beginning;
    public AudioClip end;
    
    void Start()
    {
        CheckPlaythrough();
    }
    
    void CheckPlaythrough()
    {
        //get playthrus
        playthroughs = PlayerPrefs.GetInt("Playthroughs");
        
        //check if user has played before 
        if (playthroughs > 0)
        {
            //end
            storyboardImg.sprite = corrupted;
            music.clip = end;
        }
        else
        {
            //beginning
            storyboardImg.sprite = pure;
            music.clip = beginning;
        }

        //increase variable
        playthroughs++;
        PlayerPrefs.SetInt("Playthroughs", playthroughs);
        //play music
        music.Play();
    }
}
