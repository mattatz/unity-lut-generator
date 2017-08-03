using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading
{

    public class LUTGenerator : MonoBehaviour {

        public RenderTexture source
        {
            get
            {
                return (current == 0) ? lut0 : lut1;
            }
        }

        public RenderTexture destination
        {
            get
            {
                return (current == 0) ? lut1 : lut0;
            }
        }

        [SerializeField] LUTColorGrading colorGrading;
		[SerializeField] List<LUTFilter> filters;
		[SerializeField] bool debug;

		Texture2D origin;
		[SerializeField] RenderTexture lut0, lut1;
        int current = 0;

		Texture2D CreateLUT (int resolution = 32)
		{
			var tex = new Texture2D(resolution * resolution, resolution, TextureFormat.RGBAFloat, false);
			tex.filterMode = FilterMode.Point;
			tex.wrapMode = TextureWrapMode.Clamp;
            tex.anisoLevel = 0;

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
            rt.anisoLevel = 0;
			rt.Create();
			return rt;
		}

		void UpdateLUT() {
            current = 0;
			Graphics.Blit(origin, source);
			Graphics.Blit(origin, destination);
			filters.ForEach(filter => {
				if(filter != null) {
					filter.Filter(source, destination);
                    Swap();
				}
			});
			colorGrading.lut = source;
		}

        void Swap ()
        {
            current = (1 - current);
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
			if(origin != null) {
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
			origin = CreateLUT();
			lut0 = CreateRT(origin.width, origin.height);
			lut1 = CreateRT(origin.width, origin.height);
			filters.ForEach(filter => {
				if(filter != null) {
					filter.Setup();
				}
			});
			UpdateLUT();
		}

		void Clear () {
			Destroy(origin);
			Destroy(lut0);
			Destroy(lut1);
			filters.ForEach(filter => {
				if(filter != null) {
					filter.Dispose();
				}
			});
		}

        void OnGUI()
        {
			if(debug) {
				var r = new Rect(10, 10, source.width, source.height);
            	GUI.DrawTexture(r, source);
			}
        } 

    }

}


