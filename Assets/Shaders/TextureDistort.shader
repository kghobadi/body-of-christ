// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TextureDistort"
{
	Properties
	{
		_Texture0("Texture 0", 2D) = "white" {}
		_Vector1("Vector 1", Vector) = (0.02,0.01,0,0)
		_Color0("Color 0", Color) = (0,0.38296,0.9811321,0)
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform sampler2D _Texture0;
		uniform float2 _Vector1;


		//https://www.shadertoy.com/view/XdXGW8
		float2 GradientNoiseDir( float2 x )
		{
			const float2 k = float2( 0.3183099, 0.3678794 );
			x = x * k + k.yx;
			return -1.0 + 2.0 * frac( 16.0 * k * frac( x.x * x.y * ( x.x + x.y ) ) );
		}
		
		float GradientNoise( float2 UV, float Scale )
		{
			float2 p = UV * Scale;
			float2 i = floor( p );
			float2 f = frac( p );
			float2 u = f * f * ( 3.0 - 2.0 * f );
			return lerp( lerp( dot( GradientNoiseDir( i + float2( 0.0, 0.0 ) ), f - float2( 0.0, 0.0 ) ),
					dot( GradientNoiseDir( i + float2( 1.0, 0.0 ) ), f - float2( 1.0, 0.0 ) ), u.x ),
					lerp( dot( GradientNoiseDir( i + float2( 0.0, 1.0 ) ), f - float2( 0.0, 1.0 ) ),
					dot( GradientNoiseDir( i + float2( 1.0, 1.0 ) ), f - float2( 1.0, 1.0 ) ), u.x ), u.y );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 _Vector0 = float2(25,40);
			float dotResult4_g1 = dot( ( round( ( (i.uv_texcoord*1.0 + _Vector1) * _Vector0 ) ) / _Vector0.x ) , float2( 12.9898,78.233 ) );
			float lerpResult10_g1 = lerp( -1.0 , 1.0 , frac( ( sin( dotResult4_g1 ) * 43758.55 ) ));
			float2 appendResult23 = (float2(( i.uv_texcoord.x + ( lerpResult10_g1 * 0.5 ) ) , i.uv_texcoord.y));
			float mulTime43 = _Time.y * 3.0;
			float smoothstepResult50 = smoothstep( 0.2 , 0.4 , abs( ( i.uv_texcoord.x + ( ( frac( mulTime43 ) * 1.6 ) - 1.3 ) ) ));
			float mulTime20 = _Time.y * 0.7;
			float2 temp_cast_0 = (( ( i.uv_texcoord.y * 1.0 ) + mulTime20 )).xx;
			float gradientNoise25 = GradientNoise(temp_cast_0,0.1);
			float2 lerpResult9 = lerp( i.uv_texcoord , appendResult23 , ( smoothstepResult50 * ( gradientNoise25 * 3.0 ) ));
			float4 blendOpSrc47 = _Color0;
			float4 blendOpDest47 = tex2D( _Texture0, lerpResult9 );
			float4 lerpBlendMode47 = lerp(blendOpDest47,(( blendOpDest47 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest47 ) * ( 1.0 - blendOpSrc47 ) ) : ( 2.0 * blendOpDest47 * blendOpSrc47 ) ),smoothstepResult50);
			o.Emission = ( saturate( lerpBlendMode47 )).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
596;81;1309;842;2037.201;592.4284;1.655697;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1778.986,-177.9733;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;17;-1391.073,759.0522;Inherit;False;Property;_Vector1;Vector 1;1;0;Create;True;0;0;0;False;0;False;0.02,0.01;0.02,0.01;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScaleAndOffsetNode;16;-1171.241,721.0692;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;43;-2025.531,-447.6098;Inherit;False;1;0;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-1233.6,1060.117;Inherit;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;0;False;0;False;25,40;25,40;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FractNode;44;-1848.396,-449.6465;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-941.881,941.9517;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-1691.441,-450.7909;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.RoundOpNode;15;-715.4477,838.9562;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1285.83,123.8893;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;20;-1351.875,294.1043;Inherit;False;1;0;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;11;-522.7111,1019.633;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;46;-1521.491,-426.262;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1138.454,322.6587;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1318.196,-379.2619;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.11;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;8;-560.4161,609.9201;Inherit;True;Random Range;-1;;1;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,0;False;2;FLOAT;-1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-299.3168,627.0307;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;42;-1171.379,-391.7404;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;25;-988.9493,299.0093;Inherit;True;Gradient;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;50;-968.5563,-365.5541;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-828.2447,137.7637;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-699.3974,400.99;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-510.2986,251.0495;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-563.2889,83.12251;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;1;-427.8405,-255.7652;Inherit;True;Property;_Texture0;Texture 0;0;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.LerpOp;9;-328.3927,25.15928;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;40;-206.7339,-675.4415;Inherit;False;Property;_Color0;Color 0;2;0;Create;True;0;0;0;False;0;False;0,0.38296,0.9811321,0;0,0.38296,0.9811321,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-140.2581,-59.9473;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;47;152.2898,-402.5937;Inherit;True;Overlay;True;3;0;COLOR;0,0.4938371,1,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;405.8094,-74;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TextureDistort;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;3;0
WireConnection;16;2;17;0
WireConnection;44;0;43;0
WireConnection;4;0;16;0
WireConnection;4;1;5;0
WireConnection;45;0;44;0
WireConnection;15;0;4;0
WireConnection;22;0;3;2
WireConnection;11;0;15;0
WireConnection;11;1;5;1
WireConnection;46;0;45;0
WireConnection;19;0;22;0
WireConnection;19;1;20;0
WireConnection;34;0;3;1
WireConnection;34;1;46;0
WireConnection;8;1;11;0
WireConnection;53;0;8;0
WireConnection;42;0;34;0
WireConnection;25;0;19;0
WireConnection;50;0;42;0
WireConnection;7;0;3;1
WireConnection;7;1;53;0
WireConnection;54;0;25;0
WireConnection;48;0;50;0
WireConnection;48;1;54;0
WireConnection;23;0;7;0
WireConnection;23;1;3;2
WireConnection;9;0;3;0
WireConnection;9;1;23;0
WireConnection;9;2;48;0
WireConnection;2;0;1;0
WireConnection;2;1;9;0
WireConnection;47;0;40;0
WireConnection;47;1;2;0
WireConnection;47;2;50;0
WireConnection;0;2;47;0
ASEEND*/
//CHKSM=D9D06DAF71191AA81F48933E19118E0142BF2AD9