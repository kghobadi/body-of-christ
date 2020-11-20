using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSceneColor : MonoBehaviour
{

    public Material pieceColor;
    public Material skyColor;

    public Color currentColor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        RenderSettings.fogColor = currentColor;
        RenderSettings.ambientLight = currentColor;
        pieceColor.SetColor("_Color1", currentColor);
        skyColor.SetColor("_Tint", currentColor);
    }
}
