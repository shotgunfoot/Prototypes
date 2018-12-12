// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CutoutPulse"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Speed("Speed", Float) = -0.1
		_ScanLineColor("Scan Line Color", Color) = (0,0.7511432,1,0)
		_ScanOutLineThickness("ScanOutLineThickness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample1;
		uniform float _Speed;
		uniform float _ScanOutLineThickness;
		uniform float4 _ScanLineColor;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = tex2D( _TextureSample0, uv_TextureSample0 ).rgb;
			float2 temp_cast_1 = (1.0).xx;
			float2 panner25 = ( ( _Time.y * _Speed ) * temp_cast_1 + i.uv_texcoord);
			float4 tex2DNode32 = tex2D( _TextureSample1, panner25 );
			float4 temp_cast_2 = (_ScanOutLineThickness).xxxx;
			o.Emission = ( step( tex2DNode32 , temp_cast_2 ) * _ScanLineColor ).rgb;
			o.Alpha = 1;
			clip( tex2DNode32.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
577;92;997;573;438.7511;-49.46267;1.3;False;False
Node;AmplifyShaderEditor.RangedFloatNode;27;-1594.274,683.4899;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;-0.1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;28;-1569.19,472.0227;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;31;-1277.338,544.1843;Float;False;Constant;_Direction;Direction;2;0;Create;True;0;0;False;0;1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-1145.758,229.8145;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1359.89,683.9222;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;25;-938.9954,389.2806;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;32;-467.9496,147.5685;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;e6626ff1054340f4daff48106df10eca;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;107;-363.0963,579.3504;Float;False;Property;_ScanOutLineThickness;ScanOutLineThickness;5;0;Create;True;0;0;False;0;0;0.68;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;106;-95.59641,451.4506;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;112;-18.34715,686.4874;Float;False;Property;_ScanLineColor;Scan Line Color;4;0;Create;True;0;0;False;0;0,0.7511432,1,0;0,0.7511432,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;210.3528,495.6874;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-492.8158,-158.1183;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;c442bc55151bda444bf23c8264cd3e16;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;443.5827,72.44006;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;CutoutPulse;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;27;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;28;2
WireConnection;30;1;27;0
WireConnection;25;0;26;0
WireConnection;25;2;31;0
WireConnection;25;1;30;0
WireConnection;32;1;25;0
WireConnection;106;0;32;0
WireConnection;106;1;107;0
WireConnection;111;0;106;0
WireConnection;111;1;112;0
WireConnection;0;0;1;0
WireConnection;0;2;111;0
WireConnection;0;10;32;0
ASEEND*/
//CHKSM=2787AF5932173669FAF0A1AA3F5A9741366898D8