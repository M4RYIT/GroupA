Shader "Custom/Shine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _TimeFraction ("Time Fraction", Float) = 1
        _ShineColor ("Shine Color", Color) = (1,1,1,1)
        _ShineWidth ("Shine Width", Range(0,1)) = 0.2
        _ShineAngle ("Shine Angle", Range(0,360)) = 0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha  //ALPHA BLENDING
            BlendOp Add

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            uniform float _TimeFraction;
            uniform half4 _ShineColor;
            uniform float _ShineWidth;
            uniform float _ShineAngle;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float4 texcoord : TEXCOORD0;
            };

            vertexOutput vert(vertexInput v)
            {
                vertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord.xy = v.texcoord.xy;
                return o; 
            }

            float stroke(float x, float tc, float w)
            {
                return smoothstep(x-w, x, tc) * smoothstep(x+w, x, tc);                
            }

            float2 rot(float2 tc, float2 c, float a)
            {
                tc-=float2(c.x,c.y);
                float2x2 rotM = float2x2(cos(a), -sin(a), sin(a), cos(a));
                tc = mul(rotM, tc); 
                tc += float2(c.x,c.y);
                return tc;
            }

            half4 frag(vertexOutput i) : SV_TARGET
            {
                float posx = sin(abs(_Time.y/ _TimeFraction) % 1.0);
                half4 texcol = tex2D(_MainTex, i.texcoord);
                float2 texc = rot(i.texcoord.xy, float2(posx, 0.5), radians(_ShineAngle));
                float res = stroke(posx, texc.x, _ShineWidth);

                return texcol + _ShineColor*res;
            }

            ENDCG
        }
    }
}
