using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LUTColorGrading {

	[CustomEditor (typeof(LUTFilter), true)]
	public class LUTFilterEditor : Editor {

		public override void OnInspectorGUI () {
			EditorGUI.BeginChangeCheck();
			base.OnInspectorGUI();
			if(EditorGUI.EndChangeCheck() && Application.isPlaying) {
				var filter = target as LUTFilter;
				filter.onFilterUpdate.Invoke(filter);
			}
		}

	}
		
}

