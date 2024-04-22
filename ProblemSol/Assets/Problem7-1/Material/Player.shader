Shader "Custom/Player"
{
    Properties
    {
        _Color("Main Color", Color) = (1.0, 0.0, 0.0, 1.0)
        _LightDirection("Light Direction", Vector) = (1,-1,-1,0)
        _AmbientIntensity("Ambient Intensity", Range(0, 1)) = 0.2
        _DiffuseIntensity("Diffuse Intensity", Range(0, 1)) = 0.5
        _SpecularIntensity("Specular Intensity", Range(0, 1)) = 0.5
        _SpecularPower("Specular Power", Range(1, 100)) = 10.0
        _LightColor("Light Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }

        SubShader
    {
        Tags { "LightMode" = "ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            float4 _Color;
            float3 _LightDirection;
            float _AmbientIntensity;
            float _DiffuseIntensity;
            float _SpecularIntensity;
            float _SpecularPower;
            float4 _LightColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.lightDir = normalize(_WorldSpaceLightPos0 - v.vertex);
                o.viewDir = normalize(_WorldSpaceCameraPos - v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 worldNormal = normalize(i.worldNormal);
                float3 lightDir = normalize(_LightDirection);
                float3 viewDir = normalize(i.viewDir);
                float3 halfDir = normalize(lightDir + viewDir);

                float ambient = _AmbientIntensity;
                float diffuse = max(0.0, dot(worldNormal, lightDir)) * _DiffuseIntensity;

                float specular = pow(max(0.0, dot(worldNormal, halfDir)), _SpecularPower) * _SpecularIntensity;

                fixed4 finalColor = _Color * (_LightColor * (ambient + diffuse) + specular);

                return finalColor;
            }
            ENDCG
        }
    }
}
