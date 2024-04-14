Shader "Custom/PhongShader"
{
    Properties
    {
        _DiffuseColor("Diffuse Color", Color) = (1, 1, 1, 1)
        _LightDirection("Light Direction", Vector) = (1, -1, -1, 0)
        _SpecularColor("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess("Shininess", Range(1, 100)) = 32
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Phong

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _DiffuseColor;
        float4 _LightDirection;
        fixed4 _SpecularColor;
        float _Shininess;

        void surf(Input IN, inout SurfaceOutputPhong o)
        {
            // Albedo
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _DiffuseColor;
            o.Albedo = c.rgb;

            // Normal
            o.Normal = float3(0, 0, 1); // You may want to provide the actual normal here

            // Specular
            float3 lightDir = normalize(_LightDirection.xyz);
            float3 viewDir = normalize(_WorldSpaceCameraPos - o.WorldPos);
            float3 halfDir = normalize(lightDir + viewDir);
            float specAngle = max(0.0, dot(o.Normal, halfDir));
            o.Specular = _SpecularColor.rgb * pow(specAngle, _Shininess);
        }
        ENDCG
    }
        FallBack "Diffuse"
}
