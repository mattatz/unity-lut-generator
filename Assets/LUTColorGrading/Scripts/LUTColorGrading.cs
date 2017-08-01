using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading
{

    public class LUTColorGrading : MonoBehaviour {

        public Texture lut;
        [SerializeField] Shader shader;

        Material material;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if(material != null)
            {
                material.SetTexture("_LUT", lut);
            }
            Graphics.Blit(source, destination, material);
        }

        private void OnEnable()
        {
            material = new Material(shader);
        }

        private void OnDestroy()
        {
            if(material != null)
            {
                Destroy(material);
            }
        }

    }

}

