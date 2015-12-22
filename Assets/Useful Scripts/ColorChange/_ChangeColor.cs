using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(TimeKeeper))]
    public abstract class _ChangeColor : MonoBehaviour {
        public Gradient color;
        public Curve curve;

        private TimeKeeper timeKeeper;

        protected virtual void Start() {
            timeKeeper = GetComponent<TimeKeeper>();
        }

        protected Color getColor() {
            return color.Evaluate(curve.Evaluate(timeKeeper));
        }
    }
}