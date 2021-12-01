using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DitherRender : ScriptableRendererFeature
{

    [System.Serializable]
    public class DitherRenderSettings {

        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;

        [Range(0f, 5f), Tooltip("Brightness Amount")]
        public float BrightnessTestAmount;
    }

    public DitherRenderSettings settings = new DitherRenderSettings();

    class DitherRenderPass: ScriptableRenderPass
    {
        private Material material;

        public float brightnessAmt;

        private RenderTargetIdentifier source;
        private string profilerTag;

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;

            material = new Material(Shader.Find("Hidden/Dither"));
        }

        public DitherRenderPass(string profilerTag)
        {
            this.profilerTag = profilerTag;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.SetGlobalFloat("_BrightnessAmount", brightnessAmt);
            cmd.Blit(source, source, material);
            context.ExecuteCommandBuffer(cmd);

            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }

    DitherRenderPass pass;

    public override void Create()
    {
        name = "Dither";
        pass = new DitherRenderPass("Dither");

        pass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        pass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(pass);
    }

}
