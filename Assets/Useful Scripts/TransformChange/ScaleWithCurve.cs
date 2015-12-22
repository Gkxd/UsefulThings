using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(TimeKeeper))]
    public class ScaleWithCurve : MonoBehaviour {
        public bool uniformScale = true;
        public Curve curve;

        public Curve curveX, curveY, curveZ;

        private TimeKeeper timeKeeper;
        void Start() {
            timeKeeper = GetComponent<TimeKeeper>();
        }

        void Update() {
            if (uniformScale) {
                transform.localScale = Vector3.one * curve.Evaluate(timeKeeper);
            }
            else {
                transform.localScale = new Vector3(curveX.Evaluate(timeKeeper), curveY.Evaluate(timeKeeper), curveZ.Evaluate(timeKeeper));
            }
        }
    }
}