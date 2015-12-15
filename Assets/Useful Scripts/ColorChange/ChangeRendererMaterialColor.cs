using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(Renderer))]
    public class ChangeRendererMaterialColor : _ChangeColor {

        [Tooltip("(Optional)\nSpecify the name of the color variable in the shader of the associated material.\n\nLeave blank to use the material named \"_Color\".")]
        public string colorName;
        [Tooltip("(Optional)\nSpecify which material from the renderer's material array to use.\n\nLeave this at 0 for most cases.")]
        public int materialIndex;

        private Material m;
        void Start() {
            m = GetComponent<Renderer>().materials[materialIndex];
        }

        void Update() {
            if (colorName == "") {
                m.color = getColor();
            }
            else {
                m.SetColor(colorName, getColor());
            }
        }
    }
}