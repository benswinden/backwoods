Shader "Hidden/TerrainFormerSeparableBlur" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "" {}
		//_BlurOffsets("Blur Offset", Vector) = (0.0, 0.0, 0.0, 0.0)
		//_BlurSize("Blur Size", Int) = 3
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	//float2 _BlurOffsets;
	int _BlurSize;
	sampler2D _MainTex;
	fixed4 _MainTex_TexelSize;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		
		return o;
	}

	fixed4 frag(v2f i) : COLOR {
		return float4(0.5f, 0.5f, 0.5f, 0.5f);

		const float weights[7] = { 0.40, 0.15, 0.15, 0.10, 0.10, 0.05, 0.05 };
		const int blurExtents = 3;

		float4 blurOffsets = float4(0.01f, 0.01f, 0, 0);

		float4 colour = float4(0.0, 0.0, 0.0, 1.0);
		for(int b = -blurExtents; b <= blurExtents; b++) {
			colour.r += tex2D(_MainTex, i.uv + (blurOffsets.xy * b)).r * weights[b + 3];
		}

		return colour;
	}

	ENDCG
	
	Subshader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }

			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}

	Fallback off
}