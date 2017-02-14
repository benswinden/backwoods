Shader "Unlit/Mould Preview Plane" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Shininess("Shininess", Range(0.03, 1)) = 0.078125
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader{
		Tags { 
			"Queue" = "Transparent"
			"RenderType" = "Transparent" 
		}
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert alpha:premul
		struct Input {
			float2 uv_MainTex;
			float3 normals;
		};

		float4 _Color;
		sampler2D _MainTex;
		half4 _MainTex_TexelSize;
		half _Shininess;
		sampler2D _TFMouldToolTexture;
		half4 _TFMouldToolTexture_TexelSize;

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			/**
			* Calculate normals based on the approach discussed in DICE's "Terrain Rendering in Frostbite Using Procedural Shader Splatting"
			*/
			float4 normalHeights;
			v.texcoord.y = 1.0f - v.texcoord.y;
			normalHeights[0] = tex2Dlod(_TFMouldToolTexture, float4(v.texcoord + float2(0.0f, -_TFMouldToolTexture_TexelSize.y), 0.0f, 0.0f)).r;
			normalHeights[1] = tex2Dlod(_TFMouldToolTexture, float4(v.texcoord + float2(-_TFMouldToolTexture_TexelSize.x,		0.0f), 0.0f, 0.0f)).r;
			normalHeights[2] = tex2Dlod(_TFMouldToolTexture, float4(v.texcoord + float2(_TFMouldToolTexture_TexelSize.x,		0.0f), 0.0f, 0.0f)).r;
			normalHeights[3] = tex2Dlod(_TFMouldToolTexture, float4(v.texcoord + float2(0.0f, _TFMouldToolTexture_TexelSize.y), 0.0f, 0.0f)).r;
			float3 normals;
			normals.z = normalHeights[0] - normalHeights[3];
			normals.x = normalHeights[1] - normalHeights[2];
			normals.y = 2.0f;
			o.normals = normalize(normals);

			// We have to offset by half a texel to avoid spikes
			float2 halfTexelSize = _TFMouldToolTexture_TexelSize.xy * 0.5f;
			float depth = tex2Dlod(_TFMouldToolTexture, float4(v.texcoord.x - halfTexelSize.x, v.texcoord.y - halfTexelSize.y, 0, 0)).r;
			if(depth == 1.0f) {
				v.vertex.y = 0.0f;
			} else {
				v.vertex.y = depth;
			}
		}

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_TFMouldToolTexture, IN.uv_MainTex);
			o.Specular = _Shininess;
			o.Normal = IN.normals;
			o.Alpha = _Color.a;
			o.Gloss = _Color.a;
		}
		ENDCG
	}
}
