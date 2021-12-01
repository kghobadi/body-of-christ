using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MetaballRender2D : ScriptableRendererFeature
{
    [System.Serializable]
    public class MetaballRender2DSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;

        [Range(0f, 1f), Tooltip("Outline size.")]
        public float outlineSize = 1.0f;
    }

    public MetaballRender2DSettings settings = new MetaballRender2DSettings();

    class MetaballRender2DPass : ScriptableRenderPass
    {
        private Material material;

        public float outlineSize;

        private RenderTargetIdentifier source;
        private string profilerTag;

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;

            material = new Material(Shader.Find("Hidden/Metaballs2D"));
        }

        public MetaballRender2DPass(string profilerTag)
        {
            this.profilerTag = profilerTag;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            
                cmd.SetGlobalFloat("_OutlineSize", outlineSize);


                cmd.Blit(source, source, material);

                context.ExecuteCommandBuffer(cmd);
            
            
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }

    MetaballRender2DPass pass;

    public override void Create()
    {
        name = "Metaballs (2D)";

        pass = new MetaballRender2DPass("Metaballs2D");

        pass.outlineSize = settings.outlineSize;

        pass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        pass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(pass);
    }
}
