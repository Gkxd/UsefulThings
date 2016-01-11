using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [ExecuteInEditMode]
    public class SetFullscreenOrthographicQuad : MonoBehaviour {
        public Camera camera;
        void Start() {
            float height = camera.orthographicSize * 2;
            float width = height * camera.aspect;

            transform.localScale = new Vector3(width, height, 1);
        }
    }
}