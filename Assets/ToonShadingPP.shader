Shader "Hidden/Shader/ToonShadingPP"
{
    HLSLINCLUDE

    #pragma target 4.5
    #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/FXAA.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/RTUpscale.hlsl"

    struct Attributes
    {
        uint vertexID : SV_VertexID;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float2 texcoord   : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
        output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
        output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
        return output;
    }

    // List of properties to control your post process effect
    float _Threshold;
    TEXTURE2D_X(_InputTexture);

    inline float Dither4x4Bayer(int x, int y)
    {
        const float dither[16] = {
             1,  9,  3, 11,
            13,  5, 15,  7,
             4, 12,  2, 10,
            16,  8, 14,  6 };
        int r = y * 4 + x;
        return dither[r] / 16; // same # of instructions as pre-dividing due to compiler magic
    }

    inline float Dither8x8Bayer(int x, int y)
    {
        const float dither[64] = {
             1, 49, 13, 61,  4, 52, 16, 64,
            33, 17, 45, 29, 36, 20, 48, 32,
             9, 57,  5, 53, 12, 60,  8, 56,
            41, 25, 37, 21, 44, 28, 40, 24,
             3, 51, 15, 63,  2, 50, 14, 62,
            35, 19, 47, 31, 34, 18, 46, 30,
            11, 59,  7, 55, 10, 58,  6, 54,
            43, 27, 39, 23, 42, 26, 38, 22 };
        int r = y * 8 + x;
        return dither[r] / 64; // same # of instructions as pre-dividing due to compiler magic
    }

    float4 CustomPostProcess(Varyings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        uint2 positionSS = input.texcoord * _ScreenSize.xy;
        float3 outColor = LOAD_TEXTURE2D_X(_InputTexture, positionSS).xyz;
        float3 hsvCol = RgbToHsv(outColor);
       
        //float smoothRes = smoothstep(_Threshold, (1.0 - _Threshold), outColor);
        float brightness = smoothstep(_Threshold, (1.0 - _Threshold), hsvCol.b);
        //float hue = smoothstep(_Threshold, (1.0 - _Threshold), hsvCol.r);
        float sat = smoothstep(_Threshold, (1.0 - _Threshold), hsvCol.g);
        //float dither4Col = Dither4x4Bayer(fmod(positionSS.x, 4), fmod(positionSS.y, 4));
        float ditherCol = Dither8x8Bayer(fmod(positionSS.x, 8), fmod(positionSS.y, 8));
        float ditherSat = Dither8x8Bayer(fmod(positionSS.x, 8), fmod(positionSS.y, 8));

        ditherCol = step(ditherCol, brightness);
        //ditherHue = step(ditherHue, hue);
        ditherSat = step(ditherSat, sat);
        
        hsvCol.b = ditherCol;
        //hsvCol.b = 1;
        //hsvCol.r = 0;
        hsvCol.g = ditherSat;
   
        outColor = HsvToRgb(hsvCol);
        //outColor = ditherCol;
        //return float4(temp, 1);
        return float4(outColor, 1);
    }

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "ToonShadingPP"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}
