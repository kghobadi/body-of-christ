using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(DitherRenderer), PostProcessEvent.AfterStack, "Custom/Dither")]
public sealed class Dither : PostProcessEffectSettings
{


    [Tooltip("Dither Effect Enabled")]
    public BoolParameter enable = new BoolParameter { value = false };

    [Range(0,1), Tooltip("Offset Amount")]
    public FloatParameter amount = new FloatParameter { value = 0 };

    [Range(0,1), Tooltip("Width Amount")]
    public FloatParameter width = new FloatParameter { value = 0 };
}

public sealed class DitherRenderer : PostProcessEffectRenderer<Dither>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Dither"));
        //sheet.properties.SetTexture("_DitherPattern", settings.texture);
        sheet.properties.SetInt("_Blend", settings.enable ? 0:1);
        sheet.properties.SetFloat("_Amount", settings.amount);
        sheet.properties.SetFloat("_Width", settings.width);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}