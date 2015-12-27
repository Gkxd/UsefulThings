using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(TimeKeeper))]
    public class Rotate2DWithCurve : MonoBehaviour {

        public Curve curve;

        private TimeKeeper timeKeeper;

        void Start() {
            timeKeeper = GetComponent<TimeKeeper>();
        }

        void Update() {
            transform.localEulerAngles = Vector3.forward * curve.Evaluate(timeKeeper);
        }
    }
}
