using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class CameraShake : MonoBehaviour {
        // *** This script doesn't actually shake the camera! ***

        // To actually shake the camera, offset the camera's position with CameraShakeOffset
        // in another script, and call CameraShake.ShakeCamera() from anywhere whenever you
        // want to shake the camera. (See CameraShakeTest.cs for example usage.)

        private static CameraShake instance;

        public static Vector3 CameraShakeOffset;


        public bool shakeZ;

        private float timeBetweenShakes = 0.05f;
        private float cameraShakeMagnitude;
        private float cameraShakeDuration;
        private float cameraShakeTimer;

        private Vector3 cameraShakeTargetOffset;

        void Start() {
            if (instance == null) {
                instance = this;
            }
        }

        void Update() {
            if (cameraShakeDuration > 0) {
                cameraShakeTimer += Time.deltaTime;

                if (cameraShakeTimer > timeBetweenShakes) {
                    cameraShakeTimer -= timeBetweenShakes;

                    cameraShakeTargetOffset = Random.insideUnitSphere * cameraShakeMagnitude;
                    if (!shakeZ) {
                        cameraShakeTargetOffset.z = 0;
                    }
                }

                cameraShakeDuration -= Time.deltaTime;
                cameraShakeMagnitude = Mathf.Lerp(cameraShakeMagnitude, 0, Time.deltaTime);
            }
            else if (cameraShakeDuration < 0) {
                cameraShakeDuration = 0;
                cameraShakeMagnitude = 0;
                cameraShakeTimer = 0;
                cameraShakeTargetOffset = Vector3.zero;
            }

            CameraShakeOffset = Vector3.Lerp(CameraShakeOffset, cameraShakeTargetOffset, 15 * Time.deltaTime);
        }

        public static void ShakeCamera(float magnitude, float duration) {
            ShakeCamera(magnitude, duration, 0.05f);
        }

        public static void ShakeCamera(float magnitude, float duration, float timeBetweenShakes) {
            if (instance) {
                instance.cameraShakeMagnitude += magnitude;
                instance.cameraShakeDuration += duration;
                instance.timeBetweenShakes = timeBetweenShakes;
            }
        }
    }
}