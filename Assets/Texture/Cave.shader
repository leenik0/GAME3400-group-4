Shader "Custom/URP_Unlit_Triplanar"
{
    Properties
    {
        _BaseMap ("Base Map", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _Tiling ("World Tiling", Float) = 0.1
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS  : TEXCOORD0;
                float3 normalWS    : TEXCOORD1;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _Tiling;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                float3 n = abs(normalize(IN.normalWS));
                n /= (n.x + n.y + n.z);

                float3 wp = IN.positionWS * _Tiling;

                half4 x = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, wp.yz);
                half4 y = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, wp.xz);
                half4 z = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, wp.xy);

                half4 color = x * n.x + y * n.y + z * n.z;
                return color * _BaseColor;
            }
            ENDHLSL
        }
    }
}
