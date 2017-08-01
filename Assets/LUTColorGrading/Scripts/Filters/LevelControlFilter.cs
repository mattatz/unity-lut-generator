using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading {

	[CreateAssetMenu (fileName = "LevelControlFilter", menuName = "LUTFilter/LevelControlFilter")]
	public class LevelControlFilter : CurveFilter {

		[SerializeField] AnimationCurve level;

		Texture2D levelCurve;

		public override void Setup() {
			base.Setup();
			levelCurve = CreateCurve(level);
			material.SetTexture("_Level", levelCurve);
		}

		public override void Dispose () {
			Destroy(levelCurve);
		}


	}
		
}

