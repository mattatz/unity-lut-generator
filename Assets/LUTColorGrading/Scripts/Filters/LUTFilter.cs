using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LUTColorGrading {

	[System.Serializable] public class LUTEvent : UnityEvent<LUTFilter> {}

	[CreateAssetMenu (fileName = "Filter", menuName = "LUTFilter/Filter")]
	public class LUTFilter : ScriptableObject {

		public LUTEvent onFilterUpdate;
		[SerializeField] protected Material material;

		public virtual void Setup() {}

		public virtual void Filter (RenderTexture src, RenderTexture dst) {
			Graphics.Blit(src, dst, material);
		}

		public virtual void Dispose () {}

	}
		
}

