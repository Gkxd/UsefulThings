using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(Image))]
    public class ChangeUIImageColor : _ChangeColor {

        private Image image;
        protected override void Start() {
            base.Start();
            image = GetComponent<Image>();
        }

        void Update() {
            image.color = getColor();
        }
    }
}