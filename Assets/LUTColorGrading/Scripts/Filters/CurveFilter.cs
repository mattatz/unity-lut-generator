using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading {

	public class CurveFilter : LUTFilter {

		protected Texture2D CreateCurve(AnimationCurve curve, int resolution = 64) {
			var tex = new Texture2D(resolution, 1, TextureFormat.RGBAFloat, false);
			tex.filterMode = FilterMode.Bilinear;
			tex.wrapMode = TextureWrapMode.Clamp;

			for(int i = 0; i < resolution; i++) {
				var t = 1f * i / resolution;
				var v = curve.Evaluate(t);
				tex.SetPixel(i, 0, new Color(v, v, v));
			}
			tex.Apply();
			return tex;
		}

	}
		
}

