Shader "Custom/Highlight Shader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
        _FresnelPower ("Fresnel Power", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 viewDir : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            float4 _Color;
            float _FresnelPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.viewDir = normalize(_WorldSpaceCameraPos - v.vertex.xyz);
                o.normal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float fresnel = pow(1.0 - saturate(dot(i.normal, i.viewDir)), _FresnelPower);
                return _Color * fresnel;
            }
            ENDCG
        }
    }
}
