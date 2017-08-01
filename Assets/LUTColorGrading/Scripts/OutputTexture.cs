using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading.Test {

	public class OutputTexture : MonoBehaviour {

		[SerializeField] Shader shader;
		[SerializeField] Texture2D texture;

		Material material;

		void OnRenderImage (RenderTexture src, RenderTexture dst) {
			CheckInit();
			Graphics.Blit(src, dst, material);
		}

		void CheckInit () {
			if(material == null) {
				material = new Material(shader);
				material.SetTexture("_Texture", texture);
			}
		}

		void OnDestroy () {
			if(material != null) {
				material = new Material(shader);
			}
		}

	}
		
}

