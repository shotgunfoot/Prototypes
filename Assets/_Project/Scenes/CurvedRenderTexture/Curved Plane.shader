// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/Templates/CurvedPlane"
{
	Properties
	{
		_RenderTexture("RenderTexture", 2D) = "white" {}
		_Float0("Float 0", Float) = 1
		_Scale("Scale", Float) = 1
		_Offset("Offset", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float _Float0;
			uniform float _Scale;
			uniform float _Offset;
			uniform sampler2D _RenderTexture;
			uniform float4 _RenderTexture_ST;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz += float3( (sin( ( ( _Float0 * UNITY_PI ) * v.ase_texcoord.xy ) )*_Scale + _Offset) ,  0.0 );
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv_RenderTexture = i.ase_texcoord.xy * _RenderTexture_ST.xy + _RenderTexture_ST.zw;
				
				
				finalColor = tex2D( _RenderTexture, uv_RenderTexture );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16100
757;73;701;657;1088.003;409.5793;2.354316;False;False
Node;AmplifyShaderEditor.RangedFloatNode;47;-722.6254,272.4889;Float;False;Property;_Float0;Float 0;1;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;44;-571.7295,349.7625;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;46;-561.8459,230.3666;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-368.8655,270.7384;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-281.4932,559.198;Float;False;Property;_Scale;Scale;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-143.6929,577.3981;Float;False;Property;_Offset;Offset;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;43;-227.1744,248.0889;Float;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-468,-216.5;Float;True;Property;_RenderTexture;RenderTexture;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;57;6.272303,248.676;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/CurvedPlane;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;46;0;47;0
WireConnection;45;0;46;0
WireConnection;45;1;44;0
WireConnection;43;0;45;0
WireConnection;57;0;43;0
WireConnection;57;1;59;0
WireConnection;57;2;60;0
WireConnection;0;0;1;0
WireConnection;0;1;57;0
ASEEND*/
//CHKSM=B7C314D1F5E53949D16E421E57D0DF7425A8F99F