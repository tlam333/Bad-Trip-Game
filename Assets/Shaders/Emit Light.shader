Shader "Custom/Emit Light"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}   // Main texture
        _Color ("Color", Color) = (1,1,1,1)     // Base color
        _EmissionColor ("Emission Color", Color) = (1,1,1) // Emissive color
        _EmissionStrength ("Emission Strength", Range(0, 5)) = 1 // Strength of emission
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;   // Vertex position
                float2 uv : TEXCOORD0;      // Texture coordinates
            };

            struct v2f
            {
                float4 pos : SV_POSITION;   // Transformed position
                float2 uv : TEXCOORD0;      // Pass UV to fragment shader
            };

            // Vertex shader
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // Transform position
                o.uv = v.uv;  // Pass texture UV coordinates to fragment
                return o;
            }

            // Properties
            sampler2D _MainTex;  // Texture
            float4 _Color;       // Base color
            float4 _EmissionColor; // Emission color
            float _EmissionStrength; // Emission strength

            // Fragment shader
            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;

                // Calculate emissive component
                fixed3 emissive = _EmissionStrength * _EmissionColor.rgb;

                // Final color combines texture with emissive lighting
                fixed4 finalColor = texColor + fixed4(emissive, 1.0);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Unlit"
}
