Shader "Hidden/Custom/Dither"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        int _Blend;
        float _Amount;
        float _Width;
        sampler2D _DitherPattern;
        float4 _DitherPattern_TexelSize;
        //TEXTURE2D_SAMPLER2D(_DitherPattern, sampler_DitherPattern);
        
        float random (float2 st) {
            return frac(sin(dot(st.xy,
                         float2(12.9898,78.233)))*
            43758.5453123);
        }

        float luminance(float3 col){
            return (0.299*col.r) + (0.587*col.g) + (0.114*col.b);
        }

        float4 SobelSamplePixel(Texture2D t, float2 uv, float3 offset){
                float4 center = SAMPLE_TEXTURE2D(t, sampler_MainTex, uv);
                
                float4 left = SAMPLE_TEXTURE2D(t, sampler_MainTex, uv - offset.xz);
                float4 right = SAMPLE_TEXTURE2D(t, sampler_MainTex, uv + offset.xz);
                float4 up = SAMPLE_TEXTURE2D(t, sampler_MainTex, uv + offset.zy);
                float4 down = SAMPLE_TEXTURE2D(t, sampler_MainTex, uv - offset.zy);

                return (abs(left - center) +
                    abs(right - center) +
                    abs(up    - center) +
                    abs(down  - center));
        }

        
                

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex , i.texcoord);
            float lum = luminance(texColor.rgb);
            //float texColor = tex2D(_MainTex, i.texcoord).r;);
            float2 screenPos = i.texcoord;
            float2 ditherCoord = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy;
            float ditherValue = tex2D(_DitherPattern, ditherCoord).r;
            texColor = lerp(step(0.2, texColor), texColor, _Blend);

            float3 newoffset = float3((1.0 / _ScreenParams.x), (1.0 / _ScreenParams.y), _Amount);

            float4 col = SobelSamplePixel(_MainTex, i.texcoord, newoffset);
            col = step(_Width, col.r);
            //uv.x *= _Blend;
            //uv.y *= _Blend;
            //uv.x = round(uv.x);
            //uv.y = round(uv.y);
            //uv.x /= _Blend;
            //uv.y /= _Blend;
//float col = step(ditherValue, texColor);
            return col;
            //return step(0.2, lum); 
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