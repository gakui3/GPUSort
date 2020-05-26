Shader "Template/ShaderTemplate_vertimg"
{

	Properties
	{
		_MainTex ("", 2D) = "" {}
	}

	CGINCLUDE		
	#include "UnityCG.cginc"

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _MainTex_TexelSize; 
			
	float4 frag (v2f_img i) : SV_Target
	{
		// sample the texture
		float4 col = tex2D(_MainTex, i.uv);
		return col ;
	}
	ENDCG

	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
}