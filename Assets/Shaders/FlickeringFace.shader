// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlickeringFace"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half filler;
		};

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
270.6667;941.3334;880;898;919.8596;488.5126;1.521768;True;False
Node;AmplifyShaderEditor.CommentaryNode;8;-1909.823,-291.2761;Inherit;False;750.3656;439.4174;;4;3;1;2;7;normal light;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;3;-1859.823,-30.85869;Inherit;False;True;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;1;-1794.21,-241.276;Inherit;False;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;2;-1567.954,-103.2605;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-1407.917,-106.1795;Float;False;normal_lightDir;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;9;-1124.495,-115.8431;Inherit;False;7;normal_lightDir;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;12;-344.2809,-101.4017;Inherit;False;shadow;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-642.3519,-347.5283;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;15;-751.5005,-231.1985;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;-594.958,-198.1666;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-807.5956,-34.01869;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;52e66a9243cdfed44b5e906f5910d35b;52e66a9243cdfed44b5e906f5910d35b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;14;-837.0591,285.6802;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.49;False;2;FLOAT;0.51;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;13;-303.5399,447.367;Inherit;False;12;shadow;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;243,-165;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;FlickeringFace;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;7;0;2;0
WireConnection;12;0;9;0
WireConnection;17;0;9;0
WireConnection;15;0;9;0
WireConnection;16;0;15;0
WireConnection;11;1;9;0
WireConnection;14;0;9;0
ASEEND*/
//CHKSM=52870DFBC54D6D2649C1B12F5D83FDB4BE73FBF7