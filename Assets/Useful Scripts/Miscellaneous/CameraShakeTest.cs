using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class CameraShakeTest : MonoBehaviour {

        Vector3 originalPosition;

        void Start() {
            originalPosition = transform.localPosition;
        }

        void Update() {
            transform.localPosition = originalPosition + CameraShake.CameraShakeOffset;

            if (Input.GetKeyDown(KeyCode.Space)) {
                CameraShake.ShakeCamera(1, 0.2f);
            }
        }
    }
}
