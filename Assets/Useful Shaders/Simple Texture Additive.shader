Shader "Useful Shaders/Simple Texture Additive"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color (Multiply Blended)", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "PreviewType"="Plane"}
		LOD 100

		Pass
		{
			Cull Off
			ZWrite Off
            ZTest Off
			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
                fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            fixed4 _Color;

            float _ScaleAngle;
            float4 _Scale;
			
			v2f vert (appdata v)
			{
				v2f o;
                o.color = v.color;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return tex2D(_MainTex, i.uv) * i.color * _Color;
			}
			ENDCG
		}
	}
}
