Shader "Custom/LightingThreshold"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SecTex ("Second (RGB)", 2D) = "black" {}
        _Norm ("Normal", 2D) = "white" {}
        
    }
    SubShader
    {
        Tags{ "RenderType"="Transparent" "Queue"="Geometry" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Ramp fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        #include "UnityStandardUtils.cginc"
        sampler2D _MainTex, _SecTex, _Norm;
        float4 _MainTex_ST, _SecTex_ST;
        
        struct SurfaceOutputCustom {
            fixed3 Albedo;
            fixed3 Normal;
            fixed3 Emission;
            half Specular;
            fixed Gloss;
            fixed Alpha;
            fixed3 light;
            fixed3 dark;
        };
        
        float4 LightingRamp (SurfaceOutputCustom s, float3 lightDir, float atten) {     
            float towardsLight = dot(s.Normal, lightDir);
            towardsLight = towardsLight * 0.5 + 0.5;
            atten = atten * 0.5 + 0.5;
            
            float4 totalLight;
            totalLight.rgb = (atten * towardsLight) > 0.5? s.light - s.dark: s.dark;
            totalLight.a = 1;
            return totalLight;
        }

        struct Input
        {
            float4 screenPos;
        };
        
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputCustom o)
        {
            float2 textureCoordinate1 = IN.screenPos.xy / IN.screenPos.w;
            float aspect = _ScreenParams.x / _ScreenParams.y;
            textureCoordinate1.x = textureCoordinate1.x * aspect;
            float2 textureCoordinate2 = textureCoordinate1;
            textureCoordinate1.x += _Time.w;
            textureCoordinate2.y += _Time.w;
            textureCoordinate1 = TRANSFORM_TEX(textureCoordinate1, _MainTex);
            textureCoordinate2 = TRANSFORM_TEX(textureCoordinate2, _SecTex);
            
            fixed4 c = tex2D(_MainTex, textureCoordinate1);
            fixed4 e = tex2D(_SecTex, textureCoordinate2);
            o.light = c.rgb;
            o.dark = e.rgb;
            o.Alpha = c.a;
            o.Normal = UnpackScaleNormal (tex2D (_Norm, textureCoordinate1), .25);
           
        }
        ENDCG
    }
    FallBack "Diffuse"
}
