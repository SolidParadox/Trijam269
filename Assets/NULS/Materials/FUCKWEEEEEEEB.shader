Shader "CustomRenderTexture/FUCKWEEEEEEEB"
{
    Properties
    { 
        _lfSample("Light Field Sample", 2D) = "white" {}
        _lfGain("Light Field Gain", float) = 1.0
        _lfDrain("Light Field Drain", Color) = (0, 0, 0, 0)
    }

    SubShader
    {
        Blend One OneMinusSrcAlpha  // Blend mode for transparency

        Pass
        {
            Name "FUCKWEEEEEEEB"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            sampler2D _lfSample;
            float _lfGain;
            float4 _lfDrain;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float4 lfSampleColor = tex2D(_lfSample, IN.localTexcoord.xy);
                float4 selfcol = tex2D(_SelfTexture2D, IN.localTexcoord.xy);

                // Blend lfSampleColor onto mainTexColor
                float4 finalColor = selfcol + lfSampleColor * _lfGain;
                finalColor -= float4 ( _lfDrain.rgb * _lfDrain.a, 1 );

                return finalColor;
            }
            ENDCG
        }
    }
}
