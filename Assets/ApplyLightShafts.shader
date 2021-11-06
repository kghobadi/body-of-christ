﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ApplyLightShafts" {
	
	Properties 
	{ 
		_MainTex ("", any) = "" {} 
		FogRendertargetPoint ("", any) = "" {} 
		FogRendertargetLinear ("", any) = "" {} 
		LowResDepthTexture ("", any) = "" {} 				
	}
	
	CGINCLUDE

	#include "UnityCG.cginc"
	
	struct v2f 
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;	
	sampler2D _CameraDepthTexture;
	sampler2D FogRendertargetPoint;
	sampler2D FogRendertargetLinear;
	sampler2D LowResDepthTexture;
	
	float4 _MainTex_TexelSize; // (1.0/width, 1.0/height, width, height)
	float4 _CameraDepthTexture_TexelSize;
	
	float DepthThreshold;
	
	v2f vert( appdata_img v ) 
	{
		v2f o = (v2f)0; 
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		
		return o;
	}

	void UpdateNearestSample(	inout float MinDist,
								inout float2 NearestUV,
								float Z,
								float2 UV,
								float ZFull
								)
	{
		float Dist = abs(Z - ZFull);
		if (Dist < MinDist)
		{
			MinDist = Dist;
			NearestUV = UV;
		}
	}

	float4 GetNearestDepthSample(float2 uv)
	{
		//read full resolution depth
		float ZFull = Linear01Depth( SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv) );

		//find low res depth texture texel size
		const float2 lowResTexelSize = 2.0 * _CameraDepthTexture_TexelSize.xy;
		const float depthTreshold =  DepthThreshold ;
		
		float2 lowResUV = uv; 
		
		float MinDist = 1.e8f;
		
		float2 UV00 = lowResUV - 0.5 * lowResTexelSize;
		float2 NearestUV = UV00;
		float Z00 = Linear01Depth( SAMPLE_DEPTH_TEXTURE( LowResDepthTexture, UV00) );   
		UpdateNearestSample(MinDist, NearestUV, Z00, UV00, ZFull);
		
		float2 UV10 = float2(UV00.x+lowResTexelSize.x, UV00.y);
		float Z10 = Linear01Depth( SAMPLE_DEPTH_TEXTURE( LowResDepthTexture, UV10) );  
		UpdateNearestSample(MinDist, NearestUV, Z10, UV10, ZFull);
		
		float2 UV01 = float2(UV00.x, UV00.y+lowResTexelSize.y);
		float Z01 = Linear01Depth( SAMPLE_DEPTH_TEXTURE( LowResDepthTexture, UV01) );  
		UpdateNearestSample(MinDist, NearestUV, Z01, UV01, ZFull);
		
		float2 UV11 = UV00 + lowResTexelSize;
		float Z11 = Linear01Depth( SAMPLE_DEPTH_TEXTURE( LowResDepthTexture, UV11) );  
		UpdateNearestSample(MinDist, NearestUV, Z11, UV11, ZFull);
		
		float4 fogSample = float4(0,0,0,0);
		
		[branch]
		if (abs(Z00 - ZFull) < depthTreshold &&
		    abs(Z10 - ZFull) < depthTreshold &&
		    abs(Z01 - ZFull) < depthTreshold &&
		    abs(Z11 - ZFull) < depthTreshold )
		{
			fogSample = tex2Dlod( FogRendertargetLinear, float4(lowResUV,0,0)) ; 
		}
		else
		{
			fogSample = tex2Dlod(FogRendertargetPoint, float4(NearestUV,0,0)) ;
		}
		
		return fogSample;
	}

	
	float4 frag(v2f input) : SV_Target 
	{			
		float4 fogSample = GetNearestDepthSample(input.uv);
		
		float4 colourSample = tex2D(_MainTex, input.uv);
		
		float4 result = colourSample * fogSample.a + fogSample;

		return result;
	}
	
	ENDCG
	SubShader 
	{
		 Pass 
		 {
			  ZTest Always Cull Off ZWrite Off

			  CGPROGRAM
			  #pragma vertex vert
			  #pragma fragment frag
			  ENDCG
		  }
	}
	Fallback off
}
