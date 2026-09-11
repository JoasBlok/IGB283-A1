Shader "Custom/Vertex Colour"
{
    Properties
    {
        _MainColour("Colour", Color) = (1, 0, 0, 1)
        _Metallic("Metallic", Range(0, 1)) = 0
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                half3 vertexColour : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : NORMAL;
                float3 positionWS : TEXCOORD1;
                half3 vertexColour : COLOR;
            };

            half4 _MainColour;
            float _Metallic;
            float _Smoothness;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = UnityObjectToClipPos(IN.positionOS);
                OUT.normalWS = UnityObjectToWorldNormal(IN.normalOS);
                OUT.positionWS = mul(unity_ObjectToWorld, IN.positionOS);
                OUT.vertexColour = GammaToLinearSpace(IN.vertexColour);
                return OUT;
            }

            half3 AmbientLighting()
            {
                return unity_AmbientSky;
            }

            half4 frag(Varyings IN) : SV_TARGET
            {
                // Get the light attributes
                float3 lightDirection = _WorldSpaceLightPos0.xyz;
                half3 lightColour = _LightColor0.rgb;

                // Normalize vectors
                float3 normal = normalize(IN.normalWS);
                float3 viewDirectionWS = normalize(_WorldSpaceCameraPos.xyz - IN.positionWS);
                lightDirection = normalize(lightDirection);

                // Calculate lambert lighting
                half3 diffuse = saturate(dot(normal, lightDirection));

                // Calculate shininess
                half smoothness = exp2(10 * _Smoothness + 1);
                half3 halfDirection = normalize(lightDirection + viewDirectionWS);
                float specular = pow(saturate(dot(normal, halfDirection)), smoothness) * _Smoothness;
                
                // Add a rim light based on the shininess
                float fresnelStrength = 1 - saturate(dot(normal, viewDirectionWS));
                fresnelStrength = pow(fresnelStrength, (_Smoothness + 0.01) * 3) * _Smoothness;

                // Apply the lighting to the colour
                half3 baseColour = _MainColour.rgb * IN.vertexColour;
                half3 colour = baseColour * diffuse;
                colour += AmbientLighting() * baseColour;
                colour *= (1 - _Metallic * 0.85);
                colour += (specular + fresnelStrength * AmbientLighting()) * lerp(lightColour, baseColour, _Metallic);

                return half4(colour, 1);
            }
            ENDCG
        }
    }
}
