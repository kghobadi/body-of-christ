using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    AdvanceScene scener;
    public string triggerTag = "Character";

    public TriggerType triggerType;
    public enum TriggerType
    {
        LOADNEXT, RESTARTGAME, QUIT,
    }

    private void Awake()
    {
        scener = FindObjectOfType<AdvanceScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == triggerTag)
        {
            switch (triggerType)
            {
                case TriggerType.LOADNEXT:
                    scener.LoadNextScene();
                    break;
                case TriggerType.RESTARTGAME:
                    scener.Restart();
                    break;
                case TriggerType.QUIT:
                    scener.Quit();
                    break;
            }
        }
    }
}
