// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/CrossHatch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
            //#pragma target 3.0

            #include "UnityCG.cginc"

           struct appdata_t
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			 
			struct v2f
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 screenpos : TEXCOORD1;
			};

			uniform sampler2D _MainTex;
						 
			// vertex shader
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				OUT.screenpos = ComputeScreenPos(OUT.vertex);
			 
			return OUT;
			}


			fixed4 frag (v2f i):COLOR {

				 fixed hatchYoffset = 5;
				 fixed lum_threshold_1 = 1;
				 fixed lum_threshold_2 = 0.7;
				 fixed lum_threshold_3 = 0.5;
				 fixed lum_threshold_4 = 0.3;

				fixed4 c= tex2D(_MainTex, i.texcoord);

				float lum = length (c.rgb);

				float3 tc = float3(1.0,1.0,1.0);

				float2 pos = i.screenpos * _ScreenParams.xy;

				if (lum < lum_threshold_1)
				{

					if (fmod (floor(pos.x+pos.y) , 10.0) == 0  )
					tc = float3(0.0,0.0,0.0); 

				}

				if (lum < lum_threshold_2)
				{
					if (fmod(floor(pos.x-pos.y), 10.0) == 0)
					tc = float3(0.0,0.0,0.0);
				}

				if (lum < lum_threshold_3)
				{
			
					if (fmod(floor(pos.x+pos.y - hatchYoffset), 10.0) == 0.0)
					tc = float3(0.0,0.0,0.0);
				}

				if (lum < lum_threshold_4)
				{

					if (fmod( floor(pos.x-pos.y - hatchYoffset), 10.0) == 0.0)
					tc = float3(0.0,0.0,0.0);
				}


				return float4(tc,1.0);
				//return c;
			}


			ENDCG
		}
	}
}
