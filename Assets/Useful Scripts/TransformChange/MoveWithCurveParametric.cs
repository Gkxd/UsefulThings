using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [RequireComponent(typeof(TimeKeeper))]
    public class MoveWithCurveParametric : MonoBehaviour {

        public bool moveX = true, moveY = true, moveZ = true;
        public Curve curveX, curveY, curveZ;

        private Vector3 initialPosition;
        private TimeKeeper timeKeeper;

        void Start() {
            initialPosition = transform.position;
            timeKeeper = GetComponent<TimeKeeper>();
        }

        void Update() {
            transform.position = initialPosition + new Vector3(
                moveX ? curveX.Evaluate(timeKeeper) : 0,
                moveY ? curveY.Evaluate(timeKeeper) : 0,
                moveZ ? curveZ.Evaluate(timeKeeper) : 0);
        }
    }
}