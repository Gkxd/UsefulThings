using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public abstract class _ChangeColor : _TimedBehaviour {
        public Gradient color;
        public Curve curve;

        protected Color getColor() {
            return color.Evaluate(curve.Evaluate(Time.time - lifeTime));
        }
    }
}