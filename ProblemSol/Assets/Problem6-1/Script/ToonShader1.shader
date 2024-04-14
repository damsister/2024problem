Shader "Unlit/ToonShader1"
{
    Properties
    {
        _DiffuseColor("Diffuse Color", Color) = (1, 1, 1, 1)
        _LightDirection("Light Direction", Vector) = (1, -1, -1, 0)
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0.001, 0.1)) = 0.03
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float3 normal : NORMAL;
            };

            float4 _DiffuseColor;
            float4 _OutlineColor;
            float _OutlineWidth;
            float4 _LightDirection;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 diffuseColor = _DiffuseColor;

                // Calculate outline
                half4 outlineColor = _OutlineColor;
                float2 d = fwidth(i.pos.xy);
                float outline = 1.0 - smoothstep(_OutlineWidth - d.x, _OutlineWidth + d.x, i.pos.z);

                // Apply outline
                diffuseColor = lerp(diffuseColor, outlineColor, outline);

                // Apply simple lighting
                float3 lightDir = normalize(_LightDirection.xyz);
                float ndotl = dot(i.normal, lightDir);
                diffuseColor.rgb *= ndotl;

                return diffuseColor;
            }
            ENDCG
        }
    }
}
