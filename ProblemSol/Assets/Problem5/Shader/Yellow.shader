Shader "Custom/Yellow"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1,1,0,1)
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _MainColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _MainColor;
            }
            ENDCG
        }
    }
        Fallback "Diffuse"
}
