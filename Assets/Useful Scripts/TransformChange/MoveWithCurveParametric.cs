using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class MoveWithCurveParametric : _TimedBehaviour {

        public bool moveX = true, moveY = true, moveZ = true;
        public Curve curveX, curveY, curveZ;

        private Vector3 initialPosition;

        void Start() {
            initialPosition = transform.position;
        }

        void Update() {
            transform.position = initialPosition + new Vector3(
                moveX ? curveX.Evaluate(lifeTime) : 0,
                moveY ? curveY.Evaluate(lifeTime) : 0,
                moveX ? curveZ.Evaluate(lifeTime) : 0);
        }
    }
}