Shader "Unlit/ClearColour" {
	SubShader {
		Tags { "RenderType"="Opaque" }

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			float4 vert(float4 vertexPos : POSITION) : SV_POSITION {
				return mul(UNITY_MATRIX_MVP, vertexPos);
			}

			float4 frag(void) : COLOR {
				return float4(0,0,0,0);
			}
			ENDCG
		}
	}
}
