using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LUTColorGrading
{

    public class LUTGenerator : MonoBehaviour {

        [SerializeField] LUTColorGrading colorGrading;

        LUT lut;

        private void Update()
        {
            colorGrading.lut = lut.texture;
        }

        private void OnEnable()
        {
            if(lut == null)
            {
                lut = new LUT();
            }
        }

        private void OnGUI()
        {
            var tex = lut.texture;
            var r = new Rect(10, 10, tex.width, tex.height);
            GUI.DrawTexture(r, lut.texture);
        } 

    }

}


