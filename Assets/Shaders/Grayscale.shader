Shader "Hidden/Custom/Grayscale"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _Blend;
        sampler2D _DitherPattern;
        float4 _DitherPattern_TexelSize;
        //TEXTURE2D_SAMPLER2D(_DitherPattern, sampler_DitherPattern);
        

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            //float texColor = tex2D(_MainTex, i.texcoord).r;

            float2 screenPos = i.texcoord;
            float2 ditherCoord = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
            float ditherValue = tex2D(_DitherPattern, ditherCoord).r;
           
            //uv.x *= _Blend;
            //uv.y *= _Blend;
            //uv.x = round(uv.x);
            //uv.y = round(uv.y);
            //uv.x /= _Blend;
            //uv.y /= _Blend;
            float col = step(ditherValue, texColor);
            return col; 
            //float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            //float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
            //color.rgb = lerp(color.rgb, luminance.xxx, _Blend.xxx);
            //return color;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}