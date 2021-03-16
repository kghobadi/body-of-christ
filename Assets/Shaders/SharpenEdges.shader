// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SharpenEdges"
{
	Properties
	{
		_RenderTex("RenderTex", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _RenderTex;
		uniform float4 _RenderTex_ST;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float mulTime11 = _Time.y * 0.05;
			float4 appendResult13 = (float4(mulTime11 , mulTime11 , 0.0 , 0.0));
			float simplePerlin2D14 = snoise( (i.uv_texcoord*1.0 + appendResult13.xy)*3.0 );
			simplePerlin2D14 = simplePerlin2D14*0.5 + 0.5;
			float smoothstepResult23 = smoothstep( 0.3 , 0.7 , ( ( ( distance( float2( 0.5,0.5 ) , i.uv_texcoord ) - 0.4 ) * 8.0 ) + simplePerlin2D14 ));
			float2 uv_RenderTex = i.uv_texcoord * _RenderTex_ST.xy + _RenderTex_ST.zw;
			o.Emission = ( CalculateContrast(( smoothstepResult23 + 1.0 ),tex2D( _RenderTex, uv_RenderTex )) + smoothstepResult23 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
160;18;880;880;2705.101;114.9237;2.87543;True;False
Node;AmplifyShaderEditor.Vector2Node;5;-1512.689,184.5113;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexCoordVertexDataNode;4;-1551.648,455.5963;Inherit;True;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;11;-1628.656,722.9225;Inherit;False;1;0;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;2;-1321.883,277.856;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;13;-1430.843,696.372;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1107.847,307.3769;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;16;-1260.088,614.4362;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-886.3802,312.7377;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;8;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;14;-1003.683,618.6134;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-644.0156,674.1273;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;23;-432.6668,663.6534;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.3;False;2;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1139.892,9.450196;Inherit;True;Property;_RenderTex;RenderTex;0;0;Create;True;0;0;False;0;False;-1;None;f8257aa8063a8824485fd5341717c9f9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-527.5682,116.3858;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;42;-453.0544,-216.1592;Inherit;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;3;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-179.9424,82.11115;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;SharpenEdges;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;5;0
WireConnection;2;1;4;0
WireConnection;13;0;11;0
WireConnection;13;1;11;0
WireConnection;19;0;2;0
WireConnection;16;0;4;0
WireConnection;16;2;13;0
WireConnection;25;0;19;0
WireConnection;14;0;16;0
WireConnection;10;0;25;0
WireConnection;10;1;14;0
WireConnection;23;0;10;0
WireConnection;43;0;23;0
WireConnection;42;1;1;0
WireConnection;42;0;43;0
WireConnection;45;0;42;0
WireConnection;45;1;23;0
WireConnection;0;2;45;0
ASEEND*/
//CHKSM=875E028549ACA5F97F5ECC7854866F2CFCF9B6BE