using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace LUTColorGrading
{

	[CustomEditor (typeof(LUTGenerator))]
    public class LUTGeneratorEditor : Editor {

		public override void OnInspectorGUI () {
			EditorGUI.BeginChangeCheck();
			base.OnInspectorGUI();
			if(EditorGUI.EndChangeCheck() && Application.isPlaying) {
				var generator = target as LUTGenerator;
				generator.Reset();
			}
		}

    }

}

