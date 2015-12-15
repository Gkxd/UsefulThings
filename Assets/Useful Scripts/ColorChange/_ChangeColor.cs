using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public abstract class _ChangeColor : MonoBehaviour {
        public Gradient color;
        public Curve curve;

        private float startTime;

        void Start() {
            startTime = Time.time;
        }

        protected Color getColor() {
            return color.Evaluate(curve.Evaluate(Time.time - startTime));
        }
    }
}