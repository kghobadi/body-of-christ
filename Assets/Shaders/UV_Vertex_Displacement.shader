﻿Shader "Custom/UV_Vertex_Displacement"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Direction ("Extrusion Direction", Vector) = (0,0,0,0)
        _UVThreshold ("UV Threshold", Range(0, 1)) = 0.5
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        half _UVThreshold;
        fixed4 _Color;
        fixed4 _Direction;
        

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
            UNITY_DEFINE_INSTANCED_PROP(float, _Amount)
        UNITY_INSTANCING_BUFFER_END(Props)
        void vert(inout appdata_full v) {
          v.vertex.y += step(_UVThreshold, v.texcoord.y) * UNITY_ACCESS_INSTANCED_PROP(Props, _Amount) * _Direction.y;
          v.vertex.x += step(_UVThreshold, v.texcoord.y) * UNITY_ACCESS_INSTANCED_PROP(Props, _Amount) * _Direction.x;
          v.vertex.z += step(_UVThreshold, v.texcoord.y) * UNITY_ACCESS_INSTANCED_PROP(Props, _Amount) * _Direction.z;
      }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
