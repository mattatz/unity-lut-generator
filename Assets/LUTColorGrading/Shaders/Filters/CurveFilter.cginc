#ifndef __INCLUDE_CURVE_FILTER__
#define __INCLUDE_CURVE_FILTER__

float evaluate(sampler2D curve, float t) {
	return saturate(tex2D(curve, float2(t, 0.5)).r);
}

#endif
