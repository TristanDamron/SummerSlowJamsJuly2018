Shader "Custom/CircularFillShader" {
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Fill ("Fill", Range(0,1)) = 0.5
		_Color ("Tint", Color) = (1,1,1,1)
		_BgTex ("BackgroundTexture", 2D) = "black" {}
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _BgTex;
			sampler2D _AlphaTex;
			float _Fill;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv, bool _isMain)
			{
				if (_isMain) {
					fixed4 color = tex2D (_MainTex, uv);
					return color;
				} else {
					fixed4 color = tex2D(_BgTex, uv);
					return color;
				}

//#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
//				if (_AlphaSplitEnabled)
//					color.a = tex2D (_AlphaTex, uv).r;
//#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED
			}

			fixed4 frag(v2f IN) : SV_Target {
				bool isBg = ((atan2((2 * (IN.texcoord.x) - 1), (2 * (IN.texcoord.y) - 1)) / 3.14159) < (2 * (_Fill - .5)));
				fixed4 c = SampleSpriteTexture (IN.texcoord, isBg) * IN.color;
				c.rgb *= c.a;
				return c;
			}
			ENDCG
		}
	}
}
