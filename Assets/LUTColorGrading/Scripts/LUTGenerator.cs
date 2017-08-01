using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading
{

    public class LUTGenerator : MonoBehaviour {

        [SerializeField] LUTColorGrading colorGrading;
		[SerializeField] List<LUTFilter> filters;
		[SerializeField] bool debug;

		Texture2D source;
		[SerializeField] RenderTexture lut;

		Texture2D CreateLUT (int resolution = 32)
		{
			var tex = new Texture2D(resolution * resolution, resolution, TextureFormat.RGBAFloat, false);
			tex.filterMode = FilterMode.Point;
			tex.wrapMode = TextureWrapMode.Clamp;

			var inv = 1f / (resolution - 1);

			for (int z = 0; z < resolution; z++)
			{
				var offset = z * resolution;
				var b = z * inv;
				for (int y = 0; y < resolution; y++)
				{
					var g = 1f - y * inv;
					for (int x = 0; x < resolution; x++)
					{
						var r = x * inv;
						tex.SetPixel(offset + x, y, new Color(r, g, b));
					}
				}
			}

			tex.Apply();

			return tex;
		}

		RenderTexture CreateRT (int width, int height) {
			var rt = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat);
			rt.filterMode = FilterMode.Point;
			rt.enableRandomWrite = true;
			rt.useMipMap = false;
			rt.wrapMode = TextureWrapMode.Clamp;
			rt.Create();
			return rt;
		}

		void UpdateLUT() {
			Graphics.Blit(source, lut);
			filters.ForEach(filter => {
				if(filter != null) {
					filter.Filter(lut, lut);
				}
			});
			colorGrading.lut = lut;
		}

        void OnEnable()
        {
			Setup();
			filters.ForEach(filter => {
				if(filter != null) {
					filter.onFilterUpdate.RemoveListener(OnFilterUpdate);
					filter.onFilterUpdate.AddListener(OnFilterUpdate);
				}
			});
        }

		void OnDisable () {
			if(source != null) {
				Clear();
			}
		}

		void OnFilterUpdate (LUTFilter filter) {
			Reset();
		}

		public void Reset () {
			Clear();
			Setup();
		}

		void Setup () {
			source = CreateLUT();
			lut = CreateRT(source.width, source.height);
			filters.ForEach(filter => {
				if(filter != null) {
					filter.Setup();
				}
			});
			UpdateLUT();
		}

		void Clear () {
			Destroy(source);
			Destroy(lut);
			filters.ForEach(filter => {
				if(filter != null) {
					filter.Dispose();
				}
			});
		}

        void OnGUI()
        {
			if(debug) {
				var r = new Rect(10, 10, lut.width, lut.height);
            	GUI.DrawTexture(r, lut);
			}
        } 

    }

}


