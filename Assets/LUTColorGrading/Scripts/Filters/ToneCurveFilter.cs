using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading {

	[CreateAssetMenu (fileName = "ToneCurveFilter", menuName = "LUTFilter/ToneCurveFilter")]
	public class ToneCurveFilter : CurveFilter {

		[SerializeField] AnimationCurve red;
		[SerializeField] AnimationCurve green;
		[SerializeField] AnimationCurve blue;

		Texture2D redCurve, greenCurve, blueCurve;

		public override void Setup() {
			base.Setup();
			redCurve = CreateCurve(red);
			greenCurve = CreateCurve(green);
			blueCurve = CreateCurve(blue);
			material.SetTexture("_Red", redCurve);
			material.SetTexture("_Green", greenCurve);
			material.SetTexture("_Blue", blueCurve);
		}

		public override void Dispose () {
			Destroy(redCurve);
			Destroy(greenCurve);
			Destroy(blueCurve);
		}

	}
		
}

