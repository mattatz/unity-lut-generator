Shader "LUT/Filters/ToneCurve"
{

	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Red ("Red", 2D) = "white" {}
		_Green ("Green", 2D) = "white" {}
		_Blue ("Blue", 2D) = "white" {}
	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "./CurveFilter.cginc"

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
			
			sampler2D _MainTex, _Red, _Green, _Blue;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col.r = evaluate(_Red, col.r);
				col.g = evaluate(_Green, col.g);
				col.b = evaluate(_Blue, col.b);
				return col;
			}
			ENDCG
		}
	}
}
