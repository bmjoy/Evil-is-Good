    Shader "Custom/FogOfWarMask" {
        Properties {
            _Color("Main Color", Color) = (1,1,1,1)
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _BlurPower("BlurPower", float) = 0.002
            _Cutoff("Cutoff", float) = 0.01
        }
        SubShader {
            Tags { "Queue"="Transparent" "RenderType"="Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Off
            Lighting Off
     
            LOD 200
           
            pass {
                CGPROGRAM
                #pragma vertex vert_img
                #pragma fragment frag
     
                #include "UnityCG.cginc"
     
                fixed4 _Color;
                float _Cutoff;
                float _BlurPower;
                uniform sampler2D _MainTex;
     
                fixed4 frag(v2f_img i) : SV_Target{
                    half4 baseColor1 = tex2D(_MainTex, i.uv + float2(-_BlurPower, 0));
                    half4 baseColor2 = tex2D (_MainTex, i.uv + float2(0, -_BlurPower));
                    half4 baseColor3 = tex2D (_MainTex, i.uv + float2(_BlurPower, 0));
                    half4 baseColor4 = tex2D (_MainTex, i.uv + float2(0, _BlurPower));
                    half4 baseColor = 0.25 * (baseColor1 + baseColor2 + baseColor3 + baseColor4);
     
                    float alpha = _Color.a - baseColor.g;
                    clip(alpha-_Cutoff);
                    return float4(_Color.rbg*baseColor.b, alpha);
                }
                ENDCG
            }
        }
        Fallback "Diffuse"
    }
