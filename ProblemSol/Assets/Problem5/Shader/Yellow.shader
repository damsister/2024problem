//Diectional Light 쉐이더 적용 코드 배우기
Shader "Custom/Yellow"
{
    Properties
    {
        //세미콜론 넣지 않아도 됨
        _DiffuseColor("DiffuseColor", Color) = (1, 1, 1, 1)
        _LightDirection("LightDirection", Vector) = (1, -1, -1, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } //Opaque : 불투명, Transparent : 투명


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
                float4 vertex : SV_POSITION; //SV : 픽셀 단위의 색상
                float3 normal : NORMAL;
            };

            float4 _DiffuseColor;
            float4 _LightDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target //fixed : 색을 바꿀 수 없음
            {
                // sample the texture
                //fixed4 col = float4(1.0f,1.0f,0.0,1.0f);
                float lightDir = normalize(_LightDirection); //정규화
                float lightIntensity = max(dot(i.normal,lightDir),0);

                float4 col = _DiffuseColor * lightIntensity;


                return col;
            }
            ENDCG
        }
    }
        //Fallback "Diffuse"
}
