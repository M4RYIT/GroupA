Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0,1.1)) = 0.0
    }
    SubShader
    {
        Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha  //ALPHA BLENDING
            BlendOp Add
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            uniform sampler _NoiseTex;
            uniform float4 _NoiseTex_ST;

            uniform float _Dissolve;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float4 texcoord : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
            };

            vertexOutput vert(vertexInput v)
            {
                vertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord1.xy = v.texcoord1.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
                return o;
            }

            half4 frag(vertexOutput i) : SV_Target
            {
               half4 texcol = tex2D(_MainTex, i.texcoord);
               
               half noisecol = tex2D(_NoiseTex, i.texcoord1).r - _Dissolve;     
               
               clip(noisecol);
               
               return texcol;
            }

            ENDCG
        }
    }
}
