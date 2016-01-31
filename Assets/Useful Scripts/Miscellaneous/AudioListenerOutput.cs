using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class AudioListenerOutput : MonoBehaviour {
        public static float[] soundWave;

        void OnAudioFilterRead(float[] data, int channels) {
            soundWave = data;
        }
    }
}