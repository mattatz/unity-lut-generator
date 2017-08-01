using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading
{

    public class LUT {

        public Texture2D texture;

        public LUT()
        {
            texture = Create();
        }

        Texture2D Create (int resolution = 16)
        {
            var tex = new Texture2D(resolution * resolution, resolution, TextureFormat.RGBAFloat, false);
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

    }

}


