using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class CameraShakeMovement : MonoBehaviour {

        public bool pressSpaceToShake;

        Vector3 originalPosition;

        void Start() {
            originalPosition = transform.localPosition;
        }

        void Update() {
            transform.localPosition = originalPosition + CameraShake.CameraShakeOffset;

            if (pressSpaceToShake && Input.GetKeyDown(KeyCode.Space)) {
               CameraShake.ShakeCamera(1, 0.2f);
            }
        }
    }
}
