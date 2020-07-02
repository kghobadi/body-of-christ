Shader "Unlit/ColorToonShader"
{
    Properties
    {
        _Color("Color", Color) = (0.5, 0.65, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
        _Glossiness("Glossiness", Float) = 10
    }
    SubShader
    {
        

        Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            // make fog work
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"


            struct appdata
            {
                float4 vertex : POSITION;               
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                float4 screenPosition : TEXCOORD2;
                SHADOW_COORDS(2)
                UNITY_FOG_COORDS(3)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _AmbientColor;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;
            float _Glossiness;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                o.screenPosition = ComputeScreenPos(o.pos);
                TRANSFER_SHADOW(o)
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            
            float4 _Color;

            float4 frag (v2f i) : SV_Target
            {
            
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float2 uv = float2(1 - (NdotL * 0.5 + 0.5), 0.5);
                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                float4 light = lightIntensity * _LightColor0;
                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _LightColor0;
                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = rimIntensity > _RimAmount ? 1 : 0;
                float4 rim = rimIntensity * _RimColor;
                
                float4 sample = tex2D(_MainTex, uv);
                
                //float atten = 1.0;
                //float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //float3 diffReflection = atten * max(0.0, do)
                
                
                
                
                
                
                
                float4 col = _Color * sample * (_AmbientColor + light + rim + specular);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
