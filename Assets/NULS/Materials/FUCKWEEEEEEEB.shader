Shader "CustomRenderTexture/FUCKWEEEEEEEB"
{
    Properties
    { 
        _lfSample("Light Field Sample", 2D) = "white" {}
        _lfGain("Light Field Gain", float) = 1.0
        _lfAlphaMagic("Fuc u shaders", float) = 1.0
        _lfDrain("Light Field Drain", Color) = (0, 0, 0, 0)
    }

    SubShader
    {
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
            float _lfAlphaMagic;
            float4 _lfDrain;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float4 src = tex2D(_SelfTexture2D, IN.localTexcoord.xy );
                float4 lfSampleColor = tex2D(_lfSample, IN.localTexcoord.xy);
                
                lfSampleColor *= _lfGain; 
                lfSampleColor -= _lfDrain;
                
                float4 finalColor = src + lfSampleColor;

                return finalColor;
            } 
            ENDCG
        }
    }
}
