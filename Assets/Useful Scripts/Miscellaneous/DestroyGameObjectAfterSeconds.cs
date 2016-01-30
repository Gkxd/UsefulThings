using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class DestroyGameObjectAfterSeconds : MonoBehaviour {
        public float seconds;

        void Start() {
            Destroy(gameObject, seconds);
        }
    }
}
