// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Waves"
{
	Properties
	{
		_LineWidth("LineWidth", Range( 0.015 , 0.5)) = 0.09794078
		_SpeedX("SpeedX", Float) = 3
		_TileX("TileX", Float) = 5
		_Scale("Scale", Float) = 0.27
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _TileX;
		uniform float _SpeedX;
		uniform float _Scale;
		uniform float _LineWidth;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_381_0 = ( ( ( i.uv_texcoord.x + i.uv_texcoord.y ) * _TileX ) + ( _SpeedX * _Time.y ) );
			float temp_output_390_0 = ( i.uv_texcoord.x + ( sin( temp_output_381_0 ) * _Scale ) );
			o.Alpha = (( abs( ( temp_output_390_0 - 0.5 ) ) < _LineWidth ) ? 1.0 :  0.0 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
787;73;670;648;2426.46;851.2348;2.759353;False;False
Node;AmplifyShaderEditor.CommentaryNode;414;-2838.703,-248.9962;Float;False;2637.658;854.4026;Wavy;22;390;399;398;397;394;393;392;374;376;377;380;409;410;386;385;381;378;382;375;373;412;370;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;370;-2856.925,5.816216;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;412;-2558.595,-157.0056;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;373;-2345.468,-57.50896;Float;False;Property;_TileX;TileX;4;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;380;-2263.026,47.70544;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;375;-2113.182,-75.4155;Float;False;Property;_SpeedX;SpeedX;2;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;382;-1931.089,-72.8786;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;378;-2174.058,-173.8049;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;381;-1764.418,-160.7209;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;377;-1763.293,147.1513;Float;False;Property;_Scale;Scale;6;0;Create;True;0;0;False;0;0.27;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;385;-1580.05,-87.70929;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;386;-1380.168,-133.8856;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;415;-921.4074,-541.8224;Float;False;Constant;_Float9;Float 9;2;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;390;-1104.925,-86.70016;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;416;-705.0344,-560.1017;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;418;-375.0418,-549.532;Float;False;Property;_LineWidth;LineWidth;1;0;Create;True;0;0;False;0;0.09794078;0.015;0.015;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;420;-284.5771,-350.7693;Float;False;Constant;_Float12;Float 12;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;419;-365.1943,-446.8973;Float;False;Constant;_Float8;Float 8;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;417;-501.8385,-519.1727;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;398;-1333.221,395.3104;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;376;-2177.504,258.1566;Float;False;Property;_SpeedY;SpeedY;3;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;399;-1132.548,332.3822;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;397;-1572.757,459.9299;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;394;-1827.32,315.7715;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;374;-2523.3,313.1476;Float;False;Property;_TileY;TileY;5;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;421;-74.32069,-524.175;Float;True;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;423;-1645.067,-631.959;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;409;-504.9651,250.4589;Float;True;Property;_Texture;Texture;0;0;Create;True;0;0;False;0;184411e5cbfcef94d90c27171a6135d6;184411e5cbfcef94d90c27171a6135d6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;424;-1877.475,-519.5791;Float;False;Property;_NoiseScale;NoiseScale;8;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;392;-2348.786,296.1627;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;393;-1994.067,263.8187;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;410;-746.0427,280.5585;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;422;-1294.012,-652.949;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;190;270.516,-229.6672;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Waves;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;7;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;412;0;370;1
WireConnection;412;1;370;2
WireConnection;382;0;375;0
WireConnection;382;1;380;2
WireConnection;378;0;412;0
WireConnection;378;1;373;0
WireConnection;381;0;378;0
WireConnection;381;1;382;0
WireConnection;385;0;381;0
WireConnection;386;0;385;0
WireConnection;386;1;377;0
WireConnection;390;0;370;1
WireConnection;390;1;386;0
WireConnection;416;0;390;0
WireConnection;416;1;415;0
WireConnection;417;0;416;0
WireConnection;398;0;377;0
WireConnection;398;1;397;0
WireConnection;399;0;370;2
WireConnection;399;1;398;0
WireConnection;397;0;394;0
WireConnection;394;0;393;0
WireConnection;394;1;392;0
WireConnection;421;0;417;0
WireConnection;421;1;418;0
WireConnection;421;2;419;0
WireConnection;421;3;420;0
WireConnection;423;0;381;0
WireConnection;423;1;424;0
WireConnection;409;1;410;0
WireConnection;392;0;370;2
WireConnection;392;1;374;0
WireConnection;393;0;380;2
WireConnection;393;1;376;0
WireConnection;410;0;390;0
WireConnection;410;1;399;0
WireConnection;422;0;423;0
WireConnection;190;9;421;0
ASEEND*/
//CHKSM=9CA7B1ED1910DFFB50DA09C20A4E5D498614C1D2