Shader "Custom/ModifiedFlare" {
Properties {
    _MainTex ("Particle Texture", 2D) = "black" { }
}
SubShader { 
    Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }

    Pass {
        Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
        ZTest LEqual
        ZWrite Off
        Cull Off
        Blend One One
        
        /* Vertex Shader */
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        
        struct appdata_t {
            float4 vertex : POSITION;
            float4 color : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f {
            float4 pos : SV_POSITION;
            float4 color : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;

        v2f vert(appdata_t v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.color = v.color;
            o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
            return o;
        }

        half4 frag(v2f i) : SV_Target {
            half4 texcol = tex2D(_MainTex, i.texcoord);
            return texcol * i.color;
        }
        ENDCG
    }
}
Fallback "Diffuse"
}
