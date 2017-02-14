// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/EnlargeObjects" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		_ExtendDistance("Extend Distance", Float) = 1.0
	}
	SubShader {
		Tags {
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
			"DisableBatching" = "True"
		}
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float _ExtendDistance;
			struct v2f {
				float4 vertex : SV_POSITION;
				float3 worldPosition : POSITION1;
			};

			v2f vert(float4 v : POSITION) {
				float4 modelX = float4(1.0, 0.0, 0.0, 0.0);
				float4 modelY = float4(0.0, 1.0, 0.0, 0.0);
				float4 modelZ = float4(0.0, 0.0, 1.0, 0.0);

				float3 sizeInWorld = float3(mul(unity_ObjectToWorld, modelX).x, mul(unity_ObjectToWorld, modelY).y, mul(unity_ObjectToWorld, modelZ).z);
				float3 extendedSizeInWorld = sizeInWorld + float3(_ExtendDistance, _ExtendDistance, _ExtendDistance);
				float3 scaleMultiplier = sizeInWorld / extendedSizeInWorld;

				v2f o;
				//o.vertex = mul(UNITY_MATRIX_MVP, float4(v.x * scaleMultiplier.x, v.y, v.z * scaleMultiplier.z, v.w));
				o.vertex = mul(UNITY_MATRIX_MVP, v);
				o.worldPosition = mul(unity_ObjectToWorld, v);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				//return fixed4(1, 1, 1, 1);
				return float4(i.worldPosition.z, 1.0f, 1.0f, 1.0f);
			}
			ENDCG
		}
	}
}