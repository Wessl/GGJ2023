Shader "Unlit/customText"
{
    Properties{
            _MainTex("Font Texture", 2D) = "white" {}
            _Color("Text Color", Color) = (1,1,1,1)
            _TimeScaleValA("Time Scale value A", float) = 0.25
            _TimeScaleValB("Time Scale value B", float) = 0.1
            _AlphaTimeFadeFactor("Alpha Time Fade Factor", float) = 1
            _ChangeGB("Should Green and Blue color channels change too", int) = 0
    }

        SubShader{

            Tags {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
            }
            Lighting Off Cull Off ZTest Always ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                uniform float4 _MainTex_ST;
                uniform fixed4 _Color;
                uniform float _TimeScaleValA;
                uniform float _TimeScaleValB;
                uniform float _AlphaTimeFadeFactor;
                uniform int _ChangeGB;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    o.color = v.color * _Color;
                    o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
                    o.texcoord.y += (cos((o.texcoord.x + 0.1) * _Time.y * _TimeScaleValA) + sin((1 - o.texcoord.x - 0.2) * _Time.y * _TimeScaleValB) * 0.5);
                    if (_Time.y > 60)
                    {
                        o.texcoord.x += sin(o.texcoord.y * _Time.y * 3.14159 * 3.75);
                        o.vertex.x += o.texcoord.x;
                    }
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 color = i.color;
                    color.a *= (tex2D(_MainTex, i.texcoord).a - _AlphaTimeFadeFactor * _Time.y);
                    color.r += sin(_Time.y);
                    if (_ChangeGB)
                    {
                        color.g += sin(_Time.y) + cos(_Time.x);
                        color.b += cos(_Time.z);
                    }
                    return color;
                }
                ENDCG
            }
            }
}