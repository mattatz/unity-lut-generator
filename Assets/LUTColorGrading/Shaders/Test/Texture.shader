Shader "ImageEffects/Texture"
{
	Properties
	{
		_MainTex ("Main", 2D) = "white" {}
		_Texture ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			sampler2D _Texture;
			float4 _Texture_TexelSize;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 scale = _Texture_TexelSize.xy / _MainTex_TexelSize.xy;
				float2 uv = i.uv;
				fixed4 col = tex2D(_Texture, uv);
				return col;
			}
			ENDCG
		}
	}
}
