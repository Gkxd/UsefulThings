using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class ScaleWithCurve : _TimedBehaviour {
        public bool uniformScale = true;
        public Curve curve;

        public Curve curveX, curveY, curveZ;

        void Update() {
            if (uniformScale) {
                transform.localScale = Vector3.one * curve.Evaluate(lifeTime);
            }
            else {
                transform.localScale = new Vector3(curveX.Evaluate(lifeTime), curveY.Evaluate(lifeTime), curveZ.Evaluate(lifeTime));
            }
        }
    }
}