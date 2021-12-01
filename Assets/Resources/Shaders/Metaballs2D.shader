Shader "Hidden/Metaballs2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

			float _OutlineSize;


            float4 frag (v2f_img i) : SV_Target
            {
				float4 tex = tex2D(_MainTex, i.uv);

                float4 texAmount = tex * _OutlineSize;

                return tex;
            }
            ENDCG
        }
    }
}
