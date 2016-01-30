using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class AudioListenerOutput : MonoBehaviour {

        public static float[] soundWave;
        public static float[] frequencySpectrum;

        [Range(1, 13)]
        public int logNumFrequencySamples;

        void Start() {
            frequencySpectrum = new float[1 << logNumFrequencySamples];
        }

        void Update() {
            AudioListener.GetSpectrumData(frequencySpectrum, 0, FFTWindow.Rectangular);
        }

        void OnAudioFilterRead(float[] data, int channels) {
            soundWave = data;
        }
    }
}
