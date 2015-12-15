using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(Text))]
    public class ChangeUITextColor : _ChangeColor {

        private Text text;
        void Start() {
            text = GetComponent<Text>();
        }

        void Update() {
            text.color = getColor();
        }
    }
}
