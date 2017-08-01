#ifndef __INCLUDE_LUT__
#define __INCLUDE_LUT__

#define DIM 32.0
static const float4 _ColorGrading_Scale = float4((DIM - 1) / (DIM * DIM), (DIM - 1) / DIM, 1 / DIM, 0);
static const float4 _ColorGrading_Offset = float4(1 / (2 * DIM * DIM), 1 / (2 * DIM), 0, 0);

float3 ColorGrade(sampler2D lut, float3 c) {
	float2 uv = float2(c.x, 1 - c.y) * _ColorGrading_Scale.xy + _ColorGrading_Offset.xy;
	float z = c.z * (DIM - 1);
	float2 uvz = min(floor(float2(z, z + 1)), DIM - 1);
	float t = z - uvz.x;
	uvz *= _ColorGrading_Scale.z;
    float3 c0 = tex2D(lut, uv + float2(uvz.x, 0)).rgb;
    float3 c1 = tex2D(lut, uv + float2(uvz.y, 0)).rgb;
    return lerp(c0, c1, t);
}

float4 ColorGrade(sampler2D lut, float4 c) {
    return float4(ColorGrade(lut, c.rgb), c.a);
}

#endif
