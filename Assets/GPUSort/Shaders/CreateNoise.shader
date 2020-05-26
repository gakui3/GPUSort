Shader "BitonicSort/CreateNoise"
{

	Properties
	{
		_MainTex ("", 2D) = "" {}
	}

	CGINCLUDE		
	#include "UnityCG.cginc"
	#include "Assets/CgIncludes/Random.cginc"
	#include "Assets/CgIncludes/ColorCollect.cginc"

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _MainTex_TexelSize; 
			
	float4 frag (v2f_img i) : SV_Target
	{
		// sample the texture
		float v = rand(i.uv.xy);
		float3 hsvCol = hsv2rgb(float3(v, 1, 1));
		float4 col = float4(hsvCol, 1);
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