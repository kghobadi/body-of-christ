Shader "Hidden/Dither"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            float _BrightnessAmount;

            float4 frag(v2f_img i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv);
                tex *= _BrightnessAmount;
                return tex;
            }
            ENDCG
        }
    }

}
