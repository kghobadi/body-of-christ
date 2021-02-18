using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if an Illuminate script triggers this -- text will become visible
public class IlluminateText : MonoBehaviour
{
    public MonologueManager monoMan;
    public FadeUI[] textFades;
    public bool illuminated;
    Coroutine waitToFadeOut;

    public void Illuminate()
    {
        //already illuminated
        if (illuminated)
        {
            //restart coroutine
            StopCoroutine(waitToFadeOut);
            waitToFadeOut = StartCoroutine(WaitToFadeOut(0.5f));
        }
        //need to fade in!
        else
        {
            if (monoMan.inMonologue)
            {
                for (int i = 0; i < textFades.Length; i++)
                {
                    textFades[i].FadeIn();
                }
            
                illuminated = true;

                waitToFadeOut = StartCoroutine(WaitToFadeOut(0.5f));
            }
        }
    }

    IEnumerator WaitToFadeOut(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < textFades.Length; i++)
        {
            textFades[i].FadeOut();
        }
        
        illuminated = false;
        Debug.Log("stopped illunating");
    }
}
