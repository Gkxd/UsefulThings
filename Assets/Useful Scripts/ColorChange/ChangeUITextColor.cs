using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(Text))]
    public class ChangeUITextColor : _ChangeColor {

        private Text text;
        protected override void Start() {
            base.Start();
            text = GetComponent<Text>();
        }

        void Update() {
            text.color = getColor();
        }
    }
}
