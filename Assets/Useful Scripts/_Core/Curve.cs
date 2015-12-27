using UnityEngine;
using System.Collections;

namespace UsefulThings {
    [System.Serializable]
    public class Curve {
        public AnimationCurve curve;
        [Tooltip("Amplitude")]
        public float amplitude = 1;
        [Tooltip("Frequency")]
        public float frequency = 1;
        [Tooltip("Phase Offset")]
        public float phase = 0;
        [Tooltip("Curve Offset")]
        public float offset = 0;

        public float Evaluate(float t) {
            return amplitude * curve.Evaluate(frequency * (t + phase)) + offset;
        }

        public float Evaluate(TimeKeeper tk) {
            return Evaluate(tk.lifeTime);
        }
    }
}