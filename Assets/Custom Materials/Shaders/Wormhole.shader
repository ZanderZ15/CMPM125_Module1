//This was converted from Shadertoy using Gemini
//https://share.google/aimode/ZSxtbsLDDft8yqF8q
Shader "Custom/UniverseWithin"
{
    Properties
    {
        _MainTex ("Audio FFT Texture (Optional)", 2D) = "black" {}
        _AudioBass ("Audio Bass Level", Range(0, 1)) = 0.2
        _MousePos ("Offset / Mouse Parallax", Vector) = (0, 0, 0, 0)
        _Speed ("Animation Speed", Float) = 0.5
        _UseFadeCycle ("Use 230s Fade Cycle", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define S(a, b, t) smoothstep(a, b, t)
            #define NUM_LAYERS 4.0

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _AudioBass;
            float4 _MousePos;
            float _Speed;
            float _UseFadeCycle;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float N21(float2 p)
            {
                float3 a = frac(float3(p.xyx) * float3(213.897, 653.453, 253.098));
                a += dot(a, a.yzx + 79.76);
                return frac((a.x + a.y) * a.z);
            }

            float2 GetPos(float2 id, float2 offs, float t)
            {
                float n = N21(id + offs);
                float n1 = frac(n * 10.0);
                float n2 = frac(n * 100.0);
                float a = t + n;
                return offs + float2(sin(a * n1), cos(a * n2)) * 0.4;
            }

            float df_line(in float2 a, in float2 b, in float2 p)
            {
                float2 pa = p - a;
                float2 ba = b - a;
                float h = clamp(dot(pa, ba) / dot(ba, ba), 0.0, 1.0);
                return length(pa - ba * h);
            }

            float lineSegment(float2 a, float2 b, float2 uv)
            {
                float r1 = 0.04;
                float r2 = 0.01;
                
                float d = df_line(a, b, uv);
                float d2 = length(a - b);
                float fade = S(1.5 * 4.0, 0.0, d2);
                
                fade += S(0.05, 0.02, abs(d2 - 0.75));
                return S(r1, r2, d) * fade;
            }

            float NetLayer(float2 st, float n, float t)
            {
                float2 id = floor(st) + n;
                st = frac(st) - 0.5;
               
                float2 p[9];
                int idx = 0;
                for (float y = -1.0; y <= 1.0; y++)
                {
                    for (float x = -1.0; x <= 1.0; x++)
                    {
                        p[idx++] = GetPos(id, float2(x, y), t);
                    }
                }
                
                float m = 0.0;
                float sparkle = 0.0;
                
                for (int i = 0; i < 9; i++)
                {
                    m += lineSegment(p[4], p[i], st);

                    float d = length(st - p[i]);
                    float s = (0.005 / (d * d));
                    s *= S(1.0, 0.7, d);
                    
                    float pulse = sin((frac(p[i].x) + frac(p[i].y) + t) * 5.0) * 0.4 + 0.6;
                    pulse = pow(pulse, 20.0);

                    s *= pulse;
                    sparkle += s;
                }
                
                m += lineSegment(p[1], p[3], st);
                m += lineSegment(p[1], p[5], st);
                m += lineSegment(p[7], p[5], st);
                m += lineSegment(p[7], p[3], st);
                
                float sPhase = (sin(t + n) + sin(t * 0.1)) * 0.25 + 0.5;
                sPhase += pow(sin(t * 0.1) * 0.5 + 0.5, 50.0) * 5.0;
                m += sparkle * sPhase;
                
                return m;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Center UVs and adjust aspect ratio using screen dimensions
                float2 uv = i.uv - 0.5;
                uv.x *= _ScreenParams.x / _ScreenParams.y;
                
                float2 M = _MousePos.xy;
                float time = _Time.y * _Speed;
                float t = 8.0 * sin(time * 0.4);
                
                float s = sin(t);
                float c = cos(t);
                float2x2 rot = float2x2(c, -s, s, c);
                
                // Note: In HLSL, mul(vec, mat) matches GLSL's vec * mat
                float2 st = mul(uv, rot);
                M = mul(M, rot) * 2.0;
                
                float m = 0.0;
                for (float layer = 0.0; layer < 1.0; layer += 1.0 / NUM_LAYERS)
                {
                    float z = frac(t + layer);
                    float size = lerp(15.0, 1.0, z);
                    float fade = S(0.0, 0.6, z) * S(1.0, 0.8, z);
                    
                    m += fade * NetLayer(st * size - M * z, layer, time);
                }
                
                // Replaced Shadertoy's audio texelFetch with a material property + optional texture
                float texAudio = tex2D(_MainTex, float2(0.05, 0.0)).r;
                float fft = max(_AudioBass, texAudio);
                float glow = -uv.y * fft * 4.0;
               
                float3 baseCol = float3(s, cos(t * 0.2), sin(t * 0.5));
                float3 col = baseCol * m;
                col += baseCol * glow;
                
                // Vignette
                col *= 1.0 - dot(uv, uv);
                
                // Optional 230-second fade-in/fade-out cycle from the original shader
                if (_UseFadeCycle > 0.5)
                {
                    float cycleT = fmod(time, 230.0);
                    col *= S(0.0, 20.0, cycleT) * S(224.0, 200.0, cycleT);
                }
                
                return float4(col, 1.0);
            }
            ENDCG
        }
    }
}
