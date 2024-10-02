Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintColor("Tint Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0.0, 0.5)) = 0.25
        _CutOutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.2
        _Distance("Distance", Float) = 1
        _Amplitude("Amplitude", Float) = 1
        _Speed("Speed", Float) = 1
        _Amount("Amount", Range(0.0, 1.0)) = 1
    }
    SubShader
    {
        // Queue: overlaid other rendering, render transparent first 
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        // zwrite off for drawing transparent objects
        ZWrite Off
        Blend  SrcAlpha OneMinusSrcAlpha

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

            // vert to frag
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _CutOutThresh;
            float _Distance;
            float _Amplitude;
            float _Speed;
            float _Amount;

            // for displacement and moving of object
            v2f vert (appdata v)
            {
                v2f o;
                // glitch movement (sin movement)
                v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude) * _Distance * _Amount;
                o.vertex = UnityObjectToClipPos(v.vertex); // local to world to view to clip to screen space
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // for color
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                col.a = _Transparency;
                clip(col.r - _CutOutThresh); // clip any pixels that have less intensity of color and not draw them
                return col;
            }
            ENDCG
        }
    }
}
