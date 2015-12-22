using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public abstract class _TimedBehaviour : MonoBehaviour {
        public bool resetTimeOnEnable;

        private float startTime;


        public float lifeTime { get { return Time.time - startTime; } }

        void Start() {
            startTime = Time.time;
        }

        void OnEnable() {
            if (resetTimeOnEnable) {
                startTime = Time.time;
            }
        }
    }
}