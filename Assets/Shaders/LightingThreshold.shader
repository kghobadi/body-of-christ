﻿
Shader "Custom/LightingThreshold"
{
    Properties
    {
        
        _MainTex ("Main Texture (RGB)", 2D) = "white" {}
        _SecTex ("Cast Shadow Texture (RGB)", 2D) = "black" {}
        _Third ("Facing Light Texture (RGB)", 2D) = "white" {}
        _Norm ("Normal", 2D) = "white" {}
        _OutlineWidth("Outline Width", Range(0, 1)) = 0.03
        
    }
    SubShader
    {
        Tags{ "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Ramp addshadow //alpha
        
        #pragma target 2.0
       
        sampler2D _MainTex, _SecTex, _Norm, _Third;
        float4 _MainTex_ST, _SecTex_ST, _Norm_ST;
        
        struct SurfaceOutputCustom {
            fixed3 Albedo;
            fixed3 Normal;
            fixed3 Emission;
            half Specular;
            fixed Gloss;
            fixed Alpha;
            float3 light;
            float3 dark;
            float3 high;
        };
        
        float4 LightingRamp (SurfaceOutputCustom s, float3 lightDir, float atten) {     
            float towardsLight = dot(s.Normal, lightDir);
            
            float4 totalLight;
            
           //
          // totalLight.rgb = s.dark;
          
           //totalLight.rgb = atten < 0.5 ? s.dark - s.light: s.light;
           
           //totalLight.rgb = towardsLight > 0.5 ? totalLight.rgb - s.dark : s.dark;
           
            totalLight.rgb = smoothstep(s.dark, s.light, towardsLight);
            //totalLight.rgb = towardsLight > 0.5 ? s.light : s.dark;
            totalLight.a = 1;//towardsLight > 0.5 ? 1.0 : 1.0;
            return totalLight;
        }

        struct Input
        {
            float4 screenPos;
            fixed3 viewDir;
            //float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputCustom o)
        {
            float2 textureCoordinate1 = IN.screenPos.xy/IN.screenPos.w;
            float aspect = _ScreenParams.x/_ScreenParams.y;
            textureCoordinate1.x = textureCoordinate1.x * aspect;
            float2 textureCoordinate2 = textureCoordinate1;
            float2 normTex = textureCoordinate1;
            
           // textureCoordinate1.x += _Time.y;
           // textureCoordinate2.y += _Time.y;
            // normTex.x -= _Time.x;
            
            textureCoordinate1 = TRANSFORM_TEX(textureCoordinate1, _MainTex);
            textureCoordinate2 = TRANSFORM_TEX(textureCoordinate2, _SecTex);
            normTex.xy = TRANSFORM_TEX(normTex.xy, _Norm);
            
            fixed4 c = tex2D(_MainTex, textureCoordinate1);
            fixed4 e = tex2D(_SecTex, textureCoordinate2);
            fixed4 w = tex2D(_Third, textureCoordinate1);
            
           // o.Normal = UnpackScaleNormal ( tex2D(_Norm, IN.uv_MainTex), 1);
            
            o.light = c.rgb;
            o.dark = e.rgb;
            o.high = w.rgb;
           // o.light = (1,1,1);
            //o.dark = (0,0,0);
            o.Emission = o.light;
            ///rim
            fixed3 view = normalize(IN.viewDir);
            fixed3 nml = o.Normal;
            fixed VdN = dot(view, nml);
            fixed rim = 1.0 - saturate(VdN);
            
            //o.Emission = pow(rim, 5) > 0.5 ? c.rgb - e.rgb: e.rgb;
            o.Alpha = 1;

           
        }
        ENDCG
       
    }
    
}
