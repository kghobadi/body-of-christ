using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceScene : MonoBehaviour {

    public float timeToRestart = 5f;
    public float restartTimer;

    public FadeUI fader;

	void Update ()
    {
        //next scene 
        if (Input.GetKeyDown(KeyCode.Return) )
        {
            LoadNextScene();
        }

        //previous scene 
        if (Input.GetKeyDown(KeyCode.CapsLock) )
        {
            LoadPreviousScene();
        }

        //restart game
        if (Input.GetKey(KeyCode.Delete) )
        {
            restartTimer += Time.deltaTime;

            if(restartTimer > timeToRestart)
            {
                Restart();
            }
        }
        else
        {
            restartTimer = 0;
        }
	}

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadNextScene()
    {
        if (fader)
        {
            fader.FadeIn();

            StartCoroutine(WaitToLoad(1f));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator WaitToLoad(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
