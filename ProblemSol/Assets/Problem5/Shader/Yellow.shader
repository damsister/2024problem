//Diectional Light ���̴� ���� �ڵ� ����
Shader "Custom/Yellow"
{
    Properties
    {
        //�����ݷ� ���� �ʾƵ� ��
        _DiffuseColor("DiffuseColor", Color) = (1, 1, 1, 1)
        _LightDirection("LightDirection", Vector) = (1, -1, -1, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } //Opaque : ������, Transparent : ����


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
                float4 vertex : SV_POSITION; //SV : �ȼ� ������ ����
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

            fixed4 frag (v2f i) : SV_Target //fixed : ���� �ٲ� �� ����
            {
                // sample the texture
                //fixed4 col = float4(1.0f,1.0f,0.0,1.0f);
                float lightDir = normalize(_LightDirection); //����ȭ
                float lightIntensity = max(dot(i.normal,lightDir),0);

                float4 col = _DiffuseColor * lightIntensity;


                return col;
            }
            ENDCG
        }
    }
        //Fallback "Diffuse"
}
