Shader "Custom/SSS"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Threshold ("Subsurface Threshold", Range(0.5, 1)) = 0.95

        _ScatterExp("Scatter Exponent", float) = 2.2
        _ScatterGain("Scatter Gain", float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGPROGRAM
        #pragma surface surf Subsurface fullforwardshadows
        
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        fixed4 _Color;
        fixed _Threshold;
        fixed _ScatterExp;
        fixed _ScatterGain;

        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightingSubsurface(SurfaceOutput s, float3 lightDir, float3 viewDir, half atten){
            half4 c = 1;
            half nDotL = saturate(dot(s.Normal, lightDir));
            float3 eyeVec = -viewDir;

            half lDotV = smoothstep(_Threshold, 1, saturate(dot(eyeVec, lightDir)));
            half nDotV = saturate(dot(s.Normal, viewDir));

            half scatter = nDotV * lDotV;

            c.rgb = s.Albedo * nDotL * atten * _LightColor0.rgb;
            c.rgb += pow(_LightColor0.rgb * s.Albedo * scatter, _ScatterExp) * _ScatterGain;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
